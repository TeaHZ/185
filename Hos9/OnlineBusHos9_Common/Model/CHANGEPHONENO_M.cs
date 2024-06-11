using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_Common.Model
{
    internal class CHANGEPHONENO_M
    {

            public class CHANGEPHONENO_IN
        {

                public string HOSPATID { get; set; }


                public string PHONENO { get; set; }

                public string HOS_ID { get; set; }

                public string USER_ID { get; set; }


                public string LTERMINAL_SN { get; set; }


                public string SOURCE { get; set; }

                public string FILTER { get; set; }
            }

            public class CHANGEPHONENO_OUT
        {
            public string HOSPATID { get; set; }

            public string PHONENO { get; set; }

            public string USER_ID { get; set; }

            public string LTERMINAL_SN { get; set; }
        }

        
    }
}
