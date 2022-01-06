using Microsoft.Extensions.FileProviders;

namespace App
{
    public class FileSystem : IFileSystem
    {
        private readonly IFileProvider _fileProvider;
        public FileSystem(IFileProvider fileProvider) => _fileProvider = fileProvider;
        public void ShowStructure(Action<int, string> print)
        {
            int indent = -1;
            Print("");

            void Print(string subPath)
            {
                indent++;
                foreach (var fileInfo in _fileProvider.GetDirectoryContents(subPath))
                {
                    print(indent, fileInfo.Name);
                    if (fileInfo.IsDirectory)
                    {
                        Print($@"{subPath}\{fileInfo.Name}".TrimStart('\\'));
                    }
                }
                indent--;
            }
        }
    }
}