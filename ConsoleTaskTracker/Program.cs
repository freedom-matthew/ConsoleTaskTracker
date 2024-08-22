using ConsoleTaskTracker.Services;

class Program
{
    static void Main(string[] args)
    {
        string command = args[0];
        TaskService taskService = new();

        if (command == "add")
        {
            string description = args[1];
            taskService.AddTaskToFile(description);
        }

        else if (command == "update")
        {
            int taskId = int.Parse(args[1]);
            string description = args[2];
            taskService.UpdateTaskFromFile(taskId, description);            
        }

        else if (command == "delete")
        {
            int taskId = int.Parse(args[1]);
            taskService.DeleteTaskFromFile(Convert.ToInt32(taskId));
        }
    }
}
