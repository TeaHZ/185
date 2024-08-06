using System;
using System.Collections.Generic;
using System.Text;

namespace Hos185_His.Models.OriginPay
{
    public class P0701
    {
        /// <summary>
        /// 商⼾订单号
        /// </summary>
        public string outTradeNo { get; set; }
        /// <summary>
        /// ⽀付中台订单号
        /// </summary>
        public string transactionId { get; set; } 
    }
}
