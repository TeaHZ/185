using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos185_Report
{
    class ZZJPATHOLOGYPRNBACK_M
    {
        public class ZZJPATHOLOGYPRNBACK_IN
        {
            /// <summary>
            /// 医院ID
            /// </summary>
            public string HOS_ID { get; set; }
            /// <summary>
            /// 操作员ID
            /// </summary>
            public string USER_ID { get; set; }
            /// <summary>
            /// 自助机终端号
            /// </summary>
            public string LTERMINAL_SN { get; set; }
            /// <summary>
            /// 报告单号
            /// </summary>
            public string REPORT_SN { get; set; }

            public string REPORT_TYPE { get; set; }

            /// <summary>
            /// 数据来源
            /// </summary>
            public string SOURCE { get; set; }
            /// <summary>
            /// 其他条件
            /// </summary>
            public string FILTER { get; set; }
            public string TYPE { get; set; }

        }


        public class ZZJPATHOLOGYPRNBACK_OUT
        {
            public string HIS_RTNXML { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string PARAMETERS { get; set; }

        }
    }
}
