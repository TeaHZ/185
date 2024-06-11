using System;

namespace ZZJ_InHos.Plat.Model
{
    /// <summary>
    /// pay_info_wc:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class pay_info_wc
    {
        public pay_info_wc()
        { }

        #region Model

        private string _pay_id;
        private string _wechat;
        private string _pay_type;
        private int _biz_type;
        private string _biz_sn;
        private string _comm_sn;
        private decimal _je;
        private string _comm_name;
        private string _deal_states;
        private DateTime _deal_time;
        private string _deal_sn;
        private string _lterminal_sn;
        private string _tnx_type;

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
        public string WECHAT
        {
            set { _wechat = value; }
            get { return _wechat; }
        }

        /// <summary>
        ///
        /// </summary>
        public string PAY_TYPE
        {
            set { _pay_type = value; }
            get { return _pay_type; }
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
        public string COMM_NAME
        {
            set { _comm_name = value; }
            get { return _comm_name; }
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
        public DateTime DEAL_TIME
        {
            set { _deal_time = value; }
            get { return _deal_time; }
        }

        /// <summary>
        ///
        /// </summary>
        public string DEAL_SN
        {
            set { _deal_sn = value; }
            get { return _deal_sn; }
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
        public string TNX_TYPE
        {
            set { _tnx_type = value; }
            get { return _tnx_type; }
        }

        #endregion Model
    }
}