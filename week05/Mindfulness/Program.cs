using System;

// Exceeds requirements: random prompts and reflection questions are not repeated until every option has been used,
// activity counts are saved to and loaded from mindfulness-log.txt, and a menu option displays the saved log.

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine();
            Console.WriteLine("1. Start breathing activity");
            Console.WriteLine("2. Start reflecting activity");
            Console.WriteLine("3. Start listing activity");
            Console.WriteLine("4. View activity log");
            Console.WriteLine("5. Quit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    new BreathingActivity().Run();
                    break;
                case "2":
                    new ReflectionActivity().Run();
                    break;
                case "3":
                    new ListingActivity().Run();
                    break;
                case "4":
                    Console.WriteLine();
                    ActivityLog.Display();
                    break;
                case "5":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Please choose a valid option.");
                    break;
            }

            Console.WriteLine();
        }
    }
}
