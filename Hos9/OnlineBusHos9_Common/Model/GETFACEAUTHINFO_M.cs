using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_Common.Model
{

    internal class GETFACEAUTHINFO_M
    {
        public class GETFACEAUTHINFO_IN
        {

            public string HOS_ID { get; set; }
            public string USER_ID { get; set; }
            public string LTERMINAL_SN { get; set; }
            public string SOURCE { get; set; }
            public string BUS_TYPE { get; set; }
            public string IMG_BASE64 { get; set; }
            public string PAT_NAME { get; set; }
            public string SFZ_NO { get; set; }

        }

        public class GETFACEAUTHINFO_OUT
        {
            public string RESULT { get; set; }
            public string PAT_NAME { get; set; }
            public string SFZ_NO { get; set; }
            public string FACE_ID { get; set; }
        }
    }
}
