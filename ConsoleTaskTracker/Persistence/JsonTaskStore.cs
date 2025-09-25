using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using ConsoleTaskTracker.Entities;

namespace ConsoleTaskTracker.Persistence;

public class JsonTaskStore
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    public JsonTaskStore()
    {
        _filePath = Path.GetFullPath("tasks.json", Directory.GetCurrentDirectory());
        EnsureFileExists();
    }
    
    public List<TodoItem> GetAll() => Load();

    public TodoItem Add(string description)
    {
        var todoItems = Load();

        var todoItem = new TodoItem
        {
            Id = todoItems.Count == 0 ? 1 : todoItems.Max(i => i.Id) + 1,
            Description = description,
            Status = TodoItemStatus.Todo,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow,
        };

        todoItems.Add(todoItem);
        Save(todoItems);
        
        return todoItem;
    }

    public void Update(int id, string newDescription)
    {
        var todoItems = Load();
        var todoItem = todoItems.FirstOrDefault(i => i.Id == id) ?? throw new InvalidOperationException($"Task {id} not found.");

        todoItem.Description = newDescription;
        todoItem.UpdatedAt = DateTime.UtcNow;
        Save(todoItems);
    }
    
    public void SetStatus(int id, TodoItemStatus status)
    {
        var todoItems = Load();
        var todoItem = todoItems.FirstOrDefault(i => i.Id == id) ?? throw new InvalidOperationException($"Task {id} not found.");

        todoItem.Status = status;
        todoItem.UpdatedAt = DateTime.UtcNow;
        Save(todoItems);
    }

    public void Delete(int id)
    {
        var todoItems = Load();
        var removedTodoItems = todoItems.RemoveAll(i => i.Id == id);

        if (removedTodoItems == 0)
        {
            throw new InvalidOperationException($"Task {id} not found.");
        }
        
        Save(todoItems);
    }

    private void EnsureFileExists()
    {
        if (File.Exists(_filePath))
        {
            return;
        }
        
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
        File.WriteAllText(_filePath, "[]");
    }
    
    private void Save(List<TodoItem> items)
    {
        var text = JsonSerializer.Serialize(items, _jsonOptions);
        File.WriteAllText(_filePath, text);
    }
    
    private List<TodoItem> Load()
    {
        var text = File.ReadAllText(_filePath);
        var todoItems = JsonSerializer.Deserialize<List<TodoItem>>(text, _jsonOptions) ?? [];
        return todoItems;
    }
}