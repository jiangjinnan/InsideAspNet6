using System.Globalization;

namespace App
{
public class CultureConstraint : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey,
        RouteValueDictionary values, RouteDirection routeDirection)
    {
        try
        {
            if (values.TryGetValue(routeKey, out var value) && value is not null)
            {
                return !new CultureInfo((string)value).EnglishName.StartsWith("Unknown Language");
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
}

}
