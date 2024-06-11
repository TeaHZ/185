using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_InHos.Model
{
    class GETPATYDJSD_M
    {
        public class GETPATYDJSD_IN
        {
            /// <summary>
            /// 
            /// </summary>
            public string USER_ID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string LTERMINAL_SN { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string SOURCE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string FILTER { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HOS_ID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string DEAL_TYPE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HOSPATID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string SFZ_NO { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HOS_NO { get; set; }



        }

        public class GETPATYDJSD_OUT
        {
            /// <summary>
            /// 
            /// </summary>
            public string HOS_ID { get; set; }
            /// <summary>
            /// 1:pdf的base64，2pdf地址
            /// </summary>
            public string FILE_TYPE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string FILE_DATA { get; set; }
        }
    }
}
