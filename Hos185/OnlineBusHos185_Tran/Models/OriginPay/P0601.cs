using System;
using System.Collections.Generic;
using System.Text;

namespace Hos185_His.Models.OriginPay
{
    /// <summary>
    /// 业务确认提交(P0601)
    /// 当终端（⽐如HIS窗⼝，⼩泰伴医等等）获取⽀付成功消息后，完成⾃⼰的业务提交后，不论成功失败，都要调⽤这个接⼝做业务确认提交
    /// </summary>
    public class P0601
    {
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
        public string confirmState { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string confirmDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string receiptNo { get; set; }
    }

}
