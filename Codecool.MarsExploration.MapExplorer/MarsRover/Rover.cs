using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover;

public class Rover
{
    public string ID { get; set; }
    public Coordinate Position { get; set; }
    public int Sight { get; set; }

    public List<Coordinate> MovePath;
    public Dictionary<string,List<Coordinate>> Encountered { get; set; }

    public Rover(string id, Coordinate position, int sight, List<Coordinate> movePath)
    {
        ID = id;
        Position = position;
        Sight = sight;
        MovePath = movePath;
        Encountered = new Dictionary<string, List<Coordinate>>();
    }
}