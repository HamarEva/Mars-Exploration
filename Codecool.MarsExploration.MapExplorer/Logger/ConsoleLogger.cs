using Codecool.MarsExploration.MapExplorer.Simulation;

namespace Codecool.MarsExploration.MapExplorer.Logger;

public class ConsoleLogger : ILogger
{
    public void LogPosition(SimulationContext simulationContext, string eventType)
    {
        LogMessage(simulationContext.Steps, simulationContext.Rover.ID, simulationContext.Rover.Position.X, simulationContext.Rover.Position.Y, "position");
    }

    public void LogOutcome(SimulationContext simulationContext, string eventType)
    {
        LogMessage(simulationContext.Steps, eventType, simulationContext.ExplorationOutcome.ToString());
    }

    private void LogMessage(int step, string roverID, int coordinateX, int coordinateY, string eventType)
    {
      
        Console.WriteLine($"STEP {step}; EVENT {eventType}; UNIT {roverID}; POSITION [{coordinateX},{coordinateY}]");
    }
    private void LogMessage(int step, string eventType, string outcome)
    {
            Console.WriteLine($"STEP {step}; EVENT {eventType}; OUTCOME {outcome.ToUpper()}");
            
    }


   
}

