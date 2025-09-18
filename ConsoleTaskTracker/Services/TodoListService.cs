using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using ConsoleTaskTracker.Entities;

namespace ConsoleTaskTracker.Services;

public static class TodoListService
{
    private static readonly string JsonFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "TodoList.json");

    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };
    
    public static void AddTodoItem(string description)
    {
        var todoItems = LoadTodoItems();
        var todoItem = new TodoItem
        {
            Id = GetHighestId(todoItems) + 1,
            Description = description,
            Status = TodoItemStatus.Todo,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        }; 

        todoItems.Add(todoItem);
        SaveTodoItems(todoItems);
    }
    
    public static void UpdateTodoItem(int todoItemId, string description)
    {
        var todoItems = LoadTodoItems();
        var todoItem = todoItems.FirstOrDefault(t => t.Id == todoItemId);

        if (todoItem is null)
        {
            return;
        }

        todoItem.Description = description;
        todoItem.UpdatedAt = DateTime.Now;

        SaveTodoItems(todoItems);
    }
    
    public static void DeleteTodoItem(int todoItemId)
    {
        var todoItems = LoadTodoItems();
        var todoItem = todoItems.FirstOrDefault(t => t.Id == todoItemId);

        if (todoItem is null)
        {
            return;
        }
        
        todoItems.RemoveAll(t => t.Id == todoItemId);
        SaveTodoItems(todoItems);
    }

    public static void ListTodoItems()
    {
        var todoItems = LoadTodoItems();
        if (todoItems.Count == 0)
        {
            return;
        }

        foreach (var todoItem in todoItems)
        {
            Console.WriteLine($"[{todoItem.Id}] {todoItem.Description} - {todoItem.Status}");
        }
    }

    public static void ListTodoItemsByStatus(TodoItemStatus status)
    {
        var todoItems = LoadTodoItems().Where(t => t.Status == status).ToList();
        if (todoItems.Count == 0)
        {
            return;
        }

        foreach (var todoItem in todoItems)
        {
            Console.WriteLine($"[{todoItem.Id}] {todoItem.Description} - {todoItem.Status}");
        }
    }

    public static void SetStatus(int todoItemId, TodoItemStatus newStatus)
    {
        var todoItems = LoadTodoItems();
        var todoItem = todoItems.FirstOrDefault(t => t.Id == todoItemId);
        if (todoItem == null)
        {
            Console.WriteLine($"TodoItem with ID {todoItemId} was not found.");
            return;
        }

        todoItem.Status = newStatus;
        todoItem.UpdatedAt = DateTime.Now;
        SaveTodoItems(todoItems);
    }

    private static List<TodoItem> LoadTodoItems()
    {
        if (!File.Exists(JsonFile))
        {
            return [];
        }

        var jsonString = File.ReadAllText(JsonFile);

        return string.IsNullOrWhiteSpace(jsonString) ? [] : JsonSerializer.Deserialize<List<TodoItem>>(jsonString, Options) ?? [];
    }
    
    private static void SaveTodoItems(List<TodoItem> todoItems)
    {
        var jsonString = JsonSerializer.Serialize(todoItems, Options);
        File.WriteAllText(JsonFile, jsonString);
    }

    private static int GetHighestId(List<TodoItem> todoItems)
    {
        return todoItems.Count > 0 ? todoItems.Max(todoItem => todoItem.Id) : 0;
    }
}