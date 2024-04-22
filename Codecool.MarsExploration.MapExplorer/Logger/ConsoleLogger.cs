namespace Codecool.MarsExploration.MapExplorer.Logger;

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        LogMessage(message, "INFO");
    }

    private void LogMessage(string message, string type)
    {
        var entry = $"[{DateTime.Now}] {type}: {message}";
        Console.WriteLine(entry);
    }

  
}