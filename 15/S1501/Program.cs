using Microsoft.AspNetCore;

WebHost.CreateDefaultBuilder()
    .Configure(app => app.Run(context => context.Response.WriteAsync("Hello World!")))
    .Build()
    .Run();
