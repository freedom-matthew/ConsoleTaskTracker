using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleTaskTracker.Services
{
    internal class TaskService
    {
        private readonly string _jsonFile = @"C:\Users\svobo\Desktop\tasks.json";
        // private readonly string _jsonFile = @"C:\Users\msvoboda\Desktop\tasks.json";

        private readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

        private int FindMaxId()
        {
            if (!File.Exists(_jsonFile))
            {
                return 0;
            }
            
            string jsonString = File.ReadAllText(_jsonFile);
            
            if (string.IsNullOrEmpty(jsonString))
            {
                return 0;
            }

            else
            {
                List<Entities.Task> tasks = JsonSerializer.Deserialize<List<Entities.Task>>(jsonString, _options);

                int maxId = tasks.Max(task => task.TaskId);
                return maxId;
            }            
        }

        public void AddTaskToFile(string description)
        {
            List<Entities.Task> tasks;

            if (File.Exists(_jsonFile))
            {
                string jsonString = File.ReadAllText(_jsonFile);

                if (string.IsNullOrWhiteSpace(jsonString))
                {
                    tasks = [];
                }

                else
                {
                    tasks = JsonSerializer.Deserialize<List<Entities.Task>>(jsonString, _options);
                }
            }

            else
            {
                tasks = [];
            }

            Entities.Task task = new()
            {
                TaskId = FindMaxId() + 1,
                Description = description,
                Status = Entities.TaskStatus.Todo,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            tasks.Add(task);

            string updatedJson = JsonSerializer.Serialize(tasks, _options);
            File.WriteAllText(_jsonFile, updatedJson);
        }
    }
}
