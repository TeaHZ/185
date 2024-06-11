using System.Collections.Generic;

namespace OnlineBusHos185_YYGH.Model
{
    class GETSCHPERIOD_M
    {
        public class GETSCHPERIOD_IN
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
            /// 医生工号
            /// </summary>
            public string SCH_TYPE { get; set; }

            /// <summary>
            /// 医生工号
            /// </summary>
            public string SCH_DATE { get; set; }

            /// <summary>
            /// 医生工号
            /// </summary>
            public string SCH_TIME { get; set; }

            public string FILTER { get; set; }

            public string REGISTER_TYPE { get; set; }
            public int SCH_ID { get; set; }
        }

        public class GETSCHPERIOD_OUT
        {
            public List<PERIOD> PERIODLIST { get; set; }
            public string HIS_RTNXML { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string PARAMETERS { get; set; }
        }

        public class PERIOD
        {

            public string SCH_ID { get; set; }

            public string PERIOD_ID { get; set; }

            /// <summary>
            /// 时间段开始时间
            /// </summary>
            public string PERIOD_START { get; set; }
            /// <summary>
            /// 时间段结束时间
            /// </summary>
            public string PERIOD_END { get; set; }
            /// <summary>
            /// 剩余号源数
            /// </summary>
            public string COUNT_REM { get; set; }
            /// <summary>
            ///号别
            /// </summary>
            public string REGISTER_TYPE { get; set; }
            /// <summary>
            ///排班状态
            /// </summary>
            public string STATUS { get; set; }

        }
    }
}
