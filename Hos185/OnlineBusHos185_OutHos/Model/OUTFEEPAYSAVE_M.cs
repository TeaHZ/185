using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBusHos185_OutHos.Model
{
    class OUTFEEPAYSAVE_M
    {
        public class OUTFEEPAYSAVE_IN
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
            /// 平台记录唯一标识
            /// </summary>
            public string PAY_ID { get; set; }
            /// <summary>
            /// 门诊号
            /// </summary>
            public string OPT_SN { get; set; }
            /// <summary>
            /// 院内本次处方唯一流水号
            /// </summary>
            public string HOS_SN { get; set; }
            /// <summary>
            /// 处方号
            /// </summary>
            public string PRE_NO { get; set; }
            /// <summary>
            /// 总金额
            /// </summary>
            public string JEALL { get; set; }
            /// <summary>
            /// 现金支付金额
            /// </summary>
            public string CASH_JE { get; set; }
            /// <summary>
            /// 交易方式
            /// </summary>
            public string DEAL_TYPE { get; set; }
            /// <summary>
            /// 交易流水号
            /// </summary>
            public string QUERYID { get; set; }
            /// <summary>
            /// 交易状态
            /// </summary>
            public string DEAL_STATES { get; set; }
            /// <summary>
            /// 交易时间
            /// </summary>
            public string DEAL_TIME { get; set; }
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

            public string SJH { get; set; }
            public string SETTLE_TYPE { get;  set; }
            public string TKBILL_TYPE { get;  set; }
        }
        public class OUTFEEPAYSAVE_OUT
        {
            public string HOS_PAY_SN { get; set; }
            public string RCPT_NO { get; set; }

            public string HOS_REG_SN { get; set; }

            public string OPT_SN { get; set; }

            public string HIS_RTNXML { get; set; }
            public string PARAMETERS { get; set; }
            public string DJ_ID { get;  set; }
        }

        public class OUTFEEPAYSAVE_ERROR
        {
            public string TF_RESULT { get; set; }
        }

    }
}
