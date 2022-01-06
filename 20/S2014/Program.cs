var app = WebApplication.Create();
app.MapGet("/", (Point foobar) => foobar);
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
    public static bool TryParse(string expression, out Point? point)
    {
        var split = expression.Trim('(', ')').Split(',');
        if (split.Length == 2 && int.TryParse(split[0], out var x) && int.TryParse(split[1], out var y))         
        {
            point = new Point(x, y);
            return true;
        }
        point = null;
        return false;
    }
}
