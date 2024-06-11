using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_Report.Model
{
    class GETMEDCERTIFICATE_M
    {
        public class GETMEDCERTIFICATE_IN
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
            public string YLCARD_NO { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string YLCARD_TYPE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string SFZ_NO { get; set; }
            /// <summary>
            /// 
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
            /// <summary>
            /// 
            /// </summary>
            public string IS_REPRINT { get; set; }
        }
        public class GETMEDCERTIFICATE_OUT
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
            public List<MEDICALREPORTITEM> EVIDENCELIST { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HIS_RTNXML { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public PARAMETERS PARAMETERS { get; set; }
        }
        public class MEDICALREPORTITEM
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
 
        public class PARAMETERS
        {
        }
        public class GETMEDCERTIFICATEDATA_IN
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
            public string SOURCE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string FILTER { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string IS_REPRINT { get; set; }
        }
        public class GETMEDCERTIFICATEDATA_OUT
        {
            /// <summary>
            /// 
            /// </summary>
            public string DATA_TYPE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string REPORTDATA { get; set; }
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





