using System.Collections.Generic;

namespace OnlineBusHos9_GJYB.Models
{
    #region N：6.2.2.1 【2101】药店预结算

    public class T2102
    {
        public class Root
        {
            public druginfo druginfo { get; set; }
            public List<drugdetail> drugdetail { get; set; }
        }

        /// <summary>
        /// 输入（节点标识：druginfo）
        /// </summary>
        public class druginfo
        {
            /// <summary>
            /// 人员编号
            /// </summary>
            public string psn_no { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string mdtrt_cert_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string mdtrt_cert_no { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string begntime { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string medfee_sumamt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string invono { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string insutype { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string dise_codg { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string dise_name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string acct_used_flag { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string med_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string expContent { get; set; }


        }

        public class drugdetail
        {
            /// <summary>
            /// 
            /// </summary>
            public string feedetl_sn { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string rxno { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string rx_circ_flag { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string fee_ocur_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string med_list_codg { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string medins_list_codg { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string det_item_fee_sumamt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string cnt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string pric { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string sin_dos_dscr { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string used_frqu_dscr { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string prd_days { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string medc_way_dscr { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string bilg_dr_codg { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string bilg_dr_name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string tcmdrug_used_way { get; set; }

        }
    }


    public class RT2102
    {
        public class Root
        {
            public setlinfo setlinfo { get; set; }
            public List<setldetail> setldetail { get; set; }
            public List<detlcutinfo> detlcutinfo { get; set; }
        }

        /// <summary>
        /// 输出（节点标识：setlinfo）
        /// </summary>
        public class setlinfo
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
            public string psn_name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string psn_cert_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string certno { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string gend { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string naty { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string brdy { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string age { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string insutype { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string psn_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string cvlserv_flag { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string setl_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string mdtrt_cert_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string med_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string medfee_sumamt { get; set; }
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
            public string psn_part_amt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string acct_pay { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string psn_cash_pay { get; set; }
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
            /// <summary>
            /// 
            /// </summary>
            public string clr_optins { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string clr_way { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string clr_type { get; set; }


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

        public class detlcutinfo
        {
            /// <summary>
            /// 
            /// </summary>
            public string feedetl_sn { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string det_item_fee_sumamt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string cnt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string pric { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string pric_uplmt_amt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string selfpay_prop { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string fulamt_ownpay_amt { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string overlmt_amt { get; set; }
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
            public string chrgitm_lv { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string med_chrgitm_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string bas_medn_flag { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string hi_nego_drug_flag { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string chld_medc_flag { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string list_sp_item_flag { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string drt_reim_flag { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string memo { get; set; }

        }
    }
    #endregion
}
