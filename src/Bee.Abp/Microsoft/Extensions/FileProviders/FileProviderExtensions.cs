namespace Microsoft.Extensions.FileProviders;

public static class FileProviderExtensions
{
    public static IEnumerable<(string, IFileInfo)> GetFilesRecursively(this IFileProvider fileProvider, string subPath)
    {
        subPath = subPath.EnsureEndsWith('/');
        var contents = fileProvider.GetDirectoryContents(subPath);
        foreach (var content in contents)
        {
            if (content.IsDirectory)
            {
                var path = subPath + content.Name;
                foreach (var file in GetFilesRecursively(fileProvider, path))
                {
                    yield return file;
                }
            }
            else
            {
                yield return (subPath + content.Name, content);
            }
        }
    }
}