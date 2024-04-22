using Codecool.MarsExploration.MapExplorer.MapLoader;

namespace CodeCool.MarsExploration.MapExplorer.Test;

public class MapLoader_Test
{
    private IMapLoader mapLoader;

    [SetUp]
    public void SetUp()
    {
        mapLoader = new MapLoader();
    }

    [Test]
    public void Load_ValidMapFile_ReturnsMapObject()
    {
       
        string mapFile = "testMap.txt";
        string mapContent = "##*\n#S#\n*#%";
        File.WriteAllText(mapFile, mapContent);
        var map = mapLoader.Load(mapFile);
     
        
        Assert.IsNotNull(map);
        Assert.AreEqual(3, map.Representation.GetLength(0));
        Assert.AreEqual(3,map.Representation.GetLength(1));
        Assert.AreEqual("#", map.Representation[0, 0]);
        Assert.AreEqual("S", map.Representation[1, 1]);
        Assert.AreEqual("%", map.Representation[2, 2]);


        File.Delete(mapFile);
    }


}