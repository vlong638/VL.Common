using System;
using System.Collections.Generic;
using System.Linq;
using VL.Common.DAS.Objects;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public class ComponentOfOrder : IComponentBuilder, IQueriable
    {
        public ComponentOfOrder(IQueryBuilder queryBuilder) : base(queryBuilder)
        {
            Orders = new List<ComponentValueOfOrder>();
        }

        public List<ComponentValueOfOrder> Orders { set; get; }

        public string ToQueryString(DbSession session)
        {
            return " order by " + string.Join(",", Orders.Select(c => c.FieldName + " " + c.OrderType.ToString()));
        }
    }
}
