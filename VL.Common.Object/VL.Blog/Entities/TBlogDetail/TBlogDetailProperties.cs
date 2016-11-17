using VL.Common.Object.ORM;

namespace VL.Common.Object.VL.Blog
{
    public class TBlogDetailProperties
    {
        #region Properties
        public static PDMDbProperty BlogId { get; set; } = new PDMDbProperty(nameof(BlogId), "BlogId", "Id", true, PDMDataType.uniqueidentifier, 0, 0, true, null);
        public static PDMDbProperty Content { get; set; } = new PDMDbProperty(nameof(Content), "Content", "内容", false, PDMDataType.nvarchar, 0, 0, true, null);
        #endregion
    }
}
