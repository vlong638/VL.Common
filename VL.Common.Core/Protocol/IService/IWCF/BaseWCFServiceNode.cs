using System;
using VL.Common.Core.Protocol;

namespace VL.Common.Core.Protocol//.IService.IWCF
{
    /// <summary>
    /// 服务节点规范
    /// 服务依赖建设
    /// </summary>
    public class BaseWCFServiceNode<T> where T: ServiceContext,new()
    {
        T _serviceContext;
        object ServiceLock = new object();
        public T ServiceContext
        {
            get
            {
                if (_serviceContext == null)
                {
                    lock (ServiceLock)
                    {
                        if (_serviceContext == null)
                        {
                            _serviceContext = new T();
                            _serviceContext.Init();
                        }
                    }
                }
                return _serviceContext;
            }
        }
        public bool CheckAlive()
        {
            return ServiceContext.Init().IsAllDependenciesAvailable;
        }
        public DependencyResult CheckNodeReferences()
        {
            return ServiceContext.Init();
        }
    }
}
