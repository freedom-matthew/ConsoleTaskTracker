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

        if (tasks.Count > 0)
        {
            return tasks.Max(task => task.TaskId);
        }
        else
        {
            return 0;
        }
    }

    public static void AddTaskToFile(string description)
    {
        List<Entities.Task> tasks;

        if (File.Exists(_jsonFile))
        {
            string jsonString = File.ReadAllText(_jsonFile);

            try
            {
                tasks = JsonSerializer.Deserialize<List<Entities.Task>>(jsonString, _options) ?? new List<Entities.Task>();
            }
            catch (JsonException)
            {
                tasks = new List<Entities.Task>();
            }
        }
        else
        {
            tasks = [];
        }

        int newTaskId = FindGreatestId() + 1;

        Entities.Task task = new()
        {
            TaskId = newTaskId,
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
