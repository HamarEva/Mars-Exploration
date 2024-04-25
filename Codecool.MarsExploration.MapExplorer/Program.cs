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
        ILogger logger = new ConsoleLogger();
        string workDir = AppDomain.CurrentDomain.BaseDirectory;
        var dbFile = $"{workDir}\\Resources\\MarsExploration.db";

        
        
        
        logger.LogMessage("Explore the Mars");
        string[] maps = { "exploration-0.map", "exploration-1.map", "exploration-2.map", "exit" };
        string[] rovers = { "Rover-01", "Rover-02", "WALL-E" };
       
        
        Console.CursorVisible = false;
        string map = OptionMenu(maps);
        logger.LogMessage("Select the landing coordinates:");
        logger.LogMessage($"X:");
        int x = int.Parse(Console.ReadLine());
        logger.LogMessage( $"Y:");
        int y = int.Parse(Console.ReadLine());
        Console.Clear();
        logger.LogMessage("You have to configure the rover");
        string roverUnit = OptionMenu(rovers);
        Console.Clear();
        logger.LogMessage("Sight that the rover sees:");
        int roverSight = int.Parse(Console.ReadLine());
        Console.Clear();
        logger.LogMessage("Max steps the rover can make:");
        int timeOut = int.Parse(Console.ReadLine());
        Console.Clear();
        logger.LogMessage("Symbols to monitor(separated by comma):");
        string symbols = Console.ReadLine();
        IEnumerable<string> symbolsToMonitor = symbols.Split(",").ToList();

        logger.LogMessage($"Selected map: {map}");
        logger.LogMessage($"Selected landing coordinates: [{x},{y}]");
        logger.LogMessage($"Selected unit: {roverUnit}");
        logger.LogMessage($"Selected sight: {roverSight}");
        logger.LogMessage($"Selected timeout: {timeOut}");
        logger.LogMessage("Press enter to start the simulation");
        foreach (var symbol in symbolsToMonitor)
        {
            logger.LogMessage(symbol);
        }
        Console.ReadKey();
        Console.Clear();
        
        
        ISimulationRepository simulationRepository = new SimulationRepository(dbFile);
        Random random = new Random();
        string mapFile = $@"{WorkDir}\Resources\{map}";
        
        Coordinate landingSpot = new Coordinate(x, y); 
        
        ICoordinateCalculator coordinateCalculator = new CoordinateCalculator();
        IMapLoader mapLoader = new MapLoader.MapLoader();
        IMovementRoutines movementRoutines = new MovementRoutines(coordinateCalculator, random);
        
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


