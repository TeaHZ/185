using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Core.Model
{

    /// <summary>
    /// compaylog:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ModCompaylog
    {
        public ModCompaylog()
        { }
        #region Model
        private string _orderid;
        private decimal _je;
        private string _btype;
        private string _account;
        private string _refund_status;
        private string _return_code;
        private string _datere;
        private string _datesend;
        private DateTime _now;
        /// <summary>
        /// 订单号 唯一
        /// </summary>
        public string ORDERID
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal JE
        {
            set { _je = value; }
            get { return _je; }
        }
        /// <summary>
        /// 类型
        /// </summary>
        public string BTYPE
        {
            set { _btype = value; }
            get { return _btype; }
        }
        /// <summary>
        /// 账户，由平台生成
        /// </summary>
        public string ACCOUNT
        {
            set { _account = value; }
            get { return _account; }
        }
        /// <summary>
        /// 退款状态
        /// </summary>
        public string REFUND_STATUS
        {
            set { _refund_status = value; }
            get { return _refund_status; }
        }
        /// <summary>
        /// 返回结果代码
        /// </summary>
        public string RETURN_CODE
        {
            set { _return_code = value; }
            get { return _return_code; }
        }
        /// <summary>
        /// 收到数据
        /// </summary>
        public string DATERE
        {
            set { _datere = value; }
            get { return _datere; }
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        public string DATESEND
        {
            set { _datesend = value; }
            get { return _datesend; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime now
        {
            set { _now = value; }
            get { return _now; }
        }
        #endregion Model

    }
}
