namespace OnlineBusHos185_Common.EHealthCard
{

    public class QrCodeAnalysisRequest
    {
        /// <summary>
        /// 签名
        /// </summary>
        public string appSign { get; set; }
        /// <summary>
        /// 访问时间戳
        /// </summary>
        public long timestamp { get; set; }
        /// <summary>
        /// 包名
        /// </summary>
        public string pack { get; set; }
        /// <summary>
        /// 二维码
        /// </summary>
        public string qrCode { get; set; }
    }
    public class qrCodeAnalysisResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public qrCodeAnalysisResponseData data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }
    }

    public class qrCodeAnalysisResponseData
    {
        /// <summary>
        /// 虚拟卡号
        /// </summary>
        public string virtualCardNum { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string realname { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string idNumber { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string cellphone { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int gender { get; set; }
        /// <summary>
        /// 用户PIN码
        /// </summary>
        public int pin { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string addr { get; set; }
    }
}
