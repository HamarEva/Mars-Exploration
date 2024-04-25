using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;

namespace Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapExplorer.Configuration;
public class PlaceRover
{
    private readonly Configuration _configuration;
    private readonly IConfigurationValidator _configurationValidator;
    private readonly IMapLoader _mapLoader;
    private ICoordinateCalculator _coordinateCalculator;
    private readonly int _roverSight;

    public PlaceRover(Configuration configuration, IConfigurationValidator configurationValidator, IMapLoader mapLoader, ICoordinateCalculator coordinateCalculator,int roverSight)
    {
        _configuration = configuration;
        _configurationValidator = configurationValidator;
        _mapLoader = mapLoader;
        _coordinateCalculator = coordinateCalculator;
        _roverSight = roverSight;
    }

    public Rover PlaceRoverOnMap(string ID)
    {
        var map = _mapLoader.Load(_configuration.mapFile);
        var startCoordinate = _configuration.startCoordinate;
        var adjacentCoordinates = _coordinateCalculator.GetAdjacentCoordinates(startCoordinate, map.Dimension);
        
        var emptyAdjacentCoordinates = new List<Coordinate>();

        foreach (var coordinate in adjacentCoordinates)
        {
            if (map.Representation[coordinate.X, coordinate.Y] == " ")
            {
                emptyAdjacentCoordinates.Add(coordinate);
            }
        }

        return new Rover(ID, emptyAdjacentCoordinates[0], _roverSight, new List<Coordinate>{emptyAdjacentCoordinates[0]});
    }
}