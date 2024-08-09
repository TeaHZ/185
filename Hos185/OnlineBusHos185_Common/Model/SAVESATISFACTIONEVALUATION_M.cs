using System.Collections.Generic;

namespace OnlineBusHos185_Common.Model
{
    class SAVESATISFACTIONEVALUATION_M
    {
        public class SAVESATISFACTIONEVALUATION_IN
        {
            /// <summary>
            /// 医院ID
            /// </summary>
            public string SERIAL_ID { get; set; }
            /// <summary>
            /// 问卷号
            /// </summary>
            public string SERIAL_NO { get; set; }
            public string PAT_ID { get; set; }
            public string PAT_NAME { get; set; }
            public string SFZ_NO { get; set; }
            public string MOBILE_NO { get; set; }
            public string DEPT_CODE { get; set; }
            public string DEPT_NAME { get; set; }
            public string DOC_NO { get; set; }
            public string DOC_NAME { get; set; }
            public string INVOICE_NUMBER { get; set; }
            List<ITEM> ITEMLIST { get; set; }
            /// <summary>
            /// 自助机终端号
            /// </summary>
        }


        public class SAVESATISFACTIONEVALUATION_OUT
        {
            /// <summary>
            /// 问卷唯一号	
            /// </summary>
            public string SERIAL_ID { get; set; }
            /// <summary>
            /// 患者唯一号
            /// </summary>
            public string PAT_ID { get; set; }
            /// <summary>
            /// PAT_NAME
            /// </summary>
            public string PAT_NAME { get; set; }
            /// <summary>
            /// 联系方式
            /// </summary>
            public string SFZ_NO { get; set; }
            /// <summary>
            /// 住址
            /// </summary>
            public string MOBILE_NO { get; set; }
            /// <summary>
            /// 身份证号
            /// </summary>
            public string DEPT_CODE { get; set; }

            /// <summary>
            /// 出生日期
            /// </summary>
            public string DEPT_NAME { get; set; }
            /// <summary>
            /// 监护人姓名
            /// </summary>
            public string DOC_NO { get; set; }
            /// <summary>
            /// 监护人身份证
            /// </summary>
           
        }


        public class ITEM
        {
            public string ITEM_ID { get; set; }
            public string ITEM_TYPE { get; set; }
            public string ITEM_TITLE { get; set; }
            public string ITEM_ANSWERS { get; set; }
            public string ITEM_REQUIRE { get; set; }
            public string ITEM_ANSWER { get; set; }
            public string ITEM_ADVICE { get; set; }
           



        }


    }
}
