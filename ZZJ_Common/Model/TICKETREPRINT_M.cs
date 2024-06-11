using System.Collections.Generic;

namespace ZZJ_Common.Model
{
    public class TICKETREPRINT
    {
        public class TICKETREPRINT_IN
        {
            /// <summary>
            /// 医院ID
            /// </summary>
            public string HOS_ID { get; set; }

            /// <summary>
            /// 操作员ID
            /// </summary>
            public string USER_ID { get; set; }

            /// <summary>
            /// 自助机终端号
            /// </summary>
            public string LTERMINAL_SN { get; set; }

            /// <summary>
            /// 身份证号
            /// </summary>
            public string SFZ_NO { get; set; }

            /// <summary>
            /// 凭条内容
            /// </summary>
            public string TEXT { get; set; }

            /// <summary>
            /// 操作类型 1上传凭条内容 2取数据 3打印回传
            /// </summary>
            public string TYPE { get; set; }

            /// <summary>
            /// 凭条类型 1挂号 2缴费 3预交金 4 住院结算
            /// </summary>
            public string PT_TYPE { get; set; }

            /// <summary>
            /// 唯一流水号
            /// </summary>
            public string DJ_ID { get; set; }

            /// <summary>
            /// 数据来源
            /// </summary>
            public string SOURCE { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string FILTER { get; set; }
        }

        public class TICKETREPRINT_OUT
        {
            public List<ITEM> ITEMLIST { get; set; }

            public string HIS_RTNXML { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string PARAMETERS { get; set; }
        }

        public class ITEM
        {
            public string DJ_ID { get; set; }
            public string TEXT { get; set; }
            public string CAN_PRINT { get; set; }
            public string PRINT_TIMES { get; set; }
        }
    }
}