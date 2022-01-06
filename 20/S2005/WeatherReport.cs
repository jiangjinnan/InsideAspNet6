namespace App
{
    public readonly record struct WeatherInfo(string Condition, double HighTemperature, double LowTemperature);
    public readonly record struct WeatherReport(string CityCode, string CityName, IDictionary<DateTime, WeatherInfo> WeatherInfos);
}