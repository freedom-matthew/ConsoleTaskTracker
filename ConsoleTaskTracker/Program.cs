using ConsoleTaskTracker.Services;

class Program
{
    static void Main(string[] args)
    {
        string command = args[0];
        TaskService taskService = new();

        if (command == "add")
        {
            taskService.FindLeastId();
            taskService.AddTaskToFile(args[1]);
        }

        /*else if (command == "update")
        {
            taskService.UpdateTaskInFile(Convert.ToInt32(args[1]), args[2]);
        }*/
    }
}
