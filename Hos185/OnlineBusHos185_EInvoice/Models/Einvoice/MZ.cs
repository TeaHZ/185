using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hos185_His.Models.Einvoice
{
    public class MZ
    {
        public class MZ_IN
        {
            /// <summary>
            /// 
            /// </summary>
            public string cardNo { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string cardType { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string startTime { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string endTime { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string validFlag { get; set; }

        }

        public class DataItem
        {
            /// <summary>
            /// （打印标识:0:未打印,1:已打印）
            /// </summary>
            public string printState { get; set; }     
            private string _printFlagQH = "0";
            /// <summary>
            /// 
            /// </summary>
            public string cardNo { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string invoiceNo { get; set; }

            /// <summary>
            /// 自费
            /// </summary>
            public string pactName { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string clinicCode { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string totCost { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string ownCost { get; set; }

            /// <summary>
            /// 某某
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string sexCode { get; set; }

            /// <summary>
            /// 33岁
            /// </summary>
            public string age { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string operDate { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string printFlag { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string powerTranId { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string doctDept { get; set; }

            /// <summary>
            /// 骨科门诊
            /// </summary>
            public string doctDeptName { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string doctCode { get; set; }

            /// <summary>
            /// 徐广春
            /// </summary>
            public string doctName { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string invoiceSeq { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string feeDate { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string seeDate { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string seeNo { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string feeCpCD { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string confirmFlag { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string markNo { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string drugFlag { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string jsAeroNo { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string hxNo { get; set; }
            public string printFlagQH { get { return _printFlagQH; } set { _printFlagQH = value; } }
            public string jssjh { get; set; }
            public string invoiceSource { get; set; }

        }



    }
}
