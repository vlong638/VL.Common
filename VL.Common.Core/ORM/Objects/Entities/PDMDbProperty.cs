//using System;

//namespace VL.Common.ORM
//{
//    /// <summary>
//    /// 由pdm解析的列字段
//    /// </summary>
//    public class PDMDbProperty : ComponentValueOfSelect
//    {
//        #region Properties
//        /// <summary>
//        /// 字段名称
//        /// </summary>
//        public string Title { get; set; }
//        /// <summary>
//        /// 字段编码
//        /// </summary>
//        public string Code { get; set; }
//        /// <summary>
//        /// 备注
//        /// </summary>
//        public string Comment { get; set; }
//        /// <summary>
//        /// 是否是主键
//        /// </summary>
//        public bool IsPrimaryKey { get; set; }
//        /// <summary>
//        /// pdm字段类型
//        /// </summary>
//        public PDMDataType Type { get; set; }
//        /// <summary>
//        /// 字段长度
//        /// </summary>
//        public int Length { get; set; }
//        /// <summary>
//        /// 字段精度
//        /// </summary>
//        public int Precision { get; set; }
//        /// <summary>
//        /// 是否允许空值
//        /// </summary>
//        public bool Nullable { get; set; }
//        /// <summary>
//        /// 默认值
//        /// </summary>
//        public string DefaultValue { get; set; }
//        #endregion

//        /// <summary>
//        /// PDM模型属性
//        /// </summary>
//        /// <param name="title"></param>
//        /// <param name="code"></param>
//        /// <param name="comment"></param>
//        /// <param name="isPrimaryKey"></param>
//        /// <param name="type"></param>
//        /// <param name="length"></param>
//        /// <param name="precision"></param>
//        /// <param name="nullable"></param>
//        /// <param name="defaultValue"></param>
//        public PDMDbProperty(string title, string code, string comment
//            , bool isPrimaryKey, PDMDataType type, int length, int precision, bool nullable, string defaultValue = null) : base(title, null)
//        {
//            this.Title = title;
//            this.Code = code;
//            this.Comment = comment;
//            this.IsPrimaryKey = isPrimaryKey;
//            //this.Type = type;
//            this.Type = type;
//            this.Length = length;
//            this.Precision = precision;
//            this.Nullable = nullable;
//            this.DefaultValue = defaultValue;
//        }

//        /// <summary>
//        /// CSharp数据类型
//        /// </summary>
//        /// <returns></returns>
//        public CSharpDataType GetCSharpDataType()
//        {
//            switch (Type)
//            {
//                case PDMDataType.varchar:
//                    return CSharpDataType.@string;
//                case PDMDataType.numeric:
//                    if (Precision > 0)
//                    {
//                        return CSharpDataType.Decimal;
//                    }
//                    if (Length > 32 || Length == 0)
//                    {
//                        return CSharpDataType.Int64;
//                    }
//                    else if (Length > 16)
//                    {
//                        return CSharpDataType.Int32;
//                    }
//                    else if (Length > 1)
//                    {
//                        return CSharpDataType.Int16;
//                    }
//                    else
//                    {
//                        return CSharpDataType.Boolean;
//                    }
//                case PDMDataType.datetime:
//                    return CSharpDataType.DateTime;
//                case PDMDataType.uniqueidentifier:
//                    return CSharpDataType.Guid;
//                default:
//                    throw new NotImplementedException("该PDM字段类型未设置对应的C#类型");
//            }
//        }
//    }
//}
