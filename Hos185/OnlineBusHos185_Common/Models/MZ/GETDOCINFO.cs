using System;
using System.Collections.Generic;
using System.Text;

namespace Hos185_His.Models.MZ
{
    /// <summary>
    /// 获取医⽣基本信息
    /// </summary>
    internal class GETDOCINFO
    {
        /// <summary>
        /// 科室code
        /// </summary>
        public string deptid { get; set; }


    }

    public class GETDOCINFODATA
    {
        /// <summary>
        /// 
        /// </summary>
        public string certNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string deptid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string deptname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string docid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string docname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string docsex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goodat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hisdocid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string idcard { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string idcardtype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string orgid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picture { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string titleremark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tjdocflag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string username { get; set; }
    }



}
