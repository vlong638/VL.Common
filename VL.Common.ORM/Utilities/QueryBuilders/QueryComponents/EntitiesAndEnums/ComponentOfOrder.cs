using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using VL.Common.DAS.Objects;
using VL.Common.ORM.Utilities.Interfaces;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public class ComponentOfOrder : IComponentBuilder, IQueriable
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="queryBuilder"></param>
        public ComponentOfOrder(IQueryBuilder queryBuilder) : base(queryBuilder)
        {
            Orders = new List<ComponentValueOfOrder>();
        }

        List<ComponentValueOfOrder> Orders { set; get; }
        /// <summary>
        /// 新增排序项
        /// </summary>
        /// <param name="order"></param>
        public void Add(ComponentValueOfOrder order)
        {
            Orders.Add(order);
        }
        /// <summary>
        /// 新增排序项
        /// </summary>
        public void Add(string fieldName, OrderType orderType = OrderType.asc)
        {
            Orders.Add(new ComponentValueOfOrder(fieldName, orderType));
        }
        /// <summary>
        /// 添加参数,Order不需要参数传值
        /// </summary>
        /// <param name="command"></param>
        /// <param name="session"></param>
        public void AddParameter(DbCommand command, DbSession session)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 转换为Query
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public string ToQueryString(DbSession session)
        {
            if (Orders.Count()==0)
            {
                return "";
            }

            return " order by " + string.Join(",", Orders.Select(c => c.FieldName + " " + c.OrderType.ToString()));
        }
    }
}
