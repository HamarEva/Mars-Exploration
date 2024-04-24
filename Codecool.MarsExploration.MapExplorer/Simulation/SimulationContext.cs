using Codecool.MarsExploration.MapExplorer.Exploration;
using Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Simulation;

public class SimulationContext
{
    public int Steps { get; set; }
    public int MaxSteps { get; set; }
    public Rover Rover { get; set; }
    public Coordinate SpaceShipLocation { get; set; }
    public Map Map { get; set; }
    public IEnumerable<string> Symbols { get; set; }
    public ExplorationOutcome ExplorationOutcome { get; set; }

    public SimulationContext(int maxSteps, Rover rover, Coordinate spaceShipLocation, Map map, IEnumerable<string> symbols)
    {
        MaxSteps = maxSteps;
        Rover = rover;
        SpaceShipLocation = spaceShipLocation;
        Map = map;
        Symbols = symbols;
        ExplorationOutcome = ExplorationOutcome.Null;
    }
}