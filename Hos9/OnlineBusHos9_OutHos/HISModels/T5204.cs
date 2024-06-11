using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_OutHos.HISModels
{
    public class T5204
    {
        public class input
        {
            /// <summary>
            /// 杭小平
            /// </summary>
            public string bingRenXM { get; set; }
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
            public string hospitalId { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string bingRenID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string yiBaoBH { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string yiBaoData { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string duKaFS { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string jiuZhenKH { get; set; }
            /// <summary>
            /// 320200|320223195302147992|188347080|320200D156000005AD932A48BDC3B9E9|杭小平|0081544C9786843202AD932A48|3.00|20221116|20321116|320200909173|00010100202111010884|JSB045526778|6230661635021663331|
            /// </summary>
            public string yiBaoXX { get; set; }
        }
        public class MingXiJEsItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string chuFangId { get; set; }
            /// <summary>
            /// 盒
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
            /// 枸地氯雷他定片
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
            /// 西药/中成药
            /// </summary>
            public string guiLeiMC { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double jinE { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public List<MingXiJEsItem> mingXiJEs { get; set; }
        }

        public class data
        {

            public string visitNo { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string jiuZhenJLID { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string jiuZhenRQ { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string keShiDM { get; set; }
            /// <summary>
            /// 皮肤科
            /// </summary>
            public string keShiMC { get; set; }
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
            public string yiShengDM { get; set; }
            /// <summary>
            /// 吴伟庆
            /// </summary>
            public string yiShengXM { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string zongJinE { get; set; }


            public string warningInfo { get; set; }
        }

    }
}
