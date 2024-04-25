using Codecool.MarsExploration.MapExplorer.Exploration;
using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapExplorer.Repository;

namespace Codecool.MarsExploration.MapExplorer.Simulation;

public class SimulationStep
{
    private readonly IMovementRoutines _movementRoutines;
    private readonly IOutcomeAnalyzer _outcomeAnalyzer;
    private readonly ILogger _logger;
    private readonly ISimulationRepository _simulationRepo;

    public SimulationStep(IMovementRoutines movementRoutines, IOutcomeAnalyzer outcomeAnalyzer, ILogger logger, ISimulationRepository simulationRepo)
    {
        _movementRoutines = movementRoutines;
        _outcomeAnalyzer = outcomeAnalyzer;
        _logger = logger;
        _simulationRepo = simulationRepo;
    }

    public void OneStep(SimulationContext simulationContext)
    {
        _movementRoutines.Move(simulationContext);
        _outcomeAnalyzer.Outcome(simulationContext);
        
        _logger.LogPosition(simulationContext,"position");
        
        if (simulationContext.ExplorationOutcome != ExplorationOutcome.Null)
        {
            _logger.LogOutcome(simulationContext,"outcome");
            _movementRoutines.TeleportBackToShip(simulationContext);
            Console.WriteLine($"Rover teleported back to the Spaceship. POSITION [{simulationContext.Rover.Position.X},{simulationContext.Rover.Position.Y}]");

            int resourcesCount = 0;
            foreach (var keyValuePair in simulationContext.Rover.Encountered)
            {
                if (keyValuePair.Key == "*")
                {
                    resourcesCount += keyValuePair.Value.Count;
                }
                if (keyValuePair.Key == "%")
                {
                    resourcesCount += keyValuePair.Value.Count;
                }
            }
            _simulationRepo.Add(DateTime.Now.ToString(), simulationContext.Steps, resourcesCount, simulationContext.ExplorationOutcome.ToString());
            Console.WriteLine("Simulation data added to MarsExploration database.");
            Console.WriteLine("Press any button to exit");
            Console.ReadKey();
        }
    }
    
}