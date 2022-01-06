namespace App
{
public class WebApplicationBuilder
{
    private readonly HostBuilder _hostBuilder = new HostBuilder();
    private WebApplication? _application;
    public ConfigurationManager Configuration { get; } = new ConfigurationManager();
    public IServiceCollection Services { get; } = new ServiceCollection();
    public IWebHostEnvironment Environment { get; }
    public ConfigureHostBuilder Host { get; }
    public ConfigureWebHostBuilder WebHost { get; }
    public ILoggingBuilder Logging { get; } 
    public WebApplicationBuilder(WebApplicationOptions options)
    {
        //创建BootstrapHostBuilder并利用它收集初始化过程中设置的配置、服务和针对依赖注入容器的设置
        var args = options.Args;
        var bootstrap = new BootstrapHostBuilder();
        bootstrap
            .ConfigureDefaults(null)
            .ConfigureWebHostDefaults(webHostBuilder => webHostBuilder.Configure(app => app.Run(_application!.BuildRequestDelegate())))
            .ConfigureHostConfiguration(config => {
                // 添加命令行配置源
                if (args?.Any() == true)
                {
                    config.AddCommandLine(args);
                }

                // 将WebApplicationOptions配置选项转移到配置中
                Dictionary<string, string>? settings = null;
                if (options.EnvironmentName is not null) (settings ??= new())[HostDefaults.EnvironmentKey] = options.EnvironmentName;
                if (options.ApplicationName is not null) (settings ??= new())[HostDefaults.ApplicationKey] = options.ApplicationName;
                if (options.ContentRootPath is not null) (settings ??= new())[HostDefaults.ContentRootKey] = options.ContentRootPath;
                //if (options.WebRootPath is not null) (settings ??= new())[WebHostDefaults.WebRootKey] = options.EnvironmentName;
                if (settings != null)
                {
                    config.AddInMemoryCollection(settings);
                }
            });

        // 将BootstrapHostBuilder收集到配置和服务转移到Configuration和Services上
        // 将应用到BootstrapHostBuilder上针对依赖注入溶质的设置转移到_hostBuilder上
        // 得到BuilderContext上下文
        bootstrap.Apply(_hostBuilder, Configuration, Services, out var builderContext);

        // 如果提供了命令行参数，在Configuration上添加对应配置源
        if (options.Args?.Any() == true)
        {
            Configuration.AddCommandLine(options.Args);
        }

        // 构建WebHostBuilderContext上下文
        // 初始化Host和WebHost属性
        var webHostContext = (WebHostBuilderContext)builderContext.Properties[typeof(WebHostBuilderContext)];
        Environment = webHostContext.HostingEnvironment;
        Host = new ConfigureHostBuilder(builderContext, Configuration, Services);
        WebHost = new ConfigureWebHostBuilder(webHostContext, Configuration, Services);
        Logging = new LogginigBuilder(Services);
    }


    public WebApplication Build()
    {
        // 将ConfigurationManager的配置转移到_hostBuilder
        _hostBuilder.ConfigureAppConfiguration(builder =>
        {            
            builder.AddConfiguration(Configuration);
            foreach (var kv in ((IConfigurationBuilder)Configuration).Properties)
            {
                builder.Properties[kv.Key] = kv.Value;
            }
        });

        // 将添加的服务注册转移到_hostBuilder
        _hostBuilder.ConfigureServices((_, services) =>
        {
            foreach (var service in Services)
            {
                services.Add(service);
            }
        });

        // 将应用到Host属性上的设置转移到_hostBuilder
        Host.Apply(_hostBuilder);

        // 利用_hostBuilder构建的IHost对象创建WebApplication
        return _application = new WebApplication(_hostBuilder.Build());
    }

        private class LogginigBuilder : ILoggingBuilder
        {
            public LogginigBuilder(IServiceCollection services)=> Services = services;
            public IServiceCollection Services { get; }
        }
    }
}


