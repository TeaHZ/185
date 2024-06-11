using System;
using System.Collections.Generic;
using System.Text;

namespace Hos185_His.Models.MZ
{
    public class GETCHSREGSAVE
    {
        /// <summary>
        /// 
        /// </summary>
        public string hosId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hosSn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lterminalSn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mdtrtCertType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mdtrtCertNO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string chsInput2206 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string chsOutput2206 { get; set; }
    }
    public class GETCHSREGSAVEDATA
    {
        /// <summary>
        /// 
        /// </summary>
        public string chsInput2207 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ybdjh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mzno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zfamtOuthp { get; set; }
    }

}
