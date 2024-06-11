using System;
using System.Collections.Generic;
using System.Text;

namespace Hos185_His.Models.Report
{
    public class outPatientEmrInfo
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
        public string days { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string idCardNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string idCardType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mcardNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mcardNoType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recordType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string signState { get; set; }
    }
    public class outPatientEmrInfoData
    {
        /// <summary>
        /// 打印标识:0:未打印,1:已打印）
        /// </summary>
        public string printState { get; set; }
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
        public string deptCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string deptName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string diagnosisName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hpi { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string invoiceNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isPrint { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ph { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recordId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recordName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recordType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string regDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string seeDoctCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string seeDoctName { get; set; }

        public string sourceType { get; set; }
    }

}
