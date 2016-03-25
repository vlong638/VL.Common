using System.Collections.Generic;
using System.Linq;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public class ComponentOrder : ISubQueryBuilder
    {
        public ComponentOrder(IQueryBuilder queryBuilder) : base(queryBuilder)
        {
            Orders = new List<Order>();
        }

        public List<Order> Orders { set; get; }

        public string ToQueryComponentOfOrders()
        {
            return string.Join(",", Orders.Select(c => c.FieldName + " " + c.OrderType.ToString()));
        }
    }
    /// <summary>
    /// Order只能由内部设置进行操作,不能通过外部传递参数
    /// 内部直接拼接字符串
    /// </summary>
    public class Order
    {
        public Order()
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
