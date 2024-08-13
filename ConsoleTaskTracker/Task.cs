namespace ConsoleTaskTracker
{
    public class Task
    {
        public int TaskId { get; set; }
        public string? Description { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Task()
        {
            TaskId = Convert.ToInt32(Guid.NewGuid());
            Description = null;
            Status = Status.Todo;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
