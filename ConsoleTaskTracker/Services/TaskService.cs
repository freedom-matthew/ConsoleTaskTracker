using System.Text.Json;

namespace ConsoleTaskTracker.Services
{
    internal class TaskService
    {
        // private readonly string _jsonFile = @"C:\Users\svobo\Desktop\tasks.json";
        private readonly string _jsonFile = @"C:\Users\msvoboda\Desktop\tasks.json";

        public int FindLeastId()
        {
            if (File.Exists(_jsonFile))
            {
                string jsonString = File.ReadAllText(_jsonFile);
                List<Entities.Task> tasks = JsonSerializer.Deserialize<List<Entities.Task>>(jsonString);
                
                foreach (Entities.Task task in tasks)
                {
                    task.TaskId.CompareTo(0);
                }
            }
            return 0;
        }

        public void AddTaskToFile(string description)
        {
            Entities.Task task = new()
            {
                TaskId = FindLeastId() + 1,
                Description = description,
                Status = Entities.TaskStatus.Todo,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            string json = JsonSerializer.Serialize(task);
            File.WriteAllText(_jsonFile, json);
        }

        //public void UpdateTaskInFile(int id, string description)
        //{
        //}
    }
}
