using System.Collections.Generic;
namespace OnlineBusHos9_GJYB.Models
{

    public class T2301
    {
        /// <summary>
        /// 6.2.4.1 【2301】住院费用明细上传
        /// 输入（节点标识：feedetail）
        /// </summary>
        public class Feedetail
        {
            public string feedetl_sn { get; set; }// 费用明细流水号
            public string init_feedetl_sn { get; set; }// 原费用流水号
            public string mdtrt_id { get; set; }//就诊ID
            public string drord_no { get; set; }//医嘱号
            public string psn_no { get; set; }// 人员编号
            public string med_type { get; set; }//医疗类别
            public string fee_ocur_time { get; set; }//费用发生时间
            public string med_list_codg { get; set; }// 医疗目录编码
            public string medins_list_codg { get; set; }// 医药机构目录编码
            public decimal det_item_fee_sumamt { get; set; }//明细项目费用总额
            public decimal cnt { get; set; }//数量
            public decimal pric { get; set; }//单价
            public string bilg_dept_codg { get; set; }//开单科室编码
            public string bilg_dept_name { get; set; }//开单科室名称
            public string bilg_dr_codg { get; set; }//开单医生编码
            public string bilg_dr_name { get; set; }//开单医师姓名
            public string acord_dept_codg { get; set; }//受单科室编码
            public string acord_dept_name { get; set; }//受单科室名称
            public string orders_dr_code { get; set; }//受单医生编码
            public string orders_dr_name { get; set; }//受单医生姓名
            public string hosp_appr_flag { get; set; }//医院审批标志
            public string tcmdrug_used_way { get; set; }//中药使用方式
            public string etip_flag { get; set; }//外检标志
            public string etip_hosp_code { get; set; }//外检医院编码
            public string dscg_tkdrug_flag { get; set; }//出院带药标志
            public string matn_fee_flag { get; set; }//生育费用标志
            public string memo { get; set; }//备注

            public ExpContent expContent { get; set; }

        }

        public class ExpContent
        {
            public string tcmherb_prov_code { get; set; }

            public string mcs_prov_code { get; set; }
        }

        public class Root
        {
            public List<Feedetail> feedetail { get; set; }
        }
    }


    public class RT2301
    {
        /// <summary>
        /// 输出（节点标识：result）
        /// </summary>
        public class Result
        {
            public string feedetl_sn { get; set; }//费用明细流水号
            public decimal det_item_fee_sumamt { get; set; }// 明细项目费用总额
            public decimal cnt { get; set; }//数量
            public decimal pric { get; set; }//单价
            public decimal pric_uplmt_amt { get; set; }//定价上限金额
            public decimal selfpay_prop { get; set; }//自付比例
            public decimal? fulamt_ownpay_amt { get; set; }//全自费金额
            public decimal? overlmt_amt { get; set; }//超限价金额
            public decimal? preselfpay_amt { get; set; }//先行自付金额
            public decimal? inscp_scp_amt { get; set; }//符合政策范围金额
            public string chrgitm_lv { get; set; }//收费项目等级
            public string med_chrgitm_type { get; set; }// 医疗收费项目类别
            public string bas_medn_flag { get; set; }//基本药物标志
            public string hi_nego_drug_flag { get; set; }// 医保谈判药品标志
            public string chld_medc_flag { get; set; }//儿童用药标志
            public string list_sp_item_flag { get; set; }//目录特项标志
            public string lmt_used_flag { get; set; }//限制使用标志
            public string drt_reim_flag { get; set; }//直报标志
            public string memo { get; set; }//备注
            public string expContent { get; set; }
        }

        public class Root
        {
            public List<Result> result { get; set; }
        }
    }
}
