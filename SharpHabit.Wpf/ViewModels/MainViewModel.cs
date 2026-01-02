using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharpHabit.Core;
using SharpHabit.Core.Storage;
using SharpHabit.Infrastructure;

namespace SharpHabit.Wpf.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private DateOnly today;

        private int year;
        private int month;
        private int daysInMonth;



        private readonly HabitTracker tracker = new HabitTracker();
        private readonly JsonStateStorage storage = new JsonStateStorage();

        public ObservableCollection<HabitItemViewModel> Habits { get; } = new();

        [ObservableProperty]
        private string newHabitName = "";

        public int TotalHabits => this.tracker.Habits.Count;

        public int DoneHabits => this.tracker.Habits.Count(h => tracker.IsDone(h.Id, today));

        public double TodayPercentage => this.tracker.DayPercentage(today);


        public ObservableCollection<int> MonthDays { get; } = new();
        public ObservableCollection<MonthRowViewModel> MonthRows { get; } = new();


        public ObservableCollection<DayPercentViewModel> DayPercents { get; } = new();


        public MainViewModel()
        {
            this.today = DateOnly.FromDateTime(DateTime.Now);
            this.year = today.Year;
            this.month = today.Month;
            this.daysInMonth = DateTime.DaysInMonth(year, month);

            TrackerState? state = storage.Load();
            if (state != null)
            {
                this.tracker.ImportState(state);
            }
            else
            {
                tracker.AddHabit("Get Up Early");
                tracker.AddHabit("Brush Teeth");
                tracker.AddHabit("Read Book");
                tracker.AddHabit("Workout");
                tracker.AddHabit("Study");
            }

            RefreshMonthDays();
            RefreshMonthGrid();
            RefreshDayPercents();
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

            foreach (var habit in tracker.Habits)
            {
                MonthRowViewModel monthRow = new(habit.Id, habit.Name);

                for (int day = 1; day <= daysInMonth; day++)
                {
                    DateOnly date = new DateOnly(year, month, day);

                    HabitDayCellViewModel cell = new(habit.Id, date);
                    cell.IsDone = tracker.IsDone(habit.Id, date);

                    monthRow.Cells.Add(cell);
                }

                MonthRows.Add(monthRow);
            }
        }


        public void RefreshDayPercents()
        {
            this.DayPercents.Clear();

            for ( int day = 1; day <= daysInMonth; day++)
            {
                DateOnly date = new DateOnly(year, month, day);

                DayPercentViewModel dayPercent = new(day);

                dayPercent.Percent = tracker.DayPercentage(date);

                this.DayPercents.Add(dayPercent);
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
            RefreshDayPercents();

            this.storage.Save(tracker.ExportState());
        }

        [RelayCommand]
        public void ToggleToday(HabitItemViewModel habit)
        {
            tracker.Toggle(habit.HabitId, today);

            habit.IsDoneToday = tracker.IsDone(habit.HabitId, today);

            RefreshStats();

            this.storage.Save(tracker.ExportState());
        }

        [RelayCommand]
        public void ToggleCell(HabitDayCellViewModel cell)
        {
            tracker.Toggle(cell.HabitId, cell.Date);

            cell.IsDone = tracker.IsDone(cell.HabitId, cell.Date);

            RefreshStats();
            RefreshDayPercents();

            this.storage.Save(tracker.ExportState());
        }
    }
}
