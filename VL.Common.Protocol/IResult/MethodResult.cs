using System.Runtime.Serialization;

namespace VL.Common.Protocol.IResult
{
    [DataContract]
    public class MethodResult
    {
        [DataMember]
        public EResultCode ResultCode { set; get; }
        [DataMember]
        public string Content { set; get; }
    }
    [DataContract]
    public class MethodResult<T>
    {
        [DataMember]
        public EResultCode ResultCode { set; get; }
        [DataMember]
        public T SubResultCode { set; get; }
        [DataMember]
        public string Content { set; get; }
    }
}
