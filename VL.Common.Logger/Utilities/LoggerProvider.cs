using VL.Common.Logger.Objects;

namespace VL.Common.Logger.Utilities
{
    public class LoggerProvider
    {
        public static ILogger GetLog4netLogger(string loggerName)
        {
            return new Log4netLogger(loggerName);
        }
        public static ILogger GetTextLogger(string fileName, string directoryName)
        {
            return new TextLogger(fileName, directoryName);
        }
    }
}
