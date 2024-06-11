using System;
using System.Collections.Generic;
using System.Text;

namespace Hos185_His.Models.MZ
{
    public class REGISTERPAYCANCEL
    {
        /// <summary>
        /// 
        /// </summary>
        public string apointMentCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cardNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string clinicCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string merchantOrderNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string operCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int returnFee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rrn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sourceType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string thirdOrderNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string transSerialNo { get; set; }
    }
    public class REGISTERPAYCANCELDATA
    {
        /// <summary>
        /// 
        /// </summary>
        public string cardNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string clinicCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string invoiceNo { get; set; }
    }

}
