using System.Text.Json;

namespace ConsoleTaskTracker.Services
{
    internal class TaskService
    {
        // private readonly string _filePath = @"C:\Users\svobo\Desktop\tasks.json";
        private readonly string _filePath = @"C:\Users\msvoboda\Desktop\tasks.json";

        public TaskService()
        {

        }

        public void AddTaskToFile(string description)
        {
            ConsoleTaskTracker.Entities.Task task = new()
            {
                TaskId = 1,
                Description = description,
                Status = ConsoleTaskTracker.Entities.TaskStatus.Todo,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            string json = JsonSerializer.Serialize(task);
            File.WriteAllText(_filePath, json);
        }

        public void UpdateTaskInFile(int id, string description)
        {
        }
    }
}
