using Codecool.MarsExploration.MapExplorer.Simulation;

namespace Codecool.MarsExploration.MapExplorer.MarsRover;

public interface IMovementRoutines
{
    public void Move(SimulationContext simulationContext);
    public void TeleportBackToShip(SimulationContext simulationContext);
    public int LeastStepsBackToShip(SimulationContext simulationContext);
}