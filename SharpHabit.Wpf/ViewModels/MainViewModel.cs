using System.Collections.ObjectModel;
using System.Windows.Input;
using SharpHabit.Wpf.Commands;
using System.Linq;

namespace SharpHabit.Wpf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<HabitItemViewModel> Habits { get; } = new();


        public int TotalHabits => Habits.Count;
        public int DoneHabits => Habits.Count(h => h.IsDoneToday);
        public double CompletionPercent
        {
            get
            {
                if (TotalHabits == 0) return 0;
                return (double)DoneHabits / TotalHabits * 100.0;
            }
        }


        private readonly RelayCommand refreshStatsCommand;
        public ICommand RefreshStatsCommand => refreshStatsCommand;



        private readonly RelayCommand<HabitItemViewModel> habitToggledCommand;
        public ICommand HabitToggledCommand => habitToggledCommand;



        private readonly RelayCommand addHabitCommand;
        public ICommand AddHabitCommand => addHabitCommand;

        private string newHabitName = "";
        public string NewHabitName 
        {
            get => newHabitName;
            set 
            {
                if (SetField(ref newHabitName, value))
                {
                    addHabitCommand.RaiseCanExecuteChanged();
                }
            }
        }



        public ObservableCollection<int> MonthDays { get; } = new();
        public ObservableCollection<HabitMonthRowViewModel> MonthRows { get; } = new();



        private readonly RelayCommand<HabitDayCellViewModel> toggleCellCommand;
        public ICommand ToggleCellCommand => toggleCellCommand;
    

        public MainViewModel()
        {
            Habits.Add(new HabitItemViewModel { Name = "Wokout" });
            Habits.Add(new HabitItemViewModel { Name = "Code" });
            Habits.Add(new HabitItemViewModel { Name = "Creatine" });

            addHabitCommand = new RelayCommand(AddHabit, CanAddHabit);
            refreshStatsCommand = new RelayCommand(RefreshStats);
            habitToggledCommand = new RelayCommand<HabitItemViewModel>( _ => RefreshStats());
            toggleCellCommand = new RelayCommand<HabitDayCellViewModel>(ToggleCell);


            BuildMonthView();
        }

        private void BuildMonthView()
        {
            MonthDays.Clear();
            MonthRows.Clear();

            var current = DateTime.Now;
            int daysInMonth = DateTime.DaysInMonth(current.Year, current.Month);

            for (int day = 1;  day <= daysInMonth; day++)
            {
                MonthDays.Add(day);
            }

            foreach (var habit in Habits)
            {
                MonthRows.Add(new HabitMonthRowViewModel(habit.Name, daysInMonth));
            }
        }

        private void ToggleCell(HabitDayCellViewModel? cell)
        {
            if (cell == null) return;
            cell.IsDone = !cell.IsDone;
        }


        private void RefreshStats()
        {
            OnPropertyChange(nameof(TotalHabits));
            OnPropertyChange(nameof(DoneHabits));
            OnPropertyChange(nameof(CompletionPercent));
        }


        private void AddHabit()
        {
            Habits.Add(new HabitItemViewModel
            {
                Name = this.newHabitName.Trim(),
            });

            NewHabitName = "";
        }

        private bool CanAddHabit()
        {
            return !string.IsNullOrWhiteSpace(newHabitName);
        }
    }
}
