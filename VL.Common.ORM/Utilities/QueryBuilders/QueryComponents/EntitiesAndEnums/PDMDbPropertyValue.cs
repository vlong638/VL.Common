using VL.ORM.DbOperateLib.Objects;

namespace VL.Common.ORM.Utilities.QueryBuilders
{
    /// <summary>
    /// 属性,操作,值
    /// </summary>
    public class PDMDbPropertyValue
    {
        public PDMDbProperty Property { set; get; }
        public object Value { set; get; }

        public PDMDbPropertyValue(PDMDbProperty property,  object value)
        {
            this.Property = property;
            this.Value = value;
        }
    }
}
