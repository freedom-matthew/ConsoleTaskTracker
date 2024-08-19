using System.Text.Json;

namespace ConsoleTaskTracker
{
    public class Task
    {
        private static int _counter = 0;
        private readonly string filePath = @"C:\Users\svobo\Desktop\tasks.json";
        public int TaskId { get; private set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }

        public Task(string description)
        {
            this.TaskId = ++_counter;
            this.Description = description;
            this.Status = TaskStatus.Todo;
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }

        public void AddTaskToFile(Task task)
        {
            List<Task> tasks = [];
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                tasks = JsonSerializer.Deserialize<List<Task>>(json) ?? [];
            }

            tasks.Add(task);
            string updatedJson = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, updatedJson);
        }
    }
}
