using System.Collections.Generic;

namespace OnlineBusHos9_GJYB.Models
{
    #region N：人员累计信息查询

    public class T5206
    {
        /// <summary>
        /// 输入（节点标识：data）
        /// </summary>
        public class Data
        {
            /// <summary>
            /// 人员编号
            /// </summary>
            public string psn_no { get; set; }
            /// <summary>
            /// 累计年月
            /// </summary>
            public string cum_ym { get; set; }


        }

        public class Root
        {
            public Data data { get; set; }

        }
    }


    public class RT5206
    {
        /// <summary>
        /// 输出（节点标识：cuminfo）
        /// </summary>
        public class cuminfo
        {
            /// <summary>
            /// 险种类型
            /// </summary>
            public string insutype { get; set; }
            /// <summary>
            /// 年度
            /// </summary>
            public string year { get; set; }
            /// <summary>
            /// 累计年月
            /// </summary>
            public string cum_ym { get; set; }
            /// <summary>
            /// 累计类别代码
            /// </summary>
            public string cum_type_code { get; set; }
            public decimal cum { get; set; }

        }

        public class Root
        {
            public List<cuminfo> cuminfo { get; set; }
        }
    }
    #endregion
}
