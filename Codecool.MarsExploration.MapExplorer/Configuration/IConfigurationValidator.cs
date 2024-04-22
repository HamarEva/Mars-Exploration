using Codecool.MarsExploration.MapGenerator.Calculators.Model;

namespace Codecool.MarsExploration.MapExplorer.Configuration;

public interface IConfigurationValidator
{
   public bool Validate(Configuration configuration);
}