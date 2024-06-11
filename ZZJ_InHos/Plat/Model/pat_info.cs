using System;

namespace ZZJ_InHos.Plat.Model
{
    /// <summary>
    /// pat_info:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class pat_info
    {
        public pat_info()
        { }

        #region Model

        private int _pat_id;
        private string _sfz_no;
        private string _pat_name;
        private string _sex;
        private string _birthday;
        private string _address = "";
        private string _mobile_no;
        private string _guardian_name;
        private string _guardian_sfz_no;
        private DateTime _create_time;
        private bool _mark_del = false;
        private DateTime _del_time;
        private DateTime _oper_time;
        private string _note;

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
        public string SFZ_NO
        {
            set { _sfz_no = value; }
            get { return _sfz_no; }
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
        public string SEX
        {
            set { _sex = value; }
            get { return _sex; }
        }

        /// <summary>
        ///
        /// </summary>
        public string BIRTHDAY
        {
            set { _birthday = value; }
            get { return _birthday; }
        }

        /// <summary>
        ///
        /// </summary>
        public string ADDRESS
        {
            set { _address = value; }
            get { return _address; }
        }

        /// <summary>
        ///
        /// </summary>
        public string MOBILE_NO
        {
            set { _mobile_no = value; }
            get { return _mobile_no; }
        }

        /// <summary>
        ///
        /// </summary>
        public string GUARDIAN_NAME
        {
            set { _guardian_name = value; }
            get { return _guardian_name; }
        }

        /// <summary>
        ///
        /// </summary>
        public string GUARDIAN_SFZ_NO
        {
            set { _guardian_sfz_no = value; }
            get { return _guardian_sfz_no; }
        }

        /// <summary>
        ///
        /// </summary>
        public DateTime CREATE_TIME
        {
            set { _create_time = value; }
            get { return _create_time; }
        }

        /// <summary>
        ///
        /// </summary>
        public bool MARK_DEL
        {
            set { _mark_del = value; }
            get { return _mark_del; }
        }

        /// <summary>
        ///
        /// </summary>
        public DateTime DEL_TIME
        {
            set { _del_time = value; }
            get { return _del_time; }
        }

        /// <summary>
        ///
        /// </summary>
        public DateTime OPER_TIME
        {
            set { _oper_time = value; }
            get { return _oper_time; }
        }

        /// <summary>
        ///
        /// </summary>
        public string NOTE
        {
            set { _note = value; }
            get { return _note; }
        }

        public string YB_CARDNO
        {
            get;
            set;
        }

        public string SMK_CARDNO
        {
            get;
            set;
        }

        public string XNH_CARDNO
        {
            get;
            set;
        }

        public string HOS_CARDNO
        {
            get;
            set;
        }

        #endregion Model
    }
}