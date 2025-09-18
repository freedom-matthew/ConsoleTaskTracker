using System;
using System.Linq;
using ConsoleTaskTracker.Entities;

namespace ConsoleTaskTracker.Services;

public class CommandHandler : ICommandHandler
{
    public void Handle(string userInput)
    {
        var arguments = userInput.Split(' ');
        var command = arguments[0].ToLower();

        switch (command)
        {
            case "add" when arguments.Length < 2:
                PrintError("For command 'add' you must enter a task description.");
                return;
            case "add":
            {
                var description = string.Join(' ', arguments.Skip(1));
                TodoListService.AddTodoItem(description);
                PrintSuccess($"TodoItem '{description}' has been added.", ConsoleColor.Green);
                break;
            }
            case "update" when arguments.Length < 3:
                PrintError("For command 'update' you must enter a task ID and new description.");
                return;
            case "update":
            {
                var todoItemId = int.Parse(arguments[1]);
                var newDescription = string.Join(' ', arguments.Skip(2));
                TodoListService.UpdateTodoItem(todoItemId, newDescription);
                PrintSuccess($"TodoItem {todoItemId} has been updated.", ConsoleColor.Yellow);
                break;
            }
            case "delete" when arguments.Length < 2:
                PrintError("For command 'delete' you must enter a task ID.");
                return;
            case "delete":
            {
                var todoItemId = int.Parse(arguments[1]);
                TodoListService.DeleteTodoItem(todoItemId);
                PrintSuccess($"TodoItem {todoItemId} has been deleted.", ConsoleColor.Red);
                break;
            }
            case "list" when arguments.Length == 1:
                TodoListService.ListTodoItems();
                break;
            case "list" when arguments.Length == 2 && Enum.TryParse<TodoItemStatus>(arguments[1], true, out var status):
                TodoListService.ListTodoItemsByStatus(status);
                break;
            case "list":
                PrintError("Usage: list [todo|in-progress|done]");
                break;
            case "mark-in-progress" or "mark-done" when arguments.Length < 2:
                PrintError($"Usage: {command} <taskId>");
                return;
            case "mark-in-progress" or "mark-done":
            {
                var todoItemId = int.Parse(arguments[1]);
                var markStatus = command == "mark-done" ? TodoItemStatus.Done : TodoItemStatus.InProgress;
                Console.ForegroundColor = markStatus == TodoItemStatus.Done ? ConsoleColor.Cyan : ConsoleColor.Blue;
                TodoListService.SetStatus(todoItemId, markStatus);
                Console.WriteLine($"TodoItem {todoItemId} marked as {markStatus}.");
                Console.ResetColor();
                break;
            }
            default:
                PrintError("Invalid command. Use 'add', 'update', 'delete', 'list', 'mark-in-progress', 'mark-done' or 'exit'.");
                break;
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