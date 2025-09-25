using System.Collections.Generic;
using ConsoleTaskTracker.Entities;

namespace ConsoleTaskTracker.Persistence;

public interface ITaskStore
{
    List<TodoItem> GetAll();
    TodoItem Add(string description);
    void Update(int id, string newDescription);
    void Delete(int id);
    void SetStatus(int id, TodoItemStatus status);
}