using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VL.Common.Protocol//.IService.IResult
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

        public Report GetReport(string methodName, int operationType, string message="")
        {
            var code = operationType;
            var locator = ModuleName + "_" + ClassName + "_" + methodName + "_" + operationType;
            return new Report(code, message) { Locator = locator };
        }
    }
    [DataContract]
    public class Report
    {
        public const int CodeOfSuccess = 1;
        public const int CodeOfError = 2;
        public const int CodeOfManualStart = 3;

        public Report()
        {
            Code = CodeOfError;
            Messages.Add("未执行处理");
        }
        public Report(int code = CodeOfError,params string[] messages)
        {
            Code = code;
            Messages.AddRange(messages);
        }

        [DataMember]
        public int Code { set; get; }
        [DataMember]
        public List<string> Messages { set; get; } = new List<string>();
        public string Locator { set; get; }
    }
    [DataContract]
    public class Report<T> : Report
    {
        public Report():base()
        {
        }
        public Report(T data, int code= CodeOfError, params string[] messages) : base(code, messages)
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
        public Report(T1 data1, T2 data2, int code = CodeOfError, params string[] messages) : base(code, messages)
        {
            Data1 = data1;
            Data2 = data2;
        }

        [DataMember]
        public T1 Data1 { set; get; } = default(T1);
        [DataMember]
        public T2 Data2 { set; get; } = default(T2);
    }
}
