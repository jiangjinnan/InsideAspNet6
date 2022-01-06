using App;
using App.Properties;
using System.Globalization;

var builder = WebApplication.CreateBuilder();
var template = "resources/{lang:culture}/{resourceName:required}";
builder.Services.Configure<RouteOptions>(options => options.ConstraintMap.Add("culture", typeof(CultureConstraint)));
var app = builder.Build();
app.MapGet(template, GetResource);
app.Run();

IResult GetResource(string lang, string resourceName)
{ 

    CultureInfo.CurrentUICulture = new CultureInfo(lang);
    var text = Resources.ResourceManager.GetString(resourceName);
    return string.IsNullOrEmpty(text)? Results.NotFound(): Results.Content(text);
}