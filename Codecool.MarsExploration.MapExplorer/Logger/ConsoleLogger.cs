namespace Codecool.MarsExploration.MapExplorer.Logger;

public class ConsoleLogger : ILogger
{
    public void LogPosition(int step, string roverID, int coordinateX, int coordinateY, string eventType)
    {
        LogMessage(step, roverID, coordinateX, coordinateY, eventType);
    }

    public void LogOutcome(int step, string eventType, string outcome)
    {
        LogMessage(step, eventType,outcome);
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

