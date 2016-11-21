using System;
using VL.Common.Object.ORM;

namespace VL.Common.Object.VL.Blog
{
    public class TTagProperties
    {
        #region Properties
        public static PDMDbProperty<String> UserName { get; set; } = new PDMDbProperty<String>(nameof(UserName), "UserName", "用户名", false, PDMDataType.nvarchar, 20, 0, true);
        public static PDMDbProperty<Guid> TagId { get; set; } = new PDMDbProperty<Guid>(nameof(TagId), "TagId", "Id", true, PDMDataType.uniqueidentifier, 0, 0, true);
        public static PDMDbProperty<String> Name { get; set; } = new PDMDbProperty<String>(nameof(Name), "Name", "标签名", false, PDMDataType.nvarchar, 20, 0, true);
        #endregion
    }
}
