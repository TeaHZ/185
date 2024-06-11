using System.Collections.Generic;

namespace ZZJ_InHos.Model
{
    public class GETHOSDAILY_M
    {
        public class GETHOSDAILY_IN
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
            /// 住院号
            /// </summary>
            public string HOS_NO { get; set; }

            /// <summary>
            /// 住院患者唯一索引
            /// </summary>
            public string HOS_PAT_ID { get; set; }

            /// <summary>
            /// 开始时间
            /// </summary>
            public string BEGIN_DATE { get; set; }

            /// <summary>
            /// 结束时间
            /// </summary>
            public string END_DATE { get; set; }

            /// <summary>
            /// 数据来源
            /// </summary>
            public string SOURCE { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string FILTER { get; set; }
        }

        public class GETHOSDAILY_OUT
        {
            /// <summary>
            /// 已缴纳金额
            /// </summary>
            public string JE_PAY { get; set; }

            /// <summary>
            /// 已经发生总费用
            /// </summary>
            public string JE_YET { get; set; }

            /// <summary>
            /// 预交金余额
            /// </summary>
            public string JE_REMAIN { get; set; }

            /// <summary>
            /// 时间段内发生金额
            /// </summary>
            public string JE_TODAY { get; set; }

            public List<BIGITEM> BIGITEMLIST { get; set; }

            public string HIS_RTNXML { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string PARAMETERS { get; set; }
        }

        public class BIGITEM
        {
            /// <summary>
            /// 费用类别
            /// </summary>
            public string ITEM_NAME { get; set; }

            /// <summary>
            /// 费用金额
            /// </summary>
            public string JE_ALL { get; set; }

            public List<ITEM> ITEMLIST { get; set; }
        }

        public class ITEM
        {
            /// <summary>
            /// 项目名称
            /// </summary>
            public string NAME { get; set; }

            /// <summary>
            /// 项目规格
            /// </summary>
            public string GG { get; set; }

            /// <summary>
            /// 项目数量
            /// </summary>
            public string AMOUNT { get; set; }

            /// <summary>
            /// 单位
            /// </summary>
            public string CAMT { get; set; }

            /// <summary>
            /// 单价
            /// </summary>
            public string JE { get; set; }

            /// <summary>
            /// 费用总金额
            /// </summary>
            public string JE_ALL { get; set; }

            /// <summary>
            /// 费用发生日期
            /// </summary>
            public string DJ_DATE { get; set; }
        }
    }
}