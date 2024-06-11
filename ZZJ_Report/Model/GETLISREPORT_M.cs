using System.Collections.Generic;

namespace ZZJ_Report.Model
{
    internal class GETLISREPORT_M
    {
        public class GETLISREPORT_IN
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
            /// 身份证号
            /// </summary>
            public string SFZ_NO { get; set; }

            /// <summary>
            /// 医疗卡类型
            /// </summary>
            public string YLCARD_TYPE { get; set; }

            /// <summary>
            /// 医疗卡号
            /// </summary>
            public string YLCARD_NO { get; set; }

            /// <summary>
            /// 院内卡号
            /// </summary>
            public string HOSPATID { get; set; }

            /// <summary>
            /// 数据来源
            /// </summary>
            public string SOURCE { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string FILTER { get; set; }
        }

        public class GETLISREPORT_OUT
        {
            public string REPORT_ALL_NUM { get; set; }
            public string REPORT_PRINT_NUM { get; set; }
            public List<LisReport> LISREPORT { get; set; }
            public string HIS_RTNXML { get; set; }
            public string PARAMETERS { get; set; }
        }

        public class LisReport
        {
            /// <summary>
            /// 报告单号
            /// </summary>
            public string REPORT_SN { get; set; }

            /// <summary>
            /// 报告单名称
            /// </summary>
            public string REPORT_TYPE { get; set; }

            /// <summary>
            /// 报告日期
            /// </summary>
            public string REPORT_DATE { get; set; }

            /// <summary>
            /// 打印标记
            /// </summary>
            public string PRINT_FLAG { get; set; }

            /// <summary>
            /// 打印时间
            /// </summary>
            public string PRINT_TIME { get; set; }

            /// <summary>
            /// 检验日期
            /// </summary>
            public string TEST_DATE { get; set; }

            /// <summary>
            /// 检验医师姓名
            /// </summary>
            public string TEST_DOC_NAME { get; set; }

            /// <summary>
            /// 检验科室名称
            /// </summary>
            public string TEST_DEPT_NAME { get; set; }

            /// <summary>
            /// 审核日期
            /// </summary>
            public string AUDIT_DATE { get; set; }

            /// <summary>
            /// 审核医生姓名
            /// </summary>
            public string AUDIT_DOC_NAME { get; set; }

            /// <summary>
            /// 数据类型
            /// </summary>
            public string DATA_TYPE { get; set; }

            /// <summary>
            /// PDF  base64
            /// </summary>
            public string REPORTDATA { get; set; }
        }
    }
}