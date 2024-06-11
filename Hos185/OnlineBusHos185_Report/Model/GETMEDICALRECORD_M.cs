using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos185_Report
{
    class GETMEDICALRECORD_M
    {
        public class GETMEDICALRECORD_IN
        {
            /// <summary>
            /// 医院ID
            /// </summary>
            public string HOS_ID { get; set; }
            /// <summary>
            /// 操作员唯一ID
            /// </summary>
            public string USER_ID { get; set; }
            /// <summary>
            /// 自助终端编号
            /// </summary>
            public string LTERMINAL_SN { get; set; }
            /// <summary>
            /// 医疗卡号
            /// </summary>
            public string YLCARD_NO { get; set; }
            /// <summary>
            /// 医疗卡类型
            /// </summary>
            public string YLCARD_TYPE { get; set; }
            /// <summary>
            /// 身份证号
            /// </summary>
            public string SFZ_NO { get; set; }
            /// <summary>
            /// 院内号
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

        public class GETMEDICALRECORD_OUT
        {
            /// <summary>
            /// 
            /// </summary>
            public string REPORT_ALL_NUM { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string REPORT_AUDIT_NUM { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string REPORT_PRINT_NUM { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<MEDICALREPORT> MEDICALREPORT { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HIS_RTNXML { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string PARAMETERS { get; set; }
        }
        public class MEDICALREPORT
        {
            /// <summary>
            /// 
            /// </summary>
            public string REPORT_SN { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string REPORT_TYPE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string REPORT_NAME { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string REPORT_DATE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string REPORT_DEPT_NAME { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string REPORT_DOC_NAME { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string PRINT_FLAG { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string PRINT_TIME { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string PAT_NAME { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string NOTE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string DATA_TYPE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string REPORTDATA { get; set; }
        }
    }
}
