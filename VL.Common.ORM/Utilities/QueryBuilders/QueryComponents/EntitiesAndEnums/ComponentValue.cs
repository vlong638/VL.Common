using System.Collections.Generic;
using System.Linq;
using VL.Common.DAS.Objects;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public class ComponentValue : ISubQueryBuilder
    {
        public ComponentValue(IQueryBuilder queryBuilder) : base(queryBuilder)
        {
            Values = new List<PDMDbPropertyValue>();
        }

        public List<PDMDbPropertyValue> Values { set; get; } 


        public string ToQueryComponentOfFields()
        {
            return string.Join(",", Values.Select(c => c.Property.Title));
        }
        public string ToQueryComponentOfValues(DbSession session)
        {
            return string.Join(",", Values.Select(c => session.GetParameterPrefix() + c.Property.Title));
        }
        public string ToQueryComponentOfSets(DbSession session)
        {
            return string.Join(",", Values.Select(c => c.Property.Title + OperatorType.Equal.ToQueryString() + session.GetParameterPrefix() + c.Property.Title));
        }
    }
}
