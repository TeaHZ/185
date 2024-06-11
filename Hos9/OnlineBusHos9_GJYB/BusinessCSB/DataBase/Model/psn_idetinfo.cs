using System;
namespace OnlineBusHos9_GJYB.Model
{
    //psn_idetinfo
    public class psn_idetinfo
    {

        /// <summary>
        /// psn_no
        /// </summary>		
        private string _psn_no;
        public string psn_no
        {
            get { return _psn_no; }
            set { _psn_no = value; }
        }
        /// <summary>
        /// psn_idet_type
        /// </summary>		
        private string _psn_idet_type;
        public string psn_idet_type
        {
            get { return _psn_idet_type; }
            set { _psn_idet_type = value; }
        }
        /// <summary>
        /// psn_type_lv
        /// </summary>		
        private string _psn_type_lv;
        public string psn_type_lv
        {
            get { return _psn_type_lv; }
            set { _psn_type_lv = value; }
        }
        /// <summary>
        /// memo
        /// </summary>		
        private string _memo;
        public string memo
        {
            get { return _memo; }
            set { _memo = value; }
        }
        /// <summary>
        /// begntime
        /// </summary>		
        private DateTime? _begntime;
        public DateTime? begntime
        {
            get { return _begntime; }
            set { _begntime = value; }
        }
        /// <summary>
        /// endtime
        /// </summary>		
        private DateTime? _endtime;
        public DateTime? endtime
        {
            get { return _endtime; }
            set { _endtime = value; }
        }

    }
}

