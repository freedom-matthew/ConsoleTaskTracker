using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleTaskTracker.Services;

internal class TaskService
{
    private static readonly string _jsonFile = @"C:\Users\msvoboda\Desktop\tasks.json";

    private static readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };

    private static int FindGreatestId()
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

        List<Entities.Task> tasks = JsonSerializer.Deserialize<List<Entities.Task>>(jsonString, _options) ?? [];

        return tasks.Count > 0 ? tasks.Max(task => task.TaskId) : 0;
    }

    public static void AddTaskToFile(string description)
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
                tasks = JsonSerializer.Deserialize<List<Entities.Task>>(jsonString, _options) ?? [];
            }
        }

        else
        {
            tasks = [];
        }

        Entities.Task task = new()
        {
            TaskId = FindGreatestId() + 1,
            Description = description,
            Status = Entities.TaskStatus.Todo,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        tasks.Add(task);

        string updatedJsonString = JsonSerializer.Serialize(tasks, _options);
        File.WriteAllText(_jsonFile, updatedJsonString);
    }

    public static void UpdateTaskFromFile(int taskId, string description)
    {
        throw new NotImplementedException();
    }

    public static void DeleteTaskFromFile(int taskId)
    {
        List<Entities.Task> tasks;

        string jsonString = File.ReadAllText(_jsonFile);
        tasks = JsonSerializer.Deserialize<List<Entities.Task>>(jsonString, _options) ?? [];

        tasks.RemoveAll(task => task.TaskId == taskId);

        string updatedJsonString = JsonSerializer.Serialize(tasks, _options);
        File.WriteAllText(_jsonFile, updatedJsonString);
    }
}
