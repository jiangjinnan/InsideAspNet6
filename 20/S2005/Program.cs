using App;
var template = "weather/{city}/{year}.{month}.{day}";
var app = WebApplication.Create();
app.MapGet(template, ForecastAsync);
app.Run();

static Task ForecastAsync(HttpContext context)
{
    var routeValues = context.GetRouteData().Values;
    var city = routeValues["city"]!.ToString();
    var year = int.Parse(routeValues["year"]!.ToString()!);
    var month = int.Parse(routeValues["month"]!.ToString()!);
    var day = int.Parse(routeValues["day"]!.ToString()!);
    var report = WeatherReportUtility.Generate(city!, new DateTime(year,month,day));
    return WeatherReportUtility.RenderAsync(context, report);
}