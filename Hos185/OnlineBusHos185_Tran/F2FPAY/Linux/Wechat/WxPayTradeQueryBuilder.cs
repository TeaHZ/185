using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos185_Tran.F2FPAY.Linux
{
    public class WxPayTradeQueryBuilder : JsonBuilder
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
        /// 用户标识	openid	是	String(128)	oUpF8uMuAJO_M2pxb1Q9zNjWeS6o	用户在商户appid下的唯一标识
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 交易类型	trade_type	是	String(16)	JSAPI	调用接口提交的交易类型，取值如下：JSAPI，NATIVE，APP，MICROPAY，详细说明见参数规定
        /// </summary>
        public string trade_type { get; set; }

        /// <summary>
        /// 交易状态SUCCESS—支付成功  REFUND—转入退款   NOTPAY—未支付  CLOSED—已关闭  REVOKED—已撤销（刷卡支付） USERPAYING--用户支付中 PAYERROR--支付失败(其他原因，如银行返回失败)
        /// </summary>
        public string trade_state { get; set; }


        /// <summary>
        /// 订单金额 入参 total_fee 是   100.00 订单总金额，单位为元
        /// </summary>
        public decimal total_fee { get; set; }
        /// <summary>
        /// *商户订单号 out_trade_no 是 String(32) 1212321211201407033568112322 商户系统的订单号，与请求一致。 
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// *微信支付订单号 transaction_id 是 String(32) 1217752501201407033233368018 微信支付订单号
        /// </summary>
        public string transaction_id { get; set; }
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
        /// 接收到的数据
        /// </summary>
        public string DateRe { get; set; }


        public override bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
