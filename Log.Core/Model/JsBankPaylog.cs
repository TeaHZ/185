using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Core.Model
{
    /// <summary>
	/// jsbankpaylog:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
    public partial class jsbankpaylog
    {
        public jsbankpaylog()
        { }
        #region Model
        private string _orderid;
        private string _mechid;
        private decimal? _je;
        private string _trade_no;
        private string _trade_status;
        private DateTime? _now;
        private string _datesend;
        private string _datere;
        private string _errcode;
        private string _errmsg;
        private string _BType;
        /// <summary>
        /// 
        /// </summary>
        public string OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MechID
        {
            set { _mechid = value; }
            get { return _mechid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Je
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
        public DateTime? Now
        {
            set { _now = value; }
            get { return _now; }
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
        /// <summary>
        /// 
        /// </summary>
        public string errcode
        {
            set { _errcode = value; }
            get { return _errcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string errmsg
        {
            set { _errmsg = value; }
            get { return _errmsg; }
        }
        public string BType
        {
            set { _BType = value; }
            get { return _BType; }
        }
        #endregion Model
    }
}
