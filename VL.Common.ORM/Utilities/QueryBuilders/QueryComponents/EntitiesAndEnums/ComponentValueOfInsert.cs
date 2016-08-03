using System;
using System.Collections.Generic;
using System.Data.Common;
using VL.Common.DAS.Objects;
using VL.Common.ORM.Objects;
using VL.Common.ORM.Utilities.Interfaces;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public class ComponentValueOfInsert : IParameterizable, IQueriable
    {
        public PDMDbProperty Property { set; get; }
        public object Value { set; get; }
        public string NickName { set; get; }

        public ComponentValueOfInsert(PDMDbProperty property, object value, string nickName = null)
        {
            this.Property = property;
            this.Value = value;
            this.NickName = !string.IsNullOrEmpty(nickName) ? nickName : property.Title;
        }

        public override string GetParameterName(DbSession session)
        {
            return session.GetParameterPrefix() + (!string.IsNullOrEmpty(NickName) ? NickName : Property.Title);
        }
        public override void AddParameter(DbSession session, DbCommand command)
        {
            command.Parameters.Add(this.Property.GetDbParameter(session, this.Value, this.NickName));
        }

        public string ToQueryString(DbSession session)
        {
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public static class PDMDbPropertyValueEx
    {
        /// <summary>
        /// 基于(属性,值)的集合添加参数
        /// </summary>
        public static void AddParameter(this List<ComponentValueOfInsert> pdmDbPropertyValues, DbSession session, DbCommand command)
        {
            foreach (var pdmDbPropertyValue in pdmDbPropertyValues)
            {
                pdmDbPropertyValue.AddParameter(session, command);
            }
        }
    }
}
