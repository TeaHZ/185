using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_OutHos.Model
{
    internal class GETSELFBILLINGLIST_M
    {
        public class GETSELFBILLINGLIST_IN
        {

            public string HOS_ID { get; set; }
            public string USER_ID { get; set; }
            public string LTERMINAL_SN { get; set; }
            public string FILTER { get; set; }
            public string ITEM_TYPE { get; set; }

        }
        public class GETSELFBILLINGLIST_OUT
        {
            /// <summary>
            /// 
            /// </summary>
            public List<ITEMLISTItem> ITEMLIST { get; set; }
            public string PARAMETERS { get; set; }
        }
            public class ITEMLISTItem
            {
                /// <summary>
                /// 
                /// </summary>
                public string ITEM_CODE { get; set; }
                /// <summary>
                /// 血常规自动分析(五分类)
                /// </summary>
                public string ITEM_NAME { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string PRICE { get; set; }
                /// <summary>
                /// 
                /// </summary>
                public string ITEM_TYPE { get; set; }
                /// <summary>
                /// 检验
                /// </summary>
                public string ITEM_TYPE_NAME { get; set; }
            }
 
        }
 }