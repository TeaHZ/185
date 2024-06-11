using Alipay.AopSdk.Core.Domain;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZZJ_Tran.F2FPAY.Linux
{
    internal class AlipayTradeQuery
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

        [JsonProperty("trade_no")]
        public string TradeNo { get; set; }

        [JsonProperty("total_amount")]
        public string TotalAmount { get; set; }

        [JsonProperty("terminal_id")]
        public string TerminalId { get; set; }

        [JsonProperty("store_name")]
        public string StoreName { get; set; }

        [JsonProperty("store_id")]
        public string StoreId { get; set; }

        [JsonProperty("send_pay_date")]
        public string SendPayDate { get; set; }

        [JsonProperty("receipt_amount")]
        public string ReceiptAmount { get; set; }

        [JsonProperty("point_amount")]
        public string PointAmount { get; set; }

        [JsonProperty("trade_status")]
        public string TradeStatus { get; set; }

        [JsonProperty("out_trade_no")]
        public string OutTradeNo { get; set; }

        [JsonProperty("invoice_amount")]
        public string InvoiceAmount { get; set; }

        [JsonProperty("industry_sepc_detail")]
        public string IndustrySepcDetail { get; set; }

        [JsonProperty("fund_bill_list")]
        public List<TradeFundBill> FundBillList { get; set; }

        [JsonProperty("discount_goods_detail")]
        public string DiscountGoodsDetail { get; set; }

        [JsonProperty("buyer_user_id")]
        public string BuyerUserId { get; set; }

        [JsonProperty("buyer_pay_amount")]
        public string BuyerPayAmount { get; set; }

        [JsonProperty("buyer_logon_id")]
        public string BuyerLogonId { get; set; }

        [JsonProperty("alipay_store_id")]
        public string AlipayStoreId { get; set; }

        [JsonProperty("open_id")]
        public string OpenId { get; set; }

        //[JsonProperty("voucher_detail_list")]
        // public List<VoucherDetail> VoucherDetailList { get; set; }
    }
}