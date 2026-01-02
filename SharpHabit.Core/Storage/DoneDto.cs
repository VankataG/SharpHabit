namespace SharpHabit.Core.Storage
{
    public class DoneDto
    {
        public Guid HabitId { get; set; }

        public DateOnly Date { get; set; }

        public DoneDto(Guid habitId, DateOnly date)
        {
            this.HabitId = habitId;
            this.Date = date;
        }
    }
}