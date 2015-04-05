#region Using statements
using System;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
#endregion

namespace TraceRoutePlus
{
	class Traceroute
	{
		protected static byte[] DataPacket = Encoding.ASCII.GetBytes("abcdefghijklmnopqrstuvwxyzabcdef");

		public static void TraceToTarget(string target, int timeout = 8, int retryCount = 5, int maxHops = -1, float waitBetween = 0)
		{
			timeout *= 1000; // the retry count should be in milliseconds

			Console.WriteLine("=== {0} ===", target);

			int hopNumber = 1;
			while(true)
			{
				if(maxHops != -1 && hopNumber > maxHops)
				{
					Console.WriteLine("=== {0} end (max hops reached) ===", target);
					break;
				}
				Console.Write("{0,2}: ", hopNumber);

				Ping pinger = new Ping();
				PingOptions pingerOptions = new PingOptions(hopNumber, true);
				PingReply pingResponse;

				int tries = 0;
				long elapsed;
				do
				{
					Stopwatch timer = Stopwatch.StartNew();
					pingResponse = pinger.Send(target, timeout, DataPacket, pingerOptions);
					timer.Stop();
					elapsed = timer.ElapsedMilliseconds;

					if (waitBetween > 0)
						Thread.Sleep((int)(waitBetween * 1000));

					if (pingResponse.Status == IPStatus.TtlExpired ||
						pingResponse.Status == IPStatus.TimeExceeded ||
						pingResponse.Status == IPStatus.Success)
						break;

					if(Program.Options["bare"] == "false")
						Console.Write("*");
					//Console.Write("({0})", pingResponse.Status);

					tries++;
				} while (tries < retryCount);

				if(tries == retryCount)
				{
					// we failed on this node
					Console.Write("~{0}ms", elapsed);

					if (Program.Options["bare"] == "false")
						Console.Write(" ({0})", pingResponse.Status);
				}

				//Console.Write("<{0} el:{1}ms rtt:{2}>", pingResponse.Address, elapsed, pingResponse.RoundtripTime);

				//Console.WriteLine(pingResponse.ToString());

				if(pingResponse.Status == IPStatus.Success)
				{
					// we are done
					Console.Write("{0,-16} ", pingResponse.Address);
					PrintHostName(pingResponse.Address.ToString());
					Console.WriteLine("{0,-8}", pingResponse.RoundtripTime + "ms");

					if (Program.Options["bare"] == "false")
						Console.WriteLine("=== {0} end ===\n", target);

					break;
				}
				else if(pingResponse.Status == IPStatus.TimeExceeded || pingResponse.Status == IPStatus.TtlExpired)
				{
					// we found another host along the way
					Console.Write("{0,-16}", pingResponse.Address);
					PrintHostName(pingResponse.Address.ToString());

					PingReply intermediateResponse;
					int subtries = 0;
					do
					{
						intermediateResponse = pinger.Send(pingResponse.Address, timeout, DataPacket);
						subtries++;

						if (waitBetween > 0)
							Thread.Sleep((int)(waitBetween * 1000));

						if (intermediateResponse.Status == IPStatus.Success)
							break;

						if (Program.Options["bare"] == "false")
							Console.Write("*");
						//Console.Write(intermediateResponse.Status);
					} while (subtries < retryCount);

					if(intermediateResponse.Status != IPStatus.Success)
					{
						Console.Write("~{0}ms", elapsed);
						if(Program.Options["bare"] == "false")
							Console.Write(" ({0})", intermediateResponse.Status);
					}
					else
					{
						Console.Write("{0,-8}", intermediateResponse.RoundtripTime + "ms");
					}
				}
				else
				{
					Console.Write("~{0}ms", elapsed);
					if (Program.Options["bare"] == "false")
						Console.Write(" ({0})", pingResponse.Status);
				}

				Console.WriteLine();
				hopNumber++;
			}
		}

		public static void PrintHostName(string IPAddress)
		{
			if (Program.Options["dns"] == "false")
				return;

			int columnSize = 20;
			try
			{
				string hostName = Dns.GetHostEntry(IPAddress).HostName;
				if (Program.Options["bare"] == "false")
					Console.Write(String.Format("<{0}>", hostName).PadRight(columnSize));
				else
					Console.Write(hostName);
			}
			catch (Exception error)
			{
				string shortMessage = "";
				if(error.Message.Contains("no data of the requested type was found"))
				{
					shortMessage = "No HostName Found";
				}
				else
				{
					shortMessage = error.Message;
				}
				Console.Write(String.Format("({0})", shortMessage).PadRight(columnSize));
			}
			Console.Write(" ");
		}
	}
}
