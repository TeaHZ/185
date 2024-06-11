using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Core.Model
{/// <summary>
 /// xzlhfkpaylog:实体类(属性说明自动提取数据库字段的描述信息)
 /// </summary>
    [Serializable]
    public partial class xzlhfkpaylog
    {
        public xzlhfkpaylog()
        { }
        #region Model
        private string _orderid;
        private int? _hos_id;
        private decimal? _je;
        private string _trade_no;
        private DateTime? _now;
        private string _datesend;
        private string _datere;
        private string _btype;
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
        public int? HOS_ID
        {
            set { _hos_id = value; }
            get { return _hos_id; }
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
        public string BType
        {
            set { _btype = value; }
            get { return _btype; }
        }
        #endregion Model

    }
}
