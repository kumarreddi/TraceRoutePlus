#region Using statements
using System;
using System.Collections.Generic;
#endregion

class ArgumentParser
{
	/// <summary>
	/// Parses an array of strings into a dictionary of options.
	/// </summary>
	/// <param name="args">The array of strings to parse.</param>
	/// <param name="optionDefinitions">A dictionary full of commands and whterh they take a paramter or not.</param>
	/// <param name="extras">A list to fill with extra terms that the user added.</param>
	/// <returns>A dictionary containing the parsed keys and values.</returns>
	public static Dictionary<string, string> Parse(string[] args,
		Dictionary<string, bool> optionDefinitions,
		out List<string> extras)
	{
		Dictionary<string, string> result = new Dictionary<string,string>();
		extras = new List<string>();

		for (int i = 0; i < args.Length; i++)
		{
			if (args[i].StartsWith("/"))
			{
				string key = args[i].Trim('/').ToLower();
				string value = "true";

				// make sure that the programmer intended this option to be specified
				if(!optionDefinitions.ContainsKey(key))
					throw new ArgumentException("Unrecognised command.", key);

				// find the value of the argument that he user entered
				if (i < args.Length - 1 && optionDefinitions[key] && !args[i + 1].StartsWith("/"))
				{
					value = args[i + 1];
					i++;
				}

				result[key] = value;
			}
			else
			{
				extras.Add(args[i]);
			}
		}
		return result;
	}

	public static bool CheckRequired(Dictionary<string, string> parsedOptions, string[] requiredOptions)
	{
		foreach(string optionName in requiredOptions)
		{
			if(!parsedOptions.ContainsKey(optionName))
			{
				throw new ArgumentException(String.Format("Missing required option {0}.", optionName));
			}
		}

		return true;
	}

	/// <summary>
	/// Fills in the unspecified arguments. Operates on the output of Parse().
	/// </summary>
	/// <param name="parsedOptions">The parsed options.</param>
	/// <param name="defaultValues">Their associated default values.</param>
	/// <returns></returns>
	public static Dictionary<string, string> ApplyDefaults(Dictionary<string, string> parsedOptions, Dictionary<string, string> defaultValues)
	{
		foreach(KeyValuePair<string, string> defaultValue in defaultValues)
		{
			if(!parsedOptions.ContainsKey(defaultValue.Key))
			{
				parsedOptions[defaultValue.Key] = defaultValue.Value;
			}
		}
		
		return parsedOptions;
	}

	public static void PrintHelp(Dictionary<string, string> helpDictionary, string prefix = "    ")
	{
		foreach(KeyValuePair<string, string> item in helpDictionary)
		{
			Console.WriteLine("{0}/{1} - {2}", prefix, item.Key, item.Value);
		}
		Console.WriteLine();
	}
}
