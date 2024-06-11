using System;
using System.Collections.Generic;
using System.Text;

namespace Log.Core.Model
{
    public class ModLogQHZZJ
    {
        /// <summary>
        /// 模块
        /// </summary>
        public string BUS;
        /// <summary>
        /// 模块ID
        /// </summary>
        public string BUS_NAME;
        /// <summary>
        /// 子模块ID
        /// </summary>
        public string SUB_BUSNAME;
        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime InTime;
        /// <summary>
        /// 请求入参
        /// </summary>
        public string InData;
        /// <summary>
        /// 返回出参
        /// </summary>
        public string OutData;
        /// <summary>
        /// 返回时间时间
        /// </summary>
        public DateTime OutTime;

    }
}
