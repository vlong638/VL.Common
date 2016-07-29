using System;
using System.Runtime.Serialization;
using VL.Common.Logger.Objects;

namespace VL.Common.Protocol.IService
{
    [DataContract]
    public class Result
    {
        [DataMember]
        public EResultCode ResultCode { set; get; } = EResultCode.Success;
        [DataMember]
        public string Message { set; get; } = "";
        [DataMember]
        public string MethodName { set; get; } = "";

        public Result()
        {
        }
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
        public void CopyAll(Result result)
        {
            MethodName = result.MethodName;
            ResultCode = result.ResultCode;
            Message = result.Message;
        }
        public void CopyContent(Result result)
        {
            ResultCode = result.ResultCode;
            Message = result.Message;
        }
    }

    [DataContract]
    public class Result<T> : Result
    {
        public Result()
        {
        }
        public Result(string methodName) : base(methodName)
        {
        }

        [DataMember]
        public T Data { set; get; } = default(T);

        public void CopyAll(Result<T> result)
        {
            Data = result.Data;
            base.CopyAll(result);
        }
        public void CopyContent(Result<T> result)
        {
            Data = result.Data;
            base.CopyContent(result);
        }
    }
    [DataContract]
    public class Result<T1,T2> : Result
    {
        public Result()
        {
        }
        public Result(string methodName) : base(methodName)
        {
        }

        [DataMember]
        public T1 Data1 { set; get; } = default(T1);
        [DataMember]
        public T2 Data2 { set; get; } = default(T2);

        public void CopyAll(Result<T1,T2> result)
        {
            Data1 = result.Data1;
            Data2 = result.Data2;
            base.CopyAll(result);
        }
        public void CopyContent(Result<T1, T2> result)
        {
            Data1 = result.Data1;
            Data2 = result.Data2;
            base.CopyContent(result);
        }
    }
}
