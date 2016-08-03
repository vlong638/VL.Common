using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VL.Common.ORM.Objects;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// 字段
    /// </summary>
    public class ComponentValueOfSelects : List<ComponentValueOfSelect>
    {
        public void Add(PDMDbProperty property)
        {
            this.Add(new ComponentValueOfSelect(property.Title));
        }
        public void AddRange(List<PDMDbProperty> properties)
        {
            foreach (var property in properties)
            {
                this.Add(new ComponentValueOfSelect(property.Title));
            }
        }
    }
    /// <summary>
    /// 字段
    /// </summary>
    public class ComponentValueOfSelect
    {
        public string FieldName { set; get; }
        public string Alias { set; get; }

        public ComponentValueOfSelect(string fieldName, string alias = null)
        {
            this.FieldName = fieldName;
            this.Alias = Alias;
        }
    }
}
