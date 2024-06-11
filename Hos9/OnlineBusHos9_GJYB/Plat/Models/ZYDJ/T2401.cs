using System.Collections.Generic;
namespace OnlineBusHos9_GJYB.Models
{

    public class T2401
    {
        /// <summary>
        /// 6.2.5.1 【2401】入院办理
        /// </summary>
        public class Root
        {
            public T2401_mdtrtinfo mdtrtinfo { get; set; }
            public List<T2401_diseinfo> diseinfo { get; set; }
        }

        /// <summary>
        /// 输入（节点标识：mdtrtinfo）
        /// </summary>
        public class T2401_mdtrtinfo
        {
            public string psn_no { get; set; }//	人员编号
            public string insutype { get; set; }//	险种类型
            public string coner_name { get; set; }//	联系人姓名
            public string tel { get; set; }//	联系电话
            public string begntime { get; set; }//	开始时间
            public string mdtrt_cert_type { get; set; }//	就诊凭证类型
            public string mdtrt_cert_no { get; set; }//	就诊凭证编号
            public string med_type { get; set; }//	医疗类别
            public string ipt_no { get; set; }//	住院号
            public string medrcdno { get; set; }//	病历号
            public string atddr_no { get; set; }//	主治医生编码
            public string chfpdr_name { get; set; }//	主诊医师姓名
            public string adm_diag_dscr { get; set; }//	入院诊断描述
            public string adm_dept_codg { get; set; }//	入院科室编码
            public string adm_dept_name { get; set; }//	入院科室名称
            public string adm_bed { get; set; }//	入院床位
            public string dscg_maindiag_code { get; set; }//	住院主诊断代码
            public string dscg_maindiag_name { get; set; }//	住院主诊断名称
            public string main_cond_dscr { get; set; }//	主要病情描述
            public string dise_codg { get; set; }//	病种编码
            public string dise_name { get; set; }//	病种名称
            public string oprn_oprt_code { get; set; }//	手术操作代码
            public string oprn_oprt_name { get; set; }//	手术操作名称
            public string fpsc_no { get; set; }//	计划生育服务证号
            public string matn_type { get; set; }//	生育类别
            public string birctrl_type { get; set; }//	计划生育手术类别
            public string latechb_flag { get; set; }//	晚育标志
            public int? geso_val { get; set; }//	孕周数
            public int? fetts { get; set; }//	胎次
            public int? fetus_cnt { get; set; }//	胎儿数
            public string pret_flag { get; set; }//	早产标志
            public string birctrl_matn_date { get; set; }//	计划生育手术或生育日期
            public string dise_type_code { get; set; }//	病种类型代码
            public string expContent { get; set; }//	字段扩展

        }
        public class T2401_diseinfo
        {
            public string psn_no { get; set; }//	人员编号
            public string diag_type { get; set; }//	诊断类别
            public string maindiag_flag { get; set; }//	主诊断标志
            public string diag_srt_no { get; set; }//	诊断排序号
            public string diag_code { get; set; }//	诊断代码
            public string diag_name { get; set; }//	诊断名称
            public string adm_cond { get; set; }//	入院病情
            public string diag_dept { get; set; }//	诊断科室
            public string dise_dor_no { get; set; }//	诊断医生编码
            public string dise_dor_name { get; set; }//	诊断医生姓名
            public string diag_time { get; set; }//	诊断时间

        }
    }

    public class RT2401
    {
        /// <summary>
        /// 输出（节点标识：result）
        /// </summary>
        public class Result
        {
            public string mdtrt_id { get; set; }//  就诊ID 
        }

        public class Root
        {
            public Result result { get; set; }
        }
    }
}
