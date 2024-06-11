using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Core.Model
{
    /// <summary>
    /// sql报错日志
    /// </summary>
    public class ModSqlError
    {
        /// <summary>
        /// 语句类型
        /// </summary>
        public string TYPE { get; set; }
        /// <summary>
        /// 错误原因
        /// </summary>
        public string EXCEPTION { get; set; }
        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime time { get; set; }

    }
}
