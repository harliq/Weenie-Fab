using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace EmoteScriptLib.StringMap
{
    public static class Reader
    {
        public static Dictionary<uint, string> GetIDToNames(string filename)
        {
            var records = ReadCSV(filename);

            var results = new Dictionary<uint, string>();

            for (var i = 0; i < records.Count; i++)
            {
                var record = records[i];

                if (record.Length < 2)
                {
                    Console.WriteLine($"Failed to parse line {i + 1} in {filename}: expected 2 fields in {string.Join(",", record)}");
                    continue;
                }

                if (record.Length > 2)
                    record[1] = string.Join(",", record.Skip(1));

                if (!uint.TryParse(record[0], out var id))
                {
                    Console.WriteLine($"Failed to parse line {i + 1} in {filename}: {record[0]} is not an unsigned int");
                    continue;
                }

                if (results.ContainsKey(id))
                {
                    Console.WriteLine($"Failed to parse line {i + 1} in {filename}: {record[0]} is a duplicate key");
                    continue;
                }

                results.Add(id, record[1]);
            }

            return results;
        }

        public static Dictionary<string, uint> GetNameToIDs(Dictionary<uint, string> dict)
        {
            var newDict = new Dictionary<string, uint>();
            foreach (var kvp in dict)
            {
                if (!newDict.ContainsKey(kvp.Value))
                    newDict.Add(kvp.Value, kvp.Key);
            }
            return newDict;
        }

        public static List<string[]> ReadCSV(string filename)
        {
            var records = new List<string[]>();

            var lines = File.ReadAllLines("StringMap" + Path.DirectorySeparatorChar + filename);

            foreach (var line in lines)
            {
                if (line.StartsWith("//") || !line.Contains(","))
                    continue;

                records.Add(line.Split(','));
            }

            return records;
        }
    }
}
