namespace Codecool.MarsExploration.MapExplorer.Logger;

public interface ILogger
{
    void LogPosition(int step, string roverID, int coordinateX, int coordinateY, string eventType);
    void LogOutcome(int step, string eventType, string outcome);
}
