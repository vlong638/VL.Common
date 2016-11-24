using System;
using System.Collections.Generic;
using VL.Common.Core.ORM;

namespace VL.Common.Object.VL.Blog
{
    public partial class TBlog : IPDMTBase
    {
        public List<TTag> Tags { get; set; }
    }
}
