using System;
using VL.Common.Core.ORM;

namespace VL.Common.Core.Object.VL.Blog
{
    public class TBlogTagMapperProperties
    {
        #region Properties
        public static PDMDbProperty<Guid> BlogId { get; set; } = new PDMDbProperty<Guid>(nameof(BlogId), "BlogId", "Id", true, PDMDataType.uniqueidentifier, 0, 0, true);
        public static PDMDbProperty<String> TagName { get; set; } = new PDMDbProperty<String>(nameof(TagName), "TagName", "标签名", true, PDMDataType.nvarchar, 20, 0, true);
        #endregion
    }
}
