using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos185_Report
{
    class GETMEDICALRESULT_M
    {
        public class GETMEDICALRESULT_IN
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

            public string REPORT_TYPE { get; set; }
        }

        public class GETMEDICALRESULT_OUT
        {
            public string DATA_TYPE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string REPORTDATA { get; set; }

            // public List<MEDICALRESULTMX> MEDICALRESULT { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HIS_RTNXML { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string PARAMETERS { get; set; }
        }

        public class MEDICALRESULTMX
        {
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
