using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Core.Model
{
   public  class ModCCBpaylog
    {
        #region Model
        private string _orderid;
        private string _merchantid;
        private string _posid;
        private string _branchid;
        private decimal _je;
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
        public string MerchantID
        {
            set { _merchantid = value; }
            get { return _merchantid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PosID
        {
            set { _posid = value; }
            get { return _posid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BranchID
        {
            set { _branchid = value; }
            get { return _branchid; }
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
