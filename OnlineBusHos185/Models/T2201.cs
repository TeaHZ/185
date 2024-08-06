using System;
using System.Collections.Generic;
using System.Text;

namespace QHSiInterface
{
    #region N：6.2.3.1 【2201】门诊挂号

    public class T2201
    {
        /// <summary>
        /// 输入（节点标识：data）
        /// </summary>
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
            /// 开始时间
            /// 挂号时间 yyyy-MM-dd HH:mm:ss
            /// </summary>
            public string begntime { get; set; }

            /// <summary>
            /// 就诊凭证类型
            /// </summary>
            public string mdtrt_cert_type { get; set; }
            /// <summary>
            /// 就诊凭证编号
            /// 就诊凭证类型为“01”时填写电子凭证令牌，为“02”时填写身份证号，为“03”时填写社会保障卡卡号
            /// </summary>
            public string mdtrt_cert_no { get; set; }
            /// <summary>
            /// 住院/门诊号(院内唯一流水)
            /// </summary>
            public string ipt_otp_no { get; set; }
            /// <summary>
            /// 医师编码
            /// </summary>
            public string atddr_no { get; set; }
            /// <summary>
            /// 医师姓名
            /// </summary>
            public string dr_name { get; set; }
            /// <summary>
            /// 科室编码
            /// </summary>
            public string dept_code { get; set; }
            /// <summary>
            /// 科室名称
            /// </summary>
            public string dept_name { get; set; }
            /// <summary>
            /// 科别
            /// </summary>
            public string caty { get; set; }
            /// <summary>
            /// 字段扩展
            /// </summary>
            public string expContent { get; set; }

        }

        public class Input
        {

            public Data data { get; set; }
        }

        public class Root
        {

            /// <summary>
            /// 
            /// </summary>
            public string infno { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string msgid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string mdtrtarea_admvs { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string insuplc_admdvs { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string recer_sys_code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string dev_no { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string dev_safe_info { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string cainfo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string signtype { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string infver { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string opter_type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string opter { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string opter_name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string inf_time { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string fixmedins_code { get; set; }
            /// <summary>
            /// 南京医科大学第二附属医院
            /// </summary>
            public string fixmedins_name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string sign_no { get; set; }
            /// <summary>
            /// 北大医疗信息技术有限公司
            /// </summary>
            public string fixmedins_soft_fcty { get; set; }
            public Input input { get; set; }

        }
    }


    public class RT2201
    {
        /// <summary>
        /// 输出（节点标识：data）
        /// </summary>
        public class Data
        {
            /// <summary>
            /// 就诊ID(医保返回唯一流水)  
            /// </summary>
            public string mdtrt_id { get; set; }
            /// <summary>
            /// 人员编号
            /// </summary>
            public string psn_no { get; set; }
            /// <summary>
            /// 住院/门诊号(院内唯一流水)
            /// </summary>
            public string ipt_otp_no { get; set; }
            /// <summary>
            /// 字段扩展
            /// </summary>
            public string expContent { get; set; }

        }
        public class Output
        {
            /// <summary>
            /// 
            /// </summary>
            public Data data { get; set; }
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

    public class RTNJ2201
    {
        public class Data
        {
            /// <summary>
            /// 
            /// </summary>
            public string ipt_otp_no { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string mdtrt_id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string psn_no { get; set; }
        }

        public class Output
        {
            /// <summary>
            /// 
            /// </summary>
            public Data data { get; set; }
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
