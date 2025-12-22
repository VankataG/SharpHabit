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
            Habit shower = tracker.AddHabit("Cold shower");

            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            tracker.Toggle(study.Id, today);
            //tracker.Toggle(workout.Id, today);
            //tracker.Toggle(shower.Id, today);


            Console.WriteLine($"Done today: {tracker.DayPercentage(today):f2}%");
            Console.WriteLine($"Month: {tracker.MonthPercentage(today.Year, today.Month):f2}%");
        }
    }
}
