using System.Collections.ObjectModel;

namespace TrafficControlService.Repositories;

public class DaprVehicleStateRepository : IVehicleStateRepository
{
    private const string DAPR_STORE_NAME = "statestore";

    private readonly HttpClient _httpClient;
    private readonly DaprClient _daprClient;
    public DaprVehicleStateRepository(HttpClient httpClient, DaprClient daprClient)
    {
        _httpClient = httpClient;
        _daprClient = daprClient;
    }

    public async Task<VehicleState?> GetVehicleStateAsync(string licenseNumber)
    {

        //var state = await _httpClient.GetFromJsonAsync<VehicleState>($"http://localhost:3600/v1.0/state/{DAPR_STORE_NAME}/{licenseNumber}");
        var state = await _daprClient.GetStateAsync<VehicleState>(DAPR_STORE_NAME,licenseNumber);
        return state;
    }

    public async Task SaveVehicleStateAsync(VehicleState vehicleState)
    {
        var state = new[] 
        {
            new { key= vehicleState.LicenseNumber, value = vehicleState}
        };

        
        var metadata = new ReadOnlyDictionary<string, string>(new Dictionary<string,string>{ {"ttlInSeconds", "120"}});

        await _daprClient.SaveStateAsync(
            DAPR_STORE_NAME, vehicleState.LicenseNumber, vehicleState, metadata: metadata);
        //await _httpClient.PostAsJsonAsync( $"http://localhost:3600/v1.0/state/{DAPR_STORE_NAME}", state);
    }
}