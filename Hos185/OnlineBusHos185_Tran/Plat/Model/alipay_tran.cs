using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos185_Tran.Plat.Model
{
    /// <summary>
    /// alipay_tran:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class alipay_tran
    {
        public alipay_tran()
        { }
        #region Model
        private string _comm_sn;
        private string _trade_no;
        private string _trade_status;
        private decimal _je;
        private string _txn_type;
        private DateTime? _gmt_create;
        private DateTime? _gmt_payment;
        private DateTime? _notify_time;
        private string _notify_type;
        private string _notify_id;
        private string _payment_type;
        private string _seller_id;
        private string _seller_email;
        private string _buyer_id;
        private string _buyer_email;
        private string _body;
        private string _subject;
        private string _refund_status;
        private DateTime _gmt_refund;
        private string _batch_no;

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
        public string TRADE_NO
        {
            set { _trade_no = value; }
            get { return _trade_no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TRADE_STATUS
        {
            set { _trade_status = value; }
            get { return _trade_status; }
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
        public string TXN_TYPE
        {
            set { _txn_type = value; }
            get { return _txn_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? gmt_create
        {
            set { _gmt_create = value; }
            get { return _gmt_create; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? gmt_payment
        {
            set { _gmt_payment = value; }
            get { return _gmt_payment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? notify_time
        {
            set { _notify_time = value; }
            get { return _notify_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string notify_type
        {
            set { _notify_type = value; }
            get { return _notify_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string notify_id
        {
            set { _notify_id = value; }
            get { return _notify_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string payment_type
        {
            set { _payment_type = value; }
            get { return _payment_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string seller_id
        {
            set { _seller_id = value; }
            get { return _seller_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string seller_email
        {
            set { _seller_email = value; }
            get { return _seller_email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string buyer_id
        {
            set { _buyer_id = value; }
            get { return _buyer_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string buyer_email
        {
            set { _buyer_email = value; }
            get { return _buyer_email; }
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
        public string subject
        {
            set { _subject = value; }
            get { return _subject; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string refund_status
        {
            set { _refund_status = value; }
            get { return _refund_status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime gmt_refund
        {
            set { _gmt_refund = value; }
            get { return _gmt_refund; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string batch_no
        {
            set { _batch_no = value; }
            get { return _batch_no; }
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
