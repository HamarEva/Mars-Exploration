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
    static int selectedOptionIndex = 0;

    public static void Main(string[] args)
    {
        
        string workDir = AppDomain.CurrentDomain.BaseDirectory;
        var dbFile = $"{workDir}\\Resources\\MarsExploration.db";
        
        
        Console.WriteLine("Explore the Mars");
        string[] maps = { "exploration-0.map", "exploration-1.map", "exploration-2.map", "exit" };
        string[] rovers = { "Rover-01", "Rover-02", "WALL-E" };
       
        
        Console.CursorVisible = false;
        string map = OptionMenu(maps);
        Console.WriteLine("Select the landing coordinates:");
        Console.WriteLine($"X:");
        int x = int.Parse(Console.ReadLine());
        Console.WriteLine( $"Y:");
        int y = int.Parse(Console.ReadLine());
        Console.Clear();
        Console.WriteLine("You have to configure the rover");
        string roverUnit = OptionMenu(rovers);
        Console.Clear();
        Console.WriteLine("Sight that the rover sees:");
        int roverSight = int.Parse(Console.ReadLine());
        Console.Clear();
        Console.WriteLine("Max steps the rover can make:");
        int timeOut = int.Parse(Console.ReadLine());
        Console.Clear();

        Console.WriteLine($"Selected map: {map}");
        Console.WriteLine($"Selected landing coordinates: [{x},{y}]");
        Console.WriteLine($"Selected unit: {roverUnit}");
        Console.WriteLine($"Selected sight: {roverSight}");
        Console.WriteLine($"Selected timeout: {timeOut}");
        Console.WriteLine("Press enter to start the simulation");
        Console.ReadKey();
        Console.Clear();
        
        
        ISimulationRepository simulationRepository = new SimulationRepository(dbFile);
        
        string mapFile = $@"{WorkDir}\Resources\{map}";
        Random random = new Random();
        ILogger logger = new ConsoleLogger();
        Coordinate landingSpot = new Coordinate(x, y); 
        
        ICoordinateCalculator coordinateCalculator = new CoordinateCalculator();
        IMapLoader mapLoader = new MapLoader.MapLoader();
        IMovementRoutines movementRoutines = new MovementRoutines(coordinateCalculator, random);
        IEnumerable<string> symbolsToMonitor = new List<string> { "*", "%" };
      
        
        Configuration.Configuration configuration = new Configuration.Configuration(
            mapFile: mapFile,
            startCoordinate: landingSpot,
            symbols: symbolsToMonitor,
            timeOut: timeOut);

        IConfigurationValidator configurationValidator = new ConfigurationValidator(mapLoader,coordinateCalculator);
        IOutcomeAnalyzer outcomeAnalyzer = new OutcomeAnalyzer(configuration);
        
        var simulationStep = new SimulationStep(movementRoutines, outcomeAnalyzer, logger, simulationRepository);
        var placeRover = new PlaceRover(configuration, configurationValidator, mapLoader, coordinateCalculator,roverSight);
        var rover = placeRover.PlaceRoverOnMap(roverUnit);
        var contextBuilder = new ContextBuilder(configuration, rover, mapLoader, configurationValidator);
        ExplorationSimulator explorationSimulator = new ExplorationSimulator(contextBuilder, simulationStep);
        
        /*---------------------------SIMULATE-----------------------------------*/
        explorationSimulator.Simulate();
    }
    
    static void PrintMenu(string[] options)
    {
        for (int i = 0; i < options.Length; i++)
        {
            if (i == selectedOptionIndex)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Yellow;
            }

            Console.WriteLine(options[i]);

            Console.ResetColor();
        }
    }

    static string OptionMenu(string[] options)
    {
        ConsoleKeyInfo key;
        do
        {
            PrintMenu(options);
            key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedOptionIndex = (selectedOptionIndex == 0) ? options.Length - 1 : selectedOptionIndex - 1;
                    Console.Clear();
                    break;
                case ConsoleKey.DownArrow:
                    selectedOptionIndex = (selectedOptionIndex == options.Length - 1) ? 0 : selectedOptionIndex + 1;
                    Console.Clear();
                    break;
                case ConsoleKey.Enter:
                    Console.Clear();
                    break;
            }
        } while (key.Key != ConsoleKey.Enter);

        return options[selectedOptionIndex];
    }
}


