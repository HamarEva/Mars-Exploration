using System.Threading.Channels;

namespace Codecool.MarsExploration.MapExplorer.Logger;

public class FileLogger : ILogger
{
  
    public void LogPosition(int step, string roverID, int coordinateX, int coordinateY, string eventType)
    {
        WriteFile( step,  roverID,  coordinateX,  coordinateY,  eventType);
    }

    public void LogOutcome(int step, string eventType, string outcome)
    {
        WriteFile(step,eventType,outcome);
    }

    private void WriteFile(int step, string roverID, int coordinateX, int coordinateY, string eventType)
    {
        var dir = AppDomain.CurrentDomain.BaseDirectory;
        var path = $"{dir}\\Resources\\FileWriter.txt";
        string text = ($"STEP {step}; EVENT {eventType}; UNIT {roverID}; POSITION [{coordinateX},{coordinateY}]");
        
        File.WriteAllText(path,text);
    }
    
    private void WriteFile(int step, string eventType, string outcome)
    {
        var dir = AppDomain.CurrentDomain.BaseDirectory;
        var path = $"{dir}\\Resources\\FileWriter.txt";
        string text = ($"STEP {step}; EVENT {eventType}; OUTCOME {outcome.ToUpper()}");
        File.WriteAllText(path,text);
    }

  
}
