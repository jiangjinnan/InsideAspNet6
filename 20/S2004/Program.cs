using App;
var template = @"weather/{city=010}/{days=4}";
var app = WebApplication.Create();
app.MapGet(template, ForecastAsync);
app.Run();

static Task ForecastAsync(HttpContext context)
{
    var routeValues = context.GetRouteData().Values;
    var city = routeValues["city"]!.ToString();
    var days = int.Parse(routeValues["days"]!.ToString()!);
    var report = WeatherReportUtility.Generate(city!, days);
    return WeatherReportUtility.RenderAsync(context, report);
}