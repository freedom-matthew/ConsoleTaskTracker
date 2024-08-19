using ConsoleTaskTracker;

class Program
{
    static void Main(string[] args)
    {
        string command = args[0];
        string item = args[1];

        switch (command)
        {
            case "add":
                ConsoleTaskTracker.Task task = new(item);
                task.AddTaskToFile(task);
                break;

            case "read":
                break;

            case "update":
                break;

            case "delete":
                break;

            case "mark-todo":
                break;

            case "mark-in-progress":
                break;

            case "mark-done":
                break;       
        }
    }
}
