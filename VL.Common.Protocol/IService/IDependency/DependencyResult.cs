using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace VL.Common.Protocol.IService
{
    /// <summary>
    /// 依赖检测结果 单元
    /// </summary>
    [DataContract]
    public class DependencyResult
    {
        /// <summary>
        /// 依赖单元的自身元素检测
        /// </summary>
        [DataMember]
        public List<DependencyDetail> DependencyDetails { set; get; } = new List<DependencyDetail>();
        /// <summary>
        /// 相关的依赖单元的检测
        /// </summary>
        [DataMember]
        public List<DependencyResult> DependencyResults { set; get; } = new List<DependencyResult>();

        /// <summary>
        /// 单元名称
        /// </summary>
        [DataMember]
        public string UnitName { set; get; }
        /// <summary>
        /// 单元检测结果描述
        /// </summary>
        [DataMember]
        public string Message
        {
            get
            {
                return "单元<" + UnitName + ">" + (IsAllDependenciesAvailable ? "依赖检测通过" : "依赖检测未通过,错误详情:" + System.Environment.NewLine
                    + DependencyDetails.Where(c => c.IsDependencyAvailable == false).Select(c => c.Message + System.Environment.NewLine)
                    + DependencyResults.Where(c => c.IsAllDependenciesAvailable == false).Select(c => c.Message + System.Environment.NewLine));
            }
        }
        /// <summary>
        /// 单元检测结果
        /// </summary>
        [DataMember]
        public bool IsAllDependenciesAvailable
        {
            get
            {
                return DependencyDetails.Where(c => c.IsDependencyAvailable != true).Count() == 0
                    && DependencyResults.Where(c => c.IsAllDependenciesAvailable != true).Count() == 0;
            }
        }
    }
}
