namespace ConsoleTaskTracker.Services;

public interface ICommandHandler
{
    void Handle(string command);
}