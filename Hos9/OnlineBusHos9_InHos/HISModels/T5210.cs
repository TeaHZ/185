using System.Collections.Generic;

namespace OnlineBusHos9_InHos.HISModels
{
    internal class T5210
    {
        public class input
        {
            /// <summary>
            /// 
            /// </summary>
            public string zhuYuanHao { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string zhengJianHM { get; set; }
            public string duKaFS { get; set; }
            public string Ybxx { get; set; }
            

        }

        public class data
        {
            /// <summary>
            /// A二十病区
            /// </summary>
            public string bingQu { get; set; }
            /// <summary>
            /// 吴焕明
            /// </summary>
            public string bingRenXM { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string chuangWei { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string patientId { get; set; }
            /// <summary>
            /// 呼吸与危重症医学科一
            /// </summary>
            public string ruYuanKS { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string ruYuanRQ { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double yuJiaoJYE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double yuJiaoJZE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double zhuYuanFY { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string zhuYuanHao { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int zhuangTai { get; set; }
            public string idNo { get; set; }

            public string VisitNo { get; set; }
            public string greenIndicator { get; set; }
        }

        public class DataItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string VisitNo { get; set; }
            /// <summary>
            /// A九病区
            /// </summary>
            public string bingQu { get; set; }
            /// <summary>
            /// 万超
            /// </summary>
            public string bingRenXM { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string chuangWei { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string idNo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string patientId { get; set; }
            /// <summary>
            /// 肿瘤科一
            /// </summary>
            public string ruYuanKS { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string ruYuanRQ { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string yuJiaoJYE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string yuJiaoJZE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string zhuYuanFY { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string zhuYuanHao { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string zhuangTai { get; set; }
            public string greenIndicator { get; set; }
        }
        

        public class output
        {

            /// <summary>
            /// 
            /// </summary>
            public List<DataItem> zylist { get; set; }
        }
    }
}
