using Newtonsoft.Json;

namespace WorkoutGenerator.Utils;

public class HttpClientWrapper
{
    private readonly HttpClient _client;
    
    public HttpClientWrapper(HttpClient client)
    {
        _client = client;
    }
    
    public async Task<T> PostDeserializedAsync<T>(string api, object requestBody)
    {
        var jsonRequestBody = JsonConvert.SerializeObject(requestBody);
        var httpContent = new StringContent(jsonRequestBody, System.Text.Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(api, httpContent);
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseContent);
    }
}