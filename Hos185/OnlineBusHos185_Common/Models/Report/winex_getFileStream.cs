using System;
using System.Collections.Generic;
using System.Text;

namespace Hos185_His.Models.Report
{

    /// <summary>
    /// 卫宁报告下载
    /// </summary>
    public class winex_getFileStream
    {

        public string applyno { get; set; }// 报告单号
        public string barcode { get; set; }// 条形码string
        public string examcode { get; set; }// 报告类别代码
        public string examname { get; set; }// 报告类别名称string
        public string pdfbase64str { get; set; }//Pdf base64 字符串 string Pdf base64 文件流
        public string pdffilename { get; set; }// Pdf 文件名称string
        public string printflag { get; set; }// 打印标识string 打印标识“1”代表已经打印过, “0”代表未打印过
        public string pubdatetime { get; set; }//报告时间string 格式“YYYY-MM-DD HH:mm:SS”
        public string techno { get; set; }// 样本号string
        public string message { get; set; }// 结果描述
        public string statusCode { get; set; }//


    }
}
