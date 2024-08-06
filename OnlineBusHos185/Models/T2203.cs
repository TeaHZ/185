using System;
using System.Collections.Generic;
using System.Text;

namespace QHSiInterface
{

    #region N：6.2.3.3 【2203】门诊就诊信息上传 

    public class T2203
    {
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
            /// 南京医科大学第二附属医院
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
        public class Input
        {
            /// <summary>
            /// 
            /// </summary>
            public mdtrtinfo mdtrtinfo { get; set; }
            public List<diseinfo> diseinfo { get; set; }
        }
        /// <summary>
        /// 输入-就诊信息（节点标识：mdtrtinfo）
        /// </summary>
        public class mdtrtinfo
        {
            public string mdtrt_id { get; set; }
            public string psn_no { get; set; }
            public string med_type { get; set; }
            /// <summary>
            /// 就诊时间
            /// yyyy-MM-dd HH:mm:ss
            /// </summary>
            public string begntime { get; set; }
            /// <summary>
            /// 主要病情描述
            /// </summary>
            /// 
            public string main_cond_dscr { get; set; }
            /// <summary>
            /// 按照标准编码填写：
            ///按病种结算病种目录代码(bydise_setl_list_code)、
            ///门诊慢特病病种目录代码(opsp_dise_cod)
            /// </summary>
            public string dise_codg { get; set; }
            /// <summary>
            /// 病种名称
            /// </summary>
            public string dise_name { get; set; }
            /// <summary>
            /// 计划生育手术类别
            /// </summary>
            public string birctrl_type { get; set; }
            /// <summary>
            /// 计划生育手术或生育日期
            /// yyyy-MM-dd
            /// </summary>
            public string birctrl_matn_date { get; set; }
            /// <summary>
            /// 生育类别
            /// </summary>
            public string matn_type { get; set; }
            /// <summary>
            /// 孕周数
            /// </summary>
            public string geso_val { get; set; }
            /// <summary>
            /// 字段扩展
            /// </summary>
            public expContent expContent { get; set; }

        }

        public class expContent
        {
            public string matn_locl_dise_code1 { get; set; }

            public string matn_locl_dise_code2 { get; set; }

            public string matn_locl_dise_code3 { get; set; }

            public string matn_locl_dise_code4 { get; set; }
        }

        /// <summary>
        /// 输入-诊断信息（节点标识：diseinfo）
        /// </summary>
        public class diseinfo
        {
            /// <summary>
            /// 诊断类别
            /// </summary>
            public string diag_type { get; set; }
            /// <summary>
            /// 诊断排序号
            /// </summary>
            public string diag_srt_no { get; set; }
            /// <summary>
            /// 诊断代码
            /// </summary>
            public string diag_code { get; set; }
            /// <summary>
            /// 诊断名称
            /// </summary>
            public string diag_name { get; set; }
            /// <summary>
            /// 诊断科室
            /// </summary>
            public string diag_dept { get; set; }
            /// <summary>
            /// 诊断医生编码
            /// </summary>
            public string dise_dor_no { get; set; }
            /// <summary>
            /// 诊断医生姓名
            /// </summary>
            public string dise_dor_name { get; set; }
            /// <summary>
            /// 诊断时间 (日期时间型)
            /// yyyy-MM-dd HH:mm:ss
            /// </summary>
            public string diag_time { get; set; }
            /// <summary>
            /// 有效标志
            /// </summary>
            public string vali_flag { get; set; }

        }
    }

    #endregion
}
