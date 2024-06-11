using System.Collections.Generic;

namespace ZZJ_InHos.Model
{
    internal class JZHOUTJS_M
    {
        public class JZHOUTJS_IN
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
            /// 单据唯一ID
            /// </summary>
            public string DJ_ID { get; set; }

            /// <summary>
            /// 单据号
            /// </summary>
            public string DJ_NO { get; set; }

            /// <summary>
            /// 预交金
            /// </summary>
            public string JE_YJJ { get; set; }

            /// <summary>
            /// 余额
            /// </summary>
            public string JE_REMAIN { get; set; }

            /// <summary>
            /// 总费用
            /// </summary>
            public string JE_ALL { get; set; }

            /// <summary>
            /// 医保金额
            /// </summary>
            public string YB_PAY { get; set; }

            /// <summary>
            /// 现金金额
            /// </summary>
            public string CASH_JE { get; set; }

            /// <summary>
            /// 医保出参明细
            /// </summary>
            public string YBPAY_MX { get; set; }

            /// <summary>
            /// 支付方式
            /// </summary>
            public string DEAL_TYPE { get; set; }

            /// <summary>
            /// 流水号
            /// </summary>
            public string QUERYID { get; set; }

            /// <summary>
            /// 数据来源
            /// </summary>
            public string SOURCE { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string FILTER { get; set; }
        }

        public class JZHOUTJS_OUT
        {
            /// <summary>
            /// 单据流水号
            /// </summary>
            public string HOS_PAY_SN { get; set; }

            /// <summary>
            /// 发票号
            /// </summary>
            public string RCPT_NO { get; set; }

            public List<REFUND> REFUNDLIST { get; set; }

            public string HIS_RTNXML { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string PARAMETERS { get; set; }
        }

        public class REFUND
        {
            /// <summary>
            /// 金额
            /// </summary>
            public string JE { get; set; }

            /// <summary>
            /// 交易方式
            /// </summary>
            public string DEAL_TYPE { get; set; }

            /// <summary>
            /// 流水号
            /// </summary>
            public string QUERYID { get; set; }
        }
    }
}