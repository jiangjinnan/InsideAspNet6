Host.CreateDefaultBuilder()
    .ConfigureWebHostDefaults(webHostBuilder => webHostBuilder.UseStartup<Startup>())
    .Build()
    .Run();
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
        => services.AddSingleton<IGreeter, Greeter>();
    public void Configure(IApplicationBuilder app, IGreeter greeter)
        => app.Run(context => context.Response.WriteAsync(greeter.Greet()));
}

public interface IGreeter
{
    string Greet();
}

public class Greeter : IGreeter
{
    public string Greet() => "Hello World!";
}