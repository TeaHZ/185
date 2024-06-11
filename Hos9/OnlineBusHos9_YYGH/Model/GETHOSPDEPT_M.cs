using System.Collections.Generic;

namespace OnlineBusHos9_YYGH.Model
{
    class GETHOSPDEPT_M
    {
        public class GETHOSPDEPT_IN
        {
            public string HOS_ID { get; set; }
            /// <summary>
            /// 操作员ID
            /// </summary>
            public string USER_ID { get; set; }
            /// <summary>
            /// 自助机终端号
            /// </summary>
            public string LTERMINAL_SN { get; set; }
            /// <summary>
            /// 类型代码
            /// 如：01可用于预约的数据（科室）；
            ///02 可用于实时挂号的数据；（科室）
            ///05可用于预约的数据（专家）
            ///06可用于实时挂号的数据（专家）
            ///07急诊科室 空值不过滤
            ///08预约（科室和专家）
            ///09挂号（科室和专家）
            /// </summary>
            public string USE_TYPE { get; set; }
            /// <summary>
            /// 筛选类型
            /// 01:按大科
            /// 02：按科室名称
            /// 03：按科室类别
            /// 04：按科室代码
            /// 05不过滤
            /// </summary>
            public string FILT_TYPE { get; set; }

            public string FILT_VALUE { get; set; }

            public string PAGEINDEX { get; set; }
            public string PAGESIZE { get; set; }
            public string RETURN_TYPE { get; set; }
            public string FILTER { get; set; }
        }

        public class GETHOSPDEPT_OUT
        {
            public List<DEPT> DEPTLIST { get; set; }
            public string HIS_RTNXML { get; set; }
            /// <summary>
            /// 其他条件
            /// </summary>
            public string PARAMETERS { get; set; }
        }
        public class DEPT
        {
            /// <summary>
            /// 科室代码
            /// </summary>
            public string DEPT_CODE { get; set; }
            /// <summary>
            /// 科室名称
            /// </summary>
            public string DEPT_NAME { get; set; }
            /// <summary>
            /// 科室介绍
            /// </summary>
            public string DEPT_INTRO { get; set; }
            /// <summary>
            /// 科室URL
            /// </summary>
            public string DEPT_URL { get; set; }
            /// <summary>
            /// 排序序号
            /// </summary>
            public string DEPT_ORDER { get; set; }
            /// <summary>
            /// 科室类型
            /// </summary>
            public string DEPT_TYPE { get; set; }
            /// <summary>
            /// 科室地址
            /// </summary>
            public string DEPT_ADDRESS { get; set; }
            public List<DEPT> CHILDREN { get; set; }
            
        }
      
    }
}
