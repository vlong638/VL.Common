namespace VL.Common.Logger.Objects
{
    public interface IFileBased : ILogger
    {
        string FileName { set; get; }
        string DirectoryName { set; get; }
        string FilePath { get; }
    }
}
