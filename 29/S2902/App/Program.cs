var app = WebApplication.Create();
app.MapGet("/", Render);
app.Run(url:"http://0.0.0.0:3721");

static IResult Render()
{
    var html = @"
<html>
    <body>
        <ul id='contacts'></ul>
        <script src='http://code.jquery.com/jquery-3.3.1.min.js'></script>
        <script>
        $(function()
        {
            var url = 'http://www.qux.com:8080/contacts';
            $.getJSON(url, null, function(contacts) {
                $.each(contacts, function(index, contact)
                {
                    var html = '<li><ul>';
                    html += '<li>Name: ' + contact.name + '</li>';
                    html += '<li>Phone No:' + contact.phoneNo + '</li>';
                    html += '<li>Email Address: ' + contact.emailAddress + '</li>';
                    html += '</ul>';
                    $('#contacts').append($(html));
                });
            });
        });
        </script >
    </body>
</html>";
    return Results.Text(content: html, contentType: "text/html");
}
