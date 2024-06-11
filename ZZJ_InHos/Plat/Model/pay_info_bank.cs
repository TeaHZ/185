using System;

namespace ZZJ_InHos.Plat.Model
{
    /// <summary>
    /// pay_info_bank:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class pay_info_bank
    {
        public pay_info_bank()
        { }

        #region Model

        private string _pay_id;
        private int _biz_type;
        private string _bdj_id;
        private decimal _je;
        private int _bank_type;
        private string _return_code;
        private string _bank_card;
        private string _card_type;
        private string _search_code;
        private string _refcode;
        private string _termcode;
        private string _card_compay;
        private string _comm_name;
        private string _comm_sn;
        private string _sfz_no;
        private DateTime _dj_time;

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
        public int BANK_TYPE
        {
            set { _bank_type = value; }
            get { return _bank_type; }
        }

        /// <summary>
        ///
        /// </summary>
        public string RETURN_CODE
        {
            set { _return_code = value; }
            get { return _return_code; }
        }

        /// <summary>
        ///
        /// </summary>
        public string BANK_CARD
        {
            set { _bank_card = value; }
            get { return _bank_card; }
        }

        /// <summary>
        ///
        /// </summary>
        public string CARD_TYPE
        {
            set { _card_type = value; }
            get { return _card_type; }
        }

        /// <summary>
        ///
        /// </summary>
        public string SEARCH_CODE
        {
            set { _search_code = value; }
            get { return _search_code; }
        }

        /// <summary>
        ///
        /// </summary>
        public string REFCODE
        {
            set { _refcode = value; }
            get { return _refcode; }
        }

        /// <summary>
        ///
        /// </summary>
        public string TERMCODE
        {
            set { _termcode = value; }
            get { return _termcode; }
        }

        /// <summary>
        ///
        /// </summary>
        public string CARD_COMPAY
        {
            set { _card_compay = value; }
            get { return _card_compay; }
        }

        /// <summary>
        ///
        /// </summary>
        public string COMM_NAME
        {
            set { _comm_name = value; }
            get { return _comm_name; }
        }

        /// <summary>
        ///
        /// </summary>
        public string COMM_SN
        {
            set { _comm_sn = value; }
            get { return _comm_sn; }
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

        #endregion Model
    }
}