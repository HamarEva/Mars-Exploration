using System;
using System.Collections.Generic;
using Codecool.MarsExploration.MapExplorer.Configuration;
using Codecool.MarsExploration.MapExplorer.Exploration;
using Codecool.MarsExploration.MapExplorer.MapLoader;
using Codecool.MarsExploration.MapExplorer.MarsRover;
using Codecool.MarsExploration.MapGenerator.Calculators.Model;
using Codecool.MarsExploration.MapGenerator.Calculators.Service;
using Codecool.MarsExploration.MapGenerator.MapElements.Model;
using ContextBuilder = Codecool.MarsExploration.MapExplorer.Simulation.ContextBuilder;

namespace Codecool.MarsExploration.MapExplorer.Simulation
{
  
    public class ExplorationSimulator
    {

        private readonly ContextBuilder _contextBuilder;
        private readonly SimulationStep _simulationStep;

        public ExplorationSimulator(ContextBuilder contextBuilder, SimulationStep simulationStep)
        {
            _contextBuilder = contextBuilder;
            _simulationStep = simulationStep;
        }

        public SimulationContext GetContext()

        {
            return _contextBuilder.CreateContext();
        }

        public void Simulate()
        {
            var simulationContext = GetContext();
            do
            {
                _simulationStep.OneStep(simulationContext);
            } while (simulationContext.ExplorationOutcome == ExplorationOutcome.Null);
        }
    }
}
