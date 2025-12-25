using CommunityToolkit.Mvvm.ComponentModel;

namespace SharpHabit.Wpf.ViewModels
{
    public partial class HabitDayCellViewModel : ObservableObject
    {
        public Guid HabitId { get; }

        public DateOnly Date { get; }

        [ObservableProperty]
        private bool isDone;

        public HabitDayCellViewModel(Guid habitId, DateOnly date)
        {
            this.HabitId = habitId;
            this.Date = date;
        }
    }
}
