using System;
using System.Linq;
using ConsoleTaskTracker.Entities;
using ConsoleTaskTracker.Persistence;

namespace ConsoleTaskTracker.Services;

public static class TodoListService
{
    private static readonly JsonTaskStore Store = new();

    public static void AddTodoItem(string description)
    {
        var item = Store.Add(description);
        Console.WriteLine($"Task added successfully (ID: {item.Id})");
    }

    public static void UpdateTodoItem(int id, string newDescription)
    {
        Store.Update(id, newDescription);
        Console.WriteLine($"Task {id} updated.");
    }

    public static void DeleteTodoItem(int id)
    {
        Store.Delete(id);
        Console.WriteLine($"Task {id} deleted.");
    }

    public static void SetStatus(int id, TodoItemStatus status)
    {
        Store.SetStatus(id, status);
        Console.WriteLine($"Task {id} set to {status}.");
    }

    public static void ListTodoItems()
    {
        var items = Store.GetAll().OrderBy(i => i.Id).ToList();
        if (items.Count == 0) { Console.WriteLine("(no tasks)"); return; }
        foreach (var t in items)
            Print(t);
    }

    public static void ListTodoItemsByStatus(TodoItemStatus status)
    {
        var items = Store.GetAll()
            .Where(i => i.Status == status)
            .OrderBy(i => i.Id)
            .ToList();

        if (items.Count == 0) { Console.WriteLine($"(no tasks with status {status})"); return; }
        foreach (var t in items)
            Print(t);
    }

    private static void Print(TodoItem t)
    {
        Console.WriteLine($"#{t.Id} [{t.Status}] {t.Description}");
        Console.WriteLine($"   createdAt: {t.CreatedAt:O}");
        Console.WriteLine($"   updatedAt: {t.UpdatedAt:O}");
    }
}