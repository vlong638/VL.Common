using System.ServiceModel;

namespace VL.Common.Protocol.IService//.IWCF
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
}
