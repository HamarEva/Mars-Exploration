using Codecool.MarsExploration.MapExplorer.Simulation;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;

namespace Codecool.MarsExploration.MapExplorer.MarsRover;

public class MovementRoutines : IMovementRoutines
{
    
    private readonly ICoordinateCalculator _coordinateCalculator; 
    private readonly SimulationContext _simulationContext;

    public MovementRoutines(ICoordinateCalculator coordinateCalculator, SimulationContext simulationContext)
    {
        _coordinateCalculator = coordinateCalculator;
        _simulationContext = simulationContext;
    }
    public void Move()
    {
        Coordinate nextCoordinate = GetPossibleNextCoordinates()[0];
        _simulationContext.Rover.Position = nextCoordinate;
        _simulationContext.Rover.MovePath.Add(nextCoordinate);
        AddCoordinatesInSightToEncountered(nextCoordinate);
        _simulationContext.Steps++;
    }
    
    public void TeleportBackToShip()
    {
        Coordinate returnCoordinate = _simulationContext.SpaceShipLocation;
        _simulationContext.Rover.Position = returnCoordinate;
    }

    private List<Coordinate> GetPossibleNextCoordinates()
    {
        List<Coordinate> possibleNextCoordinates = new List<Coordinate>();
        List<Coordinate> nextCoordinates = new List<Coordinate>();
        
        IEnumerable<Coordinate> adjacentCoordinates =
            _coordinateCalculator.GetAdjacentCoordinates(_simulationContext.Rover.Position, _simulationContext.Map.Representation.GetLength(0));
        
        foreach (var coordinate in adjacentCoordinates)
        {
            if (_simulationContext.Map.Representation[coordinate.X, coordinate.Y] == " "
                && _simulationContext.SpaceShipLocation != coordinate)
            {
                possibleNextCoordinates.Add(coordinate);
            }
        }

        foreach (var coordinate in possibleNextCoordinates)
        {
            if (!_simulationContext.Rover.MovePath.Contains(coordinate))
            {
                nextCoordinates.Add(coordinate);
            }
        }

        if (nextCoordinates.Count() > 0)
        {
            return nextCoordinates;
        }
        return possibleNextCoordinates;
    }

    private void AddCoordinatesInSightToEncountered(Coordinate nextCoordinate)
    {
        var coordinatesInSight =
            _coordinateCalculator.GetCoordinatesInSight(nextCoordinate, _simulationContext.Map.Dimension, _simulationContext.Rover.Sight);
        foreach (var coordinate in coordinatesInSight)
        {
            string symbol = _simulationContext.Map.GetByCoordinate(coordinate);
            if (!_simulationContext.Rover.Encountered.ContainsKey(symbol))
            {
                _simulationContext.Rover.Encountered[symbol] = new List<Coordinate>();
            }
            if (!_simulationContext.Rover.Encountered[symbol].Contains(coordinate))
            {
                _simulationContext.Rover.Encountered[symbol].Add(coordinate);
            }
        }
        
    }
}