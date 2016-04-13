using VL.Common.Logger.Objects;
using VL.Common.Logger.Utilities;

namespace VL.Common.Protocol.IService
{
    public class ServiceLogHelper
    {
        public static ILogger ServiceLogger { set; get; }= LoggerProvider.GetLog4netLogger("ServiceLog");

        public static void LogResult(Result result)
        {
            if (result.ResultCode == EResultCode.Success)
                return;
            if (result.ResultCode == EResultCode.Failure)
                ServiceLogger.Info(result.Content);
            if (result.ResultCode == EResultCode.Error)
                ServiceLogger.Error(result.Content);
        }
    }
}
