﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using VL.Common.Object.DAS;

namespace VL.Common.Object.ORM
{
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public class ComponentValueOfInsert : IParameterizable
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
        /// 辅助名称适用于参数重复
        /// </summary>
        public string NickName { set; get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <param name="nickName"></param>
        public ComponentValueOfInsert(PDMDbProperty property, object value, string nickName = null)
        {
            this.Property = property;
            this.Value = value;
            this.NickName = !string.IsNullOrEmpty(nickName) ? nickName : property.Title;
        }

        public string GetParameterName(DbSession session)
        {
            return session.GetParameterPrefix() + (!string.IsNullOrEmpty(this.NickName) ? this.NickName : this.Property.Title);
        }
        public void AddParameter(DbCommand command, DbSession session)
        {
            command.Parameters.Add(this.Property.GetDbParameter(session, this.Value, this.NickName));
        }
    }
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public static class ComponentValueOfInsertEx
    {
        /// <summary>
        /// 基于(属性,值)的集合添加参数
        /// </summary>
        public static void AddParameter(this List<ComponentValueOfInsert> pdmDbPropertyValues, DbSession session, DbCommand command)
        {
            foreach (var pdmDbPropertyValue in pdmDbPropertyValues)
            {
                pdmDbPropertyValue.AddParameter(command, session);
            }
        }
    }
}
