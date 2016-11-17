using VL.Common.Object.ORM;

namespace VL.Common.Object.VL.Blog
{
    public class TBlogProperties
    {
        #region Properties
        public static PDMDbProperty UserName { get; set; } = new PDMDbProperty(nameof(UserName), "UserName", "用户名", false, PDMDataType.nvarchar, 20, 0, true, null);
        public static PDMDbProperty BlogId { get; set; } = new PDMDbProperty(nameof(BlogId), "BlogId", "Id", true, PDMDataType.uniqueidentifier, 0, 0, true, null);
        public static PDMDbProperty Title { get; set; } = new PDMDbProperty(nameof(Title), "Title", "名称", false, PDMDataType.nvarchar, 50, 0, true, null);
        public static PDMDbProperty BreviaryContent { get; set; } = new PDMDbProperty(nameof(BreviaryContent), "BreviaryContent", "缩略内容", false, PDMDataType.nvarchar, 100, 0, true, null);
        public static PDMDbProperty CreatedTime { get; set; } = new PDMDbProperty(nameof(CreatedTime), "CreatedTime", "创建时间", false, PDMDataType.datetime, 0, 0, true, null);
        public static PDMDbProperty LastEditTime { get; set; } = new PDMDbProperty(nameof(LastEditTime), "LastEditTime", "最后更新时间", false, PDMDataType.datetime, 0, 0, true, null);
        #endregion
    }
}
