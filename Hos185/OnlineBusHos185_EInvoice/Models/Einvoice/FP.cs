using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hos185_His.Models.Einvoice
{

    public class EInvoiceDirectQuery
    {

        public string invoiceNo { get; set; }
        public string invoiceType { get; set; }
        public string settleCode { get; set; }
        public string invoiceSource { get; set; }



    }

    public class EInvoiceDirectQueryData
    {
        /// <summary>
        /// 
        /// </summary>
        public string downLoadMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string invoiceCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string invoiceNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string invoiceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pdfInvoice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pdfUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string settleCode { get; set; }

        /// <summary>
        /// 2 成功，非2 提示
        /// </summary>
        public string status { get; set; }
    }



}
