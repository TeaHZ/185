using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_InHos.Model
{
    internal class SAVEINPATYJJ
    {
        public class In
        {
            /// <summary>
            /// 
            /// </summary>
            public string HOS_ID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string USER_ID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string LTERMINAL_SN { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HOS_NO { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HOSPATID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string CASH_JE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string DEAL_STATES { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string DEAL_TIME { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string DEAL_TYPE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string QUERYID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string SFZ_NO { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string SOURCE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string FILTER { get; set; }
            public object APPID { get;  set; }
            public string DEFRAYNO { get;  set; }
            public object CHANNELTRADENO { get;  set; }

        }
     

        public class Out
        {
            /// <summary>
            /// 
            /// </summary>
            public string HOSPATID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string JE_PAY { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string CASH_JE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string JE_REMAIN { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HOS_PAY_SN { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HIS_RTNXML { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string PARAMETERS { get; set; }
        }

    }
}
