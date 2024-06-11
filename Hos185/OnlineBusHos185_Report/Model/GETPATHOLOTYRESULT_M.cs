using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos185_Report.Model
{
    class GETPATHOLOGYRESULT_M
    {
        public class GETPATHOLOGYRESULT_IN
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


        public class GETPATHOLOGYRESULT_OUT
        {
            /// <summary>
            /// 1 pdf
            /// </summary>
            public string DATA_TYPE { get; set; }
            /// <summary>
            /// PDF BASE64
            /// </summary>
            public string REPORTDATA { get; set; }

            public string HIS_RTNXML { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string PARAMETERS { get; set; }

        }
    }
}
