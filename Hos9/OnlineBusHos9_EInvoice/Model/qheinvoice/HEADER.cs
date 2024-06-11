using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_EInvoice.Model.qheinvoice
{
    public class HEADER
    {
        /// <summary>
        /// 
        /// </summary>
        public string MODULE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CZLX { get; set; } = "4";
        /// <summary>
        /// 
        /// </summary>
        public string TYPE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SOURCE { get; set; }
    }
}
