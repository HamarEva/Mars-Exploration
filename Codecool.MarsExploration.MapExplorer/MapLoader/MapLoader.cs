using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.MapLoader;

public class MapLoader : IMapLoader
{
    public Map Load(string mapFile)
    {
        string[] map = File.ReadAllLines(mapFile);
        int rowCount =map.Length;
        int columnCount = map[0].Length;
        var representationArray = StringToMultidimensionArray(map,rowCount,columnCount);

        var loadedMap = new Map(representationArray, true);
        return loadedMap;

    }

    private string[,] StringToMultidimensionArray(string[] map, int rows, int cloums)
    {
        
        string[,] arr = new string[rows, cloums];

        int index = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cloums; j++)
            {
                arr[i, j] = map[i][j].ToString();
                index++;
            }
        }

        return arr;
    }
    
}