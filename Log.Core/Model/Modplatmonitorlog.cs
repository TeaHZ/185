using System;
namespace Log.Core.Model
{
    /// <summary>
    /// platmonitorlog:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Modplatmonitorlog
    {
        public Modplatmonitorlog()
        { }
        #region Model
        private int? _id;
        private string _hos_id;
        private string _pat_name;
        private string _sfz_no;
        private string _m_type;
        private string _m_level;
        private string _m_content;
        private DateTime? _acc_time;
        private DateTime? _record_time;
        private string _deal_person;
        private DateTime? _deal_notice_time;
        private DateTime? _deal_confirm_time;
        private DateTime? _deal_reply;
        private bool _deal_status;
        private string _deal_result;
        /// <summary>
        /// 
        /// </summary>
        public int? ID
        {
            set { _id = value; }
            get { return _id; }
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
        public string PAT_NAME
        {
            set { _pat_name = value; }
            get { return _pat_name; }
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
        public string M_TYPE
        {
            set { _m_type = value; }
            get { return _m_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string M_LEVEL
        {
            set { _m_level = value; }
            get { return _m_level; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string M_CONTENT
        {
            set { _m_content = value; }
            get { return _m_content; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ACC_TIME
        {
            set { _acc_time = value; }
            get { return _acc_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? NOW
        {
            set { _record_time = value; }
            get { return _record_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DEAL_PERSON
        {
            set { _deal_person = value; }
            get { return _deal_person; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DEAL_NOTICE_TIME
        {
            set { _deal_notice_time = value; }
            get { return _deal_notice_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DEAL_CONFIRM_TIME
        {
            set { _deal_confirm_time = value; }
            get { return _deal_confirm_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DEAL_REPLY
        {
            set { _deal_reply = value; }
            get { return _deal_reply; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DEAL_STATUS
        {
            set { _deal_status = value; }
            get { return _deal_status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DEAL_RESULT
        {
            set { _deal_result = value; }
            get { return _deal_result; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string M_ALL_CONTENT
        {
            get;set;
        }
        /// <summary>
        /// 处理建议
        /// </summary>
        public string DEAL_ID
        {
            get;set;
        }
        /// <summary>
        /// 处理建议
        /// </summary>
        public string DEAL_TIPS
        {
            get; set;
        }
        /// <summary>
        /// 错误来源
        /// </summary>
        public string SOURCE { get; set; }
        #endregion Model

    }
}

