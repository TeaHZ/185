using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos185.Models.OriginPay
{
    internal class ocPayOrder
    {



        public string tradeChannel { get; set; }//yqtyzf", //交易渠道 参考附录【交易渠道】
        public string outTradeNo { get; set; }//1662597711456", //商⼾订单号
        public string description { get; set; }//⼩程序⽀付测试", //订单描述
        public decimal totalFee { get; set; }//1, //⾦额（单位：元）
        public string name { get; set; }//患者姓名",
        public string macNumber { get; set; }//调⽤⽅机器mac地址/ip地址",


        public string identityId { get; set; }//患者证件号",
        public string mobile { get; set; }//患者⼿机号",
        public string type { get; set; }//reg", //交易类型 参考附录【交易类型】
        public string userId { get; set; }//2088112423142389", //userId买家id渠道⾃⾏获取
                                          //以下为可选项
        public string operCode { get; set; }//hlwyy", //操作者 如果终端对应多个操作者(⽐如HIS窗⼝），必填
        public string notifyUrl { get; set; }//https://xx.xx.xx", //⽀付成功回调地址 具体⻅【5.⽀付成功回调】
        public int payActiveTime { get; set; } //2, //⽀付有效期（单位：分）默认2分钟
        public string cardNo { get; set; }//10000456", //患者ID
        public string optIptNo { get; set; }//1000787", //⻔诊流⽔号/住院流⽔号
        public string recipeNoList { get; set; }//3455,678" //⻔诊缴费清单号，多个清单⽤,分割

    }

    public class ocPayOrderData
    {
        public string outTradeNo { get; set; } //商⼾订单号
        public string transactionId { get; set; } //⽀付中台订单号
        public string tradeNO { get; set; } //⽀付宝⼩程序⽀付单号
    }
}
