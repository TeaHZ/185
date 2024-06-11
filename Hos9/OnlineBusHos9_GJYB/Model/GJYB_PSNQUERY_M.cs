namespace OnlineBusHos9_GJYB.Model
{
    public class GJYB_PSNQUERY_M
    {
        public class GJYB_PSNQUERY_IN
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
            /// 卡识别码
            /// </summary>
            public string CARD_SN { get; set; }
            /// <summary>
            /// 开始时间
            /// </summary>
            public string BEGNTIME { get; set; }
            /// <summary>
            /// 人员证件类型
            /// </summary>
            public string PSN_CERT_TYPE { get; set; }

            /// <summary>
            /// 证件号码
            /// </summary>
            public string CERTNO { get; set; }
            /// <summary>
            /// 人员姓名
            /// </summary>
            public string PSN_NAME { get; set; }

            /// <summary>
            /// 01 医保电子凭证 02 身份证 03社会保障卡卡号 04 电子社保卡
            /// </summary>
            public string MDTRT_CERT_TYPE { get; set; }

            public string MDTRT_CERT_NO { get; set; }

        }

        public class GJYB_PSNQUERY_OUT
        {
            /// <summary>
            /// 姓名
            /// </summary>
            public string PSN_NAME { get; set; }
            /// <summary>
            /// 个人编号
            /// </summary>
            public string PSN_NO { get; set; }
            /// <summary>
            /// 性别
            /// </summary>
            public string SEX { get; set; }
            /// <summary>
            /// 民族
            /// </summary>
            public string NATION { get; set; }
            /// <summary>
            /// 出生日期
            /// </summary>
            public string BIRTHDAY { get; set; }
            /// <summary>
            /// 年龄
            /// </summary>
            public string AGE { get; set; }

            /// <summary>
            /// 身份证号
            /// </summary>
            public string SFZ_NO { get; set; }

            /// <summary>
            /// 单位
            /// </summary>
            public string UNIT_NAME { get; set; }

            /// <summary>
            /// 余额
            /// </summary>
            public string BALANCE { get; set; }
            /// <summary>
            /// 险种类型
            /// </summary>
            public string INSUTYPE { get; set; }

            /// <summary>
            /// 医保读卡出参
            /// </summary>
            public string PAT_CARD_OUT { get; set; }


        }
    }
}
