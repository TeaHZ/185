using System;
using System.Collections.Generic;
using System.Text;

namespace Hos185_His.Models.OriginPay
{
    public class P0201
    {
        /// <summary>
        /// 
        /// </summary>
        public string outTradeNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string transactionId { get; set; }
    }



    public class P0201DATA
    {
        /// <summary>
        /// 
        /// </summary>
        public string outTradeNo { get; set; }
        /// <summary>
        /// ptlsh平台流水号
        /// </summary>
        public string transactionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string outTransactionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tradeState { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tradeChannel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tradeType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tradeTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string orderType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal totalFee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ExtraData extraData { get; set; }
    }


}
