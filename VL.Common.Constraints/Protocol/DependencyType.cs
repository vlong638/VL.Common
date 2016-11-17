using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VL.Common.Constraints.Protocol
{
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
}
