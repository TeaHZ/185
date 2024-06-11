namespace OnlineBusHos9_GJYB.Models
{
    public class T2404
    {
        /// <summary>
        /// 6.2.5.4 【2404】入院撤销
        /// 输入（节点标识：data）
        /// </summary>
        public class Root
        {
            public Data data { get; set; }
        }

        public class Data
        {
            public string mdtrt_id { get; set; }  //就诊ID
            public string psn_no { get; set; }//人员编号

        }
    }


}
