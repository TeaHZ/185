using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBusHos9_Report.Model
{
    class GETLISRESULT_M
    {
        public class GETLISRESULT_IN
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

            public string TEST_DATE { get; set; }

            public string MACHINE { get; set; }
            /// <summary>
            /// 数据来源
            /// </summary>
            public string SOURCE { get; set; }
            /// <summary>
            /// 其他条件
            /// </summary>
            public string FILTER { get; set; }

            public string IS_REPRINT { get; set; }

            /// <summary>
            /// 检验报告来源，默认空，winex-卫宁输血报告
            /// </summary>
            public string TEST_REPORT_SOURCE { get; set; }
        }


        public class GETLISRESULT_OUT
        {
            /// <summary>
            /// 1 pdf
            /// </summary>
            public string DATA_TYPE { get; set; }
            /// <summary>
            /// PDF BASE64
            /// </summary>
            public string REPORTDATA { get; set; }

            public List<ReportData> REPORTLIST { get; set; }

            public string HIS_RTNXML { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string PARAMETERS { get; set; }

        }


        public class ReportData
        {
            /// <summary>
            /// 1 pdf
            /// </summary>
            public string DATA_TYPE { get; set; }
            /// <summary>
            /// PDF BASE64
            /// </summary>
            public string REPORTDATA { get; set; }

        }
    }
}
