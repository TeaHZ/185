using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_Report.Model
{
    class PAT015
    {
        public class input
        {
            /// <summary>
            /// 
            /// </summary>
            public string patientId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string startDate { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string endDate { get; set; }
        }
        public class Data
        {
            /// <summary>
            /// 35岁
            /// </summary>
            public string age { get; set; }
            /// <summary>
            /// 呼吸与危重症医学科一
            /// </summary>
            public string dept { get; set; }
            /// <summary>
            /// 慢性肺源性心脏病
            /// </summary>
            public string diagnosis { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string doctorId { get; set; }
            /// <summary>
            /// 超级管理员
            /// </summary>
            public string doctorName { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string memo { get; set; }
            public long openDate { get; set; }
            /// <summary>
            /// 宁勇剑
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 陕西省宝鸡市扶风县
            /// </summary>
            public string nativePlace { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string patientId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string printTimes { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string restDuration { get; set; }
            /// <summary>
            /// 天
            /// </summary>
            public string restUnit { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string serviceAgency { get; set; }
            /// <summary>
            /// 男
            /// </summary>
            public string sex { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string visitDate { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string visitNo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string visitType { get; set; }
        }

        public class outdata
        {
            /// <summary>
            /// 
            /// </summary>
            public int code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<Data> data { get; set; }
            /// <summary>
            /// 成功
            /// </summary>
            public string msg { get; set; }
        }
    }
}
