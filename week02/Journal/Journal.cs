using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Journal
{
    private string _owner;
    private List<Entry> _entries;

    public Journal(string owner)
    {
        _owner = owner;
        _entries = new List<Entry>();
    }

    public string Owner
    {
        get { return _owner; }
    }

    public int EntryCount
    {
        get { return _entries.Count; }
    }

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("This journal does not have any entries yet.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"Journal of {_owner} - {_entries.Count} entries");
        Console.WriteLine(new string('-', 50));

        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    public void DisplayEntries(List<Entry> entries)
    {
        if (entries.Count == 0)
        {
            Console.WriteLine("No matching entries were found.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"{entries.Count} matching entries");
        Console.WriteLine(new string('-', 50));

        foreach (Entry entry in entries)
        {
            entry.Display();
        }
    }

    public List<Entry> Search(string keyword)
    {
        List<Entry> matches = new List<Entry>();

        foreach (Entry entry in _entries)
        {
            bool promptMatches = entry.Prompt.Contains(keyword, StringComparison.OrdinalIgnoreCase);
            bool responseMatches = entry.Response.Contains(keyword, StringComparison.OrdinalIgnoreCase);
            bool moodMatches = entry.Mood.Contains(keyword, StringComparison.OrdinalIgnoreCase);

            if (promptMatches || responseMatches || moodMatches)
            {
                matches.Add(entry);
            }
        }

        return matches;
    }

    public void DisplayStatistics()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("There are no entries to summarize yet.");
            return;
        }

        int totalWords = 0;
        Entry longestEntry = _entries[0];
        Dictionary<string, int> moodCounts = new Dictionary<string, int>();

        foreach (Entry entry in _entries)
        {
            totalWords += CountWords(entry.Response);

            if (entry.Response.Length > longestEntry.Response.Length)
            {
                longestEntry = entry;
            }

            string mood = string.IsNullOrWhiteSpace(entry.Mood) ? "Not specified" : entry.Mood;

            if (moodCounts.ContainsKey(mood))
            {
                moodCounts[mood]++;
            }
            else
            {
                moodCounts[mood] = 1;
            }
        }

        double averageWords = (double)totalWords / _entries.Count;

        Console.WriteLine();
        Console.WriteLine($"Total entries: {_entries.Count}");
        Console.WriteLine($"Average words per entry: {averageWords:F1}");

        foreach (KeyValuePair<string, int> moodCount in moodCounts)
        {
            Console.WriteLine($"Mood '{moodCount.Key}': {moodCount.Value} entries");
        }

        Console.WriteLine($"Longest entry date: {longestEntry.Date}");
        Console.WriteLine();
    }

    public void SaveToFile(string fileName)
    {
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(_entries, options);
        File.WriteAllText(fileName, json);
        Console.WriteLine($"Journal saved to {fileName}.");
    }

    public void LoadFromFile(string fileName)
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine($"File not found: {fileName}");
            return;
        }

        string json = File.ReadAllText(fileName);
        List<Entry> loadedEntries = JsonSerializer.Deserialize<List<Entry>>(json);

        if (loadedEntries == null)
        {
            Console.WriteLine($"The file {fileName} does not contain a valid journal.");
            return;
        }

        _entries = loadedEntries;
        Console.WriteLine($"Journal loaded from {fileName} - {_entries.Count} entries.");
    }

    private int CountWords(string text)
    {
        char[] separators = new char[] { ' ', '\t', '\n', '\r' };
        string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        return words.Length;
    }
}
