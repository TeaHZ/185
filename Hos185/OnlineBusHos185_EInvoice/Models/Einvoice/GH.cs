using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hos185_His.Models.Einvoice
{
    public class GH
    {
        public class GH_IN
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
            public string validFlag { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string transType { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string ynSee { get; set; }

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
            public string idCardType { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string idCardNo { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string invoiceNo { get; set; }


        }

        public class GHData
        {

            private string _printFlagQH = "0";
            /// <summary>
            /// 
            /// </summary>
            public string clinicCode { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string cardNo { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string regDate { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string noonCode { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string noonCodeName { get; set; }

            /// <summary>
            /// 某某
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string idenNo { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string sexCode { get; set; }

            /// <summary>
            /// 男
            /// </summary>
            public string sexCodeName { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string birthday { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string paykindCode { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string paykindName { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string pactCode { get; set; }

            /// <summary>
            /// 省统一
            /// </summary>
            public string pactName { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string mcardNo { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string regLevelCode { get; set; }

            /// <summary>
            /// 普通号
            /// </summary>
            public string regLevelName { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string deptCode { get; set; }

            /// <summary>
            /// 骨科门诊
            /// </summary>
            public string deptName { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string seeNo { get; set; }

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
            public string seeDate { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string ynregchrg { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string ynBook { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string ynfr { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string regFee { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string chckFee { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string diagFee { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string othFee { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string ownCost { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string pubCost { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string payCost { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string validFlag { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string operCode { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string ynSee { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string checkFlag { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string relaPhone { get; set; }

            /// <summary>
            /// 江苏省南京市栖霞区尧化街道
            /// </summary>
            public string address { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string transType { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string cardType { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string beginTime { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string endTime { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string cancelOpcd { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string cancelDate { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string invoiceNo { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string recipeNo { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string appendFlag { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string orderNo { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string schemaNo { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string operDate { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string inSource { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string isEmergency { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string accountNo { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string markNo { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string payMode { get; set; }

            /// <summary>
            /// 现金
            /// </summary>
            public string payModeName { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string realOwnfee { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string printDate { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string inTimes { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string queryid { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string seeDoctCode { get; set; }

            /// <summary>
            /// 齐新生
            /// </summary>
            public string seeDoctName { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string isInvoiced { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string diagnosisCode { get; set; }

            /// <summary>
            /// 饮食性钙缺乏
            /// </summary>
            public string diagnosisName { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string payId { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string ticketReprint { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string tkVipTypeCode { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string tkVipTypeName { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string jsaeroNo { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string jsaeroDate { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string hxNo { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string hxDate { get; set; }

            /// <summary>
            /// 门诊二楼五诊区
            /// </summary>
            public string seeAddress { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string realInvoiceNo { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string proTitleName { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string proTitleCode { get; set; }

            public string printFlagQH { get { return _printFlagQH; } set { _printFlagQH = value; } }


            /// <summary>
            /// 打印标识:0:未打印,1:已打印
            /// </summary>
            public string printState { get; set; }

        }


    }

}
