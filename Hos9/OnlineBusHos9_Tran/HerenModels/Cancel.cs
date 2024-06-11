using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_Tran.HerenModels
{
    internal class Cancel
    {
        public class bizContent
        {
            public string appTradeNo { get; set; }
        }

        public class Response
        {
            /// <summary>
            /// 
            /// </summary>
            public string appId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string appTradeNo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int cancelCompleteTime { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string code { get; set; }
            /// <summary>
            /// 撤销成功
            /// </summary>
            public string msgInfo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string sign { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string status { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string tradeNo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string tradeStatus { get; set; }
        }

    }
}
