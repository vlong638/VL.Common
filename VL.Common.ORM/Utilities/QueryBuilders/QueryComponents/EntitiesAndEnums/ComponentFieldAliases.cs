using System.Collections.Generic;
using System.Linq;
using VL.Common.ORM.Objects;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public class ComponentFieldAliases : ISubQueryBuilder
    {
        public ComponentFieldAliases(IQueryBuilder queryBuilder) : base(queryBuilder)
        {
            FieldAliases = new FieldAliases();
        }

        public FieldAliases FieldAliases { set; get; }

        public string ToQueryComponentOfFieldAliases()
        {
            if (FieldAliases.Count == 0)
            {
                return "*";
            }
            else
            {
                return string.Join(",", FieldAliases.Select(c => string.IsNullOrEmpty(c.Alias) ? c.FieldName : c.Alias));
            }
        }
    }
    /// <summary>
    /// 字段
    /// </summary>
    public class FieldAliases : List<FieldAlias>
    {
        public void Add(PDMDbProperty property)
        {
            this.Add(new FieldAlias(property.Title));
        }
        public void AddRange(List<PDMDbProperty> properties)
        {
            foreach (var property in properties)
            {
                this.Add(new FieldAlias(property.Title));
            }
        }
    }
    /// <summary>
    /// 字段
    /// </summary>
    public class FieldAlias
    {
        public string FieldName { set; get; }
        public string Alias { set; get; }

        public FieldAlias(string fieldName, string alias = null)
        {
            this.FieldName = fieldName;
            this.Alias = Alias;
        }
    }
}
