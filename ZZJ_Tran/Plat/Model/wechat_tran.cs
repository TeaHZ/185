using System;

namespace ZZJ_Tran.Plat.Model
{
    /// <summary>
    /// wechat_tran:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class wechat_tran
    {
        public wechat_tran()
        { }

        #region Model

        private string _comm_sn;
        private string _appid;
        private string _mch_id;
        private string _txn_type;
        private string _device_info;
        private string _nonce_str;
        private string _body;
        private string _currency_type;
        private decimal _je;
        private DateTime _time_start;
        private string _spbill_create_ip;
        private string _trade_type;
        private string _openid;
        private DateTime? _request_back_time;
        private string _return_code;
        private string _prepay_id;
        private string _at_time;
        private string _at_result_code;
        private string _transaction_id;
        private DateTime? _time_end;
        private string _refund_channe;
        private string _refund_recv_accout;

        private string _user_id;
        private string _ltermainal_sn;
        private string _pat_name;
        private string _sfz_no;
        private string _hospatid;

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
        public string appid
        {
            set { _appid = value; }
            get { return _appid; }
        }

        /// <summary>
        ///
        /// </summary>
        public string mch_id
        {
            set { _mch_id = value; }
            get { return _mch_id; }
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
        public string device_info
        {
            set { _device_info = value; }
            get { return _device_info; }
        }

        /// <summary>
        ///
        /// </summary>
        public string nonce_str
        {
            set { _nonce_str = value; }
            get { return _nonce_str; }
        }

        /// <summary>
        ///
        /// </summary>
        public string body
        {
            set { _body = value; }
            get { return _body; }
        }

        /// <summary>
        ///
        /// </summary>
        public string currency_type
        {
            set { _currency_type = value; }
            get { return _currency_type; }
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
        public DateTime time_start
        {
            set { _time_start = value; }
            get { return _time_start; }
        }

        /// <summary>
        ///
        /// </summary>
        public string spbill_create_ip
        {
            set { _spbill_create_ip = value; }
            get { return _spbill_create_ip; }
        }

        /// <summary>
        ///
        /// </summary>
        public string trade_type
        {
            set { _trade_type = value; }
            get { return _trade_type; }
        }

        /// <summary>
        ///
        /// </summary>
        public string openid
        {
            set { _openid = value; }
            get { return _openid; }
        }

        /// <summary>
        ///
        /// </summary>
        public DateTime? request_back_time
        {
            set { _request_back_time = value; }
            get { return _request_back_time; }
        }

        /// <summary>
        ///
        /// </summary>
        public string return_code
        {
            set { _return_code = value; }
            get { return _return_code; }
        }

        /// <summary>
        ///
        /// </summary>
        public string prepay_id
        {
            set { _prepay_id = value; }
            get { return _prepay_id; }
        }

        /// <summary>
        ///
        /// </summary>
        public string AT_TIME
        {
            set { _at_time = value; }
            get { return _at_time; }
        }

        /// <summary>
        ///
        /// </summary>
        public string AT_result_code
        {
            set { _at_result_code = value; }
            get { return _at_result_code; }
        }

        /// <summary>
        ///
        /// </summary>
        public string transaction_id
        {
            set { _transaction_id = value; }
            get { return _transaction_id; }
        }

        /// <summary>
        ///
        /// </summary>
        public DateTime? time_end
        {
            set { _time_end = value; }
            get { return _time_end; }
        }

        /// <summary>
        ///
        /// </summary>
        public string refund_channe
        {
            set { _refund_channe = value; }
            get { return _refund_channe; }
        }

        /// <summary>
        ///
        /// </summary>
        public string refund_recv_accout
        {
            set { _refund_recv_accout = value; }
            get { return _refund_recv_accout; }
        }

        /// <summary>
        ///
        /// </summary>
        public string trade_code
        {
            get;
            set;
        }

        /// <summary>
        ///
        /// </summary>
        public string trade_message
        {
            get;
            set;
        }

        /// <summary>
        ///
        /// </summary>
        public string error_code
        {
            get;
            set;
        }

        /// <summary>
        ///
        /// </summary>
        public string error_message
        {
            get;
            set;
        }

        public string COMM_MAIN
        {
            get;
            set;
        }

        public string USER_ID
        {
            set { _user_id = value; }
            get { return _user_id; }
        }

        public string lTERMINAL_SN
        {
            set { _ltermainal_sn = value; }
            get { return _ltermainal_sn; }
        }

        public string PAT_NAME
        {
            set { _pat_name = value; }
            get { return _pat_name; }
        }

        public string SFZ_NO
        {
            set { _sfz_no = value; }
            get { return _sfz_no; }
        }

        public string HOSPATID
        {
            set { _hospatid = value; }
            get { return _hospatid; }
        }

        #endregion Model
    }
}