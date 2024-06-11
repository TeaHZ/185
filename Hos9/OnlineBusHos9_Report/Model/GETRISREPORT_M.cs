using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBusHos9_Report.Model
{
    class GETRISREPORT_M
    {
        public class GETRISREPORT_IN
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
        public class GETRISREPORT_OUT
        {
            private string _REPORT_AUDIT_NUM = "0";
            public string REPORT_ALL_NUM { get; set; }

            public string REPORT_PRINT_NUM { get; set; }
            public string REPORT_AUDIT_NUM { get { return _REPORT_AUDIT_NUM; } set { _REPORT_AUDIT_NUM = value; } }

            public List<RISREPORT> RISREPORT { get; set; }

            public string HIS_RTNXML { get; set; }

            public string PARAMETERS { get; set; }
        }

        public class RISREPORT
        {
            /// <summary>
            /// 报告单号
            /// </summary>
            public string REPORT_SN { get; set; }
            /// <summary>
            /// 报告单类型
            /// </summary>
            public string REPORT_TYPE { get; set; }
            /// <summary>
            /// 报告单名称
            /// </summary>
            public string REPORT_NAME { get; set; }
            /// <summary>
            /// 报告日期
            /// </summary>
            public string REPORT_DATE { get; set; }
            /// <summary>
            /// 报告医生
            /// </summary>
            public string REPORT_DOC_NAME { get; set; }
            private string _PRINT_FLAG = "0";
            /// <summary>
            /// 打印标记
            /// </summary>
            public string PRINT_FLAG { get { return _PRINT_FLAG; } set { _PRINT_FLAG = value; } }
            /// <summary>
            /// 打印时间
            /// </summary>
            public string PRINT_TIME { get; set; }
            /// <summary>
            /// 检查日期
            /// </summary>
            public string CHECK_DATE { get; set; }
            /// <summary>
            /// 检查医师姓名
            /// </summary>
            public string CHECK_DOC_NAME { get; set; }
            /// <summary>
            /// 检查科室名称
            /// </summary>
            public string CHECK_DEPT_NAME { get; set; }
            /// <summary>
            /// 申请日期
            /// </summary>
            public string APPLY_DATE { get; set; }
            /// <summary>
            /// 申请科室
            /// </summary>
            public string APPLY_DEPT_NAME { get; set; }
            /// <summary>
            /// 申请医生
            /// </summary>
            public string APPLY_DOC_NAME { get; set; }
            /// <summary>
            /// 审核日期
            /// </summary>
            public string AUDIT_DATE { get; set; }
            /// <summary>
            /// 审核医生姓名
            /// </summary>
            public string AUDIT_DOC_NAME { get; set; }

            private string _AUDIT_FLAG = "1";
            /// <summary>
            /// 审核标志
            /// </summary>
            public string AUDIT_FLAG { get { return _AUDIT_FLAG; } set { _AUDIT_FLAG = value; } }
            /// <summary>
            /// 检查结果
            /// </summary>
            public string RESULT { get; set; }
            /// <summary>
            /// 检查总结
            /// </summary>
            public string FINAL_REPORT { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            public string NOTE { get; set; }
            /// <summary>
            /// 数据类型
            /// </summary>
            public string DATA_TYPE { get; set; }
            /// <summary>
            /// 明细
            /// </summary>
            public string REPORTDATA { get; set; }
        }
    }
}
