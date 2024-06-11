using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos185_Tran.F2FPAY.Linux
{
    public class WxPayTradeRefundBuilder : JsonBuilder
    {

        /// <summary>
        /// 商户号 mch_id 是 String(32) 1900000109 微信支付分配的商户号 
        /// </summary>
        public string mch_id { get; set; }
        /// <summary>
        /// *公众账号ID appid 是 String(32) wx8888888888888888 微信分配的公众账号ID（企业号corpid即为此appId）
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// //业务结果 result_code 是 String(16) SUCCESS SUCCESS/FAIL 
        /// </summary>
        public string result_code { get; set; }
        /// <summary>
        /// 申请退款金额 入参 total_fee 是   100.00 订单总金额，单位为元
        /// </summary>
        public decimal refund_fee { get; set; }

        /// <summary>
        /// *商户订单号 out_trade_no 是 String(32) 1212321211201407033568112322 商户系统的订单号，与请求一致。 
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// *微信支付订单号 transaction_id 是 String(32) 1217752501201407033233368018 微信支付订单号
        /// </summary>
        public string transaction_id { get; set; }

        /// <summary>
        ///    商户退款单号	out_refund_no	是	String(32)	1217752501201407033233368018	商户退款单号
        /// </summary>
        public string out_refund_no { get; set; }

        /// <summary>
        /// 微信退款单号	refund_id	是	String(28)	1217752501201407033233368018	微信退款单号
        /// </summary>
        public string refund_id { get; set; }

        /// <summary>
        /// 实际退款金额	 
        /// </summary>
        public decimal settlement_refund_fee { get; set; }



        /// <summary>
        /// 退款状态	refund_status_$n	是	String(16)	SUCCESS	
        ///退款状态：
        ///SUCCESS—退款成功
        ///FAIL—退款失败
        ///PROCESSING—退款处理中
        ///NOTSURE—未确定，需要商户原退款单号重新发起
        ///CHANGE—转入代发，退款到银行发现用户的卡作废或者冻结了，导致原路退款银行卡失败，资金回流到商户的现金帐号，需要商户人工干预，通过线下或者财付通转账的方式进行退款。
        /// </summary>
        public string refund_status { get; set; }




        /// <summary>
        /// 支付完成时间 time_end 是 String(14) 20141030133525 支付完成时间，格式为yyyyMMddHHmmss，
        /// </summary>
        public string time_end { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string return_msg { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public string err_code { get; set; }
        /// <summary>
        /// 错误代码描述
        /// </summary>
        public string err_code_des { get; set; }

        /// <summary>
        /// 商品描述 入参
        /// </summary>
        public string in_body { get; set; }

        /// <summary>
        /// 扫码二维码 入参
        /// </summary>
        public string in_auth_code { get; set; }

        /// <summary>
        /// 接收到的数据
        /// </summary>
        public string DateRe { get; set; }

        /// <summary>
        /// 撤销结果
        /// </summary>
        public bool CalcelResult { get; set; }


        public override bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
