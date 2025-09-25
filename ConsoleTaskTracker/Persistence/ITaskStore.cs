using System.Collections.Generic;
using ConsoleTaskTracker.Entities;

namespace ConsoleTaskTracker.Persistence;

public interface ITaskStore
{
    List<TodoItem> GetAll();
    void Add(string description);
    void Update(int id, string newDescription);
    void SetStatus(int id, TodoItemStatus status);
    void Delete(int id);
}