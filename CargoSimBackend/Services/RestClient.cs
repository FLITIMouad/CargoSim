
using CargoSimBackend.DTO_s;
using CargoSimBackend.Services.Infrastructure;
using Newtonsoft.Json;

namespace CargoSimBackend.Services;

public class RestClient : IRestClient
{
    private readonly HttpClient _httpClient;
    private readonly IAuthService _authService;
    private readonly IConfigurationService _configurationService;
    public RestClient(IConfigurationService configurationService, HttpClient httpClient, IAuthService authService)
    {
        _configurationService = configurationService;
        _httpClient = httpClient;
        this._authService = authService;
        _httpClient.BaseAddress = new Uri(_configurationService.GetHahnUri());
    }
    public async Task<string> StartSimulation()
    {
        try
        {
            string pathToStartSim = _configurationService.GetStartSimulationPath();
            var request = new HttpRequestMessage(HttpMethod.Post, pathToStartSim);
            string token = this._authService.getToken();
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var result = await _httpClient.SendAsync(request);
            result.EnsureSuccessStatusCode();
            return await result.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            return await Task.FromException<string>(ex);
        }
    }

    public async Task<string> StopSimulation()
    {
        try
        {
            string pathToStopSim = _configurationService.GetStopSimulationPath();
            var request = new HttpRequestMessage(HttpMethod.Post, pathToStopSim);
            string token = this._authService.getToken();
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            return await Task.FromException<string>(ex);
        }
    }

    public async Task<(Grid?,string)> GetGrid()
    {
        try
        {
            string getGridPath = _configurationService.Get_GetGridPath();
            var request = new HttpRequestMessage(HttpMethod.Get, getGridPath);
            string token = this._authService.getToken();
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result= await response.Content.ReadAsStringAsync();
            Grid grid= JsonConvert.DeserializeObject<Grid>(result); 
            return (grid, string.Empty);
        }
        catch (Exception ex)
        {
            return (null,await Task.FromException<string>(ex));
        }
    }

    public async Task<string> SignIn(string username, string password)
    {
        try
        {
            string signInPath = _configurationService.GetSignInPath();
            var request = new HttpRequestMessage(HttpMethod.Post, signInPath);
            request.Content = new StringContent($"{{\"username\":\"{username}\",\"password\":\"{password}\"}}", System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            return await Task.FromException<string>(ex);
        }
    }

}
