using ConsoleTaskTracker.Entities;

namespace ConsoleTaskTracker.Services;

public class CommandHandler(TodoListService service) : ICommandHandler
{
    private readonly TodoListService _todoListService = service ?? throw new ArgumentNullException(nameof(service));

    public void Handle(string userInput)
    {
        var arguments = userInput.Split(' ');
        var command = arguments[0].ToLower();

        IReadOnlyList<TodoItem>? todoItems;
        if (command == "add")
        {
            if (arguments.Length != 2)
            {
                PrintError($"For command {command} you must enter a task description.");
                return;
            }
            
            var description = string.Join(' ', arguments.Skip(1));
            _todoListService.AddTodoItem(description);
            PrintSuccess($"Todo '{description}' has been added.", ConsoleColor.Green);return;
        }
        else if (command == "update")
        {
            if (arguments.Length != 3)
            {
                PrintError($"For command {command} you must enter a task ID and new description.");
                return;
            }

            var todoItemId = int.Parse(arguments[1]);
            var newDescription = string.Join(' ', arguments.Skip(2));
            _todoListService.UpdateTodoItem(todoItemId, newDescription);

            PrintSuccess($"Todo {todoItemId} has been updated.", ConsoleColor.Yellow);
        }
        else if (command == "mark-in-progress" || command == "mark-done" && arguments.Length < 2)
        {
            if (arguments.Length != 2)
            {
                PrintError($"For command {command} you must enter a status.");
                return;
            }
            
            var todoItemId = int.Parse(arguments[1]);
            var markStatus = command == "mark-in-progress" ? TodoItemStatus.InProgress : TodoItemStatus.Done;
            _todoListService.SetStatus(todoItemId, markStatus);

            PrintSuccess($"Todo {todoItemId} has been marked {markStatus}.", markStatus == TodoItemStatus.InProgress ? ConsoleColor.Blue : ConsoleColor.Cyan); return;
        }
        else if (command == "delete")
        {
            if (arguments.Length != 2)
            {
                PrintError($"For command {command} you must enter a task ID.");
                return;
            }
            
            var todoItemId = int.Parse(arguments[1]);
            _todoListService.DeleteTodoItem(todoItemId);

            PrintSuccess($"Todo {todoItemId} has been deleted.", ConsoleColor.Gray);
        }
        else if (command == "list")
        {
            if (arguments.Length == 1)
            {
                todoItems = _todoListService.ListTodoItems();

                foreach (var todoItem in todoItems)
                {
                    PrintSuccess($"ID: {todoItem.Id.ToString()}", ConsoleColor.Magenta);
                    Console.WriteLine($"Description: {todoItem.Description}");
                    Console.WriteLine($"Status: {todoItem.Status}");
                    Console.WriteLine($"Created at: {todoItem.CreatedAt}");
                    Console.WriteLine($"Updated at: {todoItem.UpdatedAt}");
                    Console.WriteLine();
                }
            }
            else if (arguments.Length == 2)
            {
                var statusArgument = arguments[1].Trim();
                TodoItemStatus status;
                
                switch (statusArgument)
                {
                    case "todo":
                        status = TodoItemStatus.Todo;
                        break;
                    case "in-progress":
                        status = TodoItemStatus.InProgress;
                        break;
                    case "done":
                        status = TodoItemStatus.Done;
                        break;
                    default:
                        PrintError("Usage: list [todo|in-progress|done]");
                        return;
                }

                todoItems = _todoListService.ListTodoItemsByStatus(status);

                foreach (var todoItem in todoItems)
                {
                    PrintSuccess($"ID: {todoItem.Id.ToString()}", ConsoleColor.Magenta);
                    Console.WriteLine($"Description: {todoItem.Description}");
                    Console.WriteLine($"Created at: {todoItem.CreatedAt}");
                    Console.WriteLine($"Updated at: {todoItem.UpdatedAt}");
                    Console.WriteLine();
                }
            }
            else
            {
                PrintError($"For command {command} you must leave second argument or enter a status.");
            }
        }
        else
        {
            PrintError("Invalid command. Use 'add', 'update', 'mark-in-progress', 'mark-done', 'delete', 'list',  or 'exit'.");
        }
    }

    private static void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    private static void PrintSuccess(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}