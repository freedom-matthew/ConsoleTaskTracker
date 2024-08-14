namespace ConsoleTaskTracker
{
    internal class TaskManager
    {
        private readonly List<Task> tasks = [];

        public void AddTask(string description)
        {
            Task task = new(description);
            tasks.Add(task);
            Console.WriteLine($"Task added succesfully (ID: {task.TaskId})");
        }
    }
}
