using Codecool.MarsExploration.MapExplorer.Exploration;
using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapExplorer.MarsRover;
namespace Codecool.MarsExploration.MapExplorer.Simulation;

public class SimulationStep
{
    private readonly IMovementRoutines _movementRoutines;
    private readonly IOutcomeAnalyzer _outcomeAnalyzer;
    private readonly ILogger _logger;

    public SimulationStep(IMovementRoutines movementRoutines, IOutcomeAnalyzer outcomeAnalyzer, ILogger logger)
    {
        _movementRoutines = movementRoutines;
        _outcomeAnalyzer = outcomeAnalyzer;
        _logger = logger;
    }

    public void OneStep(SimulationContext simulationContext)
    {
        _movementRoutines.Move(simulationContext);
        _outcomeAnalyzer.Outcome(simulationContext);
        
        _logger.LogPosition(simulationContext.Steps,simulationContext.Rover.ID,simulationContext.Rover.Position.X,simulationContext.Rover.Position.Y,"position");
        
        if (simulationContext.ExplorationOutcome != ExplorationOutcome.Null)
        {
            _logger.LogOutcome(simulationContext.Steps,"outcome",simulationContext.ExplorationOutcome.ToString());
            _movementRoutines.TeleportBackToShip(simulationContext);
            Console.WriteLine($"Rover teleported back to the Spaceship. POSITION [{simulationContext.Rover.Position.X},{simulationContext.Rover.Position.Y}]");
        }
    }
    
}