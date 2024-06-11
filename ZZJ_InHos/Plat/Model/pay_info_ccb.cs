using System;

namespace ZZJ_InHos.Plat.Model
{
    /// <summary>
    /// pay_info_ccb:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class pay_info_ccb
    {
        public pay_info_ccb()
        { }

        #region Model

        private string _pay_id;
        private int _biz_type;
        private string _bdj_id;
        private decimal _je;
        private string _posid;
        private string _branchid;
        private string _orderid;
        private string _sfz_no;
        private DateTime _dj_time;
        private string _txn_type;
        private string _merchantid;

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
        public string BDj_id
        {
            set { _bdj_id = value; }
            get { return _bdj_id; }
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
        public string POSID
        {
            set { _posid = value; }
            get { return _posid; }
        }

        /// <summary>
        ///
        /// </summary>
        public string BRANCHID
        {
            set { _branchid = value; }
            get { return _branchid; }
        }

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
        public string SFZ_NO
        {
            set { _sfz_no = value; }
            get { return _sfz_no; }
        }

        /// <summary>
        ///
        /// </summary>
        public DateTime DJ_TIME
        {
            set { _dj_time = value; }
            get { return _dj_time; }
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
        public string MerchantID
        {
            set { _merchantid = value; }
            get { return _merchantid; }
        }

        #endregion Model
    }
}