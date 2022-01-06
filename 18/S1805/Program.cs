using System.Diagnostics;
var app = WebApplication.Create(args);
app.Run(context => context.Response.WriteAsync(Process.GetCurrentProcess().ProcessName));
app.Run();
