using System;
using System.Collections.Generic;
using System.Data.Common;
using VL.Common.Core.DAS;

namespace VL.Common.Core.ORM//.Utilities.QueryBuilders
{
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public class ComponentValueOfSet : IParameterizable, IQueriable
    {
        /// <summary>
        /// 目标属性
        /// </summary>
        public PDMDbProperty Property { set; get; }
        /// <summary>
        /// 值
        /// </summary>
        public object Value { set; get; }
        /// <summary>
        /// 操作符
        /// </summary>
        public UpdateType Operator { set; get; }
        /// <summary>
        /// 辅助名称适用于参数重复
        /// </summary>
        public string NickName { set; get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <param name="nickName"></param>
        public ComponentValueOfSet(PDMDbProperty property, object value, string nickName = null) : this(property, value, UpdateType.SetValue, nickName)
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <param name="updateType"></param>
        /// <param name="nickName"></param>
        public ComponentValueOfSet(PDMDbProperty property, object value, UpdateType updateType, string nickName = null)
        {
            init(property, updateType, value, !string.IsNullOrEmpty(nickName) ? nickName : "Set" + property.Title);
        }

        private void init(PDMDbProperty property, UpdateType updateType, object value, string nickName)
        {
            this.Property = property;
            this.Operator = updateType;
            this.Value = value;
            this.NickName = nickName;
        }

        /// <summary>
        /// 获取参数名
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public string GetParameterName(DbSession session)
        {
            return session.GetParameterPrefix() + NickName;
        }
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="session"></param>
        public void AddParameter(DbCommand command, DbSession session)
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
        /// <summary>
        /// 转query
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public string ToQueryString(DbSession session)
        {
            switch (Operator)
            {
                case UpdateType.SetValue:
                    return Property.Title + Operator.ToQueryString() + GetParameterName(session);
                case UpdateType.IncreaseByValue:
                    return Property.Title + UpdateType.SetValue.ToQueryString() + Property.Title + GetParameterName(session);
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
                pdmDbPropertyUpdateValue.AddParameter(command, session);
            }
        }
    }
    /// <summary>
    /// 操作符类别
    /// </summary>
    public enum UpdateType
    {
        /// <summary>
        /// 设值
        /// </summary>
        SetValue,
        /// <summary>
        /// 增长
        /// </summary>
        IncreaseByValue,
    }
}
