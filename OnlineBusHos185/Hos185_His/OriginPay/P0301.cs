using System;
using System.Collections.Generic;
using System.Text;

namespace Hos185_His.Models.OriginPay
{
    public class P0301
    {
        /// <summary>
        /// 
        /// </summary>
        public string outRefundNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string outTradeNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string transactionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal refundFee { get; set; }
        /// <summary>
        /// 退款原因
        /// </summary>
        public string refundReason { get; set; }
        /// <summary>
        /// 调⽤⽅机器mac地址/ip地址
        /// </summary>
        public string macNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string operCode { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 患者证件号
        /// </summary>
        public string identityId { get; set; }
        /// <summary>
        /// 患者⼿机号
        /// </summary>
        public string mobile { get; set; }

        public string refundId { get; set; }
    }


    public class P0301DATA
    {
        /// <summary>
        /// 
        /// </summary>
        public string outRefundNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string refundId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string outRefundId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal refundFee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tradeChannel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tradeState { get; set; }

        public ExtraData extraData { get; set; }
    }

}
