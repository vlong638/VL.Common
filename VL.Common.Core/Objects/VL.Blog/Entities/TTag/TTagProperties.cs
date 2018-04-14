using System;
using VL.Common.Core.ORM;

namespace VL.Common.Core.Object.VL.Blog
{
    public class TTagProperties
    {
        #region Properties
        public static PDMDbProperty<String> UserName { get; set; } = new PDMDbProperty<String>(nameof(UserName), "UserName", "用户名", true, PDMDataType.nvarchar, 20, 0, true, false);
        public static PDMDbProperty<String> TagName { get; set; } = new PDMDbProperty<String>(nameof(TagName), "TagName", "标签名", true, PDMDataType.nvarchar, 20, 0, true, false);
        #endregion
    }
}
