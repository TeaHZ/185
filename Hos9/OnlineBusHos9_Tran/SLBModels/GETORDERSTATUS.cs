using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_Tran.SLBModels
{
    internal class GETORDERSTATUS
    {

        public class busdata
        {
            public string HOS_ID { get; set; }
            public string CLIENT_ID { get; set; }
            public string COMM_SN { get; set; }
            public string PAY_TYPE { get; set; }

        }

        public class outbody
        {
            public string CLBZ { get; set; }
            public string CLJG { get; set; }
            /// <summary>
            /// 0未支付 1支付成功 2全部退费 3部分退费 4订单关闭
            /// </summary>
            public string STATUS { get; set; }
            public string COMM_SN { get; set; }
            public string COMM_UNIT { get; set; }

        }
    }
}
