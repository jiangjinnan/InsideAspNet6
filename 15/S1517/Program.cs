using System.Diagnostics;
using System.Reflection;

var builder = WebApplication.CreateBuilder();
var environment = builder.Environment;

Debug.Assert(Assembly.GetEntryAssembly()?.GetName().Name == environment.ApplicationName);
var currentDirectory = Directory.GetCurrentDirectory();

Debug.Assert(Equals( environment.ContentRootPath,  currentDirectory));
Debug.Assert(Equals(environment.ContentRootPath, currentDirectory));

var wwwRoot = Path.Combine(currentDirectory, "wwwroot");
if (Directory.Exists(wwwRoot))
{
    Debug.Assert(Equals(environment.WebRootPath, wwwRoot));
}
else
{
    Debug.Assert(environment.WebRootPath == null);
}

static bool Equals(string path1, string path2)
=>string.Equals(path1.Trim(Path.DirectorySeparatorChar), path2.Trim(Path.DirectorySeparatorChar),StringComparison.OrdinalIgnoreCase);
