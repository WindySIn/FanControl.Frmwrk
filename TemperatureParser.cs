using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.RegularExpressions;

// Simple parser that converts the input key/value lines into a dictionary
// and then to JSON. Handles repeated keys by collecting values into lists.
public static class TemperatureParser
{
    // Parses the input text into a dictionary where each key maps to either a single string
    // value or a List<string> when the key appears multiple times.
    public static Dictionary<string, object> ParseToDictionary(string input, Dictionary<string, object> dict)
    {
        if (string.IsNullOrWhiteSpace(input))
            return dict;

        // Split into lines and parse "Key: value" pairs. Some values may have extra spaces.
        var lines = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        var kvRegex = new Regex("^\\s*(?<key>[^:]+?):\\s*(?<value>\\d+?)\\s+\\w+\\s*$"); // Should strip the trailing units

        foreach (var raw in lines)
        {
            var line = raw.Trim();
            var m = kvRegex.Match(line);
            if (!m.Success)
            {
                // skip non-matching lines
                continue;
            }

            var key = m.Groups["key"].Value.Trim();
            var value = m.Groups["value"].Value.Trim();

            // If key already exists, convert to list or append
            // Because the --thermal command reports all three fan speeds with the same "Fan Speed" key, this will create an array of their values.
            if (dict.TryGetValue(key, out var existing))
            {
                if (existing is List<string> list)
                {
                    list.Add(value);
                }
                else if (existing is string s)
                {
                    var newList = new List<string> { s, value };
                    dict[key] = newList;
                }
                else
                {
                    // unexpected type, overwrite
                    dict[key] = value;
                }
            }
            else
            {
                dict[key] = value;
            }
        }
        
        return dict;
    }

    // Serializes the parsed dictionary to a pretty-printed JSON string.
    public static string ParseToJson(string input, Dictionary<string, object> dict)
    {
        var json = ParseToDictionary(input, dict);

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        return JsonSerializer.Serialize(json, options);
    }
}
