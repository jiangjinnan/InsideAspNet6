namespace App
{
    public class Greeter : IGreeter
    {
        private readonly IConfiguration _configuration;
        public Greeter(IConfiguration configuration) => _configuration = configuration.GetSection("greeting");
        public string Greet(DateTimeOffset time) => time.Hour switch
        {
            var h when h >= 5 && h < 12 => _configuration["morning"],
            var h when h >= 12 && h < 17 => _configuration["afternoon"],
            _ => _configuration["evening"],
        };
    }
}