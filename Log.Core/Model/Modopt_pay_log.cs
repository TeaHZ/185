using System;
namespace Log.Core.Model
{
    /// <summary>
    /// opt_pay_log:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Modopt_pay_log
    {
        public Modopt_pay_log()
        { }
        #region Model
        private string _pay_id;
        private string _states;
        private string _hos_id;
        private int _pat_id;
        private string _hsp_sn;
        private decimal _jeall = 0.00M;
        private decimal? _cash_je;
        private DateTime _dj_date;
        private string _dj_time;
        private string _lterminal_sn;
        /// <summary>
        /// 
        /// </summary>
        public string PAY_ID
        {
            set { _pay_id = value; }
            get { return _pay_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STATES
        {
            set { _states = value; }
            get { return _states; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HOS_ID
        {
            set { _hos_id = value; }
            get { return _hos_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int PAT_ID
        {
            set { _pat_id = value; }
            get { return _pat_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string HSP_SN
        {
            set { _hsp_sn = value; }
            get { return _hsp_sn; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal JEALL
        {
            set { _jeall = value; }
            get { return _jeall; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? CASH_JE
        {
            set { _cash_je = value; }
            get { return _cash_je; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DJ_DATE
        {
            set { _dj_date = value; }
            get { return _dj_date; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DJ_TIME
        {
            set { _dj_time = value; }
            get { return _dj_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string lTERMINAL_SN
        {
            set { _lterminal_sn = value; }
            get { return _lterminal_sn; }
        }
        #endregion Model

    }
}

