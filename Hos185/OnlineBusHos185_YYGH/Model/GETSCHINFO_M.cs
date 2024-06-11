using System.Collections.Generic;

namespace OnlineBusHos185_YYGH.Model
{
    class GETSCHINFO_M
    {
        public class GETSCHINFO_IN
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
            /// 排班日期
            /// </summary>
            public string SCH_DATE { get; set; }
            /// <summary>
            /// 医疗卡类型
            /// </summary>
            public string YLCARD_TYPE { get; set; }
            /// <summary>
            /// 医疗卡号
            /// </summary>
            public string YLCARD_NO { get; set; }
            public string FILTER { get; set; }

        }

        public class GETSCHINFO_OUT
        {
            public List<DEPT> DEPTLIST { get; set; }
            public List<DOC> DOCLIST { get; set; }
            public string HIS_RTNXML { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string PARAMETERS { get; set; }
        }

        public class DEPT
        {
            public string DEPT_CODE { get; set; }
            public string DEPT_NAME { get; set; }

            public string DOC_NO { get; set; }
            public string DOC_NAME { get; set; }

            public string GH_FEE { get; set; }
            public string ZL_FEE { get; set; }

            public string SCH_TYPE { get; set; }
            public string SCH_DATE { get; set; }
            public string SCH_TIME { get; set; }
            public string PERIOD_START { get; set; }
            public string PERIOD_END { get; set; }
            public string CAN_WAIT { get; set; }

            public string REGISTER_TYPE { get; set; }
            public string REGISTER_TYPE_NAME { get; set; }
            public string STATUS { get; set; }
            public string COUNT_REM { get; set; }
            public string YB_CODE { get; set; }
            public string PRO_TITLE { get; set; }

            public string SCH_ID { get; set; }
        }
        public class DOC
        {
            public string DOC_NO { get; set; }
            public string DOC_NAME { get; set; }

            public string GH_FEE { get; set; }
            public string ZL_FEE { get; set; }
            public string SCH_TYPE { get; set; }
            public string SCH_DATE { get; set; }
            public string SCH_TIME { get; set; }
            public string PERIOD_START { get; set; }
            public string PERIOD_END { get; set; }
            public string CAN_WAIT { get; set; }
            public string REGISTER_TYPE { get; set; }
            public string REGISTER_TYPE_NAME { get; set; }
            public string STATUS { get; set; }
            public string COUNT_REM { get; set; }
            public string YB_CODE { get; set; }
            public string PRO_TITLE { get; set; }
            public string SCH_ID { get; set; }


        }

    }
}
