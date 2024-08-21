namespace ConsoleTaskTracker.Entities
{
    internal class Task
    {
        private static int _counter = 0;
        public int TaskId { get; private set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }

        public Task(string description)
        {
            TaskId = ++_counter;
            Description = description;
            Status = TaskStatus.Todo;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}
