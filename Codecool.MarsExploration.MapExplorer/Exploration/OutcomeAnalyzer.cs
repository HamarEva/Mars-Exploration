using Codecool.MarsExploration.MapExplorer.Simulation;

namespace Codecool.MarsExploration.MapExplorer.Exploration;

public class OutcomeAnalyzer : IOutcomeAnalyzer
{
    private readonly SimulationContext _simulationContext;

    public OutcomeAnalyzer(SimulationContext simulationContext)
    {
        _simulationContext = simulationContext;
    }

    public bool SimulationEnds()
    {
        return Timeout() || Success() || LackOfResources();
    }
    public ExplorationOutcome Outcome()
    {
        if (Timeout())
        {
            return ExplorationOutcome.Timeout;
        }
        if (Success())
        {
            return ExplorationOutcome.Colonizable;
        }
        return ExplorationOutcome.Error;
    }
    
    private bool Timeout()
    {
        return _simulationContext.Steps == _simulationContext.MaxSteps;
    }

    private bool Success()
    {
        return _simulationContext.Rover.Encountered["*"].Count >= 4 &&
               _simulationContext.Rover.Encountered["%"].Count >= 3;
    }

    private bool LackOfResources()
    {
        int mapSize = _simulationContext.Map.Representation.GetLength(0) *
                      _simulationContext.Map.Representation.GetLength(1);
        
        int encountered = 0;
        foreach (var keyValuePair in _simulationContext.Rover.Encountered)
        {
            encountered += keyValuePair.Value.Count;
        }

        return encountered == mapSize;
    }
}