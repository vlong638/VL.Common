using System.Runtime.Serialization;

namespace VL.Common.Protocol.IResult
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
