namespace OnlineBusHos185_GJYB.Models
{
    /// <summary>
    /// 输入（节点标识：data）
    /// </summary>
    public class T2601
    {
        public class data
        {
            public string psn_no { get; set; }//人员编号
            public string omsgid { get; set; }//原发送方报文ID
            /// <summary>
            /// 
            /// </summary>
            public string oinfno { get; set; }// 原交易编号
        }
        public class Root
        {
            public data data { get; set; }
        }


    }
}
