using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration;

public record Configuration(string mapFile, Coordinate startCoordinate, IEnumerable<string> symbols, int timeOut);