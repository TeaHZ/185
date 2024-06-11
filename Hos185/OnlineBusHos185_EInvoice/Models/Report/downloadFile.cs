using System;
using System.Collections.Generic;
using System.Text;

namespace Hos185_His.Models.Report
{
    public class downloadFile
    {

        public string filePath { get; set; }
        public string inspectType { get; set; }//检验检查列表返回的inspectType   检查类型枚举   InspectTypeEnum 
        public string reportId { get; set; }//检验检查列表返回的报告id  inspectNo              
        public string visitType { get; set; }


      

    }
    public class downloadFileData
    {
        public string fileBase64String { get; set; }

        public List<string> fileBase64List { get; set; }
    }
}

