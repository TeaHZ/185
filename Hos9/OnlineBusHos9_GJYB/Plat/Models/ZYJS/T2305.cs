using System.Collections.Generic;

namespace OnlineBusHos9_GJYB.Models
{
    public class T2305
    {
        /// <summary>
        /// 6.2.4.5 【2305】住院结算撤销
        /// 输入（节点标识：data）
        /// </summary>
        public class Data
        {
            public string mdtrt_id { get; set; }// 就诊ID
            public string setl_id { get; set; }//结算ID
            public string psn_no { get; set; }// 人员编号
        }

        public class Root
        {
            public Data data { get; set; }
        }
    }

    public class RT2305
    {

        public class Root
        {
            public RT2305_setlinfo setlinfo { get; set; }
            public List<RT2305_setldetail> setldetail { get; set; }
        }


        public class RT2305_setlinfo
        {
            public string mdtrt_id { get; set; }//	就诊ID
            public string setl_id { get; set; }//	结算ID
            public string clr_optins { get; set; }//	清算经办机构
            public string setl_time { get; set; }//	结算时间
            public decimal? medfee_sumamt { get; set; }//	医疗费总额
            public decimal? fulamt_ownpay_amt { get; set; }//	全自费金额
            public decimal? overlmt_selfpay { get; set; }//	超限价自费费用
            public decimal? preselfpay_amt { get; set; }//	先行自付金额
            public decimal? inscp_scp_amt { get; set; }//	符合政策范围金额
            public decimal? act_pay_dedc { get; set; }//	实际支付起付线
            public decimal? hifp_pay { get; set; }//	基本医疗保险统筹基金支出
            public decimal? pool_prop_selfpay { get; set; }//	基本医疗保险统筹基金支付比例
            public decimal? cvlserv_pay { get; set; }//	公务员医疗补助资金支出
            public decimal? hifes_pay { get; set; }//	企业补充医疗保险基金支出
            public decimal? hifmi_pay { get; set; }//	居民大病保险资金支出
            public decimal? hifob_pay { get; set; }//	职工大额医疗费用补助基金支出
            public decimal? maf_pay { get; set; }//	医疗救助基金支出
            public decimal? oth_pay { get; set; }//	其他支出
            public decimal? fund_pay_sumamt { get; set; }//	基金支付总额
            public decimal? psn_part_amt { get; set; }//	个人负担总金额
            public decimal? acct_pay { get; set; }//	个人账户支出
            public decimal? psn_cash_pay { get; set; }//	个人现金支出
            public decimal? hosp_part_amt { get; set; }//	医院负担金额
            public decimal? balc { get; set; }//	余额
            public decimal? acct_mulaid_pay { get; set; }//	个人账户共济支付金额
            public string medins_setl_id { get; set; }//	医药机构结算ID
            public decimal? hifdm_pay { get; set; }//	伤残人员医疗保障基金支出
            public string expContent { get; set; }//	

        }

        public class RT2305_setldetail
        {
            public string fund_pay_type { get; set; }//	基金支付类型
            public decimal? inscp_scp_amt { get; set; }//	符合政策范围金额
            public decimal? crt_payb_lmt_amt { get; set; }//	本次可支付限额金额
            public decimal? fund_payamt { get; set; }//	基金支付金额
            public string fund_pay_type_name { get; set; }//	基金支付类型名称
            public string setl_proc_info { get; set; }//	结算过程信息
        }
    }
}
