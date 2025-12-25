using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharpHabit.Core;

namespace SharpHabit.Wpf.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private DateOnly today;

        private int year;
        private int month;
        private int daysInMonth;



        private readonly HabitTracker tracker = new HabitTracker();

        public ObservableCollection<HabitItemViewModel> Habits { get; } = new();

        [ObservableProperty]
        private string newHabitName = "";

        public int TotalHabits => this.tracker.Habits.Count;

        public int DoneHabits => this.tracker.Habits.Count(h => tracker.IsDone(h.Id, today));

        public double TodayPercentage => this.tracker.DayPercentage(today);


        public ObservableCollection<int> MonthDays { get; } = new();
        public ObservableCollection<MonthRowViewModel> MonthRows { get; } = new();

        public MainViewModel()
        {
            this.today = DateOnly.FromDateTime(DateTime.Now);
            this.year = today.Year;
            this.month = today.Month;
            this.daysInMonth = DateTime.DaysInMonth(year, month);

            RefreshMonthDays();

            tracker.AddHabit("Workout");
            tracker.AddHabit("Read");

            RefreshMonthGrid();

            RefreshTodayList();
            RefreshStats();
        }


        public void RefreshTodayList()
        {
            Habits.Clear();

            foreach (var habit in tracker.Habits)
            {
                HabitItemViewModel habitVM = new(habit.Id, habit.Name);

                habitVM.IsDoneToday = tracker.IsDone(habit.Id, today);

                Habits.Add(habitVM);
            }
        }

        public void RefreshStats()
        {
            OnPropertyChanged(nameof(TotalHabits));
            OnPropertyChanged(nameof(DoneHabits));
            OnPropertyChanged(nameof(TodayPercentage));
        }

        public void RefreshMonthDays()
        {
            MonthDays.Clear();
            for (int day = 1; day <= daysInMonth; day++)
            {
                MonthDays.Add(day);
            }
        }

        public void RefreshMonthGrid()
        {
            MonthRows.Clear();

            foreach (var habit in Habits)
            {
                MonthRowViewModel monthRow = new(habit.HabitId, habit.Name, year, month, daysInMonth);

                for (int day = 1; day <- daysInMonth; day++)
                {
                    DateOnly date = new DateOnly(year, month, day);

                    HabitDayCellViewModel cell = new(habit.HabitId, date);
                    cell.IsDone = tracker.IsDone(habit.HabitId, date);

                    monthRow.Cells.Add(cell);
                }

                MonthRows.Add(monthRow);
            }
        }

        [RelayCommand]
        public void AddHabit()
        {
            tracker.AddHabit(this.NewHabitName.Trim());

            NewHabitName = "";

            RefreshTodayList();
            RefreshStats();
            RefreshMonthGrid();
        }

        [RelayCommand]
        public void ToggleToday(HabitItemViewModel habit)
        {
            tracker.Toggle(habit.HabitId, today);

            habit.IsDoneToday = tracker.IsDone(habit.HabitId, today);

            RefreshStats();
            RefreshMonthGrid();
        }
    }
}
