using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VL.Common.Protocol.IService
{
    /// <summary>
    /// 
    /// </summary>
    interface IServiceNode
    {
        bool CheckAlive();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool CheckNodeReferences();
    }
}
