namespace SharpHabit.Core.Storage
{
    public class TrackerState
    {
        public List<HabitDto> Habits { get; set; } = new();

        public List<DoneDto> Done { get; set; } = new();

        

    }
}
