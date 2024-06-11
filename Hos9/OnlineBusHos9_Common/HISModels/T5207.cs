using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_Common.HISModels
{
    internal class T5207
    {
        public class input
        {
            /// <summary>
            /// 李静
            /// </summary>
            public string bingRenXM { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string kaiShiSJ { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string zhengJianHM { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string yeWuLX { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string chuShengRQ { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string hospitalId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string bingRenID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string jieShuSJ { get; set; }
            /// <summary>
            /// 女
            /// </summary>
            public string xingBieMC { get; set; }
        }

        public class MingXiJEsItem
        {
            /// <summary>
            /// 次
            /// </summary>
            public string danWei { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string jinE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shuLiang { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string xiangMUDM { get; set; }
            /// <summary>
            /// 副主任医师门诊诊察费
            /// </summary>
            public string xiangMuMC { get; set; }
        }

        public class ShouFeiGLJEListItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string guiLeiDM { get; set; }
            /// <summary>
            /// 诊查
            /// </summary>
            public string guiLeiMC { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string jinE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<MingXiJEsItem> mingXiJEs { get; set; }
        }

        public class data
        {
            public string name { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string daoZhenXX { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string dianZiFB { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string faPiaoHM { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string faPiaoLX { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string faPiaoZT { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string feiYongBZ { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string geRenZHZF { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string heji { get; set; }
            /// <summary>
            /// 陆安
            /// </summary>
            public string kaiDanYS { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string keShiDM { get; set; }
            /// <summary>
            /// 妇科
            /// </summary>
            public string keShiMC { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string qiTaZF { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<ShouFeiGLJEListItem> shouFeiGLJEList { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shouFeiID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shouFeiRQ { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shouFeiSJ { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string shouFeiYuan { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string xianJinZF { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string yeWuLSH { get; set; }
            /// <summary>
            /// 自费
            /// </summary>
            public string yiBaoLX { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string yiBaoTCZF { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string yiShengDM { get; set; }
            /// <summary>
            /// 陆安
            /// </summary>
            public string yiShengXM { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string zhangHuYE { get; set; }
            /// <summary>
            /// 云闪付
            /// </summary>
            public string zhiFuFS { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string zongJinE { get; set; }
            public string gongJiJinZF { get;  set; }
            public string age { get;  set; }
            public string sex { get; set; }
            public string bingRenID { get; set; }
        }

    }
}
