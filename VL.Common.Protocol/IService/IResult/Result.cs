using System;
using System.Runtime.Serialization;
using VL.Common.Logger.Objects;

namespace VL.Common.Protocol.IService
{
    [DataContract]
    public class Result
    {
        [DataMember]
        public EResultCode ResultCode { set; get; }
        [DataMember]
        public string Message { set; get; }
        [DataMember]
        protected string MethodName { set; get; }

        public Result(string methodName)
        {
            MethodName = methodName;
        }

        public void LogResult(ILogger logger)
        {
            if (ResultCode == EResultCode.Success)
                return;
            if (ResultCode == EResultCode.Failure)
                logger.Info(string.Format("ResultCode:{0}" + Environment.NewLine + "Method:{1}" + Environment.NewLine + "Message:{2}", ResultCode, MethodName, Message));
            if (ResultCode == EResultCode.Error)
                logger.Error(Message);
        }
    }

    [DataContract]
    public class Result<T>: Result
    {
        public Result(string methodName) : base(methodName)
        {
        }

        [DataMember]
        public T SubResultCode { set; get; }
    }
}
