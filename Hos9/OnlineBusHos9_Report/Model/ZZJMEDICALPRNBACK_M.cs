using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_Report
{
    class ZZJMEDICALPRNBACK_M
    {
        public class ZZJMEDICALPRNBACK_IN
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
            public string REPORT_SN { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string PRINT_FLAG { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string SOURCE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string FILTER { get; set; }
        }

        public class ZZJMEDICALPRNBACK_OUT
        {
            public string HIS_RTNXML { get; set; }

            public string PARAMETERS { get; set; }
        }
    }
}
