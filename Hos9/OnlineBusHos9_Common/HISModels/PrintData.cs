using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_Common.HISModels
{
    public class BlListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string charge { get; set; }
        /// <summary>
        /// 病理[胃胃粘膜活检（2瓶）]
        /// </summary>
        public string itemName { get; set; }
    }

    public class DrugListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double charge { get; set; }
        /// <summary>
        /// 夏枯草片（薄膜衣）
        /// </summary>
        public string itemName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string itemSpec { get; set; }
    }

    public class ExamListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string charge { get; set; }
        /// <summary>
        /// 放射[甲状腺CT平扫]
        /// </summary>
        public string itemName { get; set; }
        /// <summary>
        /// 咨询相关科室
        /// </summary>
        public string notice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string place { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string scheduledDateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string scheduledDateTimeStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string timeDivisionDesc { get; set; }
    }

    public class LabListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public double charge { get; set; }
        /// <summary>
        /// 血凝四项[血浆]
        /// </summary>
        public string itemName { get; set; }
    }

    public class NoOrderIdListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string charge { get; set; }
        /// <summary>
        /// 普通门诊诊察费
        /// </summary>
        public string itemName { get; set; }
    }

    public class TreatListItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string charge { get; set; }
        /// <summary>
        /// 雾化治疗
        /// </summary>
        public string itemName { get; set; }
    }

    public class PrintData
    {
        /// <summary>
        /// 
        /// </summary>
        public string accountMulaidPay { get; set; }
        /// <summary>
        /// 25岁
        /// </summary>
        public string age { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<BlListItem> blList { get; set; }
        /// <summary>
        /// 病理科
        /// </summary>
        public string blPlace { get; set; }
        /// <summary>
        /// 自费
        /// </summary>
        public string chargeType { get; set; }
        /// <summary>
        /// 请到中药房2号窗口排队取药,门诊药房2号窗口排队取药
        /// </summary>
        public string daoZhenXX { get; set; }
        /// <summary>
        /// 张俊
        /// </summary>
        public string doctorName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DrugListItem> drugList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string einvoiceUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ExamListItem> examList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string geRenZHZF { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<LabListItem> labList { get; set; }
        /// <summary>
        /// 检验科
        /// </summary>
        public string labPlace { get; set; }
        /// <summary>
        /// 现金
        /// </summary>
        public string moneyType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<NoOrderIdListItem> noOrderIdList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string operatorEmpName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string patientId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string qiTaZF { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rcptNo { get; set; }
        /// <summary>
        /// 男
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string totalCharges { get; set; }//TODO
        /// <summary>
        /// 
        /// </summary>
        public List<TreatListItem> treatList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string visitNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double xjzf { get; set; }
        /// <summary>
        /// （现金）
        /// </summary>
        public string xjzfType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string yiBaoTCZF { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zhangHuYE { get; set; }
        public string shouFeiRQ { get; set; }
    }

    public class TiketList
    {
        /// <summary>
        /// 
        /// </summary>
        public string keShiMC { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string operationDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rcptNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double totalCharges { get; set; }
    }


}
