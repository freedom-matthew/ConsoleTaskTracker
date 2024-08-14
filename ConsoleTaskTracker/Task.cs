namespace ConsoleTaskTracker
{
    public class Task
    {
        public int TaskId { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Task(string description)
        {
            TaskId = 1;
            Description = description;
            Status = TaskStatus.Todo;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Task ID: {TaskId}\n" +
                   $"Description: {Description}\n" +
                   $"Status: {Status}\n" +
                   $"Created At: {CreatedAt}\n" +
                   $"Updated At: {UpdatedAt}";
        }
    }
}
