namespace App
{
    public interface IAccountService
    { 
        bool Validate(string userName, string password);
    }
    public class AccountService: IAccountService
    {
        private Dictionary<string, string> _accounts = new(StringComparer.OrdinalIgnoreCase)
        {
            { "Foo", "password"},
            { "Bar", "password"},
            { "Baz", "password"}
        };

        public bool Validate(string userName, string password)
        =>_accounts.TryGetValue(userName, out var pwd) && pwd == password;
    }
}
