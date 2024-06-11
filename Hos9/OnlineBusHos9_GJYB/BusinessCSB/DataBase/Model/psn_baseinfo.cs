using System;
namespace OnlineBusHos9_GJYB.Model
{
    //psn_baseinfo
    public class psn_baseinfo
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
        /// psn_cert_type
        /// </summary>		
        private string _psn_cert_type;
        public string psn_cert_type
        {
            get { return _psn_cert_type; }
            set { _psn_cert_type = value; }
        }
        /// <summary>
        /// certno
        /// </summary>		
        private string _certno;
        public string certno
        {
            get { return _certno; }
            set { _certno = value; }
        }
        /// <summary>
        /// psn_name
        /// </summary>		
        private string _psn_name;
        public string psn_name
        {
            get { return _psn_name; }
            set { _psn_name = value; }
        }
        /// <summary>
        /// gend
        /// </summary>		
        private string _gend;
        public string gend
        {
            get { return _gend; }
            set { _gend = value; }
        }
        /// <summary>
        /// naty
        /// </summary>		
        private string _naty;
        public string naty
        {
            get { return _naty; }
            set { _naty = value; }
        }
        /// <summary>
        /// brdy
        /// </summary>		
        private DateTime? _brdy;
        public DateTime? brdy
        {
            get { return _brdy; }
            set { _brdy = value; }
        }
        /// <summary>
        /// expContent
        /// </summary>		
        private string _expcontent;
        public string expContent
        {
            get { return _expcontent; }
            set { _expcontent = value; }
        }
        /// <summary>
        /// Sync_flag
        /// </summary>		
        private bool _sync_flag;
        public bool Sync_flag
        {
            get { return _sync_flag; }
            set { _sync_flag = value; }
        }

    }
}

