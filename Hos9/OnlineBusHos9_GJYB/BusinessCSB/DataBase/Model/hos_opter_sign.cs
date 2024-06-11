using System;
namespace OnlineBusHos9_GJYB.Model
{
    //hos_opter_sign
    public class hos_opter_sign
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
        /// mac
        /// </summary>		
        private string _mac;
        public string mac
        {
            get { return _mac; }
            set { _mac = value; }
        }
        /// <summary>
        /// ip
        /// </summary>		
        private string _ip;
        public string ip
        {
            get { return _ip; }
            set { _ip = value; }
        }
        /// <summary>
        /// sign_no
        /// </summary>		
        private string _sign_no;
        public string sign_no
        {
            get { return _sign_no; }
            set { _sign_no = value; }
        }
        /// <summary>
        /// sign_intime
        /// </summary>		
        private DateTime _sign_intime;
        public DateTime sign_intime
        {
            get { return _sign_intime; }
            set { _sign_intime = value; }
        }
        /// <summary>
        /// sign_outtime
        /// </summary>		
        private DateTime _sign_outtime;
        public DateTime sign_outtime
        {
            get { return _sign_outtime; }
            set { _sign_outtime = value; }
        }
        /// <summary>
        /// signout_flag
        /// </summary>		
        private int _signout_flag;
        public int signout_flag
        {
            get { return _signout_flag; }
            set { _signout_flag = value; }
        }

    }
}

