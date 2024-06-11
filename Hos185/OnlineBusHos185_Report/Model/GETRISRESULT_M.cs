using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBusHos185_Report.Model
{
    class GETRISRESULT_M
    {
        public class GETRISRESULT_IN
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
        }


        public class GETRISRESULT_OUT
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
