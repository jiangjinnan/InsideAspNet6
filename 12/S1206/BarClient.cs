namespace App
{
    public class BarClient
    {
        private readonly HttpClient _httpClient;
        public BarClient(HttpClient httpClient) => _httpClient = httpClient;
        public Task<string> GetStringAsync(string path) => _httpClient.GetStringAsync(path);
    }
}