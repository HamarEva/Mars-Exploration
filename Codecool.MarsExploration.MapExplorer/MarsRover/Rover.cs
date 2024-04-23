using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.MarsRover;

public class Rover
{
    private string ID { get; set; }
    public Coordinate Position { get; set; }
    public int Sight { get; set; }
    public Dictionary<string,List<Coordinate>> Encountered { get; set; }

    public Rover(string id, Coordinate position, int sight)
    {
        ID = id;
        Position = position;
        Sight = sight;
        Encountered = new Dictionary<string, List<Coordinate>>();
    }
}