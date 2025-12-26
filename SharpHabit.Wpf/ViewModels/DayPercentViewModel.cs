using CommunityToolkit.Mvvm.ComponentModel;

namespace SharpHabit.Wpf.ViewModels
{
    public partial class DayPercentViewModel : ObservableObject
    {
        public int DayNumber { get; }

        [ObservableProperty]
        private double percent;

        public DayPercentViewModel(int dayNumber)
        {
            this.DayNumber = dayNumber;
        }
    }
}
