using System.Runtime.Serialization;

namespace VL.Common.Protocol.IResult
{
    [DataContract]
    public class Result
    {
        [DataMember]
        public EResultCode ResultCode { set; get; }
        [DataMember]
        public string Content { set; get; }
    }
    [DataContract]
    public class Result<T>
    {
        [DataMember]
        public EResultCode ResultCode { set; get; }
        [DataMember]
        public T SubResultCode { set; get; }
        [DataMember]
        public string Content { set; get; }
    }
}
