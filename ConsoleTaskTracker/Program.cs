using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        //string command = args[0];

        //if (command == "add")
        //{
        //    string item = args[1];
        //}

        ConsoleTaskTracker.Entities.Task task = new("IDC");

        JsonSerializer.Serialize(task);
    }
}
