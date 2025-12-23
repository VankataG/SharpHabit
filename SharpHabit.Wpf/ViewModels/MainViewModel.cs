using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharpHabit.Core;

namespace SharpHabit.Wpf.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly HabitTracker tracker = new HabitTracker();

        private DateOnly today = DateOnly.FromDateTime(DateTime.Now);

        public ObservableCollection<HabitItemViewModel> Habits { get; } = new();

        [ObservableProperty]
        private string newHabitName = "";

        public int TotalHabits => this.tracker.Habits.Count;

        public int DoneHabits => this.tracker.Habits.Count(h => tracker.IsDone(h.Id, today));

        public double TodayPercentage => this.tracker.DayPercentage(today);

        public MainViewModel()
        {
            tracker.AddHabit("Workout");
            tracker.AddHabit("Read");

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

        [RelayCommand]
        public void AddHabit()
        {
            tracker.AddHabit(this.NewHabitName);

            NewHabitName = "";

            RefreshTodayList();
            RefreshStats();
        }

        [RelayCommand]
        public void ToggleToday(HabitItemViewModel habit)
        {
            tracker.Toggle(habit.HabitId, today);

            habit.IsDoneToday = tracker.IsDone(habit.HabitId, today);

            RefreshStats();
        }
    }
}
