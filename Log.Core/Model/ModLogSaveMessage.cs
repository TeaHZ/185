using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Core.Model
{
    /// <summary>
    /// 存储短信发送记录
    /// </summary>
    public class ModLogSaveMessage
    {
        /// <summary>
        /// 短信类型
        /// </summary>
        public string TYPE { get; set; }
        /// <summary>
        /// 短信内容
        /// </summary>
        public string MESSAGE { get; set; }
        /// <summary>
        /// 病人姓名
        /// </summary>
        public string PAT_NAME { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public string DJ_TIME { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string MOBILE_NO { get; set; }
    }
}
