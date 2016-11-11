namespace VL.Common.Logger
{
    public interface ILogger
    {
        void Info(string message);
        void Info(string pattern, params object[] args);
        void Error(string message);
        void Error(string pattern, params object[] args);
    }
}
