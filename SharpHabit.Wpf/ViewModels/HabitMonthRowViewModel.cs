using System.Collections.ObjectModel;

namespace SharpHabit.Wpf.ViewModels
{
    public class HabitMonthRowViewModel : ViewModelBase
    {
        public string HabitName { get; }

        public ObservableCollection<HabitDayCellViewModel> Days { get; } = new();

        public HabitMonthRowViewModel(string habitName, int daysInMonth)
        {
            this.HabitName = habitName;
            
            for (int day = 1; day <= daysInMonth; day++)
            {
                Days.Add(new HabitDayCellViewModel(day));
            }
        }
    }
}
