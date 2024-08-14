class Program
{
    static void Main(string[] args)
    {
        string description = args[0];

        ConsoleTaskTracker.Task task = new(description);
        Console.WriteLine(task.Description);
    }
}
