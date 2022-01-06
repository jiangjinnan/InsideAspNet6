using System.Text;

namespace App
{
    public static class WeatherReportUtility
    {
        private static readonly Random _random = new();
        private static readonly Dictionary<string, string> _cities = new()
        {
            ["010"] = "北京",
            ["028"] = "成都",
            ["0512"] = "苏州"
        };
        private static readonly string[] _conditions = new string[] { "晴", "多云", "小雨" };
        public static WeatherReport Generate(string city, int days)
        {
            var report = new WeatherReport(city, _cities[city], new Dictionary<DateTime, WeatherInfo>());
            for (int i = 0; i < days; i++)
            {
                report.WeatherInfos[DateTime.Today.AddDays(i + 1)] = new WeatherInfo(_conditions[_random.Next(0, 2)], _random.Next(20, 30), _random.Next(10, 20));
            }
            return report;
        }

        public static WeatherReport Generate(string city, DateTime date)
        {
            var report = new WeatherReport(city, _cities[city], new Dictionary<DateTime, WeatherInfo>());
            report.WeatherInfos[date] = new WeatherInfo(_conditions[_random.Next(0, 2)], _random.Next(20, 30), _random.Next(10, 20));
            return report;
        }

        public static Task RenderAsync(HttpContext context, WeatherReport report)
        {
            context.Response.ContentType = "text/html;charset=utf-8";
            return context.Response.WriteAsync(AsHtml(report));
        }

        public static string AsHtml(WeatherReport report)
        {
            return @$"
<html>
<head><title>Weather</title></head>
<body>
    <h3>{report.CityName}</h3>
    {AsHtml(report.WeatherInfos)}
</body>
</html>
";
            static string AsHtml(IDictionary<DateTime, WeatherInfo> dictionary)
            {
                var builder = new StringBuilder();
                foreach (var kv in dictionary)
                {
                    var date = kv.Key.ToString("yyyy-MM-dd");
                    var tempFrom = $"{kv.Value.LowTemperature}℃ ";
                    var tempTo = $"{kv.Value.HighTemperature}℃ ";
                    builder.Append($"{date}: {kv.Value.Condition} （{tempFrom}~{tempTo}）<br/></br>");
                }
                return builder.ToString();
            }
        }
    }
}