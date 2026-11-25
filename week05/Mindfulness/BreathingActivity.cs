using System;

public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    protected override void PerformActivity()
    {
        while (DateTime.Now < EndTime)
        {
            Console.WriteLine("Breathe in...");
            ShowCountdown(Math.Min(4, GetRemainingSeconds()));

            if (DateTime.Now >= EndTime)
            {
                break;
            }

            Console.WriteLine("Breathe out...");
            ShowCountdown(Math.Min(6, GetRemainingSeconds()));
        }
    }
}
