using System;

namespace ZZJ_InHos.Plat.Model
{
    /// <summary>
    /// pay_info:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class pay_info
    {
        public pay_info()
        { }

        #region Model

        private string _pay_id;
        private string _hos_id;
        private int _pat_id;
        private int _regpat_id;
        private int _biz_type;
        private string _biz_sn;
        private decimal _cash_je;
        private string _sfz_no;
        private string _dj_date;
        private string _dj_time;
        private string _deal_type;
        private string _deal_states;
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
        public int REGPAT_ID
        {
            set { _regpat_id = value; }
            get { return _regpat_id; }
        }

        /// <summary>
        /// 业务类型
        /// </summary>
        public int BIZ_TYPE
        {
            set { _biz_type = value; }
            get { return _biz_type; }
        }

        /// <summary>
        /// 对应业务ID
        /// </summary>
        public string BIZ_SN
        {
            set { _biz_sn = value; }
            get { return _biz_sn; }
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
        public string SFZ_NO
        {
            set { _sfz_no = value; }
            get { return _sfz_no; }
        }

        /// <summary>
        ///
        /// </summary>
        public string DJ_DATE
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
        /// 交易方式
        /// </summary>
        public string DEAL_TYPE
        {
            set { _deal_type = value; }
            get { return _deal_type; }
        }

        /// <summary>
        ///
        /// </summary>
        public string DEAL_STATES
        {
            set { _deal_states = value; }
            get { return _deal_states; }
        }

        /// <summary>
        ///
        /// </summary>
        public string lTERMINAL_SN
        {
            set { _lterminal_sn = value; }
            get { return _lterminal_sn; }
        }

        /// <summary>
        ///
        /// </summary>
        public string USER_ID { get; set; }

        #endregion Model
    }
}