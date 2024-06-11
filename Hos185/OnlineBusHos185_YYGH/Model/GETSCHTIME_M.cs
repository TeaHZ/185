using System.Collections.Generic;

namespace OnlineBusHos185_YYGH.Model
{
    class GETSCHTIME_M
    {
        public class GETSCHTIME_IN
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
            /// 日期
            /// </summary>
            public string SCH_DATE { get; set; }
            /// <summary>
            /// 是否急诊挂号
            /// </summary>
            public string IS_JZGH { get; set; }

            public string FILTER { get; set; }
        }

        public class GETSCHTIME_OUT
        {
            public List<SCHTIME> SCHTIMELIST { get; set; }

            public string HIS_RTNXML { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string PARAMETERS { get; set; }
        }
        public class SCHTIME
        {
            public string SCH_TIME { get; set; }
        }

    }
}
