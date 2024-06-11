using System;
using System.Collections.Generic;
using System.Text;

namespace Hos185_His.Models.OriginPay
{
    public class P0102
    {
        /// <summary>
        /// 
        /// </summary>
        public string tradeChannel { get; set; }
        /// <summary>
        /// 公众号/⼩程序应⽤id
        /// </summary>
        public string appId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string openId { get; set; }
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
        public int totalFee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tradeType { get; set; }
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


    public class MiniPayRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public string package { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string nonceStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string timeStamp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string paySign { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string appId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string signType { get; set; }
    }

    public class P0102DATA
    {
        /// <summary>
        /// 
        /// </summary>
        public string outTradeNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string transactionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MiniPayRequest miniPayRequest { get; set; }
    }

}
