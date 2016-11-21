using System;
using VL.Common.Object.ORM;

namespace VL.Common.Object.VL.Blog
{
    public class TBlogProperties
    {
        #region Properties
        public static PDMDbProperty<String> UserName { get; set; } = new PDMDbProperty<String>(nameof(UserName), "UserName", "用户名", false, PDMDataType.nvarchar, 20, 0, true);
        public static PDMDbProperty<Guid> BlogId { get; set; } = new PDMDbProperty<Guid>(nameof(BlogId), "BlogId", "Id", true, PDMDataType.uniqueidentifier, 0, 0, true);
        public static PDMDbProperty<String> Title { get; set; } = new PDMDbProperty<String>(nameof(Title), "Title", "名称", false, PDMDataType.nvarchar, 50, 0, true);
        public static PDMDbProperty<String> BreviaryContent { get; set; } = new PDMDbProperty<String>(nameof(BreviaryContent), "BreviaryContent", "缩略内容", false, PDMDataType.nvarchar, 100, 0, true);
        public static PDMDbProperty<DateTime> CreatedTime { get; set; } = new PDMDbProperty<DateTime>(nameof(CreatedTime), "CreatedTime", "创建时间", false, PDMDataType.datetime, 0, 0, true);
        public static PDMDbProperty<DateTime> LastEditTime { get; set; } = new PDMDbProperty<DateTime>(nameof(LastEditTime), "LastEditTime", "最后更新时间", false, PDMDataType.datetime, 0, 0, true);
        public static PDMDbProperty<Boolean> IsVisible { get; set; } = new PDMDbProperty<Boolean>(nameof(IsVisible), "IsVisible", "是否可见", false, PDMDataType.numeric, 1, 0, true, true);
        #endregion
    }
}
