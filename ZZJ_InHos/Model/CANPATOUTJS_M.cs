namespace ZZJ_InHos.Model
{
    public class CANPATOUTJS_M
    {
        public class CANPATOUTJS_IN
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
            /// 住院病人唯一索引
            /// </summary>
            public string HOS_PAT_ID { get; set; }

            /// <summary>
            /// 身份证号
            /// </summary>
            public string SFZ_NO { get; set; }

            /// <summary>
            /// 数据来源
            /// </summary>
            public string SOURCE { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string FILTER { get; set; }
        }

        public class CANPATOUTJS_OUT
        {
            /// <summary>
            /// 是否可以出院结算
            /// </summary>
            public string CAN_OUT { get; set; }

            /// <summary>
            /// 住院号
            /// </summary>
            public string HOS_NO { get; set; }

            /// <summary>
            /// 住院病人唯一索引
            /// </summary>
            public string HOS_PAT_ID { get; set; }

            /// <summary>
            /// 是否医保结算病人
            /// </summary>
            public string HOS_YB_PAT { get; set; }

            public string HIS_RTNXML { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string PARAMETERS { get; set; }
        }
    }
}