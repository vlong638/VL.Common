using System;
using System.Collections.Generic;
using System.Linq;
using VL.Common.DAS.Objects;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public class ComponentOfInsert : IComponentBuilder, IQueriable
    {
        public ComponentOfInsert(IQueryBuilder queryBuilder) : base(queryBuilder)
        {
            Values = new List<ComponentValueOfInsert>();
        }

        public List<ComponentValueOfInsert> Values { set; get; }

        public string ToQueryString(DbSession session)
        {
            return string.Format("({0}) values({1})", string.Join(",", Values.Select(c => c.Property.Title)), string.Join(",", Values.Select(c => c.GetParameterName(session))));
        }
    }
}
