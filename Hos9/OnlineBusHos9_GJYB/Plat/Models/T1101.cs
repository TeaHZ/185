using System.Collections.Generic;

namespace OnlineBusHos9_GJYB.Models
{
    #region N：6.1.1.1 【1101】人员信息获取

    public class T1101
    {
        /// <summary>
        /// 输入（节点标识：data）
        /// </summary>
        public class Data
        {
            /// <summary>
            /// 01,02,03
            /// </summary>
            public string mdtrt_cert_type { get; set; }// 就诊凭证类型

            /// <summary>
            /// 就诊凭证编号：就诊凭证类型为“01”时填写电子凭证令牌，为“02”时填写身份证号，为“03”时填写社会保障卡卡号
            /// </summary>
            public string mdtrt_cert_no { get; set; }

            /// <summary>
            /// 卡识别码：就诊凭证类型为“03”时必填
            /// </summary>
            public string card_sn { get; set; }// 卡识别码

            public string begntime { get; set; }// 开始时间
            public string psn_cert_type { get; set; }// 人员证件类型
            public string certno { get; set; }// 证件号码
            public string psn_name { get; set; }// 人员姓名
        }

        public class Root
        {
            public Data data { get; set; }
        }
    }

    public class RT1101
    {
        /// <summary>
        /// 输出-基本信息（节点标识：baseinfo）
        /// </summary>
        public class Baseinfo
        {
            public string psn_no { get; set; }//人员编号
            public string psn_cert_type { get; set; }// 人员证件类型
            public string certno { get; set; }// 证件号码
            public string psn_name { get; set; }// 人员姓名
            public string gend { get; set; }//性别
            public string naty { get; set; }// 民族
            public string brdy { get; set; }// 出生日期
            public string age { get; set; }//年龄

            public string expContent { get; set; }

            /// <summary>
            /// 南京医保
            /// </summary>
            public string exp_content { get; set; }
        }

        /// <summary>
        /// 输出-参保信息列表（节点标识insuinfo）
        /// </summary>
        public class Insuinfo
        {
            public decimal balc { get; set; }//余额
            public string insutype { get; set; }// 险种类型
            public string psn_type { get; set; }//人员类别
            public string psn_insu_stas { get; set; }

            public string psn_insu_date { get; set; }

            public string paus_insu_date { get; set; }
            public string cvlserv_flag { get; set; }//公务员标志
            public string insuplc_admdvs { get; set; }//参保地医保区划
            public string emp_name { get; set; }//单位名称
        }

        /// <summary>
        /// 输出-身份信息列表（节点标识：idetinfo）
        /// </summary>
        public class Idetinfo
        {
            public string psn_idet_type { get; set; }//人员身份类别
            public string psn_type_lv { get; set; }//人员类别等级
            public string memo { get; set; }//备注
            public string begntime { get; set; }//开始时间
            public string endtime { get; set; }//结束时间
        }

        public class Root
        {
            public Baseinfo baseinfo { get; set; }

            public List<Insuinfo> insuinfo { get; set; }

            public List<Idetinfo> idetinfo { get; set; }
        }

        /// <summary>
        /// 南京医保扩展字段
        /// </summary>
        public class EXP_CONTENT
        {
            public string flx_emp_flag { get; set; }// 灵活就业人员标志
            public decimal opsp_balc { get; set; }//门慢剩余金额
            public decimal otp_dise_balc { get; set; }//门诊两病剩余金额
            public decimal opt_pool_balc { get; set; }// 门诊统筹剩余金额
            public decimal pery_old_exam_balc { get; set; }//小卡剩余金额

            /// <summary>
            /// 大卡剩余金额
            /// </summary>
            public decimal pery_new_exam_balc { get; set; }//

            /// <summary>
            /// 居民产前检查剩余金额
            /// </summary>
            public decimal rsdt_pery_old_exam_balc { get; set; }//

            /// <summary>
            /// 门特恶性肿瘤放化疗余额
            /// </summary>
            public decimal opt_spdise_tmor_chmo_balc { get; set; }//

            /// <summary>
            /// 门特恶性肿瘤针对性药物
            /// </summary>
            public decimal opt_spdise_tmor_medn_balc { get; set; }//

