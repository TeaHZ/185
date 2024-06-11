namespace OnlineBusHos9_Tran.HerenModels
{
    internal class Query
    {

        public class bizContent
        {
            public string appTradeNo { get; set; }
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
            public string channelTradeNo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// 查询成功
            /// </summary>
            public string msgInfo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string tradeNo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string tradeStatus { get; set; }

            public string defrayNo { get; set; }
        }

    }
}