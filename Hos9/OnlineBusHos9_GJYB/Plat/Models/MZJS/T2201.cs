namespace OnlineBusHos9_GJYB.Models
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

        public class psnbaseinfo
        {
            /// <summary>
            /// 
            /// </summary>
            public string psn_no { get; set; }//人员编号
            /// <summary>
            /// 
            /// </summary>
            public string psn_cert_type { get; set; }//人员证件类型
            /// <summary>
            /// 
            /// </summary>
            public string certno { get; set; }//证件号码
            /// <summary>
            /// 
            /// </summary>
            public string psn_name { get; set; }//人员姓名
            /// <summary>
            /// 
            /// </summary>
            public string gend { get; set; }//性别
            /// <summary>
            /// 
            /// </summary>
            public string naty { get; set; }//民族
            /// <summary>
            /// 
            /// </summary>
            public string brdy { get; set; }//出生日期
            /// <summary>
            /// 
            /// </summary>
            public string age { get; set; }//年龄
            /// <summary>
            /// 
            /// </summary>
            public string expContent { get; set; }//字段扩展
        }
        public class Root
        {
            public Data data { get; set; }

            public psnbaseinfo psnbaseinfo { get; set; }
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

        public class Root
        {
            public Data data { get; set; }
        }
    }
    #endregion
}
