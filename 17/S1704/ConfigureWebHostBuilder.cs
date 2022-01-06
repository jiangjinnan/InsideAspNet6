using Microsoft.AspNetCore.Hosting.Infrastructure;
using System.Diagnostics.CodeAnalysis;

namespace App
{
public class ConfigureWebHostBuilder : IWebHostBuilder, ISupportsStartup
{
    private readonly WebHostBuilderContext _builderContext;
    private readonly IServiceCollection _services;
    private readonly ConfigurationManager _configuration;

    public ConfigureWebHostBuilder(WebHostBuilderContext builderContext, ConfigurationManager configuration, IServiceCollection services)
    {
        _builderContext = builderContext;
        _services = services;
        _configuration = configuration;
    }

    public IWebHost Build()=> throw new NotImplementedException();       
    public IWebHostBuilder ConfigureAppConfiguration(Action<WebHostBuilderContext, IConfigurationBuilder> configureDelegate)
        => Configure(() => configureDelegate(_builderContext, _configuration));
    public IWebHostBuilder ConfigureServices(Action<IServiceCollection> configureServices)
            => Configure(() => configureServices(_services));
    public IWebHostBuilder ConfigureServices(Action<WebHostBuilderContext, IServiceCollection> configureServices)
        => Configure(() => configureServices(_builderContext, _services));
    public string? GetSetting(string key) => _configuration[key];
    public IWebHostBuilder UseSetting(string key, string? value)
            => Configure(() => _configuration[key] = value);

    IWebHostBuilder ISupportsStartup.UseStartup(Type startupType)=> throw new NotImplementedException();
    IWebHostBuilder ISupportsStartup.UseStartup<TStartup>(Func<WebHostBuilderContext, TStartup> startupFactory)=> throw new NotImplementedException();
    IWebHostBuilder ISupportsStartup.Configure(Action<IApplicationBuilder> configure)=> throw new NotImplementedException();
    IWebHostBuilder ISupportsStartup.Configure(Action<WebHostBuilderContext, IApplicationBuilder> configure)=> throw new NotImplementedException();

    private IWebHostBuilder Configure(Action configure)
    {
        configure();
        return this;
    }
}
}
