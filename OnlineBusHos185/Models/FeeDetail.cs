using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos185.Models
{
    public class MED
    {
        /// <summary>
        /// 处方号
        /// </summary>
        public string PRENO { get; set; }
        /// <summary>
        /// 医嘱执行时间
        /// </summary>
        public string DATIME { get; set; }
        /// <summary>
        /// 医嘱编号
        /// </summary>
        public string DAID { get; set; }
        /// <summary>
        /// 药品代码
        /// </summary>
        public string MED_ID { get; set; }
        /// <summary>
        /// 药品名称
        /// </summary>
        public string MED_NAME { get; set; }
        /// <summary>
        /// 药品规格
        /// </summary>
        public string MED_GG { get; set; }
        /// <summary>
        /// 组号
        /// </summary>
        public string GROUPID { get; set; }
        /// <summary>
        /// 用药途径
        /// </summary>
        public string USAGE { get; set; }
        /// <summary>
        /// 单次计量单位
        /// </summary>
        public string AUT_NAME { get; set; }
        /// <summary>
        /// 单次计量数量
        /// </summary>
        public string CAMT { get; set; }
        /// <summary>
        /// 总量单位
        /// </summary>
        public string AUT_NAMEALL { get; set; }
        /// <summary>
        /// 总量数量
        /// </summary>
        public string CAMTALL { get; set; }
        /// <summary>
        /// 用药频次
        /// </summary>
        public string TIMES { get; set; }
        /// <summary>
        /// 单价（元）
        /// </summary>
        public string PRICE { get; set; }
        /// <summary>
        /// 总价（元）
        /// </summary>
        public string AMOUNT { get; set; }
        /// <summary>
        /// 医保自编码
        /// </summary>
        public string YB_CODE { get; set; }
        /// <summary>
        /// 医保国家编码
        /// </summary>
        public string YB_CODE_GJM { get; set; }
        /// <summary>
        /// 项目类型
        /// </summary>
        public string IS_QX { get; set; }
        /// <summary>
        /// 最小计价单位标识
        /// </summary>
        public string MINAUT_FLAG { get; set; }
    }

    public class CHKT
    {
        /// <summary>
        /// 医嘱执行时间
        /// </summary>
        public string DATIME { get; set; }
        /// <summary>
        /// 医嘱编号（唯一）
        /// </summary>
        public string DAID { get; set; }
        /// <summary>
        /// 项目代码
        /// </summary>
        public string CHKIT_ID { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string CHKIT_NAME { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        public string AUT_NAME { get; set; }
        /// <summary>
        /// 总量数量
        /// </summary>
        public string CAMTALL { get; set; }
        /// <summary>
        /// 单价（元）
        /// </summary>
        public string PRICE { get; set; }
        /// <summary>
        ///  总价（元）
        /// </summary>
        public string AMOUNT { get; set; }
        /// <summary>
        ///医保自编码
        /// </summary>
        public string YB_CODE { get; set; }
        /// <summary>
        /// 医保国家编码
        /// </summary>
        public string YB_CODE_GJM { get; set; }
        /// <summary>
        /// 项目类型
        /// </summary>
        public string IS_QX { get; set; }
        /// <summary>
        /// 最小计价单位标识
        /// </summary>
        public string MINAUT_FLAG { get; set; }
        /// <summary>
        /// 费用类别
        /// </summary>
        public string FEE_TYPE { get; set; }
    }
}
