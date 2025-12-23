using SharpHabit.Core.Models;

namespace SharpHabit.Core
{
    public sealed class HabitTracker
    {
        private List<Habit> habits = new();

        public IReadOnlyList<Habit> Habits => habits;


        private HashSet<(Guid HabitId, DateOnly Date)> done = new();


        public Habit AddHabit(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Habit should have a name.");
            }

            Habit newHabit = new Habit() 
            {
                Id = Guid.NewGuid(),
                Name = name 
            };

            habits.Add(newHabit);
            
            return newHabit;
        }

        public bool RemoveHabit(Guid habitId)
        {
            Habit? habitToRemove = habits.SingleOrDefault(h => h.Id == habitId);

            if (habitToRemove is null) return false;

            done.RemoveWhere(h => h.HabitId == habitId);
            return habits.Remove(habitToRemove);
        }


        public bool IsDone(Guid habitId, DateOnly date) => done.Contains((habitId, date));
        

        public bool Toggle(Guid habitId, DateOnly date)
        {
            var key = (habitId, date);

            if (done.Contains((habitId, date)))
            {
                done.Remove(key);
                return false;
            }

            done.Add(key);
            return true;
        }


        public double DayPercentage(DateOnly date)
        {
            if (!CheckForHabits()) return 0;

            int doneHabits = 0;

            foreach (var habit in habits)
            {
                if (IsDone(habit.Id, date)) doneHabits++;
            }

            return (double)doneHabits / habits.Count * 100.0;
        }

        public double MonthPercentage(int year, int month)
        {
            if (!CheckForHabits()) return 0;

            int daysInMonth = DateTime.DaysInMonth(year, month);
            int totalCells = habits.Count * daysInMonth;

            int doneHabits = 0;

            foreach (Habit habit in habits)
            {
                for (int day = 1; day <= daysInMonth; day++)
                {
                    DateOnly date = new DateOnly(year, month, day);

                    if (IsDone(habit.Id, date)) doneHabits++;
                }
            }

            return (double)doneHabits / totalCells * 100.0;
            
        }

        public double HabitPercentage(Guid habitId, int year, int month)
        {
            if (!CheckForHabits()) return 0;

            int daysInMonth = DateTime.DaysInMonth(year, month);


            int done = 0;
            for (int day = 1; day <= daysInMonth; day++)
            {
                DateOnly date = new DateOnly(year, month, day);

                if(IsDone(habitId, date)) done++;
            }

            return (double)done / daysInMonth * 100.0;
        }

        public IEnumerable<int> GetDoneDays(Guid habitId, int year, int month)
        {
            List<int> doneDays = new List<int>();

            int daysInMonth = DateTime.DaysInMonth(year, month);

            for (int day = 1; day <= daysInMonth; day++)
            {
                DateOnly date = new DateOnly(year, month, day);

                if (IsDone(habitId, date)) doneDays.Add(day);
            }

            return doneDays;
        }


        private bool CheckForHabits()
        {
            if (habits.Count == 0) return false;

            return true;
        }
    }
}
