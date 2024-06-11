using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_OutHos.HISModels
{
    public class T1241
    {

        public class input
        {
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
            public string fuKuanZT { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string ziFeiBZ { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string jiuZhenId { get; set; }
        }


        public class MingXiJEsItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string chuFangId { get; set; }
            /// <summary>
            /// 项
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
            /// 钾测定（干化学法）
            /// </summary>
            public string xiangMuMC { get; set; }

            public string danJia { get;set; }
        }

        public class ShouFeiGLJEListItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string guiLeiDM { get; set; }
            /// <summary>
            /// 化验
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
            /// <summary>
            /// 许红芳
            /// </summary>
            public string bingRenXM { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string fuKuanZT { get; set; }
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
            /// 呼吸与危重症医学科
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
            /// 张俊
            /// </summary>
            public string yiShengXM { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public double zongJinE { get; set; }
        }

    }

}
