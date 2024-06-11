using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_InHos.Model
{
    internal class JZHOUTYJS
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
            public string SOURCE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string FILTER { get; set; }
        }

        public class FEELISTItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string FEE_NOTE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string JE { get; set; }
        }

        
        public class Out
        {
            /// <summary>
            /// 
            /// </summary>
            public string HIN_DATE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HOUT_DATE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HIN_DAYS { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string JE_ALL { get; set; }
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
            public string DJ_ID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string DJ_NO { get; set; }
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
            public List<FEELISTItem> FEELIST { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HIS_RTNXML { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string PARAMETERS { get; set; }
            public string MEDFEE_SUMAMT { get;  set; }
            public string ACCT_PAY { get;  set; }
            public string PSN_CASH_PAY { get;  set; }
            public string FUND_PAY_SUMAMT { get;  set; }
            public string OTH_PAY { get;  set; }
            public string BALC { get;  set; }
            public string ACCT_MULAID_PAY { get;  set; }
        }

    }
}
