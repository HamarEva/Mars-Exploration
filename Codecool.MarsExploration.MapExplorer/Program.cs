using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;

namespace Codecool.MarsExploration.MapExplorer;

class Program
{
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;

    public static void Main(string[] args)
    {
        string mapFile = $@"{WorkDir}\Resources\exploration-0.map";
        Coordinate landingSpot = new Coordinate(6, 6);
        IMapLoader mapLoader = new MapLoader.MapLoader();
        ICoordinateCalculator coordinateCalculator = new CoordinateCalculator();

       var map = mapLoader.Load(mapFile);

       Console.WriteLine(map);

       
    }
}
