
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_InHos.HISModels
{
    internal class T1183
    {

        public class Input
        {
            /// <summary>
            /// 
            /// </summary>
            public string yeWuLX { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string jiuZhenId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string zhengJianHM { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string kaiShiSJ { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string jieShuSJ { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string hospitalId { get; set; }
        }
        public class JiBenXX
        {
            /// <summary>
            /// 
            /// </summary>
            public string bingAnHao { get; set; }
            /// <summary>
            /// 吴焕明
            /// </summary>
            public string bingRenXM { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string chuYuanSJ { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string dangQianBQDM { get; set; }
            /// <summary>
            /// A二十病区
            /// </summary>
            public string dangQianBQMC { get; set; }
            /// <summary>
            /// 05床
            /// </summary>
            public string dangQianCW { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string dangQianKSDM { get; set; }
            /// <summary>
            /// 呼吸与危重症医学科一
            /// </summary>
            public string dangQianKSMC { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double feiYongZE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string ruYuanSJ { get; set; }
            /// <summary>
            /// 自费
            /// </summary>
            public string shouFeiLX { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double yuJiaoKuan { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string zhuYuanHao { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string zhuYuanTS { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string zhuZhiYSDM { get; set; }
            /// <summary>
            /// 唐志伟
            /// </summary>
            public string zhuZhiYSXM { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double ziFeiJE { get; set; }
        }

        public class XiangMuXQItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string faShengRQ { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string feiYongDJ { get; set; }
            /// <summary>
            /// 床位
            /// </summary>
            public string feiYongDLMC { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string feiYongDM { get; set; }
            /// <summary>
            /// 日
            /// </summary>
            public string feiYongDW { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string feiYongSL { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string jiFeiRQ { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string jinE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string kaiDanKSDM { get; set; }
            /// <summary>
            /// 呼吸与危重症医学科一
            /// </summary>
            public string kaiDanKSMC { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string kaiDanYSDM { get; set; }
            /// <summary>
            /// 唐志伟
            /// </summary>
            public string kaiDanYSXM { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string xiangMuDM { get; set; }
            /// <summary>
            /// 双人间床位费B
            /// </summary>
            public string xiangMuMC { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string yaoPinGG { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string yiLiaoZDM { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string yiLiaoZMC { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string zhiXingKSDM { get; set; }
            /// <summary>
            /// A二十病区
            /// </summary>
            public string zhiXingKSMC { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string ziFuBL { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string ziFuJE { get; set; }
        }

        public class Data
        {
            /// <summary>
            /// 
            /// </summary>
            public JiBenXX jiBenXX { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<XiangMuXQItem> xiangMuXQ { get; set; }
        }

    }
}
