namespace CargoSimBackend.Services.Infrastructure
{
    public interface ISimulationService
    {
        Task startSimulation();
        Task stopSimulation();

        bool simulationIsRuning();
    }
}
