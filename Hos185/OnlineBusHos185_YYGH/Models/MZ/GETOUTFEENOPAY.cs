using System;
using System.Collections.Generic;
using System.Text;

namespace Hos185_His.Models.MZ
{
    public class GETOUTFEENOPAY
    {
        /// <summary>
        /// 医院内部就诊卡号,唯⼀
        /// </summary>
        public string cardNo { get; set; }
        /// <summary>
        /// //挂号流⽔号
        /// </summary>
        public string clinicCode { get; set; }
        /// <summary>
        /// //证件号
        /// </summary>
        public string idCardNo { get; set; }
        /// <summary>
        /// //证件类型 01:⾝份证 03:护照 06:港澳居⺠来往内地通⾏证 07:台湾居⺠来往内地通⾏证
        /// </summary>
        public string idCardType { get; set; }
        /// <summary>
        /// //权益卡卡号
        /// </summary>
        public string lifeEquityCardNo { get; set; }
        /// <summary>
        /// //权益卡类型 2 医慧卡
        /// </summary>
        public string lifeEquityCardType { get; set; }
        /// <summary>
        /// //绑定的医疗证号
        /// </summary>
        public string mcardNo { get; set; }
        /// <summary>
        /// //绑定的医疗证类型 1:就诊卡 4:⾝份证 5:医保/市⺠卡/护照
        /// </summary>
        public string mcardNoType { get; set; }
        /// <summary>
        /// //合同编号
        /// </summary>
        public string pactCode { get; set; } 
    }

    public class GETOUTFEENOPAYDATA
    {

        public string billType { get; set; } = "0";

        /// <summary>
        /// 
        /// </summary>
        public string billMoney { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cardNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string clinicCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string deptCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string deptName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string doctCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string doctName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string drugFlag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string isEmerGency { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mainDiagCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mainDiagName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string markno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string operDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pubCost { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recipeNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string seeDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string seeNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string totalFee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ybPay { get; set; }
    }

}
