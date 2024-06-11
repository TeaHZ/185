using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Core.Model
{
    /// <summary>
    /// default_appt_log:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Moddefault_appt_log
    {
        public Moddefault_appt_log()
        { }
        #region Model
        private string _hos_id;
        private string _dept_code;
        private string _sch_date;
        private string _sch_time;
        private int _pat_id;
        private string _doc_no;
        private string _appt_time;
        private decimal _gh_fee = 0.00M;
        private decimal _zl_fee = 0.00M;
        private string _period_start;
        private string _period_cause;
        private DateTime _def_time;
        private string _save_type;
        private string _appt_type;
        private int _reg_id;
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
        public string DEPT_CODE
        {
            set { _dept_code = value; }
            get { return _dept_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SCH_DATE
        {
            set { _sch_date = value; }
            get { return _sch_date; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SCH_TIME
        {
            set { _sch_time = value; }
            get { return _sch_time; }
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
        public string DOC_NO
        {
            set { _doc_no = value; }
            get { return _doc_no; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string APPT_TIME
        {
            set { _appt_time = value; }
            get { return _appt_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal GH_FEE
        {
            set { _gh_fee = value; }
            get { return _gh_fee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal ZL_FEE
        {
            set { _zl_fee = value; }
            get { return _zl_fee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PERIOD_START
        {
            set { _period_start = value; }
            get { return _period_start; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PERIOD_CAUSE
        {
            set { _period_cause = value; }
            get { return _period_cause; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DEF_TIME
        {
            set { _def_time = value; }
            get { return _def_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SAVE_TYPE
        {
            set { _save_type = value; }
            get { return _save_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string APPT_TYPE
        {
            set { _appt_type = value; }
            get { return _appt_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int REG_ID
        {
            set { _reg_id = value; }
            get { return _reg_id; }
        }
        #endregion Model

    }
}

