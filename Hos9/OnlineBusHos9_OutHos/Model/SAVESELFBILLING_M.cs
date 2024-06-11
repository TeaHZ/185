using System;
using System.Collections.Generic;
using System.Text;
using static OnlineBusHos9_OutHos.Model.GETSELFBILLINGLIST_M;

namespace OnlineBusHos9_OutHos.Model
{
    internal class SAVESELFBILLING_M
    {
        public class SAVESELFBILLING_IN
        {

            public string HOS_ID { get; set; }
            public string USER_ID { get; set; }
            public string LTERMINAL_SN { get; set; }
            public string FILTER { get; set; }
            public string HOSPATID { get; set; }
            public string YLCARD_TYPE { get; set; }
            public string YLCARD_NO { get; set; }
            public string SFZ_NO { get; set; }
            public string BILLING_DATA { get; set; }
            public string BUS_TYPE { get; set; }
            public string ORDERLISTS { get; set; }
            public string HDINDICATOR { get; set; }
            

        }
        public class SAVESELFBILLING_OUT
        {

            public List<long> ORDERLISTS { get; set; }

        }
    }
}

