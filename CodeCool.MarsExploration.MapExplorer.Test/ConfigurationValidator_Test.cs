// using Codecool.MarsExploration.MapExplorer.Configuration;
// using Codecool.MarsExploration.MapExplorer.MapLoader;
// using Codecool.MarsExploration.MapGenerator.Calculators.Model;
// using Codecool.MarsExploration.MapGenerator.Calculators.Service;
// using Codecool.MarsExploration.MapGenerator.MapElements.Model;
// using Moq;
//
// namespace CodeCool.MarsExploration.MapExplorer.Test;
//
// public class ConfigurationValidator_Test
// {
//     private readonly MapLoader _mapLoader = new MapLoader();
//     [Test]
//     public void Validate_ReturnsTrue_WhenAllConditionsAreMet()
//     {
//         var mapLoaderMock = new Mock<IMapLoader>();
//         mapLoaderMock.Setup(x => x.Load(It.IsAny<string>())).Returns(new Map(new string[,] { { "" } }, true));
//         var validator = new ConfigurationValidator(mapLoaderMock.Object ICoordinateCalculator coordinateCalc);
//         var configuration = new Configuration("mapFile", new Coordinate(0, 0), new string[] { "symbol1", "symbol2" }, 10);
//
//   
//         var result = validator.Validate(configuration);
//
//        
//         Assert.IsTrue(result);
//     }
// }