using CargoSimBackend.Services.Infrastructure;

namespace CargoSimBackend.Services;

public class ConfigurationService : IConfigurationService
{
    private readonly IConfiguration _configuration;

    public ConfigurationService(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public string GetHahnPassword()
    {
        return this._configuration["UserCredentials:Password"];
    }
    public string Get_GetGridPath()
    {
        return this._configuration["HahnSettings:GetGrid"];
    }

    public string GetHahnUri()
    {
        return this._configuration["HahnSettings:BaseUri"];
    }

    public string GetSignInPath()
    {
         return this._configuration["HahnSettings:SigniIn"];
    }

    public string GetStartSimulationPath()
    {
        return this._configuration["HahnSettings:StartSim"];
    }

    public string GetStopSimulationPath()
    {
        return this._configuration["HahnSettings:StopSim"];
    }
}
