using VL.Common.Logger.Objects;
using VL.Common.Logger.Utilities;

namespace VL.Common.Protocol.IService
{
    public static class LoggerEx
    {
        public static void LogResult(this ILogger logger,Result result)
        {
            if (result.ResultCode == EResultCode.Success)
                return;
            if (result.ResultCode == EResultCode.Failure)
                logger.Info(result.Content);
            if (result.ResultCode == EResultCode.Error)
                logger.Error(result.Content);
        }
    }
}
