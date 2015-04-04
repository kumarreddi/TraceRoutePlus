#region Using statements
using System;
using System.Collections.Generic;
using System.IO;
using TraceRoutePlus.Properties;
#endregion

namespace TraceRoutePlus
{
	class Program
	{
		public static Dictionary<string, bool> OptionDefinitions = new Dictionary<string, bool>()
		{
			{ "file", true },

			{ "timeout", true },
			{ "retries", true },
			{ "waitbetween", true },
			{ "maxhops", true },

			{ "bare", false },

			{ "about", false },
			{ "license", false },
			{ "help", false },
			{ "verbose", false }
		};
		public static Dictionary<string, string> DefaultOptionValues = new Dictionary<string, string>()
		{
			{ "file", "" },

			{ "timeout", "8" },
			{ "retries", "5" },
			{ "waitbetween", "0" },
			{ "maxhops", "-1" },

			{ "bare", "false" },

			{ "about", "false" },
			{ "license", "false" },
			{ "help", "false" },
			{ "verbose", "false" }
		};
		public static Dictionary<string, string> Options;
		public static List<string> Targets;

		public static bool Verbose = false;

		static void Main(string[] args)
		{
			Options = ArgumentParser.Parse(args, OptionDefinitions, out Targets);
			Options = ArgumentParser.ApplyDefaults(Options, DefaultOptionValues);

			if (Options["verbose"] == "true")
				Verbose = true;

			if(Options["license"] == "true")
			{
				Console.WriteLine(Resources.LICENSE);
				return;
			}

			if(Options["about"] == "true")
			{
				Console.WriteLine(Resources.About);
				return;
			}

			if(Options["help"] == "true" || ((args.Length == 0 || Targets.Count == 0) && Options["file"].Length < 0))
			{
				Console.WriteLine(Resources.Help);
				return;
			}

			if(Options["bare"] == "false")
			{
				Console.WriteLine("Traceroute Plus");
				Console.WriteLine("===============");
				Console.WriteLine("By Starbeamrainbowlabs <https://starbeamrainbowlabs.com>\n");
			}

			if(Options["file"].Length > 0)
			{
				if(Options["file"] == "-")
				{
					string nextLine;
					do
					{
						nextLine = Console.ReadLine().Trim();

						if (!string.IsNullOrEmpty(nextLine))
						{
							Targets.Add(nextLine);
							if (Verbose) Console.WriteLine("[stdin] Added {0} as target.", nextLine);
						}
					}
					while (!string.IsNullOrEmpty(nextLine));
				}
				else
				{
					StreamReader targetReader = new StreamReader(Options["file"]);
					while(!targetReader.EndOfStream)
					{
						string nextLine = targetReader.ReadLine().Trim();
						if (Verbose) Console.WriteLine("[file] Added {0} as target.", nextLine);
						Targets.Add(nextLine);
					}
				}
			}

			int timeout = int.Parse(Options["timeout"]);
			int retryCount = int.Parse(Options["retries"]);
			int maxHops = int.Parse(Options["maxhops"]);
			float waitbetween = float.Parse(Options["waitbetween"]);

			if(Verbose)
			{
				Console.WriteLine("Options:");
				Console.Write("timeout: {0}, retryCount: {1}, maxHops: {2},", timeout, retryCount, maxHops);
				Console.Write("waitBetween: {0}", waitbetween);
				Console.WriteLine();
			}

			//-----------------------------------------------------------

			for (int i = 0; i < Targets.Count; i++)
			{
				if (Verbose) Console.WriteLine("Target {0}:", i);
				Traceroute.TraceToTarget(Targets[i], timeout, retryCount, maxHops, waitbetween);
			}
		}
	}
}
