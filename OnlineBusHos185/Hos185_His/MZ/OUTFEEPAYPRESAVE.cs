using System;
using System.Collections.Generic;
using System.Text;

namespace Hos185_His.Models.MZ
{
    public class OUTFEEPAYPRESAVE
    {
        /// <summary>
        /// 
        /// </summary>
        public string hospitalcode { get; set; }
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
        public string medicareParam { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pactCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string regid { get; set; }

        /// <summary>
        /// 处方号合集,英文逗号分隔
        /// </summary>
        public string  recipeNos { get; set; }
    }



    public class OUTFEEPAYPRESAVEDATA
    {
        /// <summary>
        /// 
        /// </summary>
        public string amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string insuranceparameters { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string insurancetype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string insurancetypedes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ownCost { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> recipeNoList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string settlementinstructions { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string totCost { get; set; }


        public string accountAmount { get; set; }//账户余额:院内预付费账户的余额，对接卫宁需要
        public string accountPay { get; set; }//账户支付:院内预付费账户支付，对接卫宁需要
        public string comInsuranceCost { get; set; }//商保直赔金额:符合商保直赔患者报销金额，对接卫宁需要
        public string discountCost { get; set; }//优惠金额，对接卫宁需要
        public string medInsuranceCost { get; set; }//医保支付:医保报销金额，对接卫宁需要
        public string payableCost { get; set; }//应付金额:本次最终应该支付的金额（=自负金额-账户支付），应该使用支付宝或微信等支付的金额，对接卫宁需要 true
        public string receiptNumber { get; set; }//收据号，对接卫宁需要  true
    }

}
