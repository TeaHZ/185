using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_Tran.HerenModels
{
    internal class Refund
    {

        public class bizContent
        {
            public string appTradeNo { get; set; }
            public string appRefundNo { get; set; }
            public string refundFee { get; set; }
            public string returnUrl { get; set; }
            public string refundReason { get; set; }
        }
    }
}
