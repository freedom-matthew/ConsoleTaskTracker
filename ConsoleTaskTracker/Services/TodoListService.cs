using ConsoleTaskTracker.Entities;
using ConsoleTaskTracker.Persistence;

namespace ConsoleTaskTracker.Services;

public class TodoListService(ITaskStore store)
{
    private readonly ITaskStore _store = store ?? throw new ArgumentNullException(nameof(store));
    
    public void AddTodoItem(string description) => _store.Add(description);

    public void UpdateTodoItem(int id, string newDescription) => _store.Update(id, newDescription);
    
    public void SetStatus(int id, TodoItemStatus status) => _store.SetStatus(id, status);

    public void DeleteTodoItem(int id) => _store.Delete(id);

    public IReadOnlyList<TodoItem> ListTodoItems()
        => _store.GetAll()
            .OrderBy(x => x.Id)
            .ToList();

    public IReadOnlyList<TodoItem> ListTodoItemsByStatus(TodoItemStatus status)
        => _store.GetAll()
            .Where(x => x.Status == status)
            .OrderBy(x => x.Id)
            .ToList();
}