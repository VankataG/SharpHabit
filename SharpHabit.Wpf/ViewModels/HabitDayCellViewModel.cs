namespace SharpHabit.Wpf.ViewModels
{
    public class HabitDayCellViewModel : ViewModelBase
    {
        public int DayNumber { get; }

        private bool isDone;
        public bool IsDone
        {
            get => isDone;
            set => SetField(ref isDone, value);
        }

        public HabitDayCellViewModel(int dayNumber)
        {
            this.DayNumber = dayNumber;
        }
    }
}
