using System;

namespace ZZJ_InHos.Plat.Model
{
    /// <summary>
    /// pat_prepay:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class pat_prepay
    {
        public pat_prepay()
        { }

        #region Model

        private int _pay_id;
        private int _pat_id;
        private string _hos_id;
        private string _hos_pat_id;
        private int _regpat_id;
        private string _hos_pay_sn;
        private decimal _cash_je;
        private DateTime _dj_time;
        private string _lterminal_sn;

        /// <summary>
        ///
        /// </summary>
        public int PAY_ID
        {
            set { _pay_id = value; }
            get { return _pay_id; }
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
        public string HOS_ID
        {
            set { _hos_id = value; }
            get { return _hos_id; }
        }

        /// <summary>
        ///
        /// </summary>
        public string HOS_PAT_ID
        {
            set { _hos_pat_id = value; }
            get { return _hos_pat_id; }
        }

        /// <summary>
        ///
        /// </summary>
        public int REGPAT_ID
        {
            set { _regpat_id = value; }
            get { return _regpat_id; }
        }

        /// <summary>
        ///
        /// </summary>
        public string HOS_PAY_SN
        {
            set { _hos_pay_sn = value; }
            get { return _hos_pay_sn; }
        }

        /// <summary>
        ///
        /// </summary>
        public decimal CASH_JE
        {
            set { _cash_je = value; }
            get { return _cash_je; }
        }

        /// <summary>
        ///
        /// </summary>
        public DateTime DJ_TIME
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

        public string USER_ID { get; set; }

        #endregion Model
    }
}