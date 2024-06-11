using System.Collections.Generic;

namespace OnlineBusHos9_GJYB.Models
{
    #region N：6.1.2.1 【1201】医药机构信息获取

    public class T1201
    {
        /// <summary>
        /// 输入（节点标识：medinsinfo）
        /// </summary>
        public class medinsinfo
        {
            public string fixmedins_type { get; set; }// 定点医疗服务机构类型
            public string fixmedins_name { get; set; }// 定点医药机构名称
            public string fixmedins_code { get; set; }// 定点医药机构编号
            public string platsave { get; set; }
        }

        public class Root
        {
            public medinsinfo medinsinfo { get; set; }
        }
    }


    public class RT1201
    {
        /// <summary>
        /// 输入（节点标识：medinsinfo）
        /// </summary>
        public class medinsinfo
        {
            public string fixmedins_type { get; set; }// 定点医疗服务机构类型
            public string fixmedins_name { get; set; }// 定点医药机构名称
            public string fixmedins_code { get; set; }// 定点医药机构编号
            public string uscc { get; set; }//统一社会信用代码
            public string hosp_lv { get; set; }//医院等级
        }

        public class Root
        {
            public List<medinsinfo> medinsinfo { get; set; }
        }
    }
    #endregion
}
