using System;
using System.Collections.Generic;
using System.Text;

namespace Hos185_His.Models.OriginPay
{
    public class P0103
    {
        /// <summary>
        /// 
        /// </summary>
        public string tradeChannel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string outTradeNo { get; set; }
        /// <summary>
        /// ⼩程序⽀付测试
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal totalFee { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 调⽤⽅机器mac地址/ip地址
        /// </summary>
        public string macNumber { get; set; }
        /// <summary>
        /// 患者证件号
        /// </summary>
        public string identityId { get; set; }
        /// <summary>
        /// 患者⼿机号
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string operCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string notifyUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int payActiveTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cardNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string optIptNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recipeNoList { get; set; }
    }


    public class P0103DATA
    {
        /// <summary>
        /// 
        /// </summary>
        public string codeUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string outTradeNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string transactionId { get; set; }
    }

}
