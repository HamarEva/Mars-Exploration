using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.Exploration;
using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapExplorer.Simulation;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapExplorer.Logger;
using Codecool.MarsExploration.MapExplorer.Repository;

namespace Codecool.MarsExploration.MapExplorer;

class Program
{
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;

    public static void Main(string[] args)
    {
        string workDir = AppDomain.CurrentDomain.BaseDirectory;
        var dbFile = $"{workDir}\\Resources\\MarsExploration.db";
        
        Console.WriteLine("Explore the Mars");

        Console.WriteLine("Select a map (0-2):");
        string map = Console.ReadLine();
        Console.WriteLine("Select the landing coordinates:");
        Console.WriteLine("X:");
        int x = int.Parse(Console.ReadLine());
        Console.WriteLine("Y:");
        int y = int.Parse(Console.ReadLine());
        Console.WriteLine("You have to configure");
        Console.WriteLine("Sight that the rover sees:");
        int roverSight = int.Parse(Console.ReadLine());
        Console.WriteLine("Max steps the rover can make:");
        int timeOut = int.Parse(Console.ReadLine());
        
        
        ISimulationRepository simulationRepository = new SimulationRepository(dbFile);
        
        string mapFile = $@"{WorkDir}\Resources\exploration-{map}.map";
        Random random = new Random();
        ILogger logger = new ConsoleLogger();
        Coordinate landingSpot = new Coordinate(x, y); 
        ICoordinateCalculator coordinateCalculator = new CoordinateCalculator();
        
        IMapLoader mapLoader = new MapLoader.MapLoader();
        IMovementRoutines movementRoutines = new MovementRoutines(coordinateCalculator, random);
        IOutcomeAnalyzer outcomeAnalyzer = new OutcomeAnalyzer();
        
        
        IEnumerable<string> symbolsToMonitor = new List<string> { "*", "%" };
      
        
        Configuration.Configuration configuration = new Configuration.Configuration(
            mapFile: mapFile,
            startCoordinate: landingSpot,
            symbols: symbolsToMonitor,
            timeOut: timeOut);

        IConfigurationValidator configurationValidator = new ConfigurationValidator(mapLoader,coordinateCalculator);
        
        var simulationStep = new SimulationStep(movementRoutines, outcomeAnalyzer, logger, simulationRepository);
        var placeRover = new PlaceRover(configuration, configurationValidator, mapLoader, coordinateCalculator,roverSight);
        var rover = placeRover.PlaceRoverOnMap("Rover-01");
        var contextBuilder = new ContextBuilder(configuration, rover, mapLoader, configurationValidator);
        ExplorationSimulator explorationSimulator = new ExplorationSimulator(contextBuilder, simulationStep);
        
        /*---------------------------SIMULATE-----------------------------------*/
        Console.WriteLine(rover.Position);
        explorationSimulator.Simulate();
   
        


    }
}


