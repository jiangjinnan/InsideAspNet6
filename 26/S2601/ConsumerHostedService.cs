namespace App
{
    public class ConsumerHostedService : BackgroundService
    {
        private readonly HttpClient[] _httpClients;
        public ConsumerHostedService(IConfiguration configuration)
        {
            var concurrency = configuration.GetValue<int>("Concurrency");
            _httpClients = Enumerable
                .Range(1, concurrency)
                .Select(_ => new HttpClient())
                .ToArray();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var tasks = _httpClients.Select(async client =>
            {
                while (true)
                {
                    var start = DateTimeOffset.UtcNow;
                    var response = await client.GetAsync("http://localhost:5000");
                    var duration = DateTimeOffset.UtcNow - start;
                    var status = $"{(int)response.StatusCode},{response.StatusCode}";
                    Console.WriteLine($"{status} [{(int)duration.TotalSeconds}s]");
                    if (!response.IsSuccessStatusCode)
                    {
                        await Task.Delay(1000);
                    }
                }
            });
            return Task.WhenAll(tasks);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            Array.ForEach(_httpClients, it => it.Dispose());
            return Task.CompletedTask;
        }
    }
}