using System;

namespace ZZJ_InHos.Plat.Model
{
    /// <summary>
    /// pay_info_zfb:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class pay_info_zfb
    {
        public pay_info_zfb()
        { }

        #region Model

        private string _pay_id;
        private int _biz_type;
        private string _biz_sn;
        private string _seller_id;
        private string _comm_sn;
        private decimal _je;
        private string _deal_states;
        private string _txn_type;
        private DateTime _deal_time;
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
        public int BIZ_TYPE
        {
            set { _biz_type = value; }
            get { return _biz_type; }
        }

        /// <summary>
        ///
        /// </summary>
        public string BIZ_SN
        {
            set { _biz_sn = value; }
            get { return _biz_sn; }
        }

        /// <summary>
        ///
        /// </summary>
        public string SELLER_ID
        {
            set { _seller_id = value; }
            get { return _seller_id; }
        }

        /// <summary>
        ///
        /// </summary>
        public string COMM_SN
        {
            set { _comm_sn = value; }
            get { return _comm_sn; }
        }

        /// <summary>
        ///
        /// </summary>
        public decimal JE
        {
            set { _je = value; }
            get { return _je; }
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
        public string TXN_TYPE
        {
            set { _txn_type = value; }
            get { return _txn_type; }
        }

        /// <summary>
        ///
        /// </summary>
        public DateTime DEAL_TIME
        {
            set { _deal_time = value; }
            get { return _deal_time; }
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