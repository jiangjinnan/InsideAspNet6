using App;
var template = "weather/{city?}/{days?}"; 
var app = WebApplication.Create();
app.MapGet(template, ForecastAsync);
app.Run();

static Task ForecastAsync(HttpContext context)
{
    var routeValues = context.GetRouteData().Values;
    var city = routeValues.TryGetValue("city", out var v1) ? v1!.ToString() : "010";
    var days = routeValues.TryGetValue("days", out var v2) ? v1!.ToString() : "4";
    var report = WeatherReportUtility.Generate(city!, int.Parse(days!));
    return WeatherReportUtility.RenderAsync(context, report);
}