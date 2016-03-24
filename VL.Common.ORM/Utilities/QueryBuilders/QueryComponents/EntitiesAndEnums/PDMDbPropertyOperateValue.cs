using System.Collections.Generic;
using VL.Common.ORM.Objects;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public class PDMDbPropertyOperateValue
    {
        public SelectBuilder SubSelect { set; get; }
        public bool IsMultipleProperties { set; get; }
        public PDMDbProperty Property { set; get; }
        public List<PDMDbProperty> Properties { set; get; }
        public OperatorType Operator { set; get; }
        public object Value { set; get; }

        public PDMDbPropertyOperateValue(PDMDbProperty property, OperatorType operatorType, SelectBuilder subSelect)
        {
            this.Property = property;
            this.IsMultipleProperties = false;
            this.Operator = operatorType;
            this.SubSelect = subSelect;
        }
        public PDMDbPropertyOperateValue(PDMDbProperty property, OperatorType operatorType, object value)
        {
            this.Property = property;
            this.IsMultipleProperties = false;
            this.Operator = operatorType;
            this.Value = value;
        }
        public PDMDbPropertyOperateValue(List<PDMDbProperty> Properties, OperatorType operatorType, object value)
        {
            this.Properties = Properties;
            this.IsMultipleProperties = true;
            this.Operator = operatorType;
            this.Value = value;
        }
    }
    /// <summary>
    /// 操作符类别
    /// </summary>
    public enum OperatorType
    {
        Equal,
        NotEqual,
        In,
        NotIn,
    }
}
