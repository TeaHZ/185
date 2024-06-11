using System.Collections.Generic;

namespace OnlineBusHos9_GJYB.Models
{
    #region N：6.5.3.1 【5301】人员慢特病备案查询

    public class T5301
    {
        /// <summary>
        /// 输入（节点标识：data）
        /// </summary>
        public class Data
        {
            public string psn_no { get; set; }// 人员编号

        }

        public class Root
        {
            public Data data { get; set; }
        }
    }


    public class RT5301
    {
        /// <summary>
        /// 输出（节点标识：feedetail）
        /// </summary>
        public class feedetail
        {
            /// <summary>
            /// 
            /// </summary>
            public string opsp_dise_code { get; set; }//门慢门特病种目录代码
            /// <summary>
            /// 
            /// </summary>
            public string opsp_dise_name { get; set; }//门慢门特病种名称
            /// <summary>
            /// 
            /// </summary>
            public string begndate { get; set; }//开始时间
            /// <summary>
            /// 
            /// </summary>
            public string enddate { get; set; }//结束时间
        }

        public class Root
        {
            public List<feedetail> feedetail { get; set; }
        }
    }
    #endregion
}
