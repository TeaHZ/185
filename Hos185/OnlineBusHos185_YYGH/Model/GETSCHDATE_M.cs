using System.Collections.Generic;

namespace OnlineBusHos185_YYGH.Model
{
    class GETSCHDATE_M
    {
        public class GETSCHDATE_IN
        {
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
            /// 科室代码
            /// </summary>
            public string DEPT_CODE { get; set; }
            /// <summary>
            /// 医生工号
            /// </summary>
            public string DOC_NO { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string USE_TYPE { get; set; }

            public string FILTER { get; set; }
        }

        public class GETSCHDATE_OUT
        {
            public List<SCHLIST> SCHDEPTLIST { get; set; }
            public List<SCHLIST> SCHDOCLIST { get; set; }

            public string HIS_RTNXML { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string PARAMETERS { get; set; }
        }
        public class SCHLIST
        {
            public string SCH_DATE { get; set; }

            public string WEEK_DAY { get; set; }
        }

    }
}
