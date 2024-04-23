using Codecool.MarsExploration.MapExplorer.Simulation;

namespace Codecool.MarsExploration.MapExplorer.Exploration;

public interface IOutcomeAnalyzer
{
    public bool SimulationEnds();
    public ExplorationOutcome Outcome();
}