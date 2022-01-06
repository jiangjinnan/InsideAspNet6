using Microsoft.Extensions.Options;

namespace App
{
    public class Greeter : IGreeter
    {
        private readonly GreetingOptions _options;
        public Greeter(IOptions<GreetingOptions> optionsAccessor) => _options = optionsAccessor.Value;
        public string Greet(DateTimeOffset time) => time.Hour switch
        {
            var h when h >= 5 && h < 12 => _options.Morning,
            var h when h >= 12 && h < 17 => _options.Afternoon,
            _ => _options.Evening
        };
    }
}