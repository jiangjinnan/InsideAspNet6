using App;

var app = WebApplication.Create();
app.MapGet("weather/{city}/{days}", Forecast);
app.Run();

static IResult Forecast(HttpContext context, string city, int days)
{ 
    var report = WeatherReportUtility.Generate(city,days);
    return Results.Content(WeatherReportUtility.AsHtml(report), "text/html");
}
