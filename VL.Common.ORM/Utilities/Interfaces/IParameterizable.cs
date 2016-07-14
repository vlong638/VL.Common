using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VL.Common.DAS.Objects;

namespace VL.Common.ORM.Utilities.Interfaces
{
    /// <summary>
    /// 可参数化
    /// </summary>
    public abstract class IParameterizable
    {
        /// <summary>
        /// 辅助名称适用于参数重复
        /// </summary>
        public string NickName { set; get; }
        /// <summary>
        /// 获取参数名
        /// </summary>
        /// <returns></returns>
        public abstract string GetParameterName(DbSession session);
    }
}
