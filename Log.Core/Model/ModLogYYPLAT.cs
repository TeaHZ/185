using System;
using System.Collections.Generic;
using System.Text;

namespace Log.Core.Model
{
    class ModLogYYPLAT
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
    }
}
