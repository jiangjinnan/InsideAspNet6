namespace App
{
    public interface IPageRenderer
    {
        IResult RenderLoginPage(string? userName = null, string? password = null, string? errorMessage = null);
        IResult RenderAccessDeniedPage(string userName);
        IResult RenderHomePage(string userName);
    }
    public class PageRenderer : IPageRenderer
    {
        public IResult RenderAccessDeniedPage(string userName)
        {
            var html = @$"
<html>
<head><title>Index</title></head>
<body>
    <h3>{userName}, your access is denied.</h3>
    <a href='/Account/Logout'>Change another account</a>
</body>
</html>";
            return Results.Content(html, "text/html");
        }

        public IResult RenderHomePage(string userName)
        {
            var html = @$"
<html>
<head><title>Index</title></head>
<body>
    <h3>Welcome {userName}</h3>
    <a href='Account/Logout'>Sign Out</a>
</body>
</html>";
            return Results.Content(html, "text/html");
        }

        public IResult RenderLoginPage(string? userName, string? password, string? errorMessage)
        {
            var html = @$"
<html>
<head><title>Login</title></head>
<body>
    <form method='post'>
        <input type='text' name='username' placeholder='User name' value = '{userName}' /> 
        <input type='password' name='password' placeholder='Password' value = '{password}' />
        <input type='submit' value='Sign In' />
    </form>
    <p style='color:red'>{errorMessage}</p>
</body>
</html>";
            return Results.Content(html, "text/html");
        }
    }
}
