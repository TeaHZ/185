using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alipay.AopSdk.Core.Domain;
using Newtonsoft.Json;

namespace OnlineBusHos185_Tran.F2FPAY.Linux
{
    class AlipayTradeRefund
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
        [JsonProperty("buyer_logon_id")]
        public string BuyerLogonId { get; set; }
        [JsonProperty("buyer_user_id")]
        public string BuyerUserId { get; set; }
        [JsonProperty("fund_change")]
        public string FundChange { get; set; }
        [JsonProperty("gmt_refund_pay")]
        public string GmtRefundPay { get; set; }
        [JsonProperty("open_id")]
        public string OpenId { get; set; }
        [JsonProperty("out_trade_no")]
        public string OutTradeNo { get; set; }
        [JsonProperty("refund_detail_item_list")]
        public List<TradeFundBill> RefundDetailItemList { get; set; }
        [JsonProperty("refund_fee")]
        public string RefundFee { get; set; }
        [JsonProperty("send_back_fee")]
        public string SendBackFee { get; set; }
        [JsonProperty("store_name")]
        public string StoreName { get; set; }
        [JsonProperty("trade_no")]
        public string TradeNo { get; set; }
    }
}
