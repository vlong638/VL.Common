﻿using System;

namespace VL.Common.Object.ORM
{
    /// <summary>
    /// 由pdm解析的列字段
    /// </summary>
    public class PDMDbProperty
    {
        #region Properties
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 字段编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 是否是主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }
        /// <summary>
        /// pdm字段类型
        /// </summary>
        public PDMDataType Type { get; set; }
        /// <summary>
        /// 字段长度
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// 字段精度
        /// </summary>
        public int Precision { get; set; }
        /// <summary>
        /// 是否允许空值
        /// </summary>
        public bool Nullable { get; set; }
        #endregion

        public PDMDbProperty(string title, string code, string comment
            , bool isPrimaryKey, PDMDataType type, int length, int precision, bool nullable)
        {
            this.Title = title;
            this.Code = code;
            this.Comment = comment;
            this.IsPrimaryKey = isPrimaryKey;
            //this.Type = type;
            this.Type = type;
            this.Length = length;
            this.Precision = precision;
            this.Nullable = nullable;
        }

        /// <summary>
        /// CSharp数据类型
        /// </summary>
        /// <returns></returns>
        public CSharpDataType GetCSharpDataType()
        {
            switch (Type)
            {
                case PDMDataType.varchar:
                    return CSharpDataType.@string;
                case PDMDataType.numeric:
                    if (Precision > 0)
                    {
                        return CSharpDataType.Decimal;
                    }
                    if (Length > 32 || Length == 0)
                    {
                        return CSharpDataType.Int64;
                    }
                    else if (Length > 16)
                    {
                        return CSharpDataType.Int32;
                    }
                    else if (Length > 1)
                    {
                        return CSharpDataType.Int16;
                    }
                    else
                    {
                        return CSharpDataType.Boolean;
                    }
                case PDMDataType.datetime:
                    return CSharpDataType.DateTime;
                case PDMDataType.uniqueidentifier:
                    return CSharpDataType.Guid;
                default:
                    throw new NotImplementedException("该PDM字段类型未设置对应的C#类型");
            }
        }

        public static ComponentValueOfWhere operator == (PDMDbProperty property, object value)
        {
            return new ComponentValueOfWhere(property, value, LocateType.Equal);
        }
        public static ComponentValueOfWhere operator !=(PDMDbProperty property, object value)
        {
            return new ComponentValueOfWhere(property, value, LocateType.NotEqual);
        }
    }
    /// <summary>
    /// 由pdm解析的列字段
    /// </summary>
    public class PDMDbProperty<T>: PDMDbProperty
    {
        #region Properties
        /// 默认值
        /// </summary>
        public T DefaultValue { get; set; }
        #endregion

        public PDMDbProperty(string title, string code, string comment, bool isPrimaryKey, 
            PDMDataType type, int length, int precision, bool nullable, T defaultValue = default(T))
            : base(title, code, comment, isPrimaryKey, type, length, precision, nullable)
        {
            this.DefaultValue = defaultValue;
        }
    }
}
