using System.Xml;
using SharpHabit.Core;
using SharpHabit.Core.Models;

namespace SharpHabit.Playground
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Ivan!");

            HabitTracker tracker = new HabitTracker();

            Habit study = tracker.AddHabit("Study");
            Habit workout = tracker.AddHabit("Workout");

            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            var toggled = tracker.Toggle(study.Id, today);
            tracker.Toggle(workout.Id, today);

            Console.WriteLine(toggled);

            Console.WriteLine($"Done today: {tracker.DayPercentage(today)}%");
            Console.WriteLine($"Month: {tracker.MonthPercentage(today.Year, today.Month):f2}%");
        }
    }
}
