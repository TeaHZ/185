using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Core.Model
{
    /// <summary>
    /// histranlog:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class histranlog
    {
        public histranlog()
        { }
        #region Model
        private string _comm_sn;
        private string _paytype;
        private string _hos_id;
        private string _comm_his;
        private decimal _je = 0.00M;
        private string _txn_type;
        private string _trade_status;
        private string _callin;
        private string _callrec;
        private string _client_info;
        private DateTime? _now;
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
        public string PAYTYPE
        {
            set { _paytype = value; }
            get { return _paytype; }
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
        public string COMM_HIS
        {
            set { _comm_his = value; }
            get { return _comm_his; }
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
        public string trade_Status
        {
            set { _trade_status = value; }
            get { return _trade_status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CallIn
        {
            set { _callin = value; }
            get { return _callin; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CallRec
        {
            set { _callrec = value; }
            get { return _callrec; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string client_Info
        {
            set { _client_info = value; }
            get { return _client_info; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? Now
        {
            set { _now = value; }
            get { return _now; }
        }
        #endregion Model

    }

    /// <summary>
    /// ifpsPOSlog:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ifpsPOSlog
    {
        public ifpsPOSlog()
        { }
        #region Model
        private string _comm_sn;
        private string _datain;
        private string _datarec;
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
        public string DataIn
        {
            set { _datain = value; }
            get { return _datain; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DataRec
        {
            set { _datarec = value; }
            get { return _datarec; }
        }
        #endregion Model

    }

}
