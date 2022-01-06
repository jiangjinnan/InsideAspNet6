namespace App
{
internal static class HostingPathResolver
{
    public static string ResolvePath(string? contentRootPath) => ResolvePath(contentRootPath, AppContext.BaseDirectory);
    public static string ResolvePath(string? contentRootPath, string basePath)
        => string.IsNullOrEmpty(contentRootPath)
        ? Path.GetFullPath(basePath)
        : Path.IsPathRooted(contentRootPath)
        ? Path.GetFullPath(contentRootPath)
        : Path.GetFullPath(Path.Combine(Path.GetFullPath(basePath), contentRootPath));
}
}
