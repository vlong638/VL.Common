using System;
using System.Collections.Generic;
using System.Linq;
using VL.Common.DAS.Objects;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public class ComponentOfSet : IComponentBuilder, IQueriable
    {
        public ComponentOfSet(IQueryBuilder queryBuilder) : base(queryBuilder)
        {
            Values = new List<ComponentValueOfSet>();
        }

        public List<ComponentValueOfSet> Values { set; get; }

        public string ToQueryString(DbSession session)
        {
            return " set " + string.Join(",", Values.Select(c => c.ToQueryString(session)));
        }
    }
}
