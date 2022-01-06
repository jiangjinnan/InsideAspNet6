using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseDefaultServiceProvider(options => options.ValidateScopes = true);
builder.Services.AddDbContext<FoobarDbContext>(
    options => options.UseSqlServer("{your connection string}"));
var app = builder.Build();
app.UseMiddleware<FoobarMiddleware>();
app.Run();

public class FoobarMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Foobar? _foobar;
    public FoobarMiddleware(RequestDelegate next, FoobarDbContext dbContext)
    {
        _next = next;
        _foobar = dbContext.Foobar.SingleOrDefault();
    }

    public Task InvokeAsync(HttpContext context)
    {
        return _next(context);
    }
}

public class Foobar
{
    [Key]
    public string Foo { get; set; }
    public string Bar { get; set; }
}

public class FoobarDbContext : DbContext
{
    public DbSet<Foobar> Foobar { get; set; }
    public FoobarDbContext(DbContextOptions options) : base(options) { }
}
