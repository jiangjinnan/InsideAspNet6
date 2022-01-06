namespace App
{
    public class Greeter : IGreeter
    {
        public string Greet(DateTimeOffset time) => time.Hour switch
        {
            var h when h >= 5 && h < 12 => "Good morning!",
            var h when h >= 12 && h < 17 => "Good afternoon!",
            _ => "Good evening"
        };
    }
}