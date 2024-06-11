using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos968_OutHos.Model
{
    class WXZFBTF_M
    {
        public class WXZFBTF_IN
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
            /// 交易流水号
            /// </summary>
            public string QUERYID { get; set; }
            /// <summary>
            /// 支付方式
            /// </summary>
            public string DEAL_TYPE { get; set; }

            /// <summary>
            /// 退费金额
            /// </summary>
            public string CASH_JE { get; set; }

            /// <summary>
            /// 退费原因
            /// </summary>
            public string REASON { get; set; }

            /// <summary>
            /// 姓名
            /// </summary>
            public string PAT_NAME { get; set; }
            /// <summary>
            /// 身份证号
            /// </summary>
            public string SFZ_NO { get; set; }
            /// <summary>
            /// 患者院内唯一索引
            /// </summary>
            public string HOSPATID { get; set; }

            /// <summary>
            /// 数据来源
            /// </summary>
            public string SOURCE { get; set; }
            /// <summary>
            /// 其他条件
            /// </summary>
            public string FILTER { get; set; }
        }

        public class WXZFBTF_OUT
        {
            /// <summary>
            /// 交易状态
            /// </summary>
            public string STATUS { get; set; }
            /// <summary>
            /// 退款流水号
            /// </summary>
            public string OUT_REQUEST_NO { get; set; }
            /// <summary>
            /// HIS交易出参
            /// </summary>
            public string HIS_RTNXML { get; set; }
            /// <summary>
            /// 其他条件
            /// </summary>
            public string PARAMETERS { get; set; }
        }
    }
}
