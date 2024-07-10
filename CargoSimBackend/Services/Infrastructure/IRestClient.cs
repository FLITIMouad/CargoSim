using CargoSimBackend.DTO_s;

namespace CargoSimBackend.Services.Infrastructure;

public interface IRestClient
{
    Task<string> StartSimulation();
    Task<string> StopSimulation();
    Task<string> SignIn(string username, string password);
    Task<(Grid?, string)> GetGrid();
}
