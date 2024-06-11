using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_InHos.HISModels
{
    class T5303
    {
        public class Input
        {
            public string patientId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string visitNo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string settleNo { get; set; }
        }
        public class Data
        {
            /// <summary>
            /// base64数据
            /// </summary>
            public string base64 { get; set; }
        }
        
    }
}

