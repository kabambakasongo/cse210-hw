using System;

// Creativity / Exceeding the Core Requirements:
// 1. Each entry can include a mood, which is saved and loaded with the entry.
// 2. The journal can be searched by a keyword across prompts, responses, and moods.
// 3. A statistics option reports the number of entries, average words per entry,
//    mood counts, and the date of the longest entry.
// 4. The journal is saved in a readable JSON format so it can be loaded later
//    without losing line breaks or special characters in a response.
// 5. The prompt generator can be extended with extra prompts through AddPrompt.

class Program
{
    static void Main(string[] args)
    {
        PromptGenerator promptGenerator = new PromptGenerator();
        Journal journal = new Journal(PromptOwnerName());

        bool running = true;

        while (running)
        {
            DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    WriteNewEntry(journal, promptGenerator);
                    break;
                case "2":
                    journal.DisplayAll();
                    break;
                case "3":
                    SaveJournal(journal);
                    break;
                case "4":
                    LoadJournal(journal);
                    break;
                case "5":
                    SearchJournal(journal);
                    break;
                case "6":
                    journal.DisplayStatistics();
                    break;
                case "7":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Please choose a number from 1 to 7.");
                    break;
            }
        }
    }

    static string PromptOwnerName()
    {
        Console.Write("What is your name? ");
        string name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name))
        {
            name = "My Journal";
        }

        return name;
    }

    static void DisplayMenu()
    {
        Console.WriteLine();
        Console.WriteLine("Please select one of the following choices:");
        Console.WriteLine("1. Write a new entry");
        Console.WriteLine("2. Display the journal");
        Console.WriteLine("3. Save the journal to a file");
        Console.WriteLine("4. Load the journal from a file");
        Console.WriteLine("5. Search the journal");
        Console.WriteLine("6. Show journal statistics");
        Console.WriteLine("7. Quit");
        Console.Write("What would you like to do? ");
    }

    static void WriteNewEntry(Journal journal, PromptGenerator promptGenerator)
    {
        string prompt = promptGenerator.GetRandomPrompt();
        Console.WriteLine();
        Console.WriteLine(prompt);
        Console.Write("> ");
        string response = Console.ReadLine();

        Console.Write("How would you describe your mood? (optional) ");
        string mood = Console.ReadLine();

        string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        Entry entry = new Entry(date, prompt, response, mood);
        journal.AddEntry(entry);

        Console.WriteLine("Your entry has been added to the journal.");
    }

    static void SaveJournal(Journal journal)
    {
        Console.Write("What is the filename? ");
        string fileName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(fileName))
        {
            fileName = "journal.json";
        }

        journal.SaveToFile(fileName);
    }

    static void LoadJournal(Journal journal)
    {
        Console.Write("What is the filename? ");
        string fileName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(fileName))
        {
            fileName = "journal.json";
        }

        journal.LoadFromFile(fileName);
    }

    static void SearchJournal(Journal journal)
    {
        Console.Write("What keyword would you like to search for? ");
        string keyword = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(keyword))
        {
            Console.WriteLine("Please enter a keyword to search.");
            return;
        }

        journal.DisplayEntries(journal.Search(keyword));
    }
}
