namespace SharpHabit.Core.Storage
{
    public class HabitDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public HabitDto(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}