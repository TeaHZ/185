namespace OnlineBusHos9_GJYB.Model
{
    class GJYB_REFUND_M
    {
        public class GJYB_REFUND_IN
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
            /// 就诊ID
            /// </summary>
            public string MDTRT_ID { get; set; }
            /// <summary>
            /// 结算ID
            /// </summary>
            public string SETL_ID { get; set; }
            /// <summary>
            /// 读卡出参
            /// </summary>
            public string PAT_CARD_OUT { get; set; }
            /// <summary>
            /// 读卡出参
            /// </summary>
            public string DEPT_CODE { get; set; }
            /// <summary>
            /// 读卡出参
            /// </summary>
            public string DOC_NO { get; set; }
            /// <summary>
            /// 读卡出参
            /// </summary>
            public string SCH_DATE { get; set; }
            /// <summary>
            /// 读卡出参
            /// </summary>
            public string SCH_TYPE { get; set; }
            /// <summary>
            /// 读卡出参
            /// </summary>
            public string PRO_TITLE { get; set; }
            /// <summary>
            /// 个人编号
            /// </summary>
            public string PSN_NO { get; set; }
            /// <summary>
            /// 总金额
            /// </summary>
            public string JE_ALL { get; set; }
            /// <summary>
            /// 预约流水号
            /// </summary>
            public string HOS_SN { get; set; }
            /// <summary>
            /// 挂号/门诊
            /// </summary>
            public string ISGH { get; set; }

            public string FILTER { get; set; }

        }

        public class GJYB_REFUND_OUT
        {
            /// <summary>
            /// 就诊ID
            /// </summary>
            public string MDTRT_ID { get; set; }
            /// <summary>
            /// 总金额
            /// </summary>
            public string MEDFEE_SUMAMT { get; set; }
            /// <summary>
            /// 账户支出
            /// </summary>
            public string ACCT_PAY { get; set; }
            /// <summary>
            /// 现金支出
            /// </summary>
            public string PSN_CASH_PAY { get; set; }
            /// <summary>
            /// 统筹支出
            /// </summary>
            public string FUND_PAY_SUMAMT { get; set; }
            /// <summary>
            /// 其他支出
            /// </summary>
            public string OTH_PAY { get; set; }
            /// <summary>
            /// 余额
            /// </summary>
            public string BALC { get; set; }
            /// <summary>
            /// 其他参数
            /// </summary>
            public string PARAMETER { get; set; }

        }
    }
}
