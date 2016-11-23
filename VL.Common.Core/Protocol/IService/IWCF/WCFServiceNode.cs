using System.ServiceModel;
using VL.Common.Core.Protocol;

namespace VL.Common.Core.Protocol//.IService.IWCF
{
    /// <summary>
    /// 服务节点规范
    /// 服务依赖建设
    /// </summary>
    public abstract class WCFServiceNode<T> where T : ServiceContext, new()
    {
        static BaseWCFServiceNode<T> ServiceBase = new BaseWCFServiceNode<T>();
        public bool CheckAlive()
        {
            return ServiceBase.CheckAlive();
        }
        public DependencyResult CheckNodeReferences()
        {
            return ServiceBase.CheckNodeReferences();
        }
    }
}
