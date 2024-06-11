using Alipay.AopSdk.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBusHos185_Tran.F2FPAY.Linux
{
    public class AlipayTradePayContentBuilder
    {
        public ExtendParams extend_params;

        public string timeout_express { get; set; }
        public string terminal_id { get; set; }
        public string store_id { get; set; }
        public string operator_id { get; set; }
        public List<GoodsInfo> goods_detail { get; set; }
        public string body { get; set; }
        public string subject { get; set; }
        public string undiscountable_amount { get; set; }
        public string discountable_amount { get; set; }
        public string total_amount { get; set; }
        public string seller_id { get; set; }
        public string out_trade_no { get; set; }
        public string scene { get; set; }
        public string auth_code { get; set; }

        public string COMM_HIS { get; set; }

    }
}
