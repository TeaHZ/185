using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_Tran.SLBModels
{
    internal class GETQRCODE
    {
        public class busdata
        {
            public string CLIENT_ID { get; set; }
            public string HOS_ID { get; set; }
            public decimal Je { get; set; }
            public string ORDER_DESC { get; set; }
            public string COMM_HIS { get; set; }
            public string EXPIRE_MINUTES { get; set; }
            public string PAY_TYPE { get; set; }
        }

        public class outbody
        {
            public string CLBZ { get; set; }
            public string CLJG { get; set; }
            public string COMM_SN { get; set; }
            public string COMM_UNIT { get; set; }
            public string QRCODE { get; set; }

        }
    }
}
