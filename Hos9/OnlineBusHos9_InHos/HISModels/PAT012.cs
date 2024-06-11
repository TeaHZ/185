using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_InHos.HISModels
{
    public  class PAT012
    {
        public class Body
        {
            /// <summary>
            /// 
            /// </summary>
            public string zhengJianHM { get; set; }
        }

        public class Data
        {

            public string AdmissionDateTime { get; set; }

            public string DischargeDateTime { get; set; }
            /// <summary>
            /// 在院；待入科；已出院
            /// </summary>
            public string AdtStatus { get; set; }
            /// <summary>
            /// A二十一病区
            /// </summary>
            public string Bingqu { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string Chuangwei { get; set; }
            /// <summary>
            /// 解正行
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string PatientId { get; set; }
            /// <summary>
            /// 呼吸与危重症医学科二
            /// </summary>
            public string Ruyuanks { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string VisitNo { get; set; }
        }


    }
}
