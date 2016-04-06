using log4net;

namespace VL.Common.Logger.Objects
{
    public class Log4netLogger : ILogger
    {
        public static ILog Log;

        public Log4netLogger(string loggerName)
        {
            Log = LogManager.GetLogger(loggerName);
        }

        public void Info(string message)
        {
            Log.Info(message);
        }
        public void Info(string pattern, params object[] args)
        {
            Log.InfoFormat(pattern, args);
        }
        public void Error(string message)
        {
            Log.Error(message);
        }
        public void Error(string pattern, params object[] args)
        {
            Log.ErrorFormat(pattern, args);
        }
    }
}
