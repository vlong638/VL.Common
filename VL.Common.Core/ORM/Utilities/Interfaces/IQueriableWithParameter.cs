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
    public interface IQueriableWithParameter:IQueriable
    {
        /// <summary>
        /// 基于(属性,值)添加参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="session"></param>
        void AddParameter(DbCommand command, DbSession session);
    }
}
