using System.Reflection;

var app = WebApplication.Create();
app.MapPost("/", (Point foobar) => foobar);
app.Run();
public class Point
{
    public int X { get; set; }
    public int Y { get; set; }
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
    public static ValueTask<Point?> BindAsync(HttpContext httpContext, ParameterInfo parameter)
    {
        Point? point = null;
        var name = parameter.Name;
        var value = httpContext.GetRouteData().Values.TryGetValue(name!, out var v)
            ? v
            : httpContext.Request.Query[name!].SingleOrDefault();

        if (value is string expression)
        {
            var split = expression.Trim('(', ')')?.Split(',');
            if (split?.Length == 2 && int.TryParse(split[0], out var x) && int.TryParse(split[1], out var y))
            {
                point = new Point(x, y);
            }
        }
        return new ValueTask<Point?>(point);
    }
}