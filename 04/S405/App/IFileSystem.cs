namespace App
{
    public interface IFileSystem
    {
        void ShowStructure(Action<int, string> print);
        Task<string> ReadAllTextAsync(string path);
    }
}
