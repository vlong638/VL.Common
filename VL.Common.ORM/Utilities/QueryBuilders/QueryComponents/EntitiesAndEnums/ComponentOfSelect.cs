using System;
using System.Collections.Generic;
using System.Linq;
using VL.Common.DAS.Objects;
using VL.Common.ORM.Objects;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public class ComponentOfSelect : IComponentBuilder, IQueriable
    {
        public ComponentOfSelect(IQueryBuilder queryBuilder) : base(queryBuilder)
        {
            Values = new ComponentValueOfSelects();
        }

        public ComponentValueOfSelects Values { set; get; }

        public string ToQueryString(DbSession session)
        {
            if (Values.Count == 0)
            {
                return "select *";
            }
            else
            {
                return "select " + string.Join(",", Values.Select(c => string.IsNullOrEmpty(c.Alias) ? c.FieldName : c.Alias));
            }
        }
    }
}
