namespace ZZJ_InHos.Model
{
    internal class ZYDJSAVE_M
    {
        public class ZYDJSAVE_IN
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
            /// 入院患者标识
            /// </summary>
            public string HOS_PAT_ID { get; set; }

            /// <summary>
            /// 数据来源
            /// </summary>
            public string SOURCE { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string FILTER { get; set; }
        }

        public class ZYDJSAVE_OUT
        {
            /// <summary>
            /// 住院号
            /// </summary>
            public string HOS_NO { get; set; }

            /// <summary>
            /// 住院患者索引号
            /// </summary>
            public string HOS_PAT_ID { get; set; }

            /// <summary>
            /// 入院时间
            /// </summary>
            public string HIN_TIME { get; set; }

            public string HIS_RTNXML { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string PARAMETERS { get; set; }
        }
    }
}