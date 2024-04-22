using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover;

public class Rover
{
    private string ID { get; set; }
    public Coordinate Position { get; set; }
    public int Sight { get; set; }
    public IGrouping<string,Coordinate> Encountered { get; set; }

    public Rover(string id, Coordinate position, int sight, IGrouping<string,Coordinate> encountered)
    {
        ID = id;
        Position = position;
        Sight = sight;
        Encountered = encountered;
    }
}