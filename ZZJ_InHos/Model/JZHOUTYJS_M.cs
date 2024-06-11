using System.Collections.Generic;

namespace ZZJ_InHos.Model
{
    internal class JZHOUTYJS_M
    {
        public class JZHOUTYJS_IN
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
            /// 数据来源
            /// </summary>
            public string SOURCE { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string FILTER { get; set; }
        }

        public class JZHOUTYJS_OUT
        {
            /// <summary>
            /// 是否可以出院结算
            /// </summary>
            public string HIN_DATE { get; set; }

            /// <summary>
            /// 原因
            /// </summary>
            public string HOUT_DATE { get; set; }

            /// <summary>
            /// 住院天数
            /// </summary>
            public string HIN_DAYS { get; set; }

            /// <summary>
            /// 已经发生总费用
            /// </summary>
            public string JE_ALL { get; set; }

            /// <summary>
            /// 已缴费预交金金额
            /// </summary>
            public string JE_YJJ { get; set; }

            /// <summary>
            ///余额  负数:需要退 正数:需要补交
            /// </summary>
            public string JE_REMAIN { get; set; }

            /// <summary>
            /// 单据唯一ID
            /// </summary>
            public string DJ_ID { get; set; }

            /// <summary>
            /// 单据号
            /// </summary>
            public string DJ_NO { get; set; }

            /// <summary>
            /// 医保支付金额
            /// </summary>
            public string YB_PAY { get; set; }

            /// <summary>
            /// 现金金额
            /// </summary>
            public string CASH_JE { get; set; }

            /// <summary>
            /// 统筹支付金额
            /// </summary>
            public string TC_JE { get; set; }

            /// <summary>
            /// 大病支付
            /// </summary>
            public string DB_JE { get; set; }

            /// <summary>
            /// 个人账户支付金额
            /// </summary>
            public string ZH_JE { get; set; }

            /// <summary>
            /// 民政补助金额
            /// </summary>
            public string MZBZ_JE { get; set; }

            /// <summary>
            /// 个人自理金额
            /// </summary>
            public string GRZL_JE { get; set; }

            /// <summary>
            /// 个人自付金额
            /// </summary>
            public string GRZF_JE { get; set; }

            /// <summary>
            /// 医保出参明细
            /// </summary>
            public string YBPAY_MX { get; set; }

            /// <summary>
            /// 分户账
            /// </summary>
            public List<FEE> FEELIST { get; set; }

            public string HIS_RTNXML { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string PARAMETERS { get; set; }
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