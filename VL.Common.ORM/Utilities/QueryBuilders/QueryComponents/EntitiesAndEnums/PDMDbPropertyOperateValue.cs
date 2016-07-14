using System;
using System.Collections.Generic;
using System.Linq;
using VL.Common.DAS.Objects;
using VL.Common.ORM.Objects;
using VL.Common.ORM.Utilities.Interfaces;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public class PDMDbPropertyOperateValue : IParameterizable
    {
        public SelectBuilder SubSelect { set; get; }
        public bool IsMultipleProperties { set; get; }
        public PDMDbProperty Property { set; get; }
        public List<PDMDbProperty> Properties { set; get; }
        public OperatorType Operator { set; get; }
        public object Value { set; get; }

        public PDMDbPropertyOperateValue(PDMDbProperty property, OperatorType operatorType, SelectBuilder subSelect, string nickName = null)
        {
            this.Property = property;
            this.IsMultipleProperties = false;
            this.Operator = operatorType;
            this.SubSelect = subSelect;
            this.NickName = nickName;
        }
        public PDMDbPropertyOperateValue(PDMDbProperty property, OperatorType operatorType, object value, string nickName = null)
        {
            this.Property = property;
            this.IsMultipleProperties = false;
            this.Operator = operatorType;
            this.Value = value;
            this.NickName = nickName;
        }
        public PDMDbPropertyOperateValue(List<PDMDbProperty> Properties, OperatorType operatorType, object value, string nickName = null)
        {
            this.Properties = Properties;
            this.IsMultipleProperties = true;
            this.Operator = operatorType;
            this.Value = value;
            this.NickName = nickName;
        }

        public override string GetParameterName(DbSession session)
        {
            if (IsMultipleProperties)
            {
                return session.GetParameterPrefix() + (NickName ?? string.Join("", Properties.Select(c => c.Title)));
            }
            else
            {
                return session.GetParameterPrefix() + (NickName ?? Property.Title);
            }
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
