using Codecool.MarsExploration.MapExplorer.Simulation;

namespace Codecool.MarsExploration.MapExplorer.Exploration;

public interface IOutcomeAnalyzer
{
    public bool Timeout();
    public bool Succes();
    public bool LackOfResources();
}