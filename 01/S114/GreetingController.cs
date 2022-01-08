using Microsoft.AspNetCore.Mvc;

namespace App
{
    public class GreetingController
    {
        [HttpGet("/greet")]
        public string Greet([FromServices] IGreeter greeter) => greeter.Greet(DateTimeOffset.Now);
    }
}
