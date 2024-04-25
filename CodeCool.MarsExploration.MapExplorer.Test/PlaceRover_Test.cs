using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;
using Moq;

namespace CodeCool.MarsExploration.MapExplorer.Test
{
    [TestFixture]
    public class PlaceRoverTests
    {
        private Mock<IMapLoader> _mapLoaderMock;
        private Mock<ICoordinateCalculator> _coordinateCalculatorMock;
        private Configuration _configuration;

        [SetUp]
        public void Setup()
        {
            _mapLoaderMock = new Mock<IMapLoader>();
            _coordinateCalculatorMock = new Mock<ICoordinateCalculator>();
            _configuration = new Configuration
            (
                mapFile: "map.txt",
                startCoordinate: new Coordinate(0, 0),
                symbols: new List<string> { "*", "%" },
                timeOut: 100
            );
        }

        [Test]
        public void PlaceRoverOnMap_ValidMapAndCoordinate_ReturnsRoverWithCorrectPosition()
        {
            var map = new Map(new string[,] { { " ", " ", " " }, { " ", " ", " " }, { " ", " ", " " } });
            _mapLoaderMock.Setup(m => m.Load(_configuration.mapFile)).Returns(map);
            var adjacentCoordinates = new List<Coordinate> { new Coordinate(1, 0), new Coordinate(0, 1) };
            _coordinateCalculatorMock.Setup(c => c.GetAdjacentCoordinates(_configuration.startCoordinate, map.Dimension,1)).Returns(adjacentCoordinates);

            var placeRover = new PlaceRover(_configuration, null, _mapLoaderMock.Object, _coordinateCalculatorMock.Object,3);
            
            var rover = placeRover.PlaceRoverOnMap("Rover1");
            
            Assert.IsNotNull(rover);
            Assert.AreEqual("Rover1", rover.ID);
            Assert.AreEqual(adjacentCoordinates[0], rover.Position);
        }
        
    }
}

