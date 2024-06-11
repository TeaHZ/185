using System;
using System.Collections.Generic;
using System.Text;

namespace Hos185_His.Models.OriginPay
{
    public class P0401
    {
        /// <summary>
        /// 
        /// </summary>
        public string outRefundNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string refundId { get; set; }
    }


    public class P0401DATA
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
        public string tradeState { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tradeChannel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tradeTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int totalFee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rrn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ExtraData extraData { get; set; }
    }

}
