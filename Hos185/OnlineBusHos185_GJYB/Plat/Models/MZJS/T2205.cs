namespace OnlineBusHos185_GJYB.Models
{
    #region N：6.2.3.5 【2205】门诊费用明细信息撤销
    public class T2205
    {
        /// <summary>
        /// 输入（节点标识：data）
        /// </summary>
        public class Data
        {
            /// <summary>
            /// 就诊ID
            /// </summary>
            public string mdtrt_id { get; set; }
            /// <summary>
            /// 收费批次号(传入“0000”删除所有未结算明细)
            /// </summary>
            public string chrg_bchno { get; set; }
            /// <summary>
            /// 人员编号
            /// </summary>
            public string psn_no { get; set; }

        }

        public class Root
        {
            public Data data { get; set; }
        }
    }

    #endregion
}
