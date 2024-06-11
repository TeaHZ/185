using System.Collections.Generic;

namespace OnlineBusHos9_OutHos.Model
{
    internal class OUTFEEPAYLOCK_M
    {
        public class OUTFEEPAYLOCK_IN
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
            ///
            /// </summary>
            public string YLCARD_TYPE { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string YLCARD_NO { get; set; }

            public List<PRE> PRELIST { get; set; }

            /// <summary>
            /// 患者院内唯一索引
            /// </summary>
            public string HOSPATID { get; set; }

            /// <summary>
            /// 身份证号
            /// </summary>
            public string SFZ_NO { get; set; }

            /// <summary>
            /// 姓名
            /// </summary>
            public string PAT_NAME { get; set; }

            /// <summary>
            /// 数据来源
            /// </summary>
            public string SOURCE { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string FILTER { get; set; }

            public string BUS_CARD_INFO { get; set; }

            public string CARD_INFO { get; set; }

            public string MDTRT_CERT_NO { get; set; }
            /// <summary>
            /// 01 医保电子凭证 02 身份证 03社会保障卡卡号 04 电子社保卡
            /// </summary>
            public string MDTRT_CERT_TYPE { get; set; }
        }

        public class PRE
        {
            /// <summary>
            /// 病人门诊号
            /// </summary>
            public string OPT_SN { get; set; }

            /// <summary>
            /// 处方号
            /// </summary>
            public string PRE_NO { get; set; }

            /// <summary>
            /// 院内本次处方唯一流水号
            /// </summary>
            public string HOS_SN { get; set; }

            /// <summary>
            /// 核酸缴费模板ID
            /// </summary>
            public string MB_ID { get; set; }
        }

        public class OUTFEEPAYLOCK_OUT
        {
            /// <summary>
            /// 平台记录唯一标识
            /// </summary>
            public string PAY_ID { get; set; }

            public string HIS_RTNXML { get; set; }
            public string PARAMETERS { get; set; }

            public string CASH_JE { get; set; }
            public string JEALL { get; set; }
            public string LCSYAMOUNT { get; set; }//临床试验 费用全免
       
            public string SJH { get; set; }
            public string MEDFEE_SUMAMT { get; set; }
            public string ACCT_PAY { get; set; }
            public string PSN_CASH_PAY { get; set; }
            public string FUND_PAY_SUMAMT { get; set; }
            public string OTH_PAY { get; set; }
            public string BALC { get; set; }

            public string ACCT_MULAID_PAY { get; set; }

        }
    }
}