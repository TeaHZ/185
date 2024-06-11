using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_Common.HISModels
{
    internal class T6003
    {

        public class Input
        {
            /// <summary>
            /// 
            /// </summary>
            public string rcptNo { get; set; }
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