using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
var app = builder.Build();
app.UseDeveloperExceptionPage();
app.MapControllers();
app.Run();

public class HomeController : Controller
{
    [HttpGet("/")]
    public IActionResult Index() => View();
}
