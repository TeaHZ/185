using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace OnlineBusHos185_YYGH.Model
{
    public class GETEMERGENCYLIST_M
    {
        public class GETEMERGENCYLIST_IN
        {
            public string HOS_ID { get; set; }
            public string USER_ID { get; set; }
            public string LTERMINAL_SN { get; set; }
            public string HOSPATID { get; set; }
            public string YLCARD_TYPE { get; set; }
            public string YLCARD_NO { get; set; }
            public string SFZ_NO { get; set; }
            public string PAT_NAME { get;  set; }

        }

        public class GETEMERGENCYLIST_OUT
        {
            public List<APPT> APPTLIST { get; set; }
            public string HIS_RTNXML { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string PARAMETERS { get; set; }
        }

        public class APPT
        {
            /// <summary>
            ///
            /// </summary>
            public string HOS_SN { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string APPT_PAY { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string JEALL { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string APPT_ORDER { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string APPT_TIME { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string APPT_PLACE { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string YLCARD_TYPE { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string DEPT_CODE { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string DEPT_NAME { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string DEPT_INTRO { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string DEPT_ORDER { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string DEPT_TYPE { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string DEPT_ADDRESS { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string DOC_NO { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string DOC_NAME { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string GH_FEE { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string ZL_FEE { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string ALL_FEE { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string SCH_TYPE { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string SCH_DATE { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string SCH_TIME { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string PERIOD_START { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string PERIOD_END { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string REGISTER_TYPE { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string REGISTER_TYPE_NAME { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string APPT_TYPE { get; set; }

            public string SCH_ID { get; set; }
            public string EMERGENCYLEVELCODE { get; set; }
            public string EMERGENCYLEVELNAME { get; set; }
        }
    }
}