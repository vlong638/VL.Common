//暂无良好的想法 搁置
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VL.Common.Object.ORM.CodeGenerateLib.Samples.Utilities
{
    public interface IQueryFetcher<T>
    {
        T Fetch();
    }
}
