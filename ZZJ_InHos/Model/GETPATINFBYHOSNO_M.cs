namespace ZZJ_InHos.Model
{
    public class GETPATINFBYHOSNO_M
    {
        public class GETPATINFBYHOSNO_IN
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
            /// 身份证
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

        public class GETPATINFBYHOSNO_OUT
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
            /// 患者姓名
            /// </summary>
            public string PAT_NAME { get; set; }

            /// <summary>
            /// 性别
            /// </summary>
            public string SEX { get; set; }

            /// <summary>
            /// 入院日期
            /// </summary>
            public string HIN_TIME { get; set; }

            /// <summary>
            /// 床号
            /// </summary>
            public string BED_NO { get; set; }

            /// <summary>
            /// 身份证号
            /// </summary>
            public string SFZ_NO { get; set; }

            public string HIS_RTNXML { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string PARAMETERS { get; set; }
        }
    }
}