using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VL.Common.ORM.Objects
{
    /// <summary>
    /// 由pdm解析的列字段
    /// </summary>
    public class PDMDbProperty
    {
        #region Properties
        public string Title { get; set; }
        public string Code { get; set; }
        public string Comment { get; set; }
        public bool IsPrimaryKey { get; set; }
        public PDMDataType Type { get; set; }
        public int Length { get; set; }
        public int Precision { get; set; }
        public bool Nullable { get; set; }
        public string DefaultValue { get; set; }
        #endregion

        /// <summary>
        /// PDM模型属性
        /// </summary>
        /// <param name="title"></param>
        /// <param name="code"></param>
        /// <param name="comment"></param>
        /// <param name="isPrimaryKey"></param>
        /// <param name="type"></param>
        /// <param name="length"></param>
        /// <param name="precision"></param>
        /// <param name="nullable"></param>
        /// <param name="defaultValue"></param>
        public PDMDbProperty(string title, string code, string comment
            , bool isPrimaryKey, PDMDataType type, int length, int precision, bool nullable, string defaultValue = null)
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
            this.DefaultValue = defaultValue;
        }

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
    }
}
