namespace OnlineBusHos185_GJYB.Models
{
    public class T2405
    {
        /// <summary>
        /// 输入（节点标识：data）
        /// </summary>
        public class Data
        {
            public string mdtrt_id { get; set; }//就诊ID    字符型
            public string psn_no { get; set; }//人员编号 字符型
        }

        public class Root
        {
            public Data data { get; set; }
        }
    }

}
