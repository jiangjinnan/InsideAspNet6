using System.Diagnostics;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();
app.MapGet("/", Handle);
app.Run();

static void Handle(HttpContext httpContext, HttpRequest request, HttpResponse response, ClaimsPrincipal user, CancellationToken cancellationToken, IHttpContextAccessor httpContextAccessor)
{
    var currentContext = httpContextAccessor.HttpContext;
    Debug.Assert(ReferenceEquals(httpContext, currentContext));
    Debug.Assert(ReferenceEquals(request, currentContext.Request));
    Debug.Assert(ReferenceEquals(response, currentContext.Response));
    Debug.Assert(ReferenceEquals(user, currentContext.User));
    Debug.Assert(cancellationToken == currentContext.RequestAborted);
}