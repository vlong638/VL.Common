using System.Runtime.Serialization;

namespace VL.Common.Protocol.IService
{
    public class ModuleReportHelper
    {
        public ModuleReportHelper(string moduleName)
        {
            this.ModuleName = moduleName;
        }

        public string ModuleName { set; get; }

        public ClassReportHelper GetClassReportHelper(string className)
        {
            return new ClassReportHelper(ModuleName, className);
        }
    }
    public class ClassReportHelper : ModuleReportHelper
    {
        public ClassReportHelper(string moduleName, string className) : base(moduleName)
        {
            this.ClassName = className;
        }

        public string ClassName { set; get; }

        //public MethodReportHelper GetMethodReportHelper(string methodName)
        //{
        //    return new MethodReportHelper(ModuleName, ClassName, methodName);
        //}
        public Report GetReport(string methodName, int operationType, string message="")
        {
            var code = operationType;
            var locator = ModuleName + "_" + ClassName + "_" + methodName + "_" + operationType;
            return new Report(code, message) { Locator = locator };
        }
    }
    //public class MethodReportHelper : ClassReportHelper
    //{
    //    public MethodReportHelper(string moduleName, string className, string methodName) : base(moduleName, className)
    //    {
    //        this.MethodName = methodName;
    //    }

    //    public string MethodName { set; get; }

    //    public Report GetReport(string operationName, string message)
    //    {
    //        return GetReport(MethodName, operationName, message);
    //    }
    //}
    [DataContract]
    public class Report
    {
        public const int CodeOfSuccess = 1;
        public const int CodeOfError = 2;
        public const int CodeOfManualStart = 3;

        public Report()
        {
            Code = CodeOfError;
            Message = "未执行处理";
        }
        public Report(int code = CodeOfError, string message = "")
        {
            Code = code;
            Message = message;
        }

        [DataMember]
        public int Code { set; get; }
        [DataMember]
        public string Message { set; get; }
        public string Locator { set; get; }
    }
    [DataContract]
    public class Report<T> : Report
    {
        public Report():base()
        {
        }
        public Report(T data, int code= CodeOfError, string message="") : base(code, message)
        {
            Data = data;
        }

        [DataMember]
        public T Data { set; get; } = default(T);
    }
    [DataContract]
    public class Report<T1, T2> : Report
    {
        public Report() : base()
        {
        }
        public Report(T1 data1, T2 data2, int code = CodeOfError, string message = "") : base(code, message)
        {
            Data1 = data1;
            Data2 = data2;
        }

        [DataMember]
        public T1 Data1 { set; get; } = default(T1);
        [DataMember]
        public T2 Data2 { set; get; } = default(T2);
    }


    //[DataContract]
    //public class Report
    //{
    //    [DataMember]
    //    public EResultCode ResultCode { set; get; } = EResultCode.Success;
    //    [DataMember]
    //    public string Message { set; get; } = "";
    //    [DataMember]
    //    public string MethodName { set; get; } = "";

    //    public Report()
    //    {
    //    }
    //    public Report(string methodName)
    //    {
    //        MethodName = methodName;
    //    }

    //    public void LogResult(ILogger logger)
    //    {
    //        if (ResultCode == EResultCode.Success)
    //            return;
    //        if (ResultCode == EResultCode.Failure)
    //            logger.Info(string.Format("ResultCode:{0}" + Environment.NewLine + "Method:{1}" + Environment.NewLine + "Message:{2}", ResultCode, MethodName, Message));
    //        if (ResultCode == EResultCode.Error)
    //            logger.Error(Message);
    //    }
    //    public void CopyAll(Report result)
    //    {
    //        MethodName = result.MethodName;
    //        ResultCode = result.ResultCode;
    //        Message = result.Message;
    //    }
    //    public void CopyContent(Report result)
    //    {
    //        ResultCode = result.ResultCode;
    //        Message = result.Message;
    //    }
    //}
    //[DataContract]
    //public class Result<T> : Report
    //{
    //    public Result()
    //    {
    //    }
    //    public Result(string methodName) : base(methodName)
    //    {
    //    }

    //    [DataMember]
    //    public T Data { set; get; } = default(T);

    //    public void CopyAll(Result<T> result)
    //    {
    //        Data = result.Data;
    //        base.CopyAll(result);
    //    }
    //    public void CopyContent(Result<T> result)
    //    {
    //        Data = result.Data;
    //        base.CopyContent(result);
    //    }
    //}
    //[DataContract]
    //public class Result<T1,T2> : Report
    //{
    //    public Result()
    //    {
    //    }
    //    public Result(string methodName) : base(methodName)
    //    {
    //    }

    //    [DataMember]
    //    public T1 Data1 { set; get; } = default(T1);
    //    [DataMember]
    //    public T2 Data2 { set; get; } = default(T2);

    //    public void CopyAll(Result<T1,T2> result)
    //    {
    //        Data1 = result.Data1;
    //        Data2 = result.Data2;
    //        base.CopyAll(result);
    //    }
    //    public void CopyContent(Result<T1, T2> result)
    //    {
    //        Data1 = result.Data1;
    //        Data2 = result.Data2;
    //        base.CopyContent(result);
    //    }
    //}
}
