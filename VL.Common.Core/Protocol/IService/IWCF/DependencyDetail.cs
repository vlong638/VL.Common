using System.Runtime.Serialization;

namespace VL.Common.Protocol//.IService.IWCF
{
    /// <summary>
    /// 依赖项详情 单元组件
    /// </summary>
    [DataContract]
    public class DependencyDetail
    {
        [DataMember]
        public string DependencyName { set; get; }
        [DataMember]
        public DependencyType DependencyType { set; get; }
        [DataMember]
        public bool IsDependencyAvailable { set; get; }
        [DataMember]
        public string Message { set; get; }
    }
}
