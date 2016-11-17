using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VL.Common.Object.Protocol
{
    [DataContract]
    public partial class Report
    {
        public Report()
        {
            Code = CProtocol.CReport.CError;
        }
        public Report(int code = CProtocol.CReport.CError, params string[] messages)
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
        public Report() : base()
        {
        }
        public Report(int code = CProtocol.CReport.CError, params string[] messages) : base(code, messages)
        {
        }
        public Report(T data, int code = CProtocol.CReport.CError, params string[] messages) : base(code, messages)
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
        public Report(int code = CProtocol.CReport.CError, params string[] messages) : base(code, messages)
        {
        }
        public Report(T1 data1, T2 data2, int code = CProtocol.CReport.CError, params string[] messages) : base(code, messages)
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
