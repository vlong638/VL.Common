namespace VL.Common.Object.ORM//.Utilities.QueryBuilders
{
    /// <summary>
    /// Order只能由内部设置进行操作,不能通过外部传递参数
    /// 内部直接拼接字符串
    /// </summary>
    public class ComponentValueOfOrder
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="orderType"></param>
        public ComponentValueOfOrder(string fieldName, OrderType orderType = OrderType.asc)
        {
            OrderType = orderType;
            FieldName = fieldName;
        }

        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { private set; get; }
        /// <summary>
        /// 排序方式
        /// </summary>
        public OrderType OrderType { private set; get; }
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
