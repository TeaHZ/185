using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos185_EInvoice.Model
{
    public class EinvoiceIssue_IN
    {
        /// <summary>
        /// 
        /// </summary>
        public string HOS_ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string USER_ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LTERMINAL_SN { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SOURCE { get; set; }
  
        /// <summary>
        /// 
        /// </summary>
        public string FILTER { get; set; }

        public string ISSUELIST { get; set; }
        public string invoiceSource { get; set; }


    }
    public class EinvoiceIssueInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string INVOICE_CODE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string INVOICE_NUMBER { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string INVOICING_PARTY_NAME { get; set; }
        /// <summary>
        /// 刘军伟
        /// </summary>
        public string PAYER_PARTY_NAME { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TOTAL_AMOUNT { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SFTYPE { get; set; }
        /// <summary>
        /// 挂号
        /// </summary>
        public string SFTYPENAME { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string STATUS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SAVEDDATE_TIME { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ISPRINT { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IS_CHECK { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BIZ_CODE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string IS_ISSUE { get; set; }
        /// <summary>
        /// 未开立
        /// </summary>
        public string IS_ISSUE_NAME { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ISSUE_COLOR { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SETTLECODE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BUSINESS_TYPE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string INPATIENTNO { get; set; }
        public string invoiceSource { get; set; }
    }

}
