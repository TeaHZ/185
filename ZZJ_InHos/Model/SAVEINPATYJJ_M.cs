namespace ZZJ_InHos.Model
{
    public class SAVEINPATYJJ_M
    {
        public class SAVEINPATYJJ_IN
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
            /// 住院号
            /// </summary>
            public string HOS_NO { get; set; }

            /// <summary>
            /// 住院病人唯一索引
            /// </summary>
            public string HOS_PAT_ID { get; set; }

            /// <summary>
            /// 金额
            /// </summary>
            public string CASH_JE { get; set; }

            /// <summary>
            /// 交易状态
            /// </summary>
            public string DEAL_STATES { get; set; }

            /// <summary>
            /// 交易时间
            /// </summary>
            public string DEAL_TIME { get; set; }

            /// <summary>
            /// 交易方式
            /// </summary>
            public string DEAL_TYPE { get; set; }

            /// <summary>
            /// 交易流水号
            /// </summary>
            public string QUERYID { get; set; }

            /// <summary>
            /// 身份证号
            /// </summary>
            public string SFZ_NO { get; set; }

            /// <summary>
            /// 数据来源
            /// </summary>
            public string SOURCE { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string FILTER { get; set; }
        }

        public class SAVEINPATYJJ_OUT
        {
            /// <summary>
            ///病人住院唯一索引
            /// </summary>
            public string HOS_PAT_ID { get; set; }

            /// <summary>
            ///已经缴纳现金金额
            /// </summary>
            public string JE_PAY { get; set; }

            /// <summary>
            /// 本次支付金额
            /// </summary>
            public string CASH_JE { get; set; }

            /// <summary>
            /// 余额
            /// </summary>
            public string JE_REMAIN { get; set; }

            /// <summary>
            /// 院内预交金流水号
            /// </summary>
            public string HOS_PAY_SN { get; set; }

            public string HIS_RTNXML { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string PARAMETERS { get; set; }
        }
    }
}