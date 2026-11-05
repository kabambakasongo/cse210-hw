using System;
using System.Text.Json.Serialization;

public class Entry
{
    private string _date;
    private string _prompt;
    private string _response;
    private string _mood;

    [JsonConstructor]
    public Entry(string date, string prompt, string response, string mood = "")
    {
        _date = date;
        _prompt = prompt;
        _response = response;
        _mood = mood;
    }

    public string Date
    {
        get { return _date; }
    }

    public string Prompt
    {
        get { return _prompt; }
    }

    public string Response
    {
        get { return _response; }
    }

    public string Mood
    {
        get { return _mood; }
    }

    public void Display()
    {
        Console.WriteLine($"Date: {_date}");
        Console.WriteLine($"Prompt: {_prompt}");
        Console.WriteLine($"Response: {_response}");

        if (!string.IsNullOrWhiteSpace(_mood))
        {
            Console.WriteLine($"Mood: {_mood}");
        }

        Console.WriteLine();
    }
}
