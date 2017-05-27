using log4net;

//备注!!! 下述属性配置需在获取ILog的位置声明
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace VL.Logger
{
    public class Log4netLogger : ILogger
    {
        ILog _Log;
        ILog Log
        {
            set { _Log = value; }
            get
            {
                if (_Log == null)
                {
                    _Log = log4net.LogManager.GetLogger("Default");
                }
                return _Log;
            }
        }
        public void SetupLog(string loggerName)
        {
            Log = LogManager.GetLogger(loggerName);
        }

        public void Info(LogData locator)
        {
            Log.Info(locator.ToString());
        }
        public void Error(LogData locator)
        {
            Log.Error(locator.ToString());
        }
    }
}
