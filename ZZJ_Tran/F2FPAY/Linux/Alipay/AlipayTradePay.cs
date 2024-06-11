using Alipay.AopSdk.Core.Domain;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZZJ_Tran.F2FPAY.Linux
{
    public class AlipayTradePay
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

        [JsonProperty("total_amount")]
        public string TotalAmount { get; set; }

        [JsonProperty("store_name")]
        public string StoreName { get; set; }

        [JsonProperty("receipt_amount")]
        public string ReceiptAmount { get; set; }

        [JsonProperty("point_amount")]
        public string PointAmount { get; set; }

        [JsonProperty("out_trade_no")]
        public string OutTradeNo { get; set; }

        [JsonProperty("open_id")]
        public string OpenId { get; set; }

        [JsonProperty("invoice_amount")]
        public string InvoiceAmount { get; set; }

        [JsonProperty("gmt_payment")]
        public string GmtPayment { get; set; }

        [JsonProperty("fund_bill_list")]
        public List<TradeFundBill> FundBillList { get; set; }

        [JsonProperty("discount_goods_detail")]
        public string DiscountGoodsDetail { get; set; }

        [JsonProperty("card_balance")]
        public string CardBalance { get; set; }

        [JsonProperty("buyer_user_id")]
        public string BuyerUserId { get; set; }

        [JsonProperty("buyer_pay_amount")]
        public string BuyerPayAmount { get; set; }

        [JsonProperty("buyer_logon_id")]
        public string BuyerLogonId { get; set; }

        [JsonProperty("async_payment_mode")]
        public string AsyncPaymentMode { get; set; }

        [JsonProperty("trade_no")]
        public string TradeNo { get; set; }
    }
}