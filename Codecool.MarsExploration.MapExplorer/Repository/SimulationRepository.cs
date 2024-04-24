using Codecool.MarsExploration.MapExplorer.Simulation;
using Microsoft.Data.Sqlite;

namespace Codecool.MarsExploration.MapExplorer.Repository;

public class SimulationRepository : ISimulationRepository
{
    private readonly string _dbFilePath;

    public SimulationRepository(string dbFilePath)
    {
        _dbFilePath = dbFilePath;
    }
    
    private SqliteConnection GetPhysicalDbConnection()
    {
        var dbConnection = new SqliteConnection($"Data Source ={_dbFilePath};Mode=ReadWrite");
        dbConnection.Open();
        return dbConnection;
    }
    
    private void ExecuteNonQuery(string query)
    {
        using var connection = GetPhysicalDbConnection();
        using var command = GetCommand(query, connection);
        command.ExecuteNonQuery();
    }

    private static SqliteCommand GetCommand(string query, SqliteConnection connection)
    {
        return new SqliteCommand
        {
            CommandText = query,
            Connection = connection,
        };
    }
    
    public void Add(string timeStamp, int stepsCount, int resourcesCount, string outcome)
    {
        var query = $"INSERT INTO simulations (timeStamp, stepsCount, resourcesCount, outcome) VALUES ('{timeStamp}', '{stepsCount}', '{resourcesCount}', '{outcome}')";
        ExecuteNonQuery(query);
    }

    public void Delete(int id)
    {
        var query = $"DELETE FROM simulations WHERE id = {id}";
        ExecuteNonQuery(query);
    }

    public void DeleteAll()
    {
        var query = "DELETE FROM simulatons";
        ExecuteNonQuery(query);
    }

    public SimulationData Get(int id)
    {
        var query = @$"SELECT * FROM simulations WHERE id = {id}";
        using var connection = GetPhysicalDbConnection();
        using var command = GetCommand(query, connection);

        using var reader = command.ExecuteReader();
        return new SimulationData(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4));
    }

    public IEnumerable<SimulationData> GetAll()
    {
        var simulations = new List<SimulationData>();
    
        var query = "SELECT * FROM simulations";
        using var connection = GetPhysicalDbConnection();
        using var command = GetCommand(query, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            simulations.Add(new SimulationData(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetString(4)));
        }

        return simulations;
    }
}