using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VL.Common.DAS;

namespace VL.Common.ORM//.Utilities.Interfaces
{

    /// <summary>
    /// Query元素
    /// 定义了query的字符串转换
    /// 定义了query的参数构建
    /// </summary>
    public interface IQueriable
    {
        /// <summary>
        /// 转Query
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        string ToQueryString(DbSession session);
    }
}
