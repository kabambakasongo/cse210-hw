using System;
using System.Collections.Generic;
using System.IO;

public class ActivityLog
{
    private const string LogFile = "mindfulness-log.txt";
    private static readonly Dictionary<string, int> _counts = Load();

    public static void Record(string activityName)
    {
        if (!_counts.ContainsKey(activityName))
        {
            _counts[activityName] = 0;
        }

        _counts[activityName]++;
        Save();
    }

    public static void Display()
    {
        Console.WriteLine("Activity Log");
        Console.WriteLine("------------");

        if (_counts.Count == 0)
        {
            Console.WriteLine("No activities logged yet.");
            return;
        }

        foreach (KeyValuePair<string, int> entry in _counts)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
    }

    private static Dictionary<string, int> Load()
    {
        Dictionary<string, int> counts = new Dictionary<string, int>();

        if (!File.Exists(LogFile))
        {
            return counts;
        }

        foreach (string line in File.ReadAllLines(LogFile))
        {
            string[] parts = line.Split('=');
            int count;

            if (parts.Length == 2 && int.TryParse(parts[1], out count))
            {
                counts[parts[0]] = count;
            }
        }

        return counts;
    }

    private static void Save()
    {
        List<string> lines = new List<string>();

        foreach (KeyValuePair<string, int> entry in _counts)
        {
            lines.Add($"{entry.Key}={entry.Value}");
        }

        File.WriteAllLines(LogFile, lines);
    }
}
