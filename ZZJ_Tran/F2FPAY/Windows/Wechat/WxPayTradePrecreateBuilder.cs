using System;

namespace ZZJ_Tran.F2FPAY.Windows
{
    public class WxPayTradePrecreateBuilder : JsonBuilder
    {
        /// <summary>
        /// 订单金额 入参 total_fee 是   100.00 订单总金额，单位为元
        /// </summary>
        public decimal total_fee { get; set; }

        /// <summary>
        /// 商品描述 入参
        /// </summary>
        public string in_body { get; set; }

        /// <summary>
        /// *商户订单号 out_trade_no 是 String(32) 1212321211201407033568112322 商户系统的订单号，与请求一致。
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 商品ID trade_type=NATIVE，此参数必传。此id为二维码中包含的商品ID，商户自行定义。
        /// </summary>
        public string product_id { get; set; }

        /// <summary>
        /// 设备号
        /// </summary>
        public string device_info { get; set; }

        /// <summary>
        /// 交易结束时间
        /// </summary>
        public string time_expire { get; set; }

        /// <summary>
        /// 获得统一下单接口返回的二维码链接
        /// </summary>
        public string code_url { get; set; }

        /// <summary>
        /// //业务结果 result_code 是 String(16) SUCCESS SUCCESS/FAIL
        /// </summary>
        public string result_code { get; set; }

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
        /// HIS流水号
        /// </summary>
        public string COMM_HIS { get; set; }

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