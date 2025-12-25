using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SharpHabit.Wpf.ViewModels
{
    public class MonthRowViewModel
    {
        public Guid HabitId { get; }

        public string HabitName { get; } = null!;

        public ObservableCollection<HabitDayCellViewModel> Cells { get; } = new();

        public MonthRowViewModel(Guid habitId, string habitName)
        {
            this.HabitId = habitId;
            this.HabitName = habitName;
        }
    }
}
