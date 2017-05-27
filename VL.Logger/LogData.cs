using System;
using System.Text;

namespace VL.Logger
{
    public class LogData
    {
        public LogData(string message)
        {
            Message = message;
        }
        public LogData(string className, string fuctionName, string message)
        {
            ClassName = className;
            FuctionName = fuctionName;
            Message = message;
        }

        public LogData(string className, string fuctionName, string segregation, string message)
        {
            ClassName = className;
            FuctionName = fuctionName;
            Segregation = segregation;
            Message = message;
        }

        public string ClassName { set; get; }
        public string FuctionName { set; get; }
        public string Segregation { set; get; }
        public string Message { set; get; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("----------------------------");
            sb.AppendLine(string.Format("-------ClassName  :{0}", ClassName));
            sb.AppendLine(string.Format("-------FuctionName:{0}", FuctionName));
            sb.AppendLine(string.Format("-------Segregation:{0}", Segregation));
            sb.AppendLine(string.Format("-------LogTime    :{0}", DateTime.Now));
            sb.AppendLine(string.Format("-------Message    :{0}", Message));
            return sb.ToString();
        }
    }
}
