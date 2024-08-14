namespace ConsoleTaskTracker
{
    public class Task(string description)
    {
        private static int counter = 0;
        public int TaskId { get; private set; } = ++counter;
        public string Description { get; set; } = description;
        public TaskStatus Status { get; set; } = TaskStatus.Todo;
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public DateTime UpdatedAt { get; private set; } = DateTime.Now;

        public void MarkInProgress()
        {
            Status = TaskStatus.InProgress;
            UpdatedAt = DateTime.Now;
        }

        public void MarkDone()
        {
            Status = TaskStatus.Done;
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
