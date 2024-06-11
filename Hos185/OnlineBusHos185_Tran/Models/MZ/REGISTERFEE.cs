using System;
using System.Collections.Generic;
using System.Text;

namespace Hos185_His.Models.MZ
{
    public class REGISTERFEE
    {
        /// <summary>
        /// 
        /// </summary>
        public string medicareParam { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pactCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string patientID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string scheduleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string vipCardNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string vipCardType { get; set; }

        public string preid { get; set; }
    }
    public class REGISTERFEEDATA
    {
        /// <summary>
        /// 
        /// </summary>
        public decimal actualFee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal actualRegFee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal actualTreatFee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string medicareParam { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pactCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string patientID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string receiptNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal regFee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string scheduleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal totalFee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal treatFee { get; set; }
        /// <summary>
        /// 挂号序号
        /// </summary>
        public string ghxh { get; set; }
    }

}
