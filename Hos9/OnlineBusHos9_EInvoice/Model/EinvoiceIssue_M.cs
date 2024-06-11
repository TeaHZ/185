using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_EInvoice.Model
{
    public class EinvoiceIssue_IN
    {
        /// <summary>
        /// 
        /// </summary>
        public string lTERMINAL_SN { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string USER_ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string HOS_ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SOURCE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BUS_CARD_INFO { get; set; }
        /// <summary>
        /// 320200|320223194908110225|187960449|320200D156000005AD93DF27FA77FCDE|张洪珍|0081544C9786843202AD93DF27|3.00|20221114|20321114|000000000000|00010600202304000461|JSB040933497|6230661635022025571|
        /// </summary>
        public string CARD_INFO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MDTRT_CERT_TYPE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MDTRT_CERT_NO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string FILTER { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string GUID_HOS_DUP { get; set; }
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
        public string HOSPATID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SETTLECODE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RCPT_NO { get; set; }

        public string SFZ_NO { get; set; }
    }
    public class EinvoiceIssue_OUT
    {
        //public List<INVOICE> INVOICELIST { get; set; }
        /// <summary>
        /// 1:pdf的base64; 2:pdf的url 3:ftp文件地址 4:共享文件夹地址 5:直接访问浏览器 6:图片的url 7:图片base64
        /// </summary>
        public string DATA_TYPE { get; set; }
        /// <summary>
        /// 发票文件内容,base64 编码；
        /// </summary>
        public string INVOICEFILEDATA { get; set; }
        /// <summary>
        /// 发票清单文件内容,base64 编码；
        /// </summary>
        public string INVENTORYFILEDATA { get; set; }
        //public class INVOICE
        //{
        //    /// <summary>
        //    /// 发票文件内容,base64 编码；
        //    /// </summary>
        //    public string INVOICEFILEDATA { get; set; }
        //    /// <summary>
        //    /// 发票清单文件内容,base64 编码；
        //    /// </summary>
        //    public string INVENTORYFILEDATA { get; set; }

        //}
    }

}
