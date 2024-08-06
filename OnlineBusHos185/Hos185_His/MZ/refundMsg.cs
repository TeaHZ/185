using System;
using System.Collections.Generic;
using System.Text;

namespace Hos185_His.Models.MZ
{
    internal class refundMsg
    {

        public string oldReceiptNumber { get; set; }//原收据号    N  原HIS内收据号
        public string patientName { get; set; }//患者姓名    Y  患者姓名，用于验证防止误操作
        public string receiptNumber { get; set; }//退费收据号   Y  HIS标记唯一一次退费的单据号。根据对应退费接口反馈的tsjh
        public string refundAmount { get; set; }//退费金额    Y  退费金额，用于验证
        public string refundNo { get; set; }//退费流水号   Y  支付金融机构交易流水号，如源启支付，支付宝、微信、银行等机构的原始退费流水号，用于对账
        public string refundPayType { get; set; }//退支付方式   Y

        public string balanceNo { get; set; }
    }
}
