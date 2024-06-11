namespace OnlineBusHos9_GJYB.Models
{
    #region N：6.2.3.2 【2202】门诊挂号撤销

    public class T2202
    {
        /// <summary>
        ///  输入（节点标识：data）
        /// </summary>
        public class Data
        {
            /// <summary>
            /// 人员编号
            /// </summary>
            public string psn_no { get; set; }
            /// <summary>
            /// 就诊ID
            /// </summary>
            public string mdtrt_id { get; set; }
            /// <summary>
            /// 住院/门诊号
            /// </summary>
            public string ipt_otp_no { get; set; }
            /// <summary>
            /// 字段扩展
            /// </summary>
            public string expContent { get; set; }

        }

        public class Root
        {
            public Data data { get; set; }
        }
    }
    #endregion
}
