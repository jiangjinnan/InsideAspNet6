using App;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

var source = new Dictionary<string, string>
{
    ["point"] = "(123,456)"
};

var root = new ConfigurationBuilder()
    .AddInMemoryCollection(source)
    .Build();

var point = root.GetValue<Point>("point");
Debug.Assert(point.X == 123);
Debug.Assert(point.Y == 456);