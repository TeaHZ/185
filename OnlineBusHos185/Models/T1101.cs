using System;
using System.Collections.Generic;
using System.Text;

namespace QHSiInterface
{
    #region N：6.1.1.1 【1101】人员信息获取

    public class T1101
    {
        /// <summary>
        /// 输入（节点标识：data）
        /// </summary>
        public class Data
        {
            /// <summary>
            /// 01,02,03
            /// </summary>
            public string mdtrt_cert_type { get; set; }// 就诊凭证类型
            /// <summary>
            /// 就诊凭证编号：就诊凭证类型为“01”时填写电子凭证令牌，为“02”时填写身份证号，为“03”时填写社会保障卡卡号
            /// </summary>
            public string mdtrt_cert_no { get; set; }
            /// <summary>
            /// 卡识别码：就诊凭证类型为“03”时必填
            /// </summary>
            public string card_sn { get; set; }// 卡识别码
            public string begntime { get; set; }// 开始时间
            public string psn_cert_type { get; set; }// 人员证件类型
            public string certno { get; set; }// 证件号码
            public string psn_name { get; set; }// 人员姓名

        }

        public class Root
        {
            public Data data { get; set; }
        }
    }


    public class RT1101
    {
        /// <summary>
        /// 输出-基本信息（节点标识：baseinfo）
        /// </summary>
        public class Baseinfo
        {
            public string psn_no { get; set; }//人员编号
            public string psn_cert_type { get; set; }// 人员证件类型
            public string certno { get; set; }// 证件号码
            public string psn_name { get; set; }// 人员姓名
            public string gend { get; set; }//性别
            public string naty { get; set; }// 民族
            public string brdy { get; set; }// 出生日期
            public string age { get; set; }//年龄

            public string exp_content { get; set; }
        }
        /// <summary>
        /// 输出-参保信息列表（节点标识insuinfo）
        /// </summary>
        public class Insuinfo
        {
            public string balc { get; set; }//余额
            public string insutype { get; set; }// 险种类型
            public string psn_type { get; set; }//人员类别
            public string cvlserv_flag { get; set; }//公务员标志
            public string insuplc_admdvs { get; set; }//参保地医保区划
            public string emp_name { get; set; }//单位名称

            public string psn_insu_stas { get; set; }//人员参保状态

            public string psn_insu_date { get; set; }//个人参保日期

            public string paus_insu_date { get; set; }//暂停参保日期

        }

        /// <summary>
        /// 输出-身份信息列表（节点标识：idetinfo）
        /// </summary>
        public class Idetinfo
        {
            public string psn_idet_type { get; set; }//人员身份类别
            public string psn_type_lv { get; set; }//人员类别等级
            public string memo { get; set; }//备注
            public string begntime { get; set; }//开始时间
            public string endtime { get; set; }//结束时间

        }

        public class Data
        {
            /// <summary>
            /// 人员编号
            /// </summary>
            public string psn_no { get; set; }
            /// <summary>
            /// 险种类型
            /// </summary>
            public string insutype { get; set; }
            /// <summary>
            /// 医疗类别
            /// </summary>
            public string med_type { get; set; }
            /// <summary>
            /// 病种编码
            /// </summary>
            public string dise_codg { get; set; }
            /// <summary>
            /// 病种名称
            /// </summary>
            public string dise_name { get; set; }
            /// <summary>
            /// 开始日期
            /// </summary>
            public string begndate { get; set; }
            /// <summary>
            /// 结束日期
            /// </summary>
            public string enddate { get; set; }
            /// <summary>
            /// 医保目录编码
            /// </summary>
            public string hilist_code { get; set; }
            /// <summary>
            /// 医保目录名称
            /// </summary>
            public string hilist_name { get; set; }
            public string exp_content { get; set; }
        }

        public class Output
        {
            /// <summary>
            /// 
            /// </summary>
            public Baseinfo baseinfo { get; set; }

            public List<Insuinfo> insuinfo { get; set; }

            public List<Idetinfo> idetinfo { get; set; }

            public List<Data> data { get; set; }
        }
        public class Root
        {
            /// <summary>
            /// 交易状态码
            /// </summary>
            public string infcode { get; set; }
            /// <summary>
            /// 接收方报文ID
            /// </summary>
            public string inf_refmsgid { get; set; }
            /// <summary>
            /// 接收报文时间
            /// </summary>
            public string refmsg_time { get; set; }
            /// <summary>
            /// 响应报文时间
            /// </summary>
            public string respond_time { get; set; }
            /// <summary>
            /// 错误信息
            /// </summary>
            public string err_msg { get; set; }
            public Output output { get; set; }
        }
    }

    public class RTNJ1101
    {
        public class Baseinfo
        {
            /// <summary>
            /// 
            /// </summary>
            public string age { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string brdy { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string certno { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string exp_content { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string gend { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string naty { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string psn_cert_type { get; set; }
            /// <summary>
            /// 庞冬梅
            /// </summary>
            public string psn_name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string psn_no { get; set; }
        }

        public class DataItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string begndate { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string dise_codg { get; set; }
            /// <summary>
            /// 其他恶性肿瘤
            /// </summary>
            public string dise_name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string exp_content { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string hilist_code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string hilist_name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string insutype { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string med_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string psn_no { get; set; }
        }

        public class InsuinfoItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string balc { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string cvlserv_flag { get; set; }
            /// <summary>
            /// 南京爱生认证有限公司
            /// </summary>
            public string emp_name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string insuplc_admdvs { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string insutype { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string psn_insu_date { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string psn_insu_stas { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string psn_type { get; set; }
        }

        public class Output
        {
            /// <summary>
            /// 
            /// </summary>
            public Baseinfo baseinfo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<DataItem> data { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<InsuinfoItem> insuinfo { get; set; }
        }

        public class Root
        {
            /// <summary>
            /// 
            /// </summary>
            public string enctype { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string inf_refmsgid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string infcode { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public Output output { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string refmsg_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string respond_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string signtype { get; set; }
        }
    }
    #endregion
}
