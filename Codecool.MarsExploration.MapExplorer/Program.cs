using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.Exploration;
using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapExplorer.Simulation;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapExplorer.Logger;
namespace Codecool.MarsExploration.MapExplorer;

class Program
{
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;

    public static void Main(string[] args)
    {
        string mapFile = $@"{WorkDir}\Resources\exploration-0.map";
        Random random = new Random();
        ILogger logger = new ConsoleLogger();
        Coordinate landingSpot = new Coordinate(6, 6);
        ICoordinateCalculator coordinateCalculator = new CoordinateCalculator();
        
        IMapLoader mapLoader = new MapLoader.MapLoader();
        IMovementRoutines movementRoutines = new MovementRoutines(coordinateCalculator, random);
        IOutcomeAnalyzer outcomeAnalyzer = new OutcomeAnalyzer();
        
        
        IEnumerable<string> symbolsToMonitor = new List<string> { "*", "%" };
        
        
        
        Configuration.Configuration configuration = new Configuration.Configuration(
            mapFile: mapFile,
            startCoordinate: landingSpot,
            symbols: symbolsToMonitor,
            timeOut: 100);

        IConfigurationValidator configurationValidator = new ConfigurationValidator(mapLoader,coordinateCalculator);
        
        var simulationStep = new SimulationStep(movementRoutines, outcomeAnalyzer, logger);
        var placeRover = new PlaceRover(configuration, configurationValidator, mapLoader, coordinateCalculator);
        var rover = placeRover.PlaceRoverOnMap("Rover-01");
        var contextBuilder = new ContextBuilder(configuration, rover, mapLoader, configurationValidator);
        ExplorationSimulator explorationSimulator = new ExplorationSimulator(contextBuilder, simulationStep);
        
        /*---------------------------SIMULATE-----------------------------------*/
        explorationSimulator.Simulate();
    



    }
}


