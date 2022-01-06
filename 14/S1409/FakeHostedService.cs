namespace App
{
    public class FakeHostedService : BackgroundService
    {
        private readonly IHostEnvironment _environment;
        public FakeHostedService(IHostEnvironment environment)
            => _environment = environment;
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("{0,-15}:{1}", nameof(_environment.EnvironmentName),
                _environment.EnvironmentName);
            Console.WriteLine("{0,-15}:{1}", nameof(_environment.ApplicationName),
                _environment.ApplicationName);
            Console.WriteLine("{0,-15}:{1}", nameof(_environment.ContentRootPath),
                _environment.ContentRootPath);
            return Task.CompletedTask;
        }
    }

}