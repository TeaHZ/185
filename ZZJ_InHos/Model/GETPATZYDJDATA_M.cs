namespace ZZJ_InHos.Model
{
    internal class GETPATZYDJDATA_M
    {
        public class GETPATZYDJDATA_IN
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
            /// 病人类型
            /// </summary>
            public string TYPE { get; set; }

            /// <summary>
            /// 数据来源
            /// </summary>
            public string SOURCE { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string FILTER { get; set; }
        }

        public class GETPATZYDJDATA_OUT
        {
            /// <summary>
            /// 结账方式
            /// </summary>
            public string JZ_NAME { get; set; }

            /// <summary>
            /// 病人类别
            /// </summary>
            public string PAT_TYPE { get; set; }

            /// <summary>
            /// 住院号
            /// </summary>
            public string HOS_NO { get; set; }

            /// <summary>
            /// 住院次数
            /// </summary>
            public string HOS_ORDER { get; set; }

            /// <summary>
            /// 入院病区
            /// </summary>
            public string UTB_IN_NAME { get; set; }

            /// <summary>
            ///入院病区
            /// </summary>
            public string UTB_IN_ID { get; set; }

            /// <summary>
            ///  入院日期
            /// </summary>
            public string HIN_DATE { get; set; }

            /// <summary>
            ///  门诊主诊断
            /// </summary>
            public string MZ_DIS_NAME { get; set; }

            /// <summary>
            /// 入院专科
            /// </summary>
            public string UTZ_IN_NAME { get; set; }

            /// <summary>
            /// 入院情况
            /// </summary>
            public string HST_CODE { get; set; }

            /// <summary>
            /// 入院途径
            /// </summary>
            public string HIN_WAY { get; set; }

            /// <summary>
            /// 门诊医师
            /// </summary>
            public string MZYS_NAME { get; set; }

            /// <summary>
            /// 门诊医师
            /// </summary>
            public string MZYSMAN_ID { get; set; }

            /// <summary>
            /// 门诊医师
            /// </summary>
            public string DOC_NO { get; set; }

            /// <summary>
            /// 病人姓名
            /// </summary>
            public string PAT_NAME { get; set; }

            /// <summary>
            /// 证件类型
            /// </summary>
            public string CARD_TYPE { get; set; }

            /// <summary>
            /// 证件号码
            /// </summary>
            public string CARD_NO { get; set; }

            /// <summary>
            /// 性别
            /// </summary>
            public string SEX { get; set; }

            /// <summary>
            /// 联系方式
            /// </summary>
            public string PAT_TEL { get; set; }

            /// <summary>
            /// 出生日期
            /// </summary>
            public string BIRTHDAY { get; set; }

            /// <summary>
            /// 年龄
            /// </summary>
            public string AGE { get; set; }

            /// <summary>
            /// 婚姻状况
            /// </summary>
            public string MRG { get; set; }

            /// <summary>
            /// 职业
            /// </summary>
            public string OCCUPATION { get; set; }

            /// <summary>
            /// 籍贯
            /// </summary>
            public string BPOS { get; set; }

            /// <summary>
            ///出生地
            /// </summary>
            public string BIRTH_ADDR { get; set; }

            /// <summary>
            /// 民族
            /// </summary>
            public string NATION { get; set; }

            /// <summary>
            /// 现住址
            /// </summary>
            public string NOW_REGION { get; set; }

            /// <summary>
            /// 现住址街村
            /// </summary>
            public string NOW_ADDR { get; set; }

            /// <summary>
            /// 文化程度
            /// </summary>
            public string EDUCATION { get; set; }

            /// <summary>
            /// 工作单位
            /// </summary>
            public string UNIT_NAME { get; set; }

            /// <summary>
            /// 户口地区地址
            /// </summary>
            public string HK_REGION { get; set; }

            /// <summary>
            /// 户口街村
            /// </summary>
            public string HK_ADDR { get; set; }

            /// <summary>
            /// 联系人姓名
            /// </summary>
            public string REL_NAME { get; set; }

            /// <summary>
            /// 联系人关系
            /// </summary>
            public string REL_TYPE { get; set; }

            /// <summary>
            /// 联系人电话
            /// </summary>
            public string REL_TELE { get; set; }

            /// <summary>
            /// 联系人地区地址
            /// </summary>
            public string REL_REGION { get; set; }

            /// <summary>
            /// 联系人街村
            /// </summary>
            public string REL_ADDR { get; set; }

            /// <summary>
            /// 入院时间
            /// </summary>
            public string HIN_TIME { get; set; }

            /// <summary>
            /// 住院病人唯一ID
            /// </summary>
            public string HOS_PAT_ID { get; set; }

            /// <summary>
            /// 疾病
            /// </summary>
            public string DIS_NO { get; set; }

            /// <summary>
            /// 疾病
            /// </summary>
            public string DIS_NAME { get; set; }

            public string HIS_RTNXML { get; set; }

            /// <summary>
            /// 其他条件
            /// </summary>
            public string PARAMETERS { get; set; }
        }
    }
}