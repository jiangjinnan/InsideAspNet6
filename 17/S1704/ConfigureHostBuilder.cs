namespace App
{
public class ConfigureHostBuilder : IHostBuilder
{
    private readonly ConfigurationManager _configuration;
    private readonly IServiceCollection _services;
    private readonly HostBuilderContext _context;
    private readonly List<Action<IHostBuilder>> _configureActions = new();
    internal ConfigureHostBuilder(HostBuilderContext context, ConfigurationManager configuration, IServiceCollection services)
    {
        _configuration = configuration;
        _services = services;
        _context = context;
    }

    public IDictionary<object, object> Properties => _context.Properties;
    public IHost Build() => throw new NotImplementedException();
    public IHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
    => Configure(() => configureDelegate(_context, _configuration));

    public IHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate)
    {
        var applicationName = _configuration[HostDefaults.ApplicationKey];
        var contentRoot = _context.HostingEnvironment.ContentRootPath;
        var environment = _configuration[HostDefaults.EnvironmentKey];

        configureDelegate(_configuration);

        // 与环境相关的三个配置不允许改变
        Validate(applicationName, HostDefaults.ApplicationKey, "Application name cannot be changed.");
        Validate(contentRoot, HostDefaults.ContentRootKey, "Content root cannot be changed.");
        Validate(environment, HostDefaults.EnvironmentKey, "Environment name cannot be changed.");

        return this;

        void Validate(string previousValue, string key, string message)
        {
            if (!string.Equals(previousValue, _configuration[key], StringComparison.OrdinalIgnoreCase))
            {
                throw new NotSupportedException(message);
            }
        }
    }

    public IHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
    => Configure(() => configureDelegate(_context, _services));

    public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
    => Configure(() => _configureActions.Add(b => b.UseServiceProviderFactory(factory)));

    public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factory)
    => Configure(() => _configureActions.Add(b => b.UseServiceProviderFactory(factory)));

    public IHostBuilder ConfigureContainer<TContainerBuilder>(Action<HostBuilderContext, TContainerBuilder> configureDelegate)
    => Configure(() => _configureActions.Add(b => b.ConfigureContainer(configureDelegate)));

    private IHostBuilder Configure(Action configure)
    {
        configure();
        return this;
    }

    internal void Apply(IHostBuilder hostBuilder) => _configureActions.ForEach(op => op(hostBuilder));
}
}