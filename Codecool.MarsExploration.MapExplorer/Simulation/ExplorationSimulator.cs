namespace Codecool.MarsExploration.MapExplorer.Simulation;
using Codecool.MarsExploration.MapExplorer.Configuration;

public class ExplorationSimulator
{
    //public record Configuration(string mapFile, Coordinate startCoordinate, IEnumerable<string> symbols, int timeOut);
    private readonly Configuration _configuration;
    private readonly IConfigurationValidator _configurationValidator;
    private readonly SimulationContext _simulationContext;

    public ExplorationSimulator(Configuration configuration, IConfigurationValidator configurationValidator, SimulationContext simulationContext)
    {
        
    }



}