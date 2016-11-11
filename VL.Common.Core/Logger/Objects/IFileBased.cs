namespace VL.Common.Logger
{
    public interface IFileBased : ILogger
    {
        string FileName { set; get; }
        string DirectoryName { set; get; }
        string FilePath { get; }
    }
}
