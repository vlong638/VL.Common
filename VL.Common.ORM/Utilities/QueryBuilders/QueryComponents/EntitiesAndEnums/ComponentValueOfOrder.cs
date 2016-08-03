using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// Order只能由内部设置进行操作,不能通过外部传递参数
    /// 内部直接拼接字符串
    /// </summary>
    public class ComponentValueOfOrder
    {
        public ComponentValueOfOrder()
        {
            OrderType = OrderType.asc;
        }
        public string FieldName { set; get; }
        public OrderType OrderType { set; get; }
    }
    /// <summary>
    /// 排序方式
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// 正序
        /// </summary>
        asc,
        /// <summary>
        /// 倒序
        /// </summary>
        desc
    }
}
