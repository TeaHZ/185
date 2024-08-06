using System;
using System.Collections.Generic;
using System.Text;

namespace QHSiInterface
{

    #region N：6.2.3.7 【2207】门诊结算

    public class T2207
    {
        /// <summary>
        /// data
        /// </summary>
        public class Data
        {
            /// <summary>
            /// 人员编号
            /// </summary>
            public string psn_no { get; set; }
            /// <summary>
            /// 就诊凭证类型
            /// </summary>
            public string mdtrt_cert_type { get; set; }
            /// <summary>
            /// 就诊凭证编号
            /// 就诊凭证类型为“01”时填写电子凭证令牌，为“02”时填写身份证号，为“03”时填写社会保障卡卡号
            /// </summary>
            public string mdtrt_cert_no { get; set; }
            /// <summary>
            /// 医疗类别
            /// </summary>
            public string med_type { get; set; }
            /// <summary>
            /// 医疗费总额
            /// </summary>
            public string medfee_sumamt { get; set; }
            /// <summary>
            /// 个人结算方式
            /// </summary>
            public string psn_setlway { get; set; }
            /// <summary>
            /// 就诊ID
            /// </summary>
            public string mdtrt_id { get; set; }
            /// <summary>
            /// 收费批次号
            /// </summary>
            public string chrg_bchno { get; set; }
            /// <summary>
            /// 险种类型
            /// </summary>
            public string insutype { get; set; }
            /// <summary>
            /// 个人账户使用标志
            /// </summary>
            public string acct_used_flag { get; set; }
            /// <summary>
            /// 发票号
            /// </summary>
            public string invono { get; set; }
            /// <summary>
            /// 全自费金额
            /// </summary>
            public string fulamt_ownpay_amt { get; set; }
            /// <summary>
            /// 超限价金额
            /// </summary>
            public string overlmt_selfpay { get; set; }
            /// <summary>
            /// 先行自付金额
            /// </summary>
            public string preselfpay_amt { get; set; }
            /// <summary>
            /// 符合政策范围金额
            /// </summary>
            public string inscp_scp_amt { get; set; }
            public Exp_content exp_content { get; set; }
            public string pub_hosp_rfom_flag { get; set; }

        }
        public class Exp_content
        {
            /// <summary>
            /// 
            /// </summary>
            public string oprn_flag { get; set; }
        }

        public class Input
        {
            /// <summary>
            /// 
            /// </summary>
            public Data data { get; set; }
        }

        public class Root
        {
            /// <summary>
            /// 
            /// </summary>
            public string infno { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string msgid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string mdtrtarea_admvs { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string insuplc_admdvs { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string recer_sys_code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string dev_no { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string dev_safe_info { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string cainfo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string signtype { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string infver { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string opter_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string opter { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string opter_name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string inf_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string fixmedins_code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string fixmedins_name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string sign_no { get; set; }
            /// <summary>
            /// 北大医疗信息技术有限公司
            /// </summary>
            public string fixmedins_soft_fcty { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Input input { get; set; }
 
        }
    }

    public class RT2207
    {
        /// <summary>
        /// setlinfo
        /// </summary>
        public class Setlinfo
        {
            /// <summary>
            /// 就诊ID
            /// </summary>
            public string mdtrt_id { get; set; }
            /// <summary>
            /// 结算ID
            /// </summary>
            public string setl_id { get; set; }
            /// <summary>
            /// 人员编号
            /// </summary>
            public string psn_no { get; set; }
            /// <summary>
            /// 人员姓名
            /// </summary>
            public string psn_name { get; set; }
            /// <summary>
            /// 人员证件类型
            /// </summary>
            public string psn_cert_type { get; set; }
            /// <summary>
            /// 证件号码
            /// </summary>
            public string certno { get; set; }
            /// <summary>
            /// 性别
            /// </summary>
            public string gend { get; set; }
            /// <summary>
            /// 民族
            /// </summary>
            public string naty { get; set; }
            /// <summary>
            /// 出生日期
            /// </summary>
            public string brdy { get; set; }
            /// <summary>
            /// 年龄
            /// </summary>
            public decimal age { get; set; }
            /// <summary>
            /// 险种类型
            /// </summary>
            public string insutype { get; set; }
            /// <summary>
            /// 人员类别
            /// </summary>
            public string psn_type { get; set; }
            /// <summary>
            /// 公务员标志
            /// </summary>
            public string cvlserv_flag { get; set; }
            /// <summary>
            /// 结算时间
            /// </summary>
            public string setl_time { get; set; }
            /// <summary>
            /// 就诊凭证类型
            /// </summary>
            public string mdtrt_cert_type { get; set; }
            /// <summary>
            /// 医疗类别
            /// </summary>
            public string med_type { get; set; }
            /// <summary>
            /// 医疗费总额
            /// </summary>
            public decimal medfee_sumamt { get; set; }
            /// <summary>
            /// 全自费金额
            /// </summary>
            public string fulamt_ownpay_amt { get; set; }
            /// <summary>
            /// 超限价自费费用
            /// </summary>
            public string overlmt_selfpay { get; set; }
            /// <summary>
            /// 先行自付金额
            /// </summary>
            public string preselfpay_amt { get; set; }

            /// <summary>
            /// 符合政策范围金额
            /// </summary>
            public decimal inscp_scp_amt { get; set; }
            /// <summary>
            /// 实际支付起付线
            /// </summary>
            public string act_pay_dedc { get; set; }
            /// <summary>
            /// 基本医疗保险统筹基金支出
            /// </summary>
            public decimal hifp_pay { get; set; }
            /// <summary>
            /// 基本医疗保险统筹基金支付比例
            /// </summary>
            public string pool_prop_selfpay { get; set; }
            /// <summary>
            /// 公务员医疗补助资金支出
            /// </summary>
            public string cvlserv_pay { get; set; }
            /// <summary>
            /// 企业补充医疗保险基金支出
            /// </summary>
            public string hifes_pay { get; set; }
            /// <summary>
            /// 居民大病保险资金支出
            /// </summary>
            public string hifmi_pay { get; set; }
            /// <summary>
            /// 职工大额医疗费用补助基金支出
            /// </summary>
            public string hifob_pay { get; set; }
            /// <summary>
            /// 医疗救助基金支出
            /// </summary>
            public decimal maf_pay { get; set; }
            /// <summary>
            /// 其他支出
            /// </summary>
            public string oth_pay { get; set; }
            /// <summary>
            /// 基金支付总额
            /// </summary>
            public decimal fund_pay_sumamt { get; set; }
            /// <summary>
            /// 个人负担总金额
            /// </summary>
            public decimal psn_part_amt { get; set; }
            /// <summary>
            /// 个人账户支出
            /// </summary>
            public decimal acct_pay { get; set; }
            /// <summary>
            /// 个人现金支出
            /// </summary>
            public decimal psn_cash_pay { get; set; }
            /// <summary>
            /// 医院负担金额
            /// </summary>
            public string hosp_part_amt { get; set; }
            /// <summary>
            /// 余额
            /// </summary>
            public string balc { get; set; }
            /// <summary>
            /// 个人账户共济支付金额
            /// </summary>
            public string  acct_mulaid_pay { get; set; }
            /// <summary>
            /// 医药机构结算ID
            /// </summary>
            public string medins_setl_id { get; set; }
            /// <summary>
            /// 清算经办机构
            /// </summary>
            public string clr_optins { get; set; }
            /// <summary>
            /// 清算方式
            /// </summary>
            public string clr_way { get; set; }
            /// <summary>
            /// 清算类别
            /// </summary>
            public string clr_type { get; set; }

            /// <summary>
            /// 伤残人员医疗保障基金支出
            /// </summary>
            public string hifdm_pay { get; set; }

            public Exp_content exp_content { get; set; }

        }

        public class Exp_content
        {
            /// <summary>
            /// 现金自理支付
            /// </summary>
            public string cash_selfdspo_pay { get; set; }

            /// <summary>
            /// 现金自付支付
            /// </summary>
            public string cash_selfpay_pay { get; set; }
            /// <summary>
            /// 账户自理支付
            /// </summary>
            public string acct_selfdspo_pay { get; set; }
            /// <summary>
            /// 账户自付支付
            /// </summary>
            public string acct_selfpay_pay { get; set; }

        }
        /// <summary>
        /// setldetail
        /// </summary>
        public class Setldetail
        {
            /// <summary>
            /// 基金支付类型
            /// </summary>
            public string fund_pay_type { get; set; }
            /// <summary>
            /// 符合政策范围金额
            /// </summary>
            public string inscp_scp_amt { get; set; }
            /// <summary>
            /// 本次可支付限额金额
            /// </summary>
            public string crt_payb_lmt_amt { get; set; }
            /// <summary>
            /// 基金支付金额
            /// </summary>
            public string fund_payamt { get; set; }
            /// <summary>
            /// 基金支付类型名称
            /// </summary>
            public string fund_pay_type_name { get; set; }
            /// <summary>
            /// 结算过程信息
            /// </summary>
            public string setl_proc_info { get; set; }

        }
        public class Output
        {
            public Setlinfo setlinfo { get; set; }

            public List<Setldetail> setldetail { get; set; }
        }
        public class Root
        {
            /// <summary>
            /// 交易状态码
            /// </summary>
            public string infcode { get; set; }
            /// <summary>
            /// 接收方报文ID
            /// </summary>
            public string inf_refmsgid { get; set; }
            /// <summary>
            /// 接收报文时间
            /// </summary>
            public string refmsg_time { get; set; }
            /// <summary>
            /// 响应报文时间
            /// </summary>
            public string respond_time { get; set; }
            /// <summary>
            /// 错误信息
            /// </summary>
            public string err_msg { get; set; }
            public Output output { get; set; }

        }
    }


    #endregion
}
