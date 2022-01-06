namespace App
{
    public interface IAccountService
    {
        bool Validate(string userName, string password, out string[] roles);
    }
    public class AccountService : IAccountService
    {
        private readonly Dictionary<string, string> _accounts = new(StringComparer.OrdinalIgnoreCase)
        {
            { "Foo", "password" },
            { "Bar", "password" },
            { "Baz", "password" }
        };

        private readonly Dictionary<string, string[]> _roles = new(StringComparer.OrdinalIgnoreCase)
        {
             { "Bar", new string[]{"admin" } }
        };

        public bool Validate(string userName, string password)
        => _accounts.TryGetValue(userName, out var pwd) && pwd == password;

        public bool Validate(string userName, string password, out string[] roles)
        {
            if (_accounts.TryGetValue(userName, out var pwd) && pwd == password)
            {
                roles = _roles.TryGetValue(userName, out var value) ? value : Array.Empty<string>();
                return true;
            }
            roles = Array.Empty<string>();
            return false;
        }
    }
}
