using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_InHos.HISModels
{
    internal class T1187
    {
        public class Input
        {
            /// <summary>
            /// 支付方式
            /// </summary>
            public string payChannel { get; set; }
            /// <summary>
            /// 第三方支付总金额
            /// </summary>
            public string selfPayAmount { get; set; }
            /// <summary>
            /// 支付平台流水号
            /// </summary>
            public string thirdPartHisNo { get; set; }
            /// <summary>
            /// 支付平台订单号
            /// </summary>
            public string tradeNo { get; set; }
            /// <summary>
            /// 支付平台appId
            /// </summary>
            public string appId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string defrayNo { get; set; }
            /// <summary>
            /// 第三方支付流水号
            /// </summary>
            public string channelTradeNo { get; set; }
            /// <summary>
            /// 住院流水号
            /// </summary>
            public string visitNo { get; set; }
            /// <summary>
            /// 患者ID
            /// </summary>
            public string patientId { get; set; }
            /// <summary>
            /// 操作员ID
            /// </summary>
            public string operatorId { get; set; }
            public string duKaFS { get; set; }
            public string zhengJianHM { get; set; }
        }
        public class Data
        {
            /// <summary>
            /// 自费
            /// </summary>
            public string chargeType { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double gongJiZhangHuAmount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double insPayAmount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string insPayInfo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double otherAmount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double otherInsAmount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string patientId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double selfPayAmount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double tongChouAmount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double totalCharges { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double totalNeedPay { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double totalPrepayment { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double zhangHuAmount { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double zhangHuYE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> settleNoList { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<string> rcptNoList { get; set; }
        }

    }
}
