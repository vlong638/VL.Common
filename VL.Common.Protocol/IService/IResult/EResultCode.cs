using System.Runtime.Serialization;

namespace VL.Common.Protocol.IService
{
    [DataContract]
    public enum EResultCode
    {
        [EnumMember]
        Success,
        [EnumMember]
        Failure,
        [EnumMember]
        Error,
    }
}
