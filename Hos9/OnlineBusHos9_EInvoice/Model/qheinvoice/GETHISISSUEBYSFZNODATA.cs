using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_EInvoice.Model.qheinvoice
{

    public class HisissuelistItem
    {
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
        public string total_amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string invoicing_party_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string payer_party_name { get; set; }
        /// <summary>
        /// 挂号
        /// </summary>
        public string sftypename { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string STATUS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string saveddate_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isprint { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string InvoiceType { get; set; }
    }

    public class GETHISISSUEBYSFZNODATA
    {
        /// <summary>
        /// 
        /// </summary>
        public List<HisissuelistItem> hisissuelist { get; set; }
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
