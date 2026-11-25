using System;
using System.Collections.Generic;
using System.Threading;

public abstract class Activity
{
    private string _name;
    private string _description;
    private int _duration;
    private DateTime _endTime;
    private static readonly Random _random = new Random();

    protected Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    protected int Duration
    {
        get { return _duration; }
    }

    protected DateTime EndTime
    {
        get { return _endTime; }
    }

    public void Run()
    {
        Start();
        PerformActivity();
        End();
    }

    protected abstract void PerformActivity();

    private void Start()
    {
        Console.WriteLine($"Starting {_name} Activity");
        Console.WriteLine(_description);
        Console.Write("How long, in seconds, would you like for your session? ");
        _duration = GetDuration();
        ClearScreen();
        Console.WriteLine("Get ready...");
        ShowSpinner(3);
        Console.WriteLine();
        _endTime = DateTime.Now.AddSeconds(_duration);
    }

    private void End()
    {
        Console.WriteLine();
        Console.WriteLine("Well done!!");
        ShowSpinner(3);
        Console.WriteLine();
        Console.WriteLine($"You have completed {_duration} seconds of the {_name} Activity.");
        ShowSpinner(3);
        Console.WriteLine();
        ActivityLog.Record(_name);
    }

    private int GetDuration()
    {
        while (true)
        {
            string input = Console.ReadLine();
            int seconds;

            if (int.TryParse(input, out seconds) && seconds > 0)
            {
                return seconds;
            }

            Console.Write("Please enter a positive whole number of seconds: ");
        }
    }

    protected static void ClearScreen()
    {
        if (!Console.IsOutputRedirected)
        {
            Console.Clear();
        }
    }

    protected int GetRemainingSeconds()
    {
        TimeSpan remaining = _endTime - DateTime.Now;
        return Math.Max(0, (int)Math.Ceiling(remaining.TotalSeconds));
    }

    protected void ShowSpinner(int seconds)
    {
        string[] frames = { "|", "/", "-", "\\" };

        for (int i = 0; i < seconds * frames.Length; i++)
        {
            if (i > 0)
            {
                Console.Write("\b \b");
            }

            Console.Write(frames[i % frames.Length]);
            Thread.Sleep(250);
        }

        if (seconds > 0)
        {
            Console.Write("\b \b");
        }
    }

    protected void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            int width = i.ToString().Length;
            Console.Write(new string('\b', width));
            Console.Write(new string(' ', width));
            Console.Write(new string('\b', width));
        }
    }

    protected static string GetUnusedOption(List<string> options, List<string> used)
    {
        if (used.Count >= options.Count)
        {
            used.Clear();
        }

        string option;
        do
        {
            option = options[_random.Next(options.Count)];
        }
        while (used.Contains(option));

        used.Add(option);
        return option;
    }
}
