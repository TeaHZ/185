using System;
using System.Collections.Generic;
using System.Text;

namespace Hos185_His.Models.MZ
{

 
    public class OUTFEEPAYSAVE
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
        public int existsThirdPay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lifeEquityCardNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lifeEquityCardType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MedicareInfo medicareInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string operCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pactCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recipeNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string terminalCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ThirdPayInfo thirdPayInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal totalFee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ybPay { get; set; }
        /// <summary>
        /// 应付金额:本次最终应该支付的金额（=自负金额-账户支付），应该使用支付宝或微信等支付的金额，对接卫宁需要 true
        /// </summary>

        public string payableCost { get; set; }//
        /// <summary>
        /// 收据号，对接卫宁需要  true
        /// </summary>
        public string receiptNumber { get; set; }//

        public string medicareParam { get; set; }

    }



    public class OUTFEEPAYSAVEDATA
    {
        /// <summary>
        /// 医院内部就诊卡号
        /// </summary>
        public string cardNo { get; set; }
        /// <summary>
        /// 挂号流⽔号
        /// </summary>
        public string clinicCode { get; set; }
        /// <summary>
        /// 发票号
        /// </summary>
        public string invoiceNo { get; set; }
        /// <summary>
        /// 发票序号
        /// </summary>
        public string invoiceSeqNo { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string name { get; set; }
    }

}
