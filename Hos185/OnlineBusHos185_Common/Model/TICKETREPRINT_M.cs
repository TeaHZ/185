using System.Collections.Generic;

namespace OnlineBusHos185_Common.Model
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

            /// <summary>
            /// 院内卡号
            /// </summary>
            public string HOSPATID { get; set; }
            public string PRINT_TYPE { get;  set; }
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

        public class TEXTDATA
        {
            /// <summary>
            /// 卓越
            /// </summary>
            public string PAT_NAME { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string AGE { get; set; }

            /// <summary>
            /// 男
            /// </summary>
            public string SEX { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string SFZ_NO { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string YLCARD_NO { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string MEDFEE_SUMAMT { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string ACCT_PAY { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string FUND_PAY_SUMAMT { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string PSN_CASH_PAY { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string MDTRT_ID { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string SETL_ID { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string YLLB { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string DISE_NAME { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string BALC { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string JEALL { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string APPT_PAY { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string CASH_JE { get; set; }

            /// <summary>
            /// 消化内科门诊
            /// </summary>
            public string DEPT_NAME { get; set; }

            /// <summary>
            /// 普通
            /// </summary>
            public string DOC_NAME { get; set; }

            /// <summary>
            /// 门诊一楼二诊区
            /// </summary>
            public string APPT_PLACE { get; set; }

            /// <summary>
            /// 2023-02-20 上午
            /// </summary>
            public string APPT_TIME { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string APPT_ORDER { get; set; }

            /// <summary>
            /// 普通号
            /// </summary>
            public string SCH_TYPE { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string HOS_SNAPPT { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string BIZCODE { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string RCPT_NO { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string DEAL_TIME { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string OPER_TIME { get; set; }

            /// <summary>
            /// 无
            /// </summary>
            public string DEAL_TYPE { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string DJ_ID { get; set; }

            /// <summary>
            /// 医保
            /// </summary>
            public string FEE_TYPE { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string OPT_SN { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string SJH { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string HOS_REG_SN { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string HOS_PAY_SN { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string PAY_QR_OPT { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string USER_ID { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string LTERMINAL_SN { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string SLIPMB_TYPE { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string HOSPATID { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string BLH { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string HOS_NO { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string BED_NO { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string dtmx { get; set; }

            public string selfCare { get; set; }
            public string selfPay { get; set; }
            public string commercialInsurancePay { get; set; }
            public string medicalInsurancePool { get; set; }
            public string discountAmount { get; set; }
            public string bookAmount { get; set; }
            public string triageLevel { get; set; }
            public string message { get; set; }
            public string statusCode { get; set; }
            public string insuranceOutside { get;  set; }
            public string compliesInsuranceCatalog { get;  set; }
            public string selfAmount { get;  set; }
            public string civilServants { get;  set; }
            public string employeeIllness { get; set; }
            public string residentIllness { get;  set; }
            public string civilAssistance { get; set; }
            public string enterpriseSupple { get; set; }
            public string otherPay { get;  set; }
            public string emergencyTriageName { get;  set; }
            public string settlementType { get;  set; }
            public string paykindName { get;  set; }
            public string otherFundPay { get;  set; }
            public string identityId { get;  set; }
            public string ownCost { get;  set; }

            public string cashAmount { get; set; }
            public string feeMemo { get; set; }
            public string beginTime { get; internal set; }
            public string queueCallNumberFlag { get;  set; }


            public string checkAmount { get; set; }
            public string medicalInsurancePay { get;  set; }
        }


        public class DAMX
        {
            /// <summary>
            /// 
            /// </summary>
            public string DAID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string DATIME { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string ITEM_ID { get; set; }
            /// <summary>
            /// 煅赭石
            /// </summary>
            public string ITEM_NAME { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string AUT_NAME { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string PRICE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string AMOUNT { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string CAMTALL { get; set; }

            public string RATE { get; set; }

        }

    }
}