using System;
using VL.Common.Core.ORM;

namespace VL.Common.Object.VL.Blog
{
    public class TBlogTagMapperProperties
    {
        #region Properties
        public static PDMDbProperty<Guid> TagId { get; set; } = new PDMDbProperty<Guid>(nameof(TagId), "TagId", "Id", true, PDMDataType.uniqueidentifier, 0, 0, true);
        public static PDMDbProperty<Guid> BlogId { get; set; } = new PDMDbProperty<Guid>(nameof(BlogId), "BlogId", "Id", true, PDMDataType.uniqueidentifier, 0, 0, true);
        #endregion
    }
}
