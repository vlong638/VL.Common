using System;
using System.ServiceModel;
using VL.Common.Logger.Utilities;

namespace VL.Common.Protocol.IService//.IWCF
{
    /// <summary>
    /// 服务节点规范
    /// 服务依赖建设
    /// </summary>
    public class BaseWCFServiceNode<T> where T: ServiceContext
    {
        static T _serviceContext;
        static Object ServiceLock = new object();
        protected static T ServiceContext
        {
            get
            {
                if (_serviceContext == null)
                {
                    lock (ServiceLock)
                    {
                        if (_serviceContext == null)
                        {
                            _serviceContext = default(T);
                        }
                    }
                }
                return _serviceContext;
            }
        }
        static DependencyResult _dependencyResult;
        static Object DependencyLock = new object();
        static DependencyResult DependencyResult
        {
            get
            {
                if (_dependencyResult == null)
                {
                    lock (DependencyLock)
                    {
                        if (_dependencyResult == null)
                        {
                            _dependencyResult = ServiceContext.Init();
                        }
                    }
                }
                return _dependencyResult;
            }
        }
        public bool CheckAlive()
        {
            return DependencyResult.IsAllDependenciesAvailable;
        }
        public DependencyResult CheckNodeReferences()
        {
            return DependencyResult;
        }
    }
}
