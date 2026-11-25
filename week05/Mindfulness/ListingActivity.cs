using System;
using System.Collections.Generic;

public class ListingActivity : Activity
{
    private static readonly List<string> _prompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    private static readonly List<string> _usedPrompts = new List<string>();

    public ListingActivity()
        : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    protected override void PerformActivity()
    {
        Console.WriteLine("List as many responses you can to the following prompt:");
        Console.WriteLine();
        Console.WriteLine($"--- {GetUnusedOption(_prompts, _usedPrompts)} ---");
        Console.WriteLine();
        Console.Write("You may begin in: ");
        ShowCountdown(Math.Min(5, GetRemainingSeconds()));
        Console.WriteLine();

        List<string> items = new List<string>();

        while (DateTime.Now < EndTime)
        {
            Console.Write("> ");
            string item = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(item))
            {
                items.Add(item);
            }
        }

        Console.WriteLine($"You listed {items.Count} items!");
    }
}
