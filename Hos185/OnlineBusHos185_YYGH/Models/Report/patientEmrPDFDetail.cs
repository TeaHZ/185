using System;
using System.Collections.Generic;
using System.Text;

namespace Hos185_His.Models.Report
{
    public class patientEmrPDFDetail
    {
        /// <summary>
        /// 
        /// </summary>
        public string recordId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sourceType { get; set; }
    }
    public class patientEmrPDFDetailData
    {
        /// <summary>
        /// 
        /// </summary>
        public string pdfData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pdfStatus { get; set; }
    }

}
