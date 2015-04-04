#region Using statements
using System;
using System.Collections.Generic;
using TraceRoutePlus.Properties;
#endregion

namespace TraceRoutePlus
{
	class Program
	{
		public static Dictionary<string, bool> OptionDefinitions = new Dictionary<string, bool>()
		{
			{ "timeout", true },
			{ "retries", true },
			{ "waitbetween", true },
			{ "maxhops", true },
			{ "bare", false },
			{ "about", false },
			{ "license", false },
			{ "help", false }
		};
		public static Dictionary<string, string> DefaultOptionValues = new Dictionary<string, string>()
		{
			{ "timeout", "8" },
			{ "retries", "5" },
			{ "waitbetween", "0" },
			{ "maxhops", "-1" },
			{ "bare", "false" },
			{ "about", "false" },
			{ "license", "false" },
			{ "help", "false" }
		};
		public static Dictionary<string, string> Options;
		public static List<string> Targets;

		static void Main(string[] args)
		{
			Options = ArgumentParser.Parse(args, OptionDefinitions, out Targets);
			Options = ArgumentParser.ApplyDefaults(Options, DefaultOptionValues);

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

			if(Options["help"] == "true" || args.Length == 0 || Targets.Count == 0)
			{
				Console.WriteLine(Resources.Help);
				return;
			}

			if (Options["bare"] == "false")
			{
				Console.WriteLine("Traceroute Plus");
				Console.WriteLine("===============");
				Console.WriteLine("By Starbeamrainbowlabs <https://starbeamrainbowlabs.com>\n");
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
