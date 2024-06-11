namespace OnlineBusHos185_GJYB.Models
{
    #region N：6.3.2.2 【3202】医药机构费用结算对明细账

    public class T3202
    {
        /// <summary>
        /// 输入（节点标识：data）
        /// </summary>
        public class data
        {
            public string setl_optins { get; set; }// 
            public string file_qury_no { get; set; }// 
            public string stmt_begndate { get; set; }// 
            public string stmt_enddate { get; set; }// 
            public decimal medfee_sumamt { get; set; }// 
            public decimal fund_pay_sumamt { get; set; }// 
            public decimal cash_payamt { get; set; }// 
            public int fixmedins_setl_cnt { get; set; }// 

        }

        public class Root
        {
            public data data { get; set; }
        }
    }


    public class RT3202
    {
        /// <summary>
        /// 输出（节点标识：fileinfo）
        /// </summary>
        public class fileinfo
        {
            public string file_qury_no { get; set; }//
            public string filename { get; set; }// 
            public string dld_endtime { get; set; }// 
        }

        public class Root
        {
            public fileinfo fileinfo { get; set; }

        }
    }
    #endregion
}
