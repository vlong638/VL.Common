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
    public class ComponentValueOfSet : IParameterizable, IQueriable
    {
        public PDMDbProperty Property { set; get; }
        public object Value { set; get; }
        public string NickName { set; get; }
        public UpdateType Operator { set; get; }

        public ComponentValueOfSet(PDMDbProperty property, object value, string nickName = null)
        {
            init(property, UpdateType.SetValue, value, !string.IsNullOrEmpty(nickName) ? nickName : property.Title);
        }
        public ComponentValueOfSet(PDMDbProperty property, object value, UpdateType updateType, string nickName = null)
        {
            init(property, updateType, value, !string.IsNullOrEmpty(nickName) ? nickName : property.Title);
        }

        private void init(PDMDbProperty property, UpdateType updateType, object value, string nickName)
        {
            this.Property = property;
            this.Operator = updateType;
            this.Value = value;
            this.NickName = nickName;
        }

        //public SelectBuilder SubSelect { set; get; }
        //public PDMDbPropertyUpdateValue(PDMDbProperty property, UpdateType updateType, SelectBuilder select, string nickName = null)
        //{
        //    this.Property = property;
        //    this.Operator = updateType;
        //    this.SubSelect = select;
        //    this.NickName = nickName;
        //}

        public override string GetParameterName(DbSession session)
        {
            return session.GetParameterPrefix() + NickName;
        }
        public override void AddParameter(DbSession session, DbCommand command)
        {
            switch (Operator)
            {
                case UpdateType.SetValue:
                case UpdateType.IncreaseByValue:
                    command.Parameters.Add(Property.GetDbParameter(session, Value, NickName));
                    break;
                default:
                    throw new NotImplementedException("Error08030917-暂未支持该操作符的处理" + Operator.ToString());
            }
        }

        public string ToQueryString(DbSession session)
        {
            switch (Operator)
            {
                case UpdateType.SetValue:
                case UpdateType.IncreaseByValue:
                    return Property.Title + Operator.ToQueryString() + GetParameterName(session);
                default:
                    throw new NotImplementedException("Error08030915-暂未支持该操作符的处理" + Operator.ToString());
            }
        }
    }
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public static class PDMDbPropertyUpdateValueEx
    {
        /// <summary>
        /// 基于(属性,操作,值)的集合添加参数
        /// </summary>
        public static void AddParameter(this List<ComponentValueOfSet> pdmDbPropertyUpdateValues, DbSession session, DbCommand command)
        {
            //增加Parameter
            foreach (var pdmDbPropertyUpdateValue in pdmDbPropertyUpdateValues)
            {
                pdmDbPropertyUpdateValue.AddParameter(session, command);
            }
        }
    }
    /// <summary>
    /// 操作符类别
    /// </summary>
    public enum UpdateType
    {
        SetValue,
        IncreaseByValue,
    }
}
