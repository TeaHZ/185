namespace OnlineBusHos9_GJYB
{
    public class Init_pInitInfo
    {
        /// <summary>
        /// 服务端IP地址
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 服务端端口
        /// </summary>
        public string PORT { get; set; }
        /// <summary>
        /// 超时
        /// </summary>
        public string TIMEOUT { get; set; }
        /// <summary>
        /// 动态库日志目录
        /// </summary>
        public string LOG_PATH { get; set; }
        /// <summary>
        /// 电子凭证中台URL
        /// </summary>
        public string EC_URL { get; set; }
        /// <summary>
        /// 社保卡验证密码方式
        /// </summary>
        public string CARD_PASSTYPE { get; set; }
        /// <summary>
        /// CSB的_api_name
        /// </summary>
        public string API_NAME { get; set; }
        /// <summary>
        /// CSB的_api_version
        /// </summary>
        public string API_VERSION { get; set; }
        /// <summary>
        /// CSB的_api_access_key
        /// </summary>
        public string ACCESS_KEY { get; set; }
        /// <summary>
        /// CSB的secretKey
        /// </summary>
        public string SECRETKEY { get; set; }
        /// <summary>
        /// 定点编号
        /// </summary>
        public string ORG_ID { get; set; }
        /// <summary>
        /// JSON对象字符串
        /// </summary>
        public string EXT { get; set; }
    }
}
