using Microsoft.AspNetCore.Mvc;

namespace App
{
public class GreetingController:Controller
{
    [HttpGet("/greet")]
    public IActionResult Greet()
    {
        ViewBag.Time = DateTimeOffset.Now;
        return View();
    }
}
}
