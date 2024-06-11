using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Core.Model
{
	/// <summary>
	/// alipaylog:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class icbcpaylog
    {
        public icbcpaylog()
        { }
        #region Model
        private string _orderid;
        private string _merid;
        private decimal? _amount;
        private string _btype;
        private DateTime? _now;
        private string _datasend;
        private string _datare;
        /// <summary>
        /// 
        /// </summary>
        public string ORDERID
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MERID
        {
            set { _merid = value; }
            get { return _merid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? AMOUNT
        {
            set { _amount = value; }
            get { return _amount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Btype
        {
            set { _btype = value; }
            get { return _btype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? NOW
        {
            set { _now = value; }
            get { return _now; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DataSend
        {
            set { _datasend = value; }
            get { return _datasend; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DataRe
        {
            set { _datare = value; }
            get { return _datare; }
        }
        #endregion Model

    }
}

