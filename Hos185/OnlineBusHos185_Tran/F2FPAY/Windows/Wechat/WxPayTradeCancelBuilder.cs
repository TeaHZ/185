using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos185_Tran.F2FPAY.Windows
{
    public class WxPayTradeCancelBuilder : JsonBuilder
    {

        /// <summary>
        /// *商户订单号 out_trade_no 是 String(32) 1212321211201407033568112322 商户系统的订单号，与请求一致。 
        /// </summary>
        public string out_trade_no { get; set; }


        /// <summary>
        /// 撤销结果
        /// </summary>
        public bool CalcelResult { get; set; }


        public override bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
