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
        // Move, Scan and step increment:
        _movementRoutines.Move(simulationContext);
        // Analysis
        _outcomeAnalyzer.Outcome(simulationContext);
        // Log majd loggerrel kéne:
        Console.WriteLine(
            $"STEP {simulationContext.Steps}; EVENT position; UNIT {simulationContext.Rover.ID}; POSITION [{simulationContext.Rover.Position.X},{simulationContext.Rover.Position.Y}]");
        if (simulationContext.ExplorationOutcome != ExplorationOutcome.Null)
        {
            Console.WriteLine($"STEP {simulationContext.Steps}; EVENT outcome; OUTCOME {simulationContext.ExplorationOutcome}");
            _movementRoutines.TeleportBackToShip(simulationContext);
            Console.WriteLine($"Rover teleported back to the Spaceship. POSITION [{simulationContext.Rover.Position.X},{simulationContext.Rover.Position.Y}]");
        }
    }
    
}