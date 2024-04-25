using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration;

public class ConfigurationValidator : IConfigurationValidator
{
    private readonly IMapLoader _mapLoader;
    private readonly ICoordinateCalculator _coordinateCalculator;

    public ConfigurationValidator(IMapLoader mapLoader, ICoordinateCalculator coordinateCalculator)
    {
        _mapLoader = mapLoader;
        _coordinateCalculator = coordinateCalculator;
    }
    //public record Configuration(string mapFile, Coordinate startCoordinate, IEnumerable<string> symbols, int timeOut);
    public bool Validate(Configuration configuration)
    {
       return IsNotOccupied(configuration) && IsThereEmptyAdjacent(configuration) && isEmpty(configuration) && IsTimeOutGreaterThanZero(configuration) && AreSymblosValid(configuration);
    }

    private Map GetMapFromMapFile(Configuration configuration)
    {
        return _mapLoader.Load(configuration.mapFile);
    }

    private bool IsNotOccupied(Configuration configuration)
    {
        var map = _mapLoader.Load(configuration.mapFile).Representation;

        return map[configuration.startCoordinate.X, configuration.startCoordinate.Y] == " ";
    }

    private bool IsThereEmptyAdjacent(Configuration configuration)
    {
        var map = _mapLoader.Load(configuration.mapFile);
        var coordinates =  _coordinateCalculator.GetAdjacentCoordinates(configuration.startCoordinate, map.Dimension);

        foreach (var coordinate in coordinates)
        {
            if (map.Representation[coordinate.X, coordinate.Y] == " ")
            {
                return true;
            }
        }

        return false;

    }

    private bool isEmpty(Configuration configuration)
    {
        var map = GetMapFromMapFile(configuration);
        return map.SuccessfullyGenerated;
    }

    private bool IsTimeOutGreaterThanZero(Configuration configuration)
    {
        return configuration.timeOut > 0;

    }

    private bool AreSymblosValid(Configuration configuration)
    {
        return configuration.symbols != null;
    }
    
    
}