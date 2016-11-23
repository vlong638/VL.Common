namespace VL.Common.Core.Protocol
{
    /// <summary>
    /// Protocol下的常量集合
    /// </summary>
    public static class CProtocol
    {
        /// <summary>
        /// 默认常量集合
        /// </summary>
        public static class CDefault
        {
            public const string CDbConfig = "DbConnections.config";
            public const string CProtocolConfig = "ProtocolConfig.config";
            public const string CLogger = "ServiceLog";
        }
        /// <summary>
        /// Report域常量集合
        /// </summary>
        public static class CReport
        {
            public const int CSuccess = 1;//成功
            public const int CError = 2;//错误or异常
            //public const int CFailure = 2;//失败
            public const int CManualStart = 10;//人工指定的开始值
        }
    }
}
