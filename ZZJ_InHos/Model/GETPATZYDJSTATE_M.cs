namespace ZZJ_InHos.Model
{
    internal class GETPATZYDJSTATE_M
    {
        public class GETPATZYDJSTATE_IN
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
            /// 医疗卡类型
            /// </summary>
            public string YLCARD_TYPE { get; set; }

            /// <summary>
            /// 医疗卡号
            /// </summary>
            public string YLCARD_NO { get; set; }

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

        public class GETPATZYDJSTATE_OUT
        {
            /// <summary>
            ///住院登记标识
            /// </summary>
            public string ZY_PAT_TYPE { get; set; }

            public string HIS_RTNXML { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string PARAMETERS { get; set; }
        }
    }
}