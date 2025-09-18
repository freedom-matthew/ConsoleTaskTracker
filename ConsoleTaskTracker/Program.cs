using System;
using ConsoleTaskTracker.Services;

namespace ConsoleTaskTracker;

public static class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Please enter command (add, update, delete, list, mark-in-progress, mark-done), or type 'exit' to end the program.");
            Console.Write("> ");
            var userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You entered no command. Please try again.");
                Console.ResetColor();
                continue;
            }

            if (userInput.Trim().Equals("exit", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Exiting the application. Goodbye!");
                Console.ResetColor();
                break;
            }
            
            ICommandHandler handler = new CommandHandler();
            try
            {
                handler.Handle(userInput);
            }
            catch (FormatException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input processing error: " + ex.Message);
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Unexpected error: " + ex.Message);
                Console.ResetColor();
            }
        }
    }
}