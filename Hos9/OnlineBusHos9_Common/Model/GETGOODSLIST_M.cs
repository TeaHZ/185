using System.Collections.Generic;

namespace OnlineBusHos9_Common.Model
{
    internal class GETGOODSLIST_M
    {
        public class In
        {
            /// <summary>
            ///
            /// </summary>
            public string CATE_CODE { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string PINYINCODE { get; set; }

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
            public string SOURCE { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string FILTER { get; set; }
            public string PAGE_INDEX { get; set; }
            public string PAGE_SIZE { get; set; }
        }

        public class ITEM
        {
            /// <summary>
            ///
            /// </summary>
            public string ITEM_CODE { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string ITEM_NAME { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string ITEM_UNIT { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string ITEM_PRICE { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string YL_PREC { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string ITEM_GG { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string EXCEPT { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string APPR_NUM { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string ITEM_ORDER { get; set; }

            public string INSU_TYPE { get; set; }
        }

        public class Out
        {
            /// <summary>
            ///
            /// </summary>
            public List<ITEM> ITEMLIST { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string TOTAL_NUM { get; set; }
            public string HIS_RTNXML { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string PARAMETERS { get; set; }
        }
    }
}