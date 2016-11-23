using System;
using VL.Common.Core.ORM;

namespace VL.Common.Object.VL.Blog
{
    public class TBlogDetailProperties
    {
        #region Properties
        public static PDMDbProperty<Guid> BlogId { get; set; } = new PDMDbProperty<Guid>(nameof(BlogId), "BlogId", "Id", true, PDMDataType.uniqueidentifier, 0, 0, true);
        public static PDMDbProperty<String> Content { get; set; } = new PDMDbProperty<String>(nameof(Content), "Content", "内容", false, PDMDataType.nvarchar, 0, 0, true);
        #endregion
    }
}
