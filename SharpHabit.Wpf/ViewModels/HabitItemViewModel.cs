namespace SharpHabit.Wpf.ViewModels
{
    public class HabitItemViewModel : ViewModelBase
    {
        private string name = "";
        private bool isDoneToday;

        public string Name { get => name; set => SetField(ref name, value);}

        public bool IsDoneToday { get => isDoneToday; set => SetField(ref isDoneToday, value);}
    }
}
