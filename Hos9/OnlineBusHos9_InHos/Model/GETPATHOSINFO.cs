using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_InHos.Model
{
    internal class GETPATHOSINFO
    {
        public class In
        {
            /// <summary>
            /// 
            /// </summary>
            public string HOS_ID { get; set; }
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
            public string HOS_NO { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HOSPATID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string PAT_NAME { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string SFZ_NO { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string SOURCE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string FILTER { get; set; }
        }

        public class PAYLISTItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string JE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HIN_TIME { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string JE_NOTE { get; set; }
        }

        public class FEELISTItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string JE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string FEE_NOTE { get; set; }
        }

        public class PARAMETERS
        {
        }

        public class OUt
        {
            public List<ZYList> ZYLIST { get; set; }
        }
        public class ZYList
        {
            /// <summary>
            /// 
            /// </summary>
            public string HOS_NO { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HOSPATID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string JE_PAY { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string JE_YET { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string JE_REMAIN { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string CAN_PAY { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<PAYLISTItem> PAYLIST { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<FEELISTItem> FEELIST { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HIS_RTNXML { get; set; }
            public string HIN_TIME { get; set; }//todozy
            public string GREENINDICATOR { get; set; }//todozy
            /// <summary>
            /// 
            /// </summary>
            public PARAMETERS PARAMETERS { get; set; }
            
        }

    }
}
