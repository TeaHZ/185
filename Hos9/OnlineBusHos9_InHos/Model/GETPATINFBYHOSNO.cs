using System;
using System.Collections.Generic;
using System.Text;
using static OnlineBusHos9_InHos.Model.GETPATHOSINFO;

namespace OnlineBusHos9_InHos.Model
{
    public class GETPATINFBYHOSNO
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
            public string SFZ_NO { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string SOURCE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string FILTER { get; set; }

            public string HOSPATID { get; set; }
            public string MDTRT_CERT_TYPE { get; set; }
            public string CARD_INFO { get; set; }
            

        }
        public class PARAMETERS
        {
        }

        public class Out
        {
            /// <summary>
            /// 
            /// </summary>
            public string HOS_NO { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HOSPATID { get; set; }
            /// <summary>
            /// 张三
            /// </summary>
            public string PAT_NAME { get; set; }
            /// <summary>
            /// 男
            /// </summary>
            public string SEX { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HIN_TIME { get; set; }

            public string DEPT_NAME { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string BED_NO { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string SFZ_NO { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HIS_RTNXML { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public PARAMETERS PARAMETERS { get; set; }
        }
        public class Out1
        {
            public List<ZYList1> ZYLIST { get; set; }
        }
        public class ZYList1
        {
            /// <summary>
            /// 
            /// </summary>
            public string HOS_NO { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HOSPATID { get; set; }
            /// <summary>
            /// 张三
            /// </summary>
            public string PAT_NAME { get; set; }
            /// <summary>
            /// 男
            /// </summary>
            public string SEX { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HIN_TIME { get; set; }

            public string DEPT_NAME { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string BED_NO { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string SFZ_NO { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HIS_RTNXML { get; set; }
            /// <summary>
            /// 
            /// </summary>
            ///  /// <summary>
            /// 
            /// </summary>
            public string JE_PAY { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string JE_YET { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string JE_REMAIN { get; set; }
            public PARAMETERS PARAMETERS { get; set; }
            public string GREENINDICATOR { get; set; }//todozy
            public string CAN_PAY { get; set; }//todozy
            

        }
    }
}
