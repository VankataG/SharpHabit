using CommunityToolkit.Mvvm.ComponentModel;

namespace SharpHabit.Wpf.ViewModels
{
    public partial class HabitItemViewModel : ObservableObject
    {   
        public Guid HabitId { get; }

        public string Name { get; } = null!;

        [ObservableProperty]
        private bool isDoneToday;

        public HabitItemViewModel(Guid habitId, string name)
        {
            this.HabitId = habitId;
            this.Name = name;
        }


    }
}
