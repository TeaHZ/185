using System;
using System.Collections.Generic;
using System.Text;

namespace Hos185_His.Models.Report
{
    public class inspectionCheckList
    {
        /// <summary>
        /// 
        /// </summary>
        public string bdate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string checkType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string edate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string idCard { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string idCardType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string medCardSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }

        public string blh { get; set; }

        public string inspectType { get; set; }    
    }


    public class ListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string barcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string eleReportStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string filePath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string inspectDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string inspectNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string inspectTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string inspectType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string itemId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string itemName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string nucleicAcidFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string src { get; set; }

        public string printState { get; set; }

        public string blReportType { get; set; }   

        public string testReportSource { get; set; }
    }

    public class inspectionCheckListData
    {
        /// <summary>
        /// 
        /// </summary>
        public string inspectDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ListItem> list { get; set; }
    }

}
