using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_Report.Model
{
    class ZZJMEDICALEVIDENCEPRNBACK_M
    {
        public class ZZJMEDICALEVIDENCEPRNBACK_IN
        {
            /// 
            /// </summary>
            public string USER_ID { get; set; }
            public string HOS_ID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string LTERMINAL_SN { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string REPORT_SN { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string SOURCE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string FILTER { get; set; }
        }


        public class PARAMETERS
        {
        }

        public class  ZZJMEDICALEVIDENCEPRNBACK_OUT

        {
            /// <summary>
            /// 
            /// </summary>
            public string HIS_RTNXML { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public PARAMETERS PARAMETERS { get; set; }
        }
    }
}
