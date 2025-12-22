using CommunityToolkit.Mvvm.ComponentModel;

namespace SharpHabit.Wpf.ViewModels
{
    public partial class HabitItemViewModel : ObservableObject
    {   
        public Guid HabitId;

        public required string Name;

        [ObservableProperty]
        private bool isDoneToday;
    }
}
