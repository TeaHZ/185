using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos185_Tran.Model
{
    class PAYCANCEL_M
    {
        public class PAYCANCEL_IN
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
            /// 交易方式
            /// </summary>
            public string DEAL_TYPE { get; set; }
            /// <summary>
            /// 交易流水号
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

        public class PAYCANCEL_OUT
        {
            /// <summary>
            /// 交易状态
            /// </summary>
            public string STATUS { get; set; }
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
