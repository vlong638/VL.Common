using VL.Common.DAS.Utilities;

namespace VL.Common.Protocol.IService
{
    public abstract class ServiceContext
    {
        /// <summary>
        /// 总协议配置
        /// </summary>
        public static ProtocolConfig ProtocolConfig { get; set; }
        /// <summary>
        /// 数据库配置
        /// </summary>
        public static DbConfigEntity DatabaseConfig { get; set; }

        /// <summary>
        /// 上下文初始化
        /// </summary>
        /// <returns></returns>
        public abstract bool Init();
    }
}
