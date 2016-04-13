using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VL.Common.DAS.Utilities;

namespace VL.Common.Protocol.IService
{
    public abstract class ServiceContext
    {
        public static ProtocolConfig ProtocolConfig { get; set; }
        public static DbConfigEntity DatabaseConfig { get; set; }

        public abstract bool Init();
    }
}
