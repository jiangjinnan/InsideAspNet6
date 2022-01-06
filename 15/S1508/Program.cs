using System.Text;

var builder = WebApplication.CreateBuilder();
var app = builder.Build();
app.Run(InvokeAsync);
app.Run();

Task InvokeAsync(HttpContext httpContext)
{
    var sb = new StringBuilder();
    foreach (var service in builder.Services)
    {
        var serviceTypeName = GetName(service.ServiceType);
        var implementationType = service.ImplementationType
            ?? service.ImplementationInstance?.GetType()
            ?? service.ImplementationFactory
            ?.Invoke(httpContext.RequestServices)?.GetType();
        if (implementationType != null)
        {
            sb.AppendLine(@$"{service.Lifetime,-15}{ GetName(service.ServiceType),-60}{ GetName(implementationType)}");
        }
    }
    return httpContext.Response.WriteAsync(sb.ToString());
}

static string GetName(Type type)
{
    if (!type.IsGenericType)
    {
        return type.Name;
    }
    var name = type.Name.Split('`')[0];
    var args = type.GetGenericArguments().Select(it => it.Name);
    return @$"{name}<{string.Join(",", args)}>";
}
