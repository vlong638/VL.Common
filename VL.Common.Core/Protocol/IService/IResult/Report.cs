using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using VL.Common.Constraints;

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

        public Report GetReport(string methodName, int operationType, params string[] messages)
        {
            return new Report(operationType, messages) { Locator = GetLocator(methodName, operationType) };
        }
        //public Report<T> GetReport<T>(string methodName, int operationType, params string[] messages)
        //{
        //    return new Report<T>(operationType, messages) { Locator = GetLocator(methodName, operationType) };
        //}
        public Report<T> GetReport<T>(T data, string methodName, int operationType, params string[] messages)
        {
            return new Report<T>(data, operationType, messages) { Locator = GetLocator(methodName, operationType) };
        }
        //public Report<T1, T2> GetReport<T1, T2>(string methodName, int operationType, params string[] messages)
        //{
        //    return new Report<T1, T2>(operationType, messages) { Locator = GetLocator(methodName, operationType) };
        //}
        public Report<T1, T2> GetReport<T1, T2>(T1 data1, T2 data2, string methodName, int operationType, params string[] messages)
        {
            return new Report<T1, T2>(data1, data2, operationType, messages) { Locator = GetLocator(methodName, operationType) };
        }
        private string GetLocator(string methodName, int operationType)
        {
            return ModuleName + "_" + ClassName + "_" + methodName + "_" + operationType;
        }
    }
    [DataContract]
    public class Report
    {
        public Report()
        {
            Code = ProtocolConstraits.CodeOfError;
        }
        public Report(int code = ProtocolConstraits.CodeOfError,params string[] messages)
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
        public Report(int code = ProtocolConstraits.CodeOfError, params string[] messages) : base(code, messages)
        {
        }
        public Report(T data, int code= ProtocolConstraits.CodeOfError, params string[] messages) : base(code, messages)
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
        public Report(int code = ProtocolConstraits.CodeOfError, params string[] messages) : base(code, messages)
        {
        }
        public Report(T1 data1, T2 data2, int code = ProtocolConstraits.CodeOfError, params string[] messages) : base(code, messages)
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
