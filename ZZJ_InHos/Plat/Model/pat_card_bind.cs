using System;

namespace ZZJ_InHos.Plat.Model
{
    /// <summary>
    /// pat_card_bind:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class pat_card_bind
    {
        public pat_card_bind()
        { }

        #region Model

        private string _hos_id;
        private int _pat_id;
        private int _ylcartd_type;
        private string _ylcard_no;
        private int _mark_bind;
        private DateTime? _band_time;

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
        public int MARK_BIND
        {
            set { _mark_bind = value; }
            get { return _mark_bind; }
        }

        /// <summary>
        ///
        /// </summary>
        public DateTime? BAND_TIME
        {
            set { _band_time = value; }
            get { return _band_time; }
        }

        #endregion Model
    }
}