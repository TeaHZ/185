using Newtonsoft.Json;

namespace ZZJ_Tran.F2FPAY.Linux
{
    internal class AlipayTradeCloseResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("sub_code")]
        public string SubCode { get; set; }

        [JsonProperty("sub_msg")]
        public string SubMsg { get; set; }

        public string Body { get; set; }
        public bool IsError { get; }

        [JsonProperty("out_trade_no")]
        public string OutTradeNo { get; set; }

        [JsonProperty("trade_no")]
        public string TradeNo { get; set; }
    }
}