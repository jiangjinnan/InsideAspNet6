namespace App
{
    public class FooClient
    {
        private readonly HttpClient _httpClient;
        public FooClient(HttpClient httpClient) => _httpClient = httpClient;
        public Task<string> GetStringAsync(string path) => _httpClient.GetStringAsync(path);
    }
}