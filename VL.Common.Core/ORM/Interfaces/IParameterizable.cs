using System.Data.Common;
using VL.Common.Core.DAS;

namespace VL.Common.Core.ORM//.Utilities.Interfaces
{
    /// <summary>
    /// 可参数化
    /// </summary>
    public interface IParameterizable
    {
        /// <summary>
        /// 辅助名称适用于参数重复
        /// </summary>
        string NickName { set; get; }
        /// <summary>
        /// 获取参数名
        /// </summary>
        /// <returns></returns>
        string GetParameterName(DbSession session);
        /// <summary>
        /// 基于(属性,值)添加参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="session"></param>
        void AddParameter(DbCommand command, DbSession session);
    }
}
