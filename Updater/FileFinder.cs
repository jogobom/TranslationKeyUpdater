public static class FileFinder
{
    public static  IEnumerable<string> FindCodeFiles(string dir)
    {
        var tsFiles = GetFilesRecursively(dir, "*.ts");
        var htmlFiles = GetFilesRecursively(dir, "*.html");
        return tsFiles.Concat(htmlFiles);
    }

    private static IEnumerable<string> GetFilesRecursively(string dir, string searchPattern)
    {
        return Directory.EnumerateFiles(dir, searchPattern, new EnumerationOptions { RecurseSubdirectories = true });
    }
}