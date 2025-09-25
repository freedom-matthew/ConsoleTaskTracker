using System;
using System.Linq;
using ConsoleTaskTracker.Entities;
using ConsoleTaskTracker.Persistence;

namespace ConsoleTaskTracker.Services;

public class TodoListService(ITaskStore store)
{
    private readonly ITaskStore _store = store ?? throw new ArgumentNullException(nameof(store));

    public void AddTodoItem(string description)
    {
        var item = _store.Add(description);
        Console.WriteLine($"Task added successfully (ID: {item.Id})");
    }

    public void UpdateTodoItem(int id, string newDescription)
    {
        _store.Update(id, newDescription);
        Console.WriteLine($"Task {id} updated.");
    }

    public void DeleteTodoItem(int id)
    {
        _store.Delete(id);
        Console.WriteLine($"Task {id} deleted.");
    }

    public void SetStatus(int id, TodoItemStatus status)
    {
        _store.SetStatus(id, status);
        Console.WriteLine($"Task {id} set to {status}.");
    }

    public void ListTodoItems()
    {
        var items = _store.GetAll().OrderBy(i => i.Id).ToList();
        if (items.Count == 0) { Console.WriteLine("(no tasks)"); return; }
        foreach (var t in items)
            Print(t);
    }

    public void ListTodoItemsByStatus(TodoItemStatus status)
    {
        var items = _store.GetAll()
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