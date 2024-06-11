using System.Collections.Generic;
namespace OnlineBusHos185_GJYB.Models
{

    public class T2303
    {
        /// <summary>
        /// 6.2.4.3 【2303】住院预结算
        /// 输入（节点标识：data）
        /// </summary>
        public class Data
        {
            public string psn_no { get; set; }// 人员编号
            public string mdtrt_cert_type { get; set; }// 就诊凭证类型
            /// <summary>
            /// 就诊凭证类型为“01”时填写电子凭证令牌，为“02”时填写身份证号，为“03”时填写社会保障卡卡号
            /// </summary>
            public string mdtrt_cert_no { get; set; }// 就诊凭证编号
            public decimal medfee_sumamt { get; set; }// 医疗费总额
            public string psn_setlway { get; set; }// 个人结算方式
            public string mdtrt_id { get; set; }//  就诊ID
            public string acct_used_flag { get; set; }// 个人账户使用标志
            public string insutype { get; set; }// 险种类型
            public string invono { get; set; }// 发票号

            public string mid_setl_flag { get; set; }//中途结算

            public decimal? fulamt_ownpay_amt { get; set; }//全自费金额

            public decimal? overlmt_selfpay { get; set; }//超限价金额

            public decimal? preselfpay_amt { get; set; }//先行自付金额

            public decimal? inscp_scp_amt { get; set; }//符合政策范围金额

            public string dscgTime { get; set; }//出院时间
            public string expContent { get; set; }//
        }

        public class Root
        {
            public Data data { get; set; }
        }
    }

    public class RT2303
    {
        /// <summary>
        /// 输出-结算信息（节点标识：setlinfo）
        /// </summary>
        public class Root
        {
            public setlinfo setlinfo { get; set; }
            public List<setldetail> setldetail { get; set; }
        }

        public class setlinfo
        {
            public string mdtrt_id { get; set; }//  就诊ID    字符型	30
            public string psn_no { get; set; }//  人员编号    字符型	30
            public string psn_name { get; set; }// 人员姓名    字符型	50
            public string psn_cert_type { get; set; }// 人员证件类型  字符型	6
            public string certno { get; set; }// 证件号码    字符型	50
            public string gend { get; set; }// 性别  字符型	6
            public string naty { get; set; }//  民族  字符型	3
            public string brdy { get; set; }// 出生日期    日期型
            public decimal? age { get; set; }//  年龄 数值型	4,1
            public string insutype { get; set; }//  险种类型    字符型	6
            public string psn_type { get; set; }// 人员类别    字符型	6
            public string cvlserv_flag { get; set; }//  公务员标志   字符型	3
            public string setl_time { get; set; }// 结算时间    日期时间型
            public string mdtrt_cert_type { get; set; }// 就诊凭证类型 字符型	3
            public string med_type { get; set; }// 医疗类别    字符型	6
            public decimal? medfee_sumamt { get; set; }// 医疗费总额   数值型	16,2
            public decimal? fulamt_ownpay_amt { get; set; }// 全自费金额   数值型	16,2
            public decimal? overlmt_selfpay { get; set; }// 超限价自费费用 数值型	16,2
            public decimal? preselfpay_amt { get; set; }// 先行自付金额  数值型	16,2
            public decimal? inscp_scp_amt { get; set; }// 符合政策范围金额    数值型	16,2
            public decimal? act_pay_dedc { get; set; }// 实际支付起付线 数值型	16,2
            public decimal? hifp_pay { get; set; }// 基本医疗保险统筹基金支出    数值型	16,2
            public decimal? pool_prop_selfpay { get; set; }// 基本医疗保险统筹基金支付比例  数值型	5,4
            public decimal? cvlserv_pay { get; set; }//  公务员医疗补助资金支出 数值型	16,2
            public decimal? hifes_pay { get; set; }// 企业补充医疗保险基金支出    数值型	16,2
            public decimal? hifmi_pay { get; set; }// 居民大病保险资金支出  数值型	16,2
            public decimal? hifob_pay { get; set; }// 职工大额医疗费用补助基金支出  数值型	16,2
            public decimal? maf_pay { get; set; }// 医疗救助基金支出    数值型	16,2
            public decimal? oth_pay { get; set; }// 其他支出    数值型	16,2
            public decimal? fund_pay_sumamt { get; set; }// 基金支付总额  数值型	16,2
            public decimal? psn_part_amt { get; set; }// 个人负担总金额 数值型	16,2
            public decimal? acct_pay { get; set; }// 个人账户支出  数值型	16,2
            public decimal? psn_cash_pay { get; set; }// 个人现金支出  数值型	16,2
            public decimal? hosp_part_amt { get; set; }// 医院负担金额  数值型	16,2
            public decimal? balc { get; set; }//  余额  数值型	16,2
            public decimal? acct_mulaid_pay { get; set; }// 个人账户共济支付金额  数值型	16,2
            public string medins_setl_id { get; set; }// 医药机构结算ID    字符型	30
            public string clr_optins { get; set; }// 清算经办机构  字符型	6
            public string clr_way { get; set; }// 清算方式    字符型	6
            public string clr_type { get; set; }// 清算类别    字符型	6
            public string expContent { get; set; }// 拓展字段

        }

        public class setldetail
        {
            public string fund_pay_type { get; set; }// 基金支付类型  字符型	6
            public decimal? inscp_scp_amt { get; set; }// 符合政策范围金额    数值型	16,2
            public decimal? crt_payb_lmt_amt { get; set; }// 本次可支付限额金额   数值型	16,2
            public decimal? fund_payamt { get; set; }//基金支付金额  数值型	16,2
            public string fund_pay_type_name { get; set; }// 基金支付类型名称    字符型	200
            public string setl_proc_info { get; set; }// 结算过程信息  字符型	4000

        }
    }
}
