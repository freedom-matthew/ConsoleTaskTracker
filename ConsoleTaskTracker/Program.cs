using ConsoleTaskTracker;

class Program
{
    static void Main(string[] args)
    {
        string command = args[0];

        TaskManager taskManager = new();

        if (command == "add")
        {
            taskManager.AddTask(args[1]);
        }
    }
}
