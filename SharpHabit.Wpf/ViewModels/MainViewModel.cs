using System.Collections.ObjectModel;
using System.Windows.Input;
using SharpHabit.Wpf.Commands;

namespace SharpHabit.Wpf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<HabitItemViewModel> Habits { get; } = new();

        public ICommand AddHabitCommand { get; }

        public MainViewModel()
        {
            Habits.Add(new HabitItemViewModel { Name = "Wokout" });
            Habits.Add(new HabitItemViewModel { Name = "Code" });
            Habits.Add(new HabitItemViewModel { Name = "Creatine" });

            AddHabitCommand = new RelayCommand(AddHabit, () => Habits.Count < 5);
        }

        private void AddHabit()
        {
            Habits.Add(new HabitItemViewModel
            {
                Name = "New habit",
            });
        }
    }
}
