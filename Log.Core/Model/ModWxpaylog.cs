using System;
 
namespace Log.Core.Model
{
	/// <summary>
	/// Modwxpaylog:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ModWxpaylog
	{
        public ModWxpaylog()
		{}
        #region Model
        private string _orderid;
        private string _mch_id;
        private decimal _je;
        private string _trade_no;
        private string _trade_status;
        private DateTime _now;
        private string _btype;
        private string _datesend;
        private string _datere;
        /// <summary>
        /// 
        /// </summary>
        public string OrderID
        {
            set { _orderid = value; }
            get { return _orderid; }
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
        public decimal Je
        {
            set { _je = value; }
            get { return _je; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string trade_no
        {
            set { _trade_no = value; }
            get { return _trade_no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string trade_status
        {
            set { _trade_status = value; }
            get { return _trade_status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Now
        {
            set { _now = value; }
            get { return _now; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BType
        {
            set { _btype = value; }
            get { return _btype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DateSend
        {
            set { _datesend = value; }
            get { return _datesend; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DateRe
        {
            set { _datere = value; }
            get { return _datere; }
        }
        #endregion Model


	}
}

