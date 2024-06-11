using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace OnlineBusHos185_GJYB.Models
{

    #region N：6.2.3.3 【2203】门诊就诊信息上传 

    public class T2203
    {
        public class Root
        {
            public mdtrtinfo mdtrtinfo { get; set; }

            [JsonProperty(PropertyName = "diseinfo")]
            public object diseinfo { get; set; }

            [JsonIgnore]//忽略此节点
            public List<diseinfo> diseinfos
            {
                //兼容diseinfo会传入单个对象/数组的情况
                get
                {
                    if (diseinfo is JArray)
                    {
                        return JsonConvert.DeserializeObject<List<diseinfo>>(JsonConvert.SerializeObject(diseinfo));
                    }
                    else if (diseinfo is JObject)
                    {
                        List<diseinfo> mydiseinfos = new List<diseinfo>();
                        mydiseinfos.Add(JsonConvert.DeserializeObject<diseinfo>(JsonConvert.SerializeObject(diseinfo)));
                        return mydiseinfos;
                    }
                    return null;
                }
            }
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
            public string matn_type { get; set; }
            public int? geso_val { get; set; }
            /// <summary>
            /// 字段扩展
            /// </summary>
            public string expContent { get; set; }

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