            /// <summary>
            /// 门特恶性肿瘤辅助药物余额
            /// </summary>
            public decimal opt_spdise_tmor_asst_medn_balc { get; set; }//

            /// <summary>
            /// 门特血腹透余额
            /// </summary>
            public decimal opt_spdise_blo_abd_diay_medn_balc { get; set; }//

            /// <summary>
            /// 门特血腹透辅助药物余额
            /// </summary>
            public decimal opt_spdise_blo_abd_diay_asst_medn_balc { get; set; }//

            /// <summary>
            /// 门特器官移植余额
            /// </summary>
            public decimal opt_spdise_organ_transplant_medn_balc { get; set; }//

            /// <summary>
            /// 门特器官移植辅助药物余额
            /// </summary>
            public decimal opt_spdise_organ_transplant_asst_medn_balc { get; set; }//

            /// <summary>
            /// 门诊大病恶性肿瘤放化疗余额
            /// </summary>
            public decimal opt_big_dise_tmor_chmo_balc { get; set; }//

            /// <summary>
            /// 门诊大病恶性肿瘤针对性药物余额
            /// </summary>
            public decimal opt_big_dise_tmor_medn_balc { get; set; }//

            /// <summary>
            /// 门诊大病恶性肿瘤辅助药物余额
            /// </summary>
            public decimal opt_big_dise_tomr_asst_medn_balc { get; set; }//

            /// <summary>
            /// 门诊大病血腹透余额
            /// </summary>
            public decimal opt_big_dise_blo_abd_diay_medn_balc { get; set; }//

            /// <summary>
            /// 门诊大病血腹透辅助药物余额
            /// </summary>
            public decimal opt_big_dise_blo_abd_diay_asst_balc { get; set; }//

            /// <summary>
            /// 门诊大病器官移植余额
            /// </summary>
            public decimal opt_big_dise_organ_transplant_medn_balc { get; set; }//

            /// <summary>
            /// 门诊大病器官移植辅助药物余额
            /// </summary>
            public decimal opt_big_dise_organ_transplant_asst_medn_balc { get; set; }//

            public string trt_chk_rslt { get; set; }// 不享受待遇原因
            public string inhosp_stas { get; set; }// 在院状态

            public decimal fm_acct_balc { get; set; }// 家庭账户余额
            public string nhb_flag { get; set; }//宁惠保标记

            /// <summary>
            ///  门特其他病种余额-颅内良性肿瘤
            /// </summary>
            public decimal oth_dise_balc_M01001 { get; set; }//

            /// <summary>
            /// 门特其他病种余额-骨髓纤维化
            /// </summary>
            public decimal oth_dise_balc_M00904 { get; set; }//

            /// <summary>
            ///  门特其他病种余额-运动神经元病
            /// </summary>
            public decimal oth_dise_balc_M02700 { get; set; }//

            /// <summary>
            ///   门特其他病种余额-肺结核
            /// </summary>
            public decimal oth_dise_balc_M00105 { get; set; }//

            /// <summary>
            ///  门特其他病种余额-肾衰竭非透析治疗
            /// </summary>
            public decimal oth_dise_balc_M07800 { get; set; }//

            /// <summary>
            /// 门特其他病种余额-儿童I型糖尿病
            /// </summary>
            public decimal oth_dise_balc_M01601 { get; set; }//  

            /// <summary>
            ///  门特其他病种余额-儿童孤独症
            /// </summary>
            public decimal oth_dise_balc_M02207 { get; set; }//

            /// <summary>
            ///  门特其他病种余额-儿童生长激素缺乏症
            /// </summary>
            public decimal oth_dise_balc_M01902 { get; set; }//

            /// <summary>
            /// 门特其他病种余额-系统红斑狼疮
            /// </summary>
            public decimal oth_dise_balc_M07101 { get; set; }//

            /// <summary>
            ///  门特其他病种余额-再生性障碍贫血
            /// </summary>
            public decimal oth_dise_balc_M01102 { get; set; }//
        }
    }

    #endregion N：6.1.1.1 【1101】人员信息获取
}