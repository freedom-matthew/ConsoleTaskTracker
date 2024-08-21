using System.Text.Json;

namespace ConsoleTaskTracker.Services
{
    internal class TaskService
    {
        private readonly string filePath = @"C:\Users\svobo\Desktop\tasks.json";

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
