using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_InHos.Model
{
    internal class JZHOUTJS
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
            public string DJ_ID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string DJ_NO { get; set; }
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
            public string JE_YJJ { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string JE_REMAIN { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string JE_ALL { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string YB_PAY { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string CASH_JE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string YBPAY_MX { get; set; }
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
            public string SOURCE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string FILTER { get; set; }
            public string APPID { get; set; }
            public string DEFRAYNO { get;  set; }

            public string CHANNELTRADENO { get; set; }
            public string MDTRT_CERT_TYPE { get; set; }
            public string CARD_INFO { get; set; }

        }

        public class Out
        {
            public string RCPT_NO { get; set; }
            public string HOS_PAY_SN { get; set; }
            public string QUERYID { get; set; }
            public string DEAL_TYPE { get; set; }
            public string YDJ_NO { get; set; }
            public string DJ_DATE { get; set; }
            public string MEDFEE_SUMAMT { get; set; }
            public string ACCT_PAY { get; set; }
            public string PSN_CASH_PAY { get; set; }
            public string FUND_PAY_SUMAMT { get; set; }
            public string OTH_PAY { get; set; }
            public string BALC { get; set; }
            public string ACCT_MULAID_PAY { get; set; }
            public string SETTLENOLIST { get; set; }
            public string RCPTNOLIST { get; set; }
        }
    }
}
