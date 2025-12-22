using CommunityToolkit.Mvvm.ComponentModel;
using SharpHabit.Core;

namespace SharpHabit.Wpf.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly HabitTracker tracker = new HabitTracker();

        private DateOnly today = DateOnly.FromDateTime(DateTime.Now);


    }
}
