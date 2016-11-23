namespace VL.Common.Core.ORM//.Utilities.QueryBuilders
{
    /// <summary>
    /// 字段
    /// </summary>
    public class ComponentValueOfSelect
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { set; get; }
        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { set; get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="alias"></param>
        public ComponentValueOfSelect(string fieldName, string alias = null)
        {
            this.FieldName = fieldName;
            this.Alias = Alias;
        }
    }
}
