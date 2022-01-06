using App;

var app = WebApplication.Create();
app.MapGet("weather/{city}/{days}", ForecastAsync);
app.Run();

static Task ForecastAsync(HttpContext context, string city, int days)
{ 
    var report = WeatherReportUtility.Generate(city,days);
    return WeatherReportUtility.RenderAsync(context, report);
}
