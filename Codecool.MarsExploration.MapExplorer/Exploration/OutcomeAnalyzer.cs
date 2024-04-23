using Codecool.MarsExploration.MapExplorer.Simulation;

namespace Codecool.MarsExploration.MapExplorer.Exploration;

public class OutcomeAnalyzer : IOutcomeAnalyzer
{
    private readonly SimulationContext _simulationContext;

    public OutcomeAnalyzer(SimulationContext simulationContext)
    {
        _simulationContext = simulationContext;
    }
    public bool Timeout()
    {
        if (_simulationContext.Steps == _simulationContext.MaxSteps)
        {
            return true;
        }

        return false;
    }

    public bool Succes()
    {
        int count = 0;
        foreach (var resource in _simulationContext.Rover.Encountered.Key)
        {
            if (resource.Equals('%'))
            {
                count++;
            }
        }

        return false;
    }

    public bool LackOfResources()
    {
        throw new NotImplementedException();
    }
}