using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting.Internal;

namespace App
{
public class BootstrapHostBuilder : IHostBuilder
{
    private readonly List<Action<IConfigurationBuilder>> _configureHostConfigurations = new();
    private readonly List<Action<HostBuilderContext, IConfigurationBuilder>> _configureAppConfigurations = new();
    private readonly List<Action<HostBuilderContext, IServiceCollection>> _configureServices = new();
    private readonly List<Action<IHostBuilder>> _others = new();

    public IDictionary<object, object> Properties { get; } = new Dictionary<object, object>();
    public IHost Build()=> throw new NotImplementedException();
    public IHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate)
    {
        _configureHostConfigurations.Add(configureDelegate);
        return this;
    }
    public IHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate)
    {
        _configureAppConfigurations.Add(configureDelegate);
        return this;
    }
    public IHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
    {
        _configureServices.Add(configureDelegate);
        return this;
    }
    public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory)
    {
        _others.Add(builder => builder.UseServiceProviderFactory(factory));
        return this;
    }
    public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factory)
    {
        _others.Add(builder => builder.UseServiceProviderFactory(factory));
        return this;
    }
    public IHostBuilder ConfigureContainer<TContainerBuilder>(Action<HostBuilderContext, TContainerBuilder> configureDelegate)
    {
        _others.Add(builder => builder.ConfigureContainer(configureDelegate));
        return this;
    }

    internal void Apply(IHostBuilder hostBuilder,  ConfigurationManager configuration,  IServiceCollection services, out HostBuilderContext builderContext)
    {
        // 初始化针对宿主的配置
        var hostConfiguration = new ConfigurationManager();
        _configureHostConfigurations.ForEach(it => it(hostConfiguration));

        // 创建承载环境
        var environment = new HostingEnvironment()
        {
            ApplicationName = hostConfiguration[HostDefaults.ApplicationKey],
            EnvironmentName = hostConfiguration[HostDefaults.EnvironmentKey] ?? Environments.Production,
            ContentRootPath = HostingPathResolver.ResolvePath(hostConfiguration[HostDefaults.ContentRootKey])
        };
        environment.ContentRootFileProvider = new PhysicalFileProvider(environment.ContentRootPath);

        // 创建HostBuilderContext上下文
        var hostContext = new HostBuilderContext(Properties)
        {
            Configuration = hostConfiguration,
            HostingEnvironment = environment,
        };

        // 将针对宿主的配置添加到ConfigurationManager中
        configuration.AddConfiguration(hostConfiguration, true);

        // 初始化针对应用的配置
        _configureAppConfigurations.ForEach(it => it(hostContext, configuration));

        // 收集服务注册
        _configureServices.ForEach(it => it(hostContext, services));

        // 将针对依赖注入容器的设置应用到指定的IHostBuilder对象上
        _others.ForEach(it => it(hostBuilder));

        // 将自定义属性转移到指定的IHostBuilder对象上
        foreach (var kv in Properties)
        {
            hostBuilder.Properties[kv.Key] = kv.Value;
        }

        builderContext = hostContext;
    }
}
}
