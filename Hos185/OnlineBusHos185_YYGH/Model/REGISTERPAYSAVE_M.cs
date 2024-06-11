namespace OnlineBusHos185_YYGH.Model
{
    class REGISTERPAYSAVE_M
    {
        public class REGISTERPAYSAVE_IN
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
            /// 医疗卡类型
            /// </summary>
            public string YLCARD_TYPE { get; set; }
            /// <summary>
            /// 医疗卡号
            /// </summary>
            public string YLCARD_NO { get; set; }
            /// <summary>
            /// 身份证号
            /// </summary>
            public string SFZ_NO { get; set; }
            /// <summary>
            /// 手机号码
            /// </summary>
            public string MOBILE_NO { get; set; }
            /// <summary>
            /// 姓名
            /// </summary>
            public string PAT_NAME { get; set; }
            /// <summary>
            /// 性别
            /// </summary>
            public string SEX { get; set; }
            /// <summary>
            /// 出生日期
            /// </summary>
            public string BIRTHDAY { get; set; }
            /// <summary>
            /// 常用住址
            /// </summary>
            public string ADDRESS { get; set; }
            /// <summary>
            /// 监护人姓名
            /// </summary>
            public string GUARDIAN_NAME { get; set; }
            /// <summary>
            /// 监护人身份证
            /// </summary>
            public string GUARDIAN_SFZ_NO { get; set; }
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
            /// 排班时间
            /// </summary>
            public string SCH_TIME { get; set; }
            /// <summary>
            /// 排班类型（1专科2专家 3急诊）
            /// </summary>
            public string SCH_TYPE { get; set; }
            /// <summary>
            /// 时间段开始时间
            /// </summary>
            public string PERIOD_START { get; set; }
            /// <summary>
            ///  时间段结束时间
            /// </summary>
            public string PERIOD_END { get; set; }
            /// <summary>
            /// 职称
            /// </summary>
            public string PRO_TITLE { get; set; }
            /// <summary>
            /// 预约等待标识
            /// </summary>
            public string WAIT_ID { get; set; }
            /// <summary>
            /// 号别
            /// </summary>
            public string REGISTER_TYPE { get; set; }
            /// <summary>
            /// 患者院内唯一索引
            /// </summary>
            public string HOSPATID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HOS_SN { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string JEALL { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string CASH_JE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string DEAL_TYPE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string DEAL_STATES { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string DEAL_TIME { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string QUERYID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string FILTER { get; set; }

            public string SJH { get; set; }

            /// <summary>
            /// 2 预约取号
            /// </summary>
            public string IS_YY { get; set; }

            public string SCH_ID { get; set; }
        }

        public class REGISTERPAYSAVE_OUT
        {
            /// <summary>
            /// 院内唯一流水号
            /// </summary>
            public string HOS_SN { get; set; }
            /// <summary>
            /// 支付金额
            /// </summary>
            public string APPT_PAY { get; set; }
            /// <summary>
            /// 序号
            /// </summary>
            public string APPT_ORDER { get; set; }
            /// <summary>
            /// 就诊地点
            /// </summary>
            public string APPT_PLACE { get; set; }
            /// <summary>
            /// 就诊时间
            /// </summary>
            public string APPT_TIME { get; set; }
            /// <summary>
            /// 病人门诊号
            /// </summary>
            public string OPT_SN { get; set; }

            public string RCPT_NO { get; set; }
            public string HIS_RTNXML { get; set; }
            /// <summary>
            /// 其他参数
            /// </summary>
            public string PARAMETER { get; set; }
            public string DJ_ID { get;  set; }
        }
    }
}
