using System;

namespace ZZJ_InHos.Plat.Model
{
    /// <summary>
    /// pat_card:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class pat_card
    {
        public pat_card()
        { }

        #region Model

        private int _pat_id;
        private int _ylcartd_type;
        private string _ylcard_no;
        private DateTime _create_time;
        private string _mark_del;
        private DateTime? _del_time;

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
        public int YLCARTD_TYPE
        {
            set { _ylcartd_type = value; }
            get { return _ylcartd_type; }
        }

        /// <summary>
        ///
        /// </summary>
        public string YLCARD_NO
        {
            set { _ylcard_no = value; }
            get { return _ylcard_no; }
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
        public string MARK_DEL
        {
            set { _mark_del = value; }
            get { return _mark_del; }
        }

        /// <summary>
        ///
        /// </summary>
        public DateTime? DEL_TIME
        {
            set { _del_time = value; }
            get { return _del_time; }
        }

        #endregion Model
    }
}