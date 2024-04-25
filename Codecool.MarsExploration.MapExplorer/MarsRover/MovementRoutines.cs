using Codecool.MarsExploration.MapExplorer.Simulation;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;

namespace Codecool.MarsExploration.MapExplorer.MarsRover;

public class MovementRoutines : IMovementRoutines
{
    private readonly ICoordinateCalculator _coordinateCalculator; 
    private readonly Random _random;

    public MovementRoutines(ICoordinateCalculator coordinateCalculator, Random random)
    {
        _coordinateCalculator = coordinateCalculator;
        _random = random;
    }
    public void Move(SimulationContext simulationContext)
    {
        var nextCoordinates = GetPossibleNextCoordinates(simulationContext);
        int index = _random.Next(nextCoordinates.Count);
        Coordinate nextCoordinate = nextCoordinates[index];
        simulationContext.Rover.Position = nextCoordinate;
        simulationContext.Rover.MovePath.Add(nextCoordinate);
        AddCoordinatesInSightToEncountered(nextCoordinate, simulationContext);
        simulationContext.Steps++;
    }
    
    public void TeleportBackToShip(SimulationContext simulationContext)
    {
        Coordinate returnCoordinate = simulationContext.SpaceShipLocation;
        simulationContext.Rover.Position = returnCoordinate;
    }
    
    public int LeastStepsBackToShip(SimulationContext simulationContext)
    {
        Coordinate returnCoordinate = simulationContext.SpaceShipLocation;
        Coordinate currentCoordinate = simulationContext.Rover.Position;

        List<Coordinate> currentFields = new List<Coordinate> { currentCoordinate };
        List<Coordinate> emptyFields = simulationContext.Rover.Encountered[" "].ToList();
        emptyFields.Add(returnCoordinate);
        Dictionary<int, IEnumerable<Coordinate>> emptyFieldsBySteps = new Dictionary<int, IEnumerable<Coordinate>>();

        for (int i = 1; i < simulationContext.MaxSteps; i++)
        {
            List<Coordinate> nextFields = _coordinateCalculator
                .GetAdjacentCoordinates(currentFields, simulationContext.Map.Dimension)
                .Where(c => emptyFields.Contains(c)).ToList();

            emptyFieldsBySteps.Add(i, nextFields);
            foreach (var coordinate in nextFields.ToList())
            {
                emptyFields.Remove(coordinate);
            }

            currentFields = nextFields;

            if (nextFields.Contains(returnCoordinate))
            {
                return i;
            }
        }
        return -1;
    }

    private List<Coordinate> GetPossibleNextCoordinates(SimulationContext simulationContext)
    {
        List<Coordinate> possibleNextCoordinates = new List<Coordinate>();
        List<Coordinate> nextCoordinates = new List<Coordinate>();
        
        IEnumerable<Coordinate> adjacentCoordinates =
            _coordinateCalculator.GetAdjacentCoordinates(simulationContext.Rover.Position, simulationContext.Map.Representation.GetLength(0));
        
        foreach (var coordinate in adjacentCoordinates)
        {
            if (simulationContext.Map.Representation[coordinate.X, coordinate.Y] == " "
                && simulationContext.SpaceShipLocation != coordinate)
            {
                possibleNextCoordinates.Add(coordinate);
            }
        }

        foreach (var coordinate in possibleNextCoordinates)
        {
            if (!simulationContext.Rover.MovePath.Contains(coordinate))
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

    private void AddCoordinatesInSightToEncountered(Coordinate nextCoordinate, SimulationContext simulationContext)
    {
        var coordinatesInSight =
            _coordinateCalculator.GetCoordinatesInSight(nextCoordinate, simulationContext.Map.Dimension, simulationContext.Rover.Sight);
        foreach (var coordinate in coordinatesInSight)
        {
            string symbol = simulationContext.Map.GetByCoordinate(coordinate);
            if (!simulationContext.Rover.Encountered.ContainsKey(symbol))
            {
                simulationContext.Rover.Encountered[symbol] = new List<Coordinate>();
            }
            if (!simulationContext.Rover.Encountered[symbol].Contains(coordinate))
            {
                simulationContext.Rover.Encountered[symbol].Add(coordinate);
            }
        }
        
    }
}