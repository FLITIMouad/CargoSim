namespace CargoSimBackend.Services.Infrastructure;

public interface IConfigurationService
{
    string GetHahnUri();
    string GetStartSimulationPath();
    string GetStopSimulationPath();
    string GetSignInPath();
    string GetHahnPassword();
    string Get_GetGridPath();
}
