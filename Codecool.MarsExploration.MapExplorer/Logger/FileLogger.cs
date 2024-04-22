using System.Threading.Channels;

namespace Codecool.MarsExploration.MapExplorer.Logger;

public class FileLogger : ILogger
{
    public void Log(string message)
    {
        WriteFile(message,"INFO");
    }

    private void WriteFile(string message, string type)
    {
        var dir = AppDomain.CurrentDomain.BaseDirectory;
        var path = $"{dir}\\Resources\\FileWriter.txt";
        var text = $"{type}: {message}";
        File.WriteAllText(path,message);
    }
}
