namespace OnlineBusHos9_GJYB.Models
{
    #region N：6.3.2.1 【3201】医药机构费用结算对总账

    public class T3201
    {
        /// <summary>
        /// 输入（节点标识：data）
        /// </summary>
        public class data
        {
            public string insutype { get; set; }// 
            public string clr_type { get; set; }// 
            public string setl_optins { get; set; }// 
            public string stmt_begndate { get; set; }// 
            public string stmt_enddate { get; set; }// 
            public decimal medfee_sumamt { get; set; }// 
            public decimal fund_pay_sumamt { get; set; }// 
            public decimal acct_pay { get; set; }// 
            public int fixmedins_setl_cnt { get; set; }// 
        }

        public class Root
        {
            public data data { get; set; }
        }
    }


    public class RT3201
    {
        /// <summary>
        /// 输出（节点标识：stmtinfo）
        /// </summary>
        public class stmtinfo
        {
            public string setl_optins { get; set; }//结算经办机构
            public string stmt_rslt { get; set; }// 对账结果
            public string stmt_rslt_dscr { get; set; }// 对账结果说明
        }

        public class Root
        {
            public stmtinfo stmtinfo { get; set; }

        }
    }
    #endregion
}
