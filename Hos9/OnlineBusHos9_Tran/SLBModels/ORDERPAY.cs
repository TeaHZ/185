using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_Tran.SLBModels
{
    public class ORDERPAY
    {
        public class busdata
        {

            public string CLIENT_ID { get; set; }
            public string HOS_ID { get; set; }
            /// <summary>
            /// 金额
            /// </summary>
            public decimal Je { get; set; }
            /// <summary>
            /// 说明
            /// </summary>
            public string ORDER_DESC { get; set; }

            /// <summary>
            /// HIS流水号
            /// </summary>
            public string COMM_HIS { get; set; }

            /// <summary>
            /// 付款码、支付码
            /// </summary>
            public string QRCODE { get; set; }
            /// <summary>
            /// R4：银联（宜兴），201：数字人民币
            /// 01	微信
            /// 02	支付宝
            /// 04	建行聚合支付
            /// 05	农行聚合支付
            /// 99	集成支付
            /// 101	银联POS
            /// 102	百富建行POS
            /// 103	新利建行POS
            /// 104	工行POS
            /// 201	数字人民币(默认)
            /// 202	数字人民币(硬钱包)
            /// 600	一账通
            /// 700	员工卡
            /// 800	信用付
            /// </summary>
            public string PAY_TYPE { get; set; }
        }

        public class outbody
        {

            /// <summary>
            /// 
            /// </summary>
            public string COMM_SN { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string COMM_UNIT { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string CLBZ { get; set; }
            /// <summary>
            /// 交易成功
            /// </summary>
            public string CLJG { get; set; }


        }
    }
}
