using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_YYGH.HISModels
{
    public  class T2002
    {
        public class input
        {
            /// <summary>
            /// 
            /// </summary>
            public string yiShengDM { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string yeWuLX { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string hospitalId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string keShiDM { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string guaHaoBC { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string jiuZhenRQ { get; set; }

            public string guaHaoLB { get; set; }

            public string zhuanJiaGHLB { get; set; }
        }
        public class HaoYuansItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string guaHaoBC { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string guaHaoXH { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string jiuZhenRQ { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string jiuZhenSJFW { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string riQi { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shengYuHYS { get; set; }
        }

        public class data
        {
            /// <summary>
            /// 
            /// </summary>
            public string dangTianPBID { get; set; }
            /// <summary>
            /// 风湿免疫科
            /// </summary>
            public string guaHaoLB { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string guoBiaoKSDM { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<HaoYuansItem> haoYuans { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string jieShao { get; set; }
            /// <summary>
            /// 门诊二楼西单元内科
            /// </summary>
            public string jiuZhenDD { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string keShiDM { get; set; }
            /// <summary>
            /// 风湿免疫科门诊
            /// </summary>
            public string keShiMC { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string paiBanRQ { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string paiBanSJFW { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shanChang { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shangWuHYSYS { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shangWuHYZS { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shangWuTZZT { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shangWuXHBZ { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string tingZhenBz { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string xiaWuHYSYS { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string xiaWuHYZS { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string xiaWuTZZT { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string xiaWuXHBZ { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string xingQi { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string yiShengDM { get; set; }
            /// <summary>
            /// 风湿免疫科
            /// </summary>
            public string yiShengXM { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string yiZhouPBID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string zhenLiaoFei { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string zhenLiaoJSF { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string zhiChengDM { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string zhiChengMC { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string zuiJinPBRQ { get; set; }
        }

    }
}
