using System;
using System.Collections.Generic;
using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.Exploration;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;

namespace Codecool.MarsExploration.MapExplorer.Simulation
{
  
    public class ExplorationSimulator
    {

        private readonly ContextBuilder _contextBuilder;

        public ExplorationSimulator(ContextBuilder contextBuilder)
        {
            _contextBuilder = contextBuilder;
        }

        public SimulationContext GetContext()

        {
            return _contextBuilder.CreateContext();
        }

        public void Simulate()
        {
            
        }
    }
}
