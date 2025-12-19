using System.Collections.ObjectModel;
using System.Windows.Input;
using SharpHabit.Wpf.Commands;

namespace SharpHabit.Wpf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<HabitItemViewModel> Habits { get; } = new();


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
                    (AddHabitCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        public MainViewModel()
        {
            Habits.Add(new HabitItemViewModel { Name = "Wokout" });
            Habits.Add(new HabitItemViewModel { Name = "Code" });
            Habits.Add(new HabitItemViewModel { Name = "Creatine" });

            addHabitCommand = new RelayCommand(AddHabit, CanAddHabit);
        }

        private void AddHabit()
        {
            Habits.Add(new HabitItemViewModel
            {
                Name = this.newHabitName.Trim(),
            });

            this.newHabitName = "";
        }

        private bool CanAddHabit()
        {
            return !string.IsNullOrWhiteSpace(newHabitName);
        }
    }
}
