var builder = WebApplication.CreateBuilder();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
var app = builder.Build();
app.UseDeveloperExceptionPage(
    new DeveloperExceptionPageOptions { SourceCodeLineCount = 3 });
app.MapControllers();
app.Run();
