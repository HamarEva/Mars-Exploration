using Codecool.MarsExploration.MapExplorer.Simulation;

namespace Codecool.MarsExploration.MapExplorer.Logger;

public interface ILogger
{
    void LogPosition(SimulationContext simulationContext, string eventType);
    void LogOutcome(SimulationContext simulationContext, string eventType);

    void LogMessage(string message);

}
