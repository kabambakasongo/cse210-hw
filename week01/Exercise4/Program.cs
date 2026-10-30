using System;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();

        int userNumber = -1;
        while (userNumber != 0)
        {
            Console.Write("Enter a number (0 to quit): ");

            string userResponse = Console.ReadLine();
            userNumber = int.Parse(userResponse);

            // Only add the number to the list if it is not 0
            if (userNumber != 0)
            {
                numbers.Add(userNumber);
            }
        }

        // Part 1: Compute the sum
        int sum = 0;
        foreach (int number in numbers)
        {
            sum += number;
        }

        Console.WriteLine($"The sum is: {sum}");

        // Part 2: Compute the average
        float average = ((float)sum) / numbers.Count;
        Console.WriteLine($"The average is: {average}");

        // Part 3: Find the max
        // There are several ways to do this, such as sorting the list

        int max = numbers[0];

        foreach (int number in numbers)
        {
            if (number > max)
            {

                max = number;
            }
        }

        Console.WriteLine($"The max is: {max}");
    }
}