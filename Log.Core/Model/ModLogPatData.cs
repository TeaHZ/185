using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Core.Model
{

    /// <summary>
    /// logpatdata:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ModLogPatData
    {
        public ModLogPatData()
        { }
        #region Model
        private string _guid;
        private DateTime _now;
        private string _plat_id;
        private string _hos_id;
        private string _biz_name;
        private string _biz_info;
        private int _pat_id;
        private string _pat_name;
        private string _trade_code;
        private string _trade_message;
        /// <summary>
        /// 
        /// </summary>
        public string GUID
        {
            set { _guid = value; }
            get { return _guid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime NOW
        {
            set { _now = value; }
            get { return _now; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PLAT_ID
        {
            set { _plat_id = value; }
            get { return _plat_id; }
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
        public string BIZ_NAME
        {
            set { _biz_name = value; }
            get { return _biz_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BIZ_INFO
        {
            set { _biz_info = value; }
            get { return _biz_info; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int PAT_ID
        {
            set { _pat_id = value; }
            get { return _pat_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PAT_NAME
        {
            set { _pat_name = value; }
            get { return _pat_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TRADE_CODE
        {
            set { _trade_code = value; }
            get { return _trade_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TRADE_MESSAGE
        {
            set { _trade_message = value; }
            get { return _trade_message; }
        }
        /// <summary>
        /// HIS流水号
        /// </summary>
        public string HIS_LSH { get; set; }
        #endregion Model

    }
}
