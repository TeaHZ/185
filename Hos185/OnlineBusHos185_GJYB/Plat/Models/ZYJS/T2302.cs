namespace OnlineBusHos185_GJYB.Models
{
    /// <summary>
    /// 输入（节点标识：data）
    /// </summary>
    public class T2302
    {
        public class Root
        {
            public Data data { get; set; }
        }

        public class Data
        {
            /// <summary>
            /// 传入“0000”时删除全部
            /// </summary>
            public string feedetl_sn { get; set; } //  费用明细流水号
            public string mdtrt_id { get; set; } //   就诊ID
            public string psn_no { get; set; } //  人员编号
            public string expContent { get; set; } //  字段扩展
        }

    }


    public class RT2302
    {
    }
}
