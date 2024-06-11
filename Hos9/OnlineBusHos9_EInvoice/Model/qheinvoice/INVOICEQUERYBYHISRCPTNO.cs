using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_EInvoice.Model.qheinvoice
{
    public class INVOICEQUERYBYHISRCPTNO
    {
        public string HOS_ID { get; set; } 
        public string rcpt_no { get; set; } 
    }


    public class RCPTLISTItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string his_rcpt_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string invoice_code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string invoice_number { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string issue_date { get; set; }
        /// <summary>
        /// xxx医院
        /// </summary>
        public string invoicing_party_name { get; set; }
        /// <summary>
        /// 张三
        /// </summary>
        public string payer_party_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string total_amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isprint { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string invoice_status { get; set; }
    }

    public class INVOICEQUERYBYHISRCPTNODATA
    {
        /// <summary>
        /// 
        /// </summary>
        public List<RCPTLISTItem> RCPTLIST { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CLBZ { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CLJG { get; set; }
    }

}
