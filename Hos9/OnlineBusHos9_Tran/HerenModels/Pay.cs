using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_Tran.HerenModels
{
    internal class Pay
    {


        public class bizContent
        {

            public string channel { get; set; }
            public string appTradeNo { get; set; }
            public string signNo { get; set; }
            public string contractId { get; set; }
            public string userId { get; set; }
            public string title { get; set; }
            public string body { get; set; }
            public string scene { get; set; }
            public string authCode { get; set; }
            public string authNo { get; set; }
            public string defrayFee { get; set; }
            public string abnormalUrl { get; set; }
            public string returnUrl { get; set; }
            public string payExpire { get; set; }
            public string expireTime { get; set; }
            public string goodsTag { get; set; }
            public string openId { get; set; }
            public string subOpenId { get; set; }
            public string terminalIp { get; set; }
            public string terminalId { get; set; }
            public string mac { get; set; }
        }

        public class Response
        {
            /// <summary>
            /// 
            /// </summary>
            public string appId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string appTradeNo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string buyerAccount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string channel { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string channelTradeNo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int defrayCompleteTime { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double defrayFee { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int defrayStartTime { get; set; }
            /// <summary>
            /// 支付成功
            /// </summary>
            public string msgInfo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string sign { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string tradeNo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string tradeStatus { get; set; }
        }

    }
}
