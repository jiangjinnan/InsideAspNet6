using App;
using System.Globalization;

var template = "weather/{city}/{*date}"; var app = WebApplication.Create();
app.MapGet(template, ForecastAsync);
app.Run();

static Task ForecastAsync(HttpContext context)
{
    var routeValues = context.GetRouteData().Values;
    var city = routeValues["city"]!.ToString();
    var date = DateTime.ParseExact(routeValues["date"]?.ToString()!, "yyyy/MM/dd",CultureInfo.InvariantCulture);
    var report = WeatherReportUtility.Generate(city!, date);
    return WeatherReportUtility.RenderAsync(context, report);
}