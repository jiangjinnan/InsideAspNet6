using Microsoft.Extensions.Options;

namespace App
{
    public class Greeter : IGreeter
    {
        private readonly GreetingOptions _options;
        private readonly ILogger _logger;
        public Greeter(IOptions<GreetingOptions> optionsAccessor, ILogger<Greeter> logger)
        {
            _options = optionsAccessor.Value;
            _logger = logger;
        }
        public string Greet(DateTimeOffset time)
        {
            var message = time.Hour switch
            {
                var h when h >= 5 && h < 12 => _options.Morning,
                var h when h >= 12 && h < 17 => _options.Afternoon,
                _ => _options.Evening
            };
            _logger.LogInformation(message: "{time} => {message}", time, message);
            return message;
        }
    }
}