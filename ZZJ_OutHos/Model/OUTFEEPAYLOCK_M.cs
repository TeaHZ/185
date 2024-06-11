using System.Collections.Generic;

namespace ZZJ_OutHos.Model
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
            /// 数据来源
            /// </summary>
            public string SOURCE { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string FILTER { get; set; }
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
        }

        public class OUTFEEPAYLOCK_OUT
        {
            /// <summary>
            /// 平台记录唯一标识
            /// </summary>
            public string PAY_ID { get; set; }

            public string HIS_RTNXML { get; set; }
            public string PARAMETERS { get; set; }
        }
    }
}