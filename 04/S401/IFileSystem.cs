namespace App
{
    public interface IFileSystem
    {
        void ShowStructure(Action<int, string> print);
    }
}
