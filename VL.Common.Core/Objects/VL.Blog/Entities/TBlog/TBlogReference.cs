using System;
using System.Collections.Generic;
using VL.Common.Core.ORM;

namespace VL.Common.Core.Object.VL.Blog
{
    public partial class TBlog : IPDMTBase
    {
        public List<TBlogTagMapper> BlogTagMappers { get; set; }
    }
}
