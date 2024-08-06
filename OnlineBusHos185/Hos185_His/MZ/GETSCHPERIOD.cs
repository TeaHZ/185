using System;
using System.Collections.Generic;
using System.Text;

namespace Hos185_His.Models.MZ
{
    public class GETSCHPERIOD
    {
        public string beginTime { get; set; }
        public string deptCode { get; set; }
        public string doctCode { get; set; }
        public string doctName { get; set; }
        public string endTime { get; set; }
        public string noonCode { get; set; }
        public string regLevelCode { get; set; }
        public List<int> schemaIdList { get; set; }
        public string sourceType { get; set; }
    }


    public class GETSCHPERIODDATA
    {
        /// <summary>
        /// 
        /// </summary>
        public string beginTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string darpartId { get; set; }
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
        public string doctCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string doctName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string endTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string noonCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string noonName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int numcount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int numremain { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string regLevelCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string regLevelName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string schemaId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string seeDate { get; set; }
    }

}
