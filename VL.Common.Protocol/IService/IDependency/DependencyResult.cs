using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace VL.Common.Protocol.IService
{
    /// <summary>
    /// 依赖检测结果
    /// </summary>
    [DataContract]
    public class DependencyResult
    {
        [DataMember]
        public List<DependencyDetail> DependencyDetails { set; get; } = new List<DependencyDetail>();
        [DataMember]
        public bool IsAllDependenciesAvailable
        {
            get
            {
                return DependencyDetails.Where(c => c.IsDependencyAvailable != true).Count() == 0;
            }
        }
    }
}
