using System.Collections.Generic;

namespace ZZJ_InHos.Model
{
    public class GETPATHOSINFO_M
    {
        public class GETPATHOSINFO_IN
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
            /// 身份证
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
        }

        public class GETPATHOSINFO_OUT
        {
            /// <summary>
            /// 住院号
            /// </summary>
            public string HOS_NO { get; set; }

            /// <summary>
            /// 住院患者索引号
            /// </summary>
            public string HOS_PAT_ID { get; set; }

            /// <summary>
            /// 姓名
            /// </summary>
            public string PAT_NAME { get; set; }

            /// <summary>
            /// 床号
            /// </summary>
            public string BED_NO { get; set; }

            /// <summary>
            /// 病区
            /// </summary>
            public string DEPT_NAME { get; set; }

            /// <summary>
            /// 年龄
            /// </summary>
            public string AGE { get; set; }

            /// <summary>
            /// 已缴纳金额
            /// </summary>
            public string JE_PAY { get; set; }

            /// <summary>
            /// 已经发生总费用
            /// </summary>
            public string JE_YET { get; set; }

            /// <summary>
            /// 预交金余额
            /// </summary>
            public string JE_REMAIN { get; set; }

            /// <summary>
            /// 是否可缴纳预交金
            /// </summary>
            public string CAN_PAY { get; set; }

            public List<PAY> PAYLIST { get; set; }
            public List<FEE> FEELIST { get; set; }

            public string HIS_RTNXML { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string PARAMETERS { get; set; }
        }

        public class PAY
        {
            /// <summary>
            /// 时间
            /// </summary>
            public string HIN_TIME { get; set; }

            /// <summary>
            /// 费用说明
            /// </summary>
            public string JE_NOTE { get; set; }

            /// <summary>
            /// 金额
            /// </summary>
            public string JE { get; set; }
        }

        public class FEE
        {
            /// <summary>
            /// 费用类别
            /// </summary>
            public string FEE_NOTE { get; set; }

            /// <summary>
            /// 金额
            /// </summary>
            public string JE { get; set; }
        }
    }
}