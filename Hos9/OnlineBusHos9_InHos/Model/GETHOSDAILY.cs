using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_InHos.Model
{
    internal class GETHOSDAILY
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
            public string BEGIN_DATE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string END_DATE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string SOURCE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string FILTER { get; set; }

            public string SFZ_NO { get; set; }
        }


        public class ITEMLISTItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string NAME { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string GG { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string AMOUNT { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string CAMT { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string JE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string JE_ALL { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string DJ_DATE { get; set; }

            public string PRICE { get; set; }
        }

        public class GITEMLISTItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string BIG_NAME { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string NAME { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string GG { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string AMOUNT { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string CAMT { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string JE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string JE_ALL { get; set; }
        }

        public class BIGITEMLISTItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string ITEM_NAME { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string JE_ALL { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<ITEMLISTItem> ITEMLIST { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<GITEMLISTItem> GITEMLIST { get; set; }
        }

   

        public class Out
        {
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
            public string JE_TODAY { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<BIGITEMLISTItem> BIGITEMLIST { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string HIS_RTNXML { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string PARAMETERS { get; set; }
        }


    }
}
