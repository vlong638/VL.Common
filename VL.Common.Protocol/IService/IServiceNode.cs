using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace VL.Common.Protocol.IService
{
    /// <summary>
    /// 服务节点规范
    /// 服务依赖建设
    /// </summary>
    [ServiceContract]
    public interface IWCFServiceNode
    {
        /// <summary>
        /// 存活检测只需要知道
        /// 服务是否通畅
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool CheckAlive();
        /// <summary>
        /// 服务依赖检测则规定了
        /// 依赖是什么
        /// 各项依赖检测结果
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DependencyResult CheckNodeReferences();
    }
    /// <summary>
    /// 依赖项类别
    /// </summary>
    [DataContract]
    public enum DependencyType
    {
        [EnumMember]
        Config,
        [EnumMember]
        Database,
        [EnumMember]
        Service,
    }
    /// <summary>
    /// 依赖项详情
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
    /// <summary>
    /// 依赖检测结果
    /// </summary>
    [DataContract]
    public class DependencyResult
    {
        [DataMember]
        public List<DependencyDetail> DependencyDetails { set; get; } = new List<DependencyDetail>();
        [DataMember]
        public bool IsAllDependenciesAvailable { set; get; }
    }
}
