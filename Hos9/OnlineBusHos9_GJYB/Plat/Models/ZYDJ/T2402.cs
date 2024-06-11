using System.Collections.Generic;

namespace OnlineBusHos9_GJYB.Models
{

    public class T2402
    {
        /// <summary>
        /// 6.2.5.2 【2402】出院办理
        /// </summary>
        public class Root
        {
            public T2402_dscginfo dscginfo { get; set; }
            public List<T2402_diseinfo> diseinfo { get; set; }
        }

        /// <summary>
        /// 输入-出院信息（节点标识：dscginfo）
        /// </summary>
        public class T2402_dscginfo
        {
            public string mdtrt_id { get; set; }//	就诊ID
            public string psn_no { get; set; }//	人员编号
            public string insutype { get; set; }//	险种类型
            public string endtime { get; set; }//	结束时间
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
            public string cop_flag { get; set; }//	伴有并发症标志
            public string dscg_dept_codg { get; set; }//	出院科室编码
            public string dscg_dept_name { get; set; }//	出院科室名称
            public string dscg_bed { get; set; }//	出院床位
            public string dscg_way { get; set; }//	离院方式
            public string die_date { get; set; }//	死亡日期
            public string expContent { get; set; }//拓展字段

        }
        /// <summary>
        /// 输入-出院诊断信息（节点标识：diseinfo）
        /// </summary>
        public class T2402_diseinfo
        {
            public string mdtrt_id { get; set; }//	就诊ID
            public string psn_no { get; set; }//	人员编号
            public string diag_type { get; set; }//	诊断类别
            public string maindiag_flag { get; set; }//	主诊断标志
            public decimal diag_srt_no { get; set; }//	诊断排序号
            public string diag_code { get; set; }//	诊断代码
            public string diag_name { get; set; }//	诊断名称
            public string diag_dept { get; set; }//	诊断科室
            public string dise_dor_no { get; set; }//	诊断医生编码
            public string dise_dor_name { get; set; }//	诊断医生姓名
            public string diag_time { get; set; }//	诊断时间

        }
    }
}
