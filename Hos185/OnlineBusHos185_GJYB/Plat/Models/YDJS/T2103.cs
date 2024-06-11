using System.Collections.Generic;

namespace OnlineBusHos185_GJYB.Models
{
    #region N：6.2.2.1 【2103】药店结算撤销

    public class T2103
    {
        public class Root
        {
            public data data { get; set; }
        }

        /// <summary>
        /// 输入（节点标识：data）
        /// </summary>
        public class data
        {
            /// <summary>
            /// 
            /// </summary>
            public string setl_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string mdtrt_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string psn_no { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string expContent { get; set; }
        }
    }


    public class RT2103
    {
        public class Root
        {
            public setlinfo setlinfo { get; set; }
            public List<setldetail> setldetail { get; set; }
        }

        /// <summary>
        /// 输出（节点标识：setlinfo）
        /// </summary>
        public class setlinfo
        {
            /// <summary>
            /// 
            /// </summary>
            public string mdtrt_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string setl_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string clr_optins { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string medfee_sumamt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string setl_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string fulamt_ownpay_amt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string overlmt_selfpay { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string preselfpay_amt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string inscp_scp_amt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string act_pay_dedc { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string hifp_pay { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string pool_prop_selfpay { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string cvlserv_pay { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string hifes_pay { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string hifmi_pay { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string hifob_pay { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string maf_pay { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string oth_pay { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string fund_pay_sumamt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string psn_pay { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string acct_pay { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string cash_payamt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string balc { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string acct_mulaid_pay { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string medins_setl_id { get; set; }
        }

        public class setldetail
        {
            /// <summary>
            /// 
            /// </summary>
            public string fund_pay_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string inscp_scp_amt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string crt_payb_lmt_amt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string fund_payamt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string fund_pay_type_name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string setl_proc_info { get; set; }

        }
    }
    #endregion
}
