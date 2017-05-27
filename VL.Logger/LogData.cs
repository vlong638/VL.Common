using System;
using System.Text;

namespace VL.Logger
{
    public class LogData
    {
        public string ClassName { set; get; }
        public string FuctionName { set; get; }
        public string Message { set; get; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("----------------------------");
            sb.AppendLine(string.Format("-------ClassName  :{0}", ClassName));
            sb.AppendLine(string.Format("-------FuctionName:{0}", FuctionName));
            sb.AppendLine(string.Format("-------LogTime    :{0}", DateTime.Now));
            sb.AppendLine(string.Format("-------Message    :{0}", Message));
            return sb.ToString();
        }
    }
}
