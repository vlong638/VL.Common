using System;
using VL.Common.DAS.Objects;
using VL.Common.ORM.Objects;
using VL.Common.ORM.Utilities.Interfaces;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public class PDMDbPropertyValue : IParameterizable
    {
        public PDMDbProperty Property { set; get; }
        public object Value { set; get; }
        public string NickName { set; get; }

        public PDMDbPropertyValue(PDMDbProperty property, object value, string nickName = null)
        {
            this.Property = property;
            this.Value = value;
            this.NickName = nickName;
        }

        public override string GetParameterName(DbSession session)
        {
            return session.GetParameterPrefix() + (NickName ?? Property.Title);
        }
    }
}
