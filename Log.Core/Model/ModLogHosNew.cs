using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Core.Model
{
    public class ModLogHosNew
    {
        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime inTime;
        /// <summary>
        /// 请求入参
        /// </summary>
        public string inXml;
        /// <summary>
        /// 返回出参
        /// </summary>
        public string outXml;
        /// <summary>
        /// 返回时间时间
        /// </summary>
        public DateTime outTime;
        /// <summary>
        /// 接口类型
        /// </summary>
        public string TYPE;
        /// <summary>
        /// 节点格式
        /// </summary>
        public string NODE;
        /// <summary>
        /// 医院ID
        /// </summary>
        public string HOS_ID;
    }
}
