using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VL.Common.ORM.Utilities.Interfaces
{
    /// <summary>
    /// Component元素
    /// 定义了简单上下级关联
    /// </summary>
    public abstract class IComponentBuilder
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="queryBuilder"></param>
        public IComponentBuilder(IQueryBuilder queryBuilder)
        {
            Parent = queryBuilder;
        }

        /// <summary>
        /// 继承结构
        /// </summary>
        public IQueryBuilder Parent { set; get; }
    }
}
