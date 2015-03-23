#region Using statements
using System;
using System.Collections.Generic;
#endregion

namespace TraceRoutePlus
{
	class Program
	{
		public static Dictionary<string, string> OptionsHelpText = new Dictionary<string, string>()
		{
			{ "timeout", "The maximum amount of time we should wait for a response from any given ping request. Default: 8" },
			{ "retries", "The number of times we should retry pinging a given host if we don't get a response. Default: 5" },
			{ "waitbetween", "The number of seconds that should pass between successive pings." },
			{ "maxhops", "The maximum number of hops that should be done. Default: -1 (disabled)." },
			{ "bare", "Specifying this option causes the program to output the tracereoute in a bare format." }
		};
		public static Dictionary<string, bool> OptionDefinitions = new Dictionary<string, bool>()
		{
			{ "timeout", true },
			{ "retries", true },
			{ "waitbetween", true },
			{ "maxhops", true },
			{ "bare", false }
		};
		public static Dictionary<string, string> DefaultOptionValues = new Dictionary<string, string>()
		{
			{ "timeout", "8" },
			{ "retries", "5" },
			{ "waitbetween", "0" },
			{ "maxhops", "-1" },
			{ "bare", "false" },
		};
		public static Dictionary<string, string> Options;
		public static List<string> Targets;

		static void Main(string[] args)
		{
			Options = ArgumentParser.Parse(args, OptionDefinitions, out Targets);
			Options = ArgumentParser.ApplyDefaults(Options, DefaultOptionValues);

			if (Options["bare"] == "false")
			{
				Console.WriteLine("Traceroute Plus");
				Console.WriteLine("---------------");
				Console.WriteLine("By Starbeamrainbowlabs <https://starbeamrainbowlabs.com>\n");
			}

			if(args.Length == 0 || Targets.Count == 0)
			{
				Console.WriteLine("Usage: tracerouteplus <options> domain1.com domain2.co.uk domainx.abc...\n");
				ArgumentParser.PrintHelp(OptionsHelpText);

				Console.WriteLine("Examples:");
				Console.WriteLine("  tracerouteplus google.com");
				Console.WriteLine("    > trace a route to google.com");
				Console.WriteLine("  tracerouteplus google.com yahoo.co.uk");
				Console.WriteLine("    > trace a route to google.com and then yahoo.co.uk");
				Console.WriteLine("  tracerouteplus /bare google.com");
				Console.WriteLine("    > Make the output bare");
				Console.WriteLine("  tracerouteplus /retries 20 google.com");
				Console.WriteLine("    > retry failed pings 20 times");
				Console.WriteLine("  tracerouteplus /timeout 10 google.com");
				Console.WriteLine("    > wait 10 seconds for a reply to a ping request");
				return;
			}

			int timeout = int.Parse(Options["timeout"]);
			int retryCount = int.Parse(Options["retries"]);
			int maxHops = int.Parse(Options["maxhops"]);
			float waitbetween = float.Parse(Options["waitbetween"]);

			//-----------------------------------------------------------

			for (int i = 0; i < Targets.Count; i++)
			{
				Traceroute.TraceToTarget(Targets[i], timeout, retryCount, maxHops, waitbetween);
			}
		}
	}
}
