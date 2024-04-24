using Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Simulation;
using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.MapLoader;

public class ContextBuilder
{
    /*
     *public int Steps { get; set; }
    public int MaxSteps { get; set; }
    public Rover Rover { get; set; }
    public Coordinate SpaceShipLocation { get; set; }
    public Map Map { get; set; }
    public IEnumerable<string> Symbols { get; set; }
    public ExplorationOutcome ExplorationOutcome { get; set; }
     * 
     */

    private int no = 1;
   
    private readonly IMapLoader _mapLoader;
    private readonly Rover _rover;
    private readonly Configuration _configuration;
    private readonly IConfigurationValidator _configurationValidator;
    

    public ContextBuilder(Configuration configuration, Rover rover, IMapLoader mapLoader, IConfigurationValidator configurationValidator)
    {
        _configuration = configuration;
        _rover = rover;
        _mapLoader = mapLoader;
        _configurationValidator = configurationValidator;
    }
    
    public SimulationContext CreateContext()
    {

        if (_configurationValidator.Validate(_configuration))
        {
            var map = _mapLoader.Load(_configuration.mapFile);
            return new SimulationContext(_configuration.timeOut, _rover, _configuration.startCoordinate, map, _configuration.symbols);
        }

        return null;
    }

    public bool isSuccess()
    {
        return _configurationValidator.Validate(_configuration);
    }

   
}