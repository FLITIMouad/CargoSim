using CargoSimBackend.Services.Infrastructure;

namespace CargoSimBackend.Services
{
    public class SimulationService : ISimulationService
    {
        private bool _simulationRuning = false;
        private readonly IRestClient _restClient;

        public SimulationService(IRestClient restClient)
        {
            _restClient = restClient;
        }
        public async Task startSimulation()
        {
            this._simulationRuning = true;
            await _restClient.StartSimulation();
        }

        public async Task stopSimulation()
        {
            this._simulationRuning = false;
            await _restClient.StopSimulation();
        }

        public bool simulationIsRuning()
        {
           return this._simulationRuning;
        }
    }
}
