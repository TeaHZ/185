namespace ZZJ_YYGH.Model
{
    internal class REGISTERPAYCANCEL_M
    {
        public class REGISTERPAYCANCEL_IN
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
            /// 院内预约唯一流水号
            /// </summary>
            public string HOS_SNAPPT { get; set; }

            /// <summary>
            /// 院内挂号唯一流水号
            /// </summary>
            public string HOS_SN { get; set; }

            /// <summary>
            /// 退费金额 如未支付则传0
            /// </summary>
            public string CASH_JE { get; set; }

            /// <summary>
            /// 交易方式
            /// </summary>
            public string DEAL_TYPE { get; set; }

            /// <summary>
            /// 交易状态
            /// </summary>
            public string DEAL_STATES { get; set; }

            /// <summary>
            /// 交易时间
            /// </summary>
            public string DEAL_TIME { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string FILTER { get; set; }
        }

        public class REGISTERPAYCANCEL_OUT
        {
            public string HIS_RTNXML { get; set; }

            /// <summary>
            /// 其他参数
            /// </summary>
            public string PARAMETER { get; set; }
        }
    }
}