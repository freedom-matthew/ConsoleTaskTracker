using ConsoleTaskTracker.Services;

string command = args[0];

if (command == "add")
{
    string description = args[1];
    TaskService.AddTaskToFile(description);
}

else if (command == "update")
{
    int taskId = int.Parse(args[1]);
    string description = args[2];
    TaskService.UpdateTaskFromFile(taskId, description);
}

else if (command == "delete")
{
    int taskId = int.Parse(args[1]);
    TaskService.DeleteTaskFromFile(Convert.ToInt32(taskId));
}
