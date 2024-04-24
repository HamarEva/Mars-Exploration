using Codecool.MarsExploration.MapExplorer.Simulation;

namespace Codecool.MarsExploration.MapExplorer.Repository;

public interface ISimulationRepository
{
    void Add(string timeStamp, int stepsCount, int resourcesCount, string outcome);
    
    void Delete(int id);
    
    void DeleteAll();

    SimulationData Get(int id);
    
    IEnumerable<SimulationData> GetAll();
}