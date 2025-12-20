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

        public MainViewModel()
        {
            Habits.Add(new HabitItemViewModel { Name = "Wokout" });
            Habits.Add(new HabitItemViewModel { Name = "Code" });
            Habits.Add(new HabitItemViewModel { Name = "Creatine" });

            addHabitCommand = new RelayCommand(AddHabit, CanAddHabit);
            refreshStatsCommand = new RelayCommand(RefreshStats);
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
