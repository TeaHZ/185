using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_InHos.HISModels
{
    class T5301
    {
        public class Input
        {
            /// <summary>
            /// 
            /// </summary>
            public string patientId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string visitNo { get; set; }
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
