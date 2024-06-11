using System.Collections.Generic;

namespace OnlineBusHos9_GJYB.Models
{
    /// <summary>
    /// 6.2.5.3 【2403】住院信息变更
    /// </summary>
    public class T2403
    {
        public class Root
        {
            public adminfo adminfo { get; set; }
            public List<T2403_diseinfo> diseinfo { get; set; }

        }

        /// <summary>
        /// 输入-入院登记信息（节点标识：adminfo）
        /// </summary>
        public class adminfo
        {
            public string mdtrt_id { get; set; }//	就诊ID	字符型
            public string psn_no { get; set; }//	人员编号	字符型
            public string coner_name { get; set; }//	联系人姓名	字符型
            public string tel { get; set; }//	联系电话	字符型
            public string begntime { get; set; }//	开始时间	日期时间型
            public string endtime { get; set; }//	结束时间	日期时间型
            public string mdtrt_cert_type { get; set; }//	就诊凭证类型	字符型
            public string med_type { get; set; }//	医疗类别	字符型
            public string ipt_otp_no { get; set; }//	住院/门诊号	字符型
            public string medrcdno { get; set; }//	病历号	字符型
            public string atddr_no { get; set; }//	主治医生编码	字符型
            public string chfpdr_name { get; set; }//	主诊医师姓名	字符型
            public string adm_diag_dscr { get; set; }//	入院诊断描述	字符型
            public string adm_dept_codg { get; set; }//	入院科室编码	字符型
            public string adm_dept_name { get; set; }//	入院科室名称	字符型
            public string adm_bed { get; set; }//	入院床位	字符型
            public string dscg_maindiag_code { get; set; }//	住院主诊断代码	字符型
            public string dscg_maindiag_name { get; set; }//	住院主诊断名称	字符型
            public string main_cond_dscr { get; set; }//	主要病情描述	字符型
            public string dise_codg { get; set; }//	病种编码	字符型
            public string dise_name { get; set; }//	病种名称	字符型
            public string oprn_oprt_code { get; set; }//	手术操作代码	字符型
            public string oprn_oprt_name { get; set; }//	手术操作名称	字符型
            public string fpsc_no { get; set; }//	计划生育服务证号	字符型
            public string matn_type { get; set; }//	生育类别	字符型
            public string birctrl_type { get; set; }//	计划生育手术类别	字符型
            public string latechb_flag { get; set; }//	晚育标志	字符型
            public int? geso_val { get; set; }//	孕周数	数值型
            public int? fetts { get; set; }//	胎次	数值型
            public int? fetus_cnt { get; set; }//	胎儿数	数值型
            public string pret_flag { get; set; }//	早产标志	字符型
            public string birctrl_matn_date { get; set; }//	计划生育手术或生育日期	日期型
            public string dise_type_code { get; set; }//	病种类型代码	字符型
            public string expContent { get; set; }

        }

        public class T2403_diseinfo
        {
            public string mdtrt_id { get; set; }//	就诊ID	字符型
            public string psn_no { get; set; }//	人员编号	字符型
            public string diag_type { get; set; }//	诊断类别	字符型
            public string maindiag_flag { get; set; }//	主诊断标志	字符型
            public decimal diag_srt_no { get; set; }//	诊断排序号	数值型
            public string diag_code { get; set; }//	诊断代码	字符型
            public string diag_name { get; set; }//	诊断名称	字符型
            public string adm_cond { get; set; }//	入院病情	字符型
            public string diag_dept { get; set; }//	诊断科室	字符型
            public string dise_dor_no { get; set; }//	诊断医生编码	字符型
            public string dise_dor_name { get; set; }//	诊断医生姓名	字符型
            public string diag_time { get; set; }//	诊断时间	日期时间型

        }
    }

}
