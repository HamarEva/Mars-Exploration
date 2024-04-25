using System.Threading.Channels;
using Codecool.MarsExploration.MapExplorer.Simulation;

namespace Codecool.MarsExploration.MapExplorer.Logger;

public class FileLogger : ILogger
{
  
    public void LogPosition(SimulationContext simulationContext, string eventType)
    {
        WriteFile( simulationContext.Steps,  simulationContext.Rover.ID,  simulationContext.Rover.Position.X,  simulationContext.Rover.Position.Y,  eventType);
    }

    public void LogOutcome(SimulationContext simulationContext, string eventType)
    {
        WriteFile(simulationContext.Steps,eventType,simulationContext.ExplorationOutcome.ToString());
    }

    public void LogMessage(string message)
    {
        WriteMessage(message);
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
    
    private void WriteMessage(string message)
    {
        var dir = AppDomain.CurrentDomain.BaseDirectory;
        var path = $"{dir}\\Resources\\FileWriter.txt";
        File.WriteAllText(path,message);
    }

  
}
