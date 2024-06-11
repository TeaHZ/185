using System;

namespace ZZJ_InHos.Plat.Model
{
    /// <summary>
    /// regtopat:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class regtopat
    {
        public regtopat()
        { }

        #region Model

        private int _regpat_id;
        private int _pat_id;
        private DateTime? _band_time;
        private bool _mark_del = false;
        private string _del_time;
        private string _lterminal_sn;
        private string _del_lterminal_sn;

        /// <summary>
        ///
        /// </summary>
        public int REGPAT_ID
        {
            set { _regpat_id = value; }
            get { return _regpat_id; }
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
        public DateTime? BAND_TIME
        {
            set { _band_time = value; }
            get { return _band_time; }
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
        public string DEL_TIME
        {
            set { _del_time = value; }
            get { return _del_time; }
        }

        /// <summary>
        ///
        /// </summary>
        public string lTERMINAL_SN
        {
            set { _lterminal_sn = value; }
            get { return _lterminal_sn; }
        }

        /// <summary>
        ///
        /// </summary>
        public string DEL_lTERMINAL_SN
        {
            set { _del_lterminal_sn = value; }
            get { return _del_lterminal_sn; }
        }

        #endregion Model
    }
}