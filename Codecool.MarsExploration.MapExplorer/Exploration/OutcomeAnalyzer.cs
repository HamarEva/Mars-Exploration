using Codecool.MarsExploration.MapExplorer.Simulation;

namespace Codecool.MarsExploration.MapExplorer.Exploration;

public class OutcomeAnalyzer : IOutcomeAnalyzer
{

    public void Outcome(SimulationContext simulationContext)
    {
        if (Timeout(simulationContext))
        {
            simulationContext.ExplorationOutcome = ExplorationOutcome.Timeout;
        }
        if (Success(simulationContext))
        {
            simulationContext.ExplorationOutcome = ExplorationOutcome.Colonizable;
        }
        if (LackOfResources(simulationContext))
        {
            simulationContext.ExplorationOutcome = ExplorationOutcome.Error;
        }
    }
    
    private bool Timeout(SimulationContext simulationContext)
    {
        return simulationContext.Steps == simulationContext.MaxSteps;
    }

    private bool Success(SimulationContext simulationContext)
    {
        if (!simulationContext.Rover.Encountered.ContainsKey("*") ||
            !simulationContext.Rover.Encountered.ContainsKey("%"))
        {
            return false;
        }
        
        return simulationContext.Rover.Encountered["*"].Count >= 4 &&
               simulationContext.Rover.Encountered["%"].Count >= 3;

    }

    private bool LackOfResources(SimulationContext simulationContext)
    {
        int mapSize = simulationContext.Map.Representation.Length;
        
        int encountered = 0;
        foreach (var keyValuePair in simulationContext.Rover.Encountered)
        {
            encountered += keyValuePair.Value.Count;
        }

        return encountered == mapSize;
    }
}