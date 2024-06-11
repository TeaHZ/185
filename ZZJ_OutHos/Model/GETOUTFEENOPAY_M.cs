using System.Collections.Generic;

namespace ZZJ_OutHos.Model
{
    public class GETOUTFEENOPAY_M
    {
        public class GETOUTFEENOPAY_IN
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
            /// 医疗卡号
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
        }

        public class GETOUTFEENOPAY_OUT
        {
            public List<PRE> PRELIST { get; set; }
            public string HIS_RTNXML { get; set; }
            public string PARAMETERS { get; set; }
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
            /// 科室代码
            /// </summary>
            public string DEPT_CODE { get; set; }

            /// <summary>
            /// 科室名称
            /// </summary>
            public string DEPT_NAME { get; set; }

            /// <summary>
            /// 医生代码
            /// </summary>
            public string DOC_NO { get; set; }

            /// <summary>
            /// 医生名称
            /// </summary>
            public string DOC_NAME { get; set; }

            /// <summary>
            /// 总金额
            /// </summary>
            public string JEALL { get; set; }

            /// <summary>
            /// 现金金额
            /// </summary>
            public string CASH_JE { get; set; }

            /// <summary>
            /// 医疗类别
            /// </summary>
            public string YLLB { get; set; }

            /// <summary>
            /// 疾病编码
            /// </summary>
            public string DIS_CODE { get; set; }

            /// <summary>
            /// 特殊病种
            /// </summary>
            public string DIS_TYPE { get; set; }

            /// <summary>
            /// 医保处方标志
            /// </summary>
            public string YB_PAY { get; set; }
        }
    }
}