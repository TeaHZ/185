namespace OnlineBusHos9_GJYB.Model
{
    //hos_opter
    public class hos_opter
    {

        /// <summary>
        /// HOS_ID
        /// </summary>		
        private string _hos_id;
        public string HOS_ID
        {
            get { return _hos_id; }
            set { _hos_id = value; }
        }
        /// <summary>
        /// opter_no
        /// </summary>		
        private string _opter_no;
        public string opter_no
        {
            get { return _opter_no; }
            set { _opter_no = value; }
        }
        /// <summary>
        /// opter_name
        /// </summary>		
        private string _opter_name;
        public string opter_name
        {
            get { return _opter_name; }
            set { _opter_name = value; }
        }
        /// <summary>
        /// opter_type
        /// </summary>		
        private int _opter_type;
        public int opter_type
        {
            get { return _opter_type; }
            set { _opter_type = value; }
        }
        private string _sign_no;
        public string sign_no
        {
            get { return _sign_no; }
            set { _sign_no = value; }
        }

        private string _sign_date;
        public string sign_date
        {
            get { return _sign_date; }
            set { _sign_date = value; }
        }
        private string _ip;
        public string ip
        {
            get { return _ip; }
            set { _ip = value; }
        }
        private string _mac;
        public string mac
        {
            get { return _mac; }
            set { _mac = value; }
        }
        private string _vali_flag;
        public string vali_flag
        {
            get { return _vali_flag; }
            set { _vali_flag = value; }
        }

    }
}

