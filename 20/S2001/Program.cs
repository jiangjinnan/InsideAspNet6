using App;
var app = WebApplication.Create();
app.MapGet("weather/{city}/{days}", ForecastAsync);
app.Run();

static Task ForecastAsync(HttpContext context)
{
    var routeValues = context.GetRouteData().Values;
    var city = routeValues["city"]!.ToString();
    var days = int.Parse(routeValues["days"]!.ToString()!);
    var report = WeatherReportUtility.Generate(city!, days);
    return WeatherReportUtility.RenderAsync(context, report);
}