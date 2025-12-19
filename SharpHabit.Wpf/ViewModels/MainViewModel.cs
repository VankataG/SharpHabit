using System.Collections.ObjectModel;

namespace SharpHabit.Wpf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<HabitItemViewModel> Habits { get; } = new();

        public MainViewModel()
        {
            Habits.Add(new HabitItemViewModel { Name = "Wokout" });
            Habits.Add(new HabitItemViewModel { Name = "Code" });
            Habits.Add(new HabitItemViewModel { Name = "Creatine" });
        }
    }
}
