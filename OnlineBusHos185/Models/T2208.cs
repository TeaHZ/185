using System;
using System.Collections.Generic;
using System.Text;

namespace QHSiInterface
{
    #region N：6.2.3.8 【2208】门诊结算撤销
    public class T2208
    {
        public class Data
        {
            /// <summary>
            /// 结算ID
            /// </summary>
            public string setl_id { get; set; }
            /// <summary>
            /// 就诊ID
            /// </summary>
            public string mdtrt_id { get; set; }
            /// <summary>
            /// 人员编号
            /// </summary>
            public string psn_no { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
        }
    }

    public class RT2208
    {
        /// <summary>
        /// 输出-结算信息（节点标识：setlinfo）
        /// </summary>
        public class Setlinfo
        {

            public string mdtrt_id { get; set; }//	就诊ID
            public string setl_id { get; set; }//	结算ID
            public string clr_optins { get; set; }//	清算经办机构
            public string setl_time { get; set; }//	结算时间
            public string medfee_sumamt { get; set; }//	医疗费总额
            public string fulamt_ownpay_amt { get; set; }//	全自费金额
            public string overlmt_selfpay { get; set; }//	超限价自费费用
            public string preselfpay_amt { get; set; }//	先行自付金额
            public string inscp_scp_amt { get; set; }//	符合政策范围金额
            public string act_pay_dedc { get; set; }//	实际支付起付线
            public string hifp_pay { get; set; }//	基本医疗保险统筹基金支出
            public string pool_prop_selfpay { get; set; }//	基本医疗保险统筹基金支付比例
            public string cvlserv_pay { get; set; }//	公务员医疗补助资金支出
            public string hifes_pay { get; set; }//	企业补充医疗保险基金支出
            public string hifmi_pay { get; set; }//	居民大病保险资金支出
            public string hifob_pay { get; set; }//	职工大额医疗费用补助基金支出
            public string maf_pay { get; set; }//	医疗救助基金支出
            public string oth_pay { get; set; }//	其他支出
            public string fund_pay_sumamt { get; set; }//	基金支付总额
            public string psn_part_amt { get; set; }//	个人负担总金额
            public string acct_pay { get; set; }//	个人账户支出
            public string balc { get; set; }//	余额
            public string acct_mulaid_pay { get; set; }//	个人账户共济支付金额
            public string hosp_part_amt { get; set; }//	医院负担金额
            public string medins_setl_id { get; set; }//	医药机构结算ID
            public string pdn_cash_pay { get; set; }//	个人现金支出

        }
        /// <summary>
        /// 输出-结算基金分项信息（节点标识：setldetail）
        /// </summary>
        public class Setldetail
        {
            public string fund_pay_type { get; set; }//	基金支付类型
            public string inscp_scp_amt { get; set; }//	符合政策范围金额
            public string crt_payb_lmt_amt { get; set; }//	本次可支付限额金额
            public string fund_payamt { get; set; }//	基金支付金额
            public string fund_pay_type_name { get; set; }//	基金支付类型名称
            public string setl_proc_info { get; set; }//	结算过程信息

        }

        public class Root
        {
            public Setlinfo setlinfo { get; set; }

            public List<Setldetail> setldetail { get; set; }
 
        }
    }


    #endregion
}
