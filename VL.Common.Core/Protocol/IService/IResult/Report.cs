using System.Collections.Generic;
using System.Runtime.Serialization;
using VL.Common.Core.Protocol;

namespace VL.Common.Core.Protocol//.IService.IResult
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
}
