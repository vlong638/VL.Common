using VL.Common.Object.ORM;

namespace VL.Common.Object.VL.Blog
{
    public class TBlogTagMapperProperties
    {
        #region Properties
        public static PDMDbProperty TagId { get; set; } = new PDMDbProperty(nameof(TagId), "TagId", "Id", true, PDMDataType.uniqueidentifier, 0, 0, true, null);
        public static PDMDbProperty BlogId { get; set; } = new PDMDbProperty(nameof(BlogId), "BlogId", "Id", true, PDMDataType.uniqueidentifier, 0, 0, true, null);
        #endregion
    }
}
