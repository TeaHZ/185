using System.Collections.Generic;

namespace OnlineBusHos9_GJYB.Models
{
    #region N：6.2.3.4 【2204】门诊费用明细信息上传
    public class T2204
    {
        /// <summary>
        /// 输入-费用明细列表（节点标识：feedetail）
        /// </summary>
        public class Feedetail
        {
            /// <summary>
            /// 费用明细流水号(单次就诊内唯一)
            /// </summary>
            public string feedetl_sn { get; set; }
            /// <summary>
            /// 就诊ID
            /// </summary>
            public string mdtrt_id { get; set; }
            /// <summary>
            /// 人员编号
            /// </summary>
            public string psn_no { get; set; }
            /// <summary>
            /// 收费批次号
            /// 同一收费批次号病种编号必须一致
            /// </summary>
            public string chrg_bchno { get; set; }
            /// <summary>
            /// 病种编码
            /// </summary>
            public string dise_codg { get; set; }
            /// <summary>
            /// 处方号
            /// </summary>
            public string rxno { get; set; }
            /// <summary>
            /// 外购处方标志
            /// </summary>
            public string rx_circ_flag { get; set; }
            /// <summary>
            /// 费用发生时间
            /// </summary>
            public string fee_ocur_time { get; set; }
            /// <summary>
            /// 医疗目录编码
            /// </summary>
            public string med_list_codg { get; set; }
            /// <summary>
            /// 医药机构目录编码
            /// </summary>
            public string medins_list_codg { get; set; }
            /// <summary>
            /// 明细项目费用总额
            /// </summary>
            public decimal det_item_fee_sumamt { get; set; }
            /// <summary>
            /// 数量
            /// </summary>
            public decimal cnt { get; set; }
            /// <summary>
            /// 单价
            /// </summary>
            public decimal pric { get; set; }
            /// <summary>
            /// 单次剂量描述
            /// </summary>
            public string sin_dos_dscr { get; set; }
            /// <summary>
            /// 使用频次描述
            /// </summary>
            public string used_frqu_dscr { get; set; }
            /// <summary>
            /// 周期天数
            /// </summary>
            public decimal? prd_days { get; set; }

            /// <summary>
            /// 用药途径描述
            /// </summary>
            public string medc_way_dscr { get; set; }
            /// <summary>
            /// 开单科室编码
            /// </summary>
            public string bilg_dept_codg { get; set; }

            /// <summary>
            /// 开单科室名称
            /// </summary>
            public string bilg_dept_name { get; set; }
            /// <summary>
            /// 开单医生编码
            /// </summary>
            public string bilg_dr_codg { get; set; }

            /// <summary>
            /// 开单医师姓名
            /// </summary>
            public string bilg_dr_name { get; set; }

            /// <summary>
            /// 受单科室编码
            /// </summary>
            public string acord_dept_codg { get; set; }

            /// <summary>
            /// 受单科室名称
            /// </summary>
            public string acord_dept_name { get; set; }
            /// <summary>
            /// 受单医生编码
            /// </summary>
            public string orders_dr_code { get; set; }
            /// <summary>
            /// 受单医生姓名
            /// </summary>
            public string orders_dr_name { get; set; }
            /// <summary>
            /// 医院审批标志 
            /// 当目录限制使用标志为“是”时:
            /// “0”或“2”时，明细按照自费处理；
            /// 为“1”时，明细按纳入报销处理。
            /// 当目录限制使用标志为“否”时:
            /// 医院审批标志为“0”或“1”时，明细按照实际情况处理；
            /// 医院审批标志为“2”时，明细按照自费处理。
            /// </summary>
            public string hosp_appr_flag { get; set; }
            /// <summary>
            /// 中药使用方式
            /// </summary>
            public string tcmdrug_used_way { get; set; }
            /// <summary>
            /// 外检标志
            /// </summary>
            public string etip_flag { get; set; }
            /// <summary>
            /// 外检医院编码
            /// </summary>
            public string etip_hosp_code { get; set; }
            /// <summary>
            /// 出院带药标志
            /// </summary>
            public string dscg_tkdrug_flag { get; set; }
            /// <summary>
            /// 生育费用标志
            /// </summary>
            public string matn_fee_flag { get; set; }

            public ExpContent exp_content { get; set; }

        }

        public class ExpContent
        {
            /// <summary>
            /// 草药省码
            /// </summary>
            public string tcmherb_prov_code { get; set; }
            /// <summary>
            /// 耗材省码
            /// </summary>
            public string mcs_prov_code { get; set; }
        }

        public class Root
        {
            public List<Feedetail> feedetail { get; set; }
        }
    }


    public class RT2204
    {
        /// <summary>
        /// 输出（节点标识：result）
        /// </summary>
        public class Result
        {
            /// <summary>
            /// 费用明细流水号
            /// </summary>
            public string feedetl_sn { get; set; }
            /// <summary>
            /// 明细项目费用总额
            /// </summary>
            public decimal det_item_fee_sumamt { get; set; }
            /// <summary>
            /// 数量
            /// </summary>
            public decimal cnt { get; set; }
            /// <summary>
            /// 单价
            /// </summary>
            public decimal pric { get; set; }
            /// <summary>
            /// 定价上限金额
            /// </summary>
            public decimal? pric_uplmt_amt { get; set; }
            /// <summary>
            /// 自付比例
            /// </summary>
            public decimal? selfpay_prop { get; set; }
            /// <summary>
            /// 全自费金额
            /// </summary>
            public decimal? fulamt_ownpay_amt { get; set; }
            /// <summary>
            /// 超限价金额
            /// </summary>
            public decimal? overlmt_amt { get; set; }
            /// <summary>
            /// 先行自付金额
            /// </summary>
            public decimal? preselfpay_amt { get; set; }
            /// <summary>
            /// 符合政策范围金额
            /// </summary>
            public decimal? inscp_scp_amt { get; set; }
            /// <summary>
            /// 收费项目等级
            /// </summary>
            public string chrgitm_lv { get; set; }
            /// <summary>
            /// 医疗收费项目类别
            /// </summary>
            public string med_chrgitm_type { get; set; }
            /// <summary>
            /// 基本药物标志
            /// </summary>
            public string bas_medn_flag { get; set; }
            /// <summary>
            /// 医保谈判药品标志
            /// </summary>
            public string hi_nego_drug_flag { get; set; }
            /// <summary>
            /// 儿童用药标志
            /// </summary>
            public string chld_medc_flag { get; set; }
            /// <summary>
            /// 目录特项标志(特检特治项目或特殊药品)
            /// </summary>
            public string list_sp_item_flag { get; set; }
            /// <summary>
            /// 限制使用标志
            /// </summary>
            public string lmt_used_flag { get; set; }
            /// <summary>
            /// 直报标志
            /// </summary>
            public string drt_reim_flag { get; set; }

            /// <summary>
            /// 备注
            /// </summary>
            public string memo { get; set; }
            /// <summary>
            /// 字段扩展
            /// </summary>
            public string expContent { get; set; }
        }

        public class Root
        {
            public List<Result> result { get; set; }
        }
    }

    #endregion
}
