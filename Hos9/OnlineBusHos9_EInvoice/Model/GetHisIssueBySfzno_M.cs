using System.Collections.Generic;

namespace OnlineBusHos9_EInvoice.Model
{
    public class GetHisIssueBySfzno_IN
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
        /// 医疗卡类型
        /// </summary>
        public string YLCARD_TYPE { get; set; }

        /// <summary>
        /// 医疗卡号
        /// </summary>
        public string YLCARD_NO { get; set; }

        /// <summary>
        /// 病人院内号
        /// </summary>
        public string HOSPATID { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public string BEGIN_DATE { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public string END_DATE { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string SFZ_NO { get; set; }

        /// <summary>
        /// 0/空：正常打印 1：补打
        /// </summary>
        public string IS_REPRINT { get; set; }

        /// <summary>
        /// 其他条件
        /// </summary>
        public string FILTER { get; set; }
    }

    public class GetHisIssueBySfzno_OUT
    {
        public List<Hisissuelist> HISISSUELISTS { get; set; }

        public class Hisissuelist
        {
            /// <summary>
            /// 票据代码
            /// </summary>
            public string INVOICE_CODE { get; set; }

            /// <summary>
            /// 电子票据号码
            /// </summary>
            public string INVOICE_NUMBER { get; set; }

            /// <summary>
            /// 开票单位名称
            /// </summary>
            public string INVOICING_PARTY_NAME { get; set; }

            /// <summary>
            /// 交款人名称
            /// </summary>
            public string PAYER_PARTY_NAME { get; set; }

            /// <summary>
            /// 总金额
            /// </summary>
            public string TOTAL_AMOUNT { get; set; }

            /// <summary>
            /// 收费类型名称
            /// </summary>
            public string SFTYPENAME { get; set; }

            /// <summary>
            /// 票据状态
            /// </summary>
            public string STATUS { get; set; }

            /// <summary>
            /// 开具时间
            /// </summary>
            public string SAVEDDATE_TIME { get; set; }

            /// <summary>
            /// 是否已打印
            /// </summary>
            public string ISPRINT { get; set; }

            public string print_times { get; set; }
            public string IS_ISSUE { get; internal set; }
            public string IS_CHECK { get; internal set; }
            public string SETTLECODE { get; internal set; }
            public string BUSINESS_TYPE { get; internal set; }
            public string INPATIENTNO { get; internal set; }
        }
    }
}