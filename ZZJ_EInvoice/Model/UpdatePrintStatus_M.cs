namespace ZZJ_EInvoice.Class
{
    internal class UpdatePrintStatus_IN
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
        /// 票据代码
        /// </summary>
        public string INVOICE_CODE { get; set; }

        /// <summary>
        /// 电子票据号码
        /// </summary>
        public string INVOICE_NUMBER { get; set; }

        /// <summary>
        /// 其他条件
        /// </summary>
        public string FILTER { get; set; }
    }
}