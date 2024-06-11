namespace OnlineBusHos9_GJYB.Models
{
    public class OutputRoot
    {
        /// <summary>
        /// 交易状态码
        /// 0成功，-1失败
        /// </summary>
        public string infcode { get; set; }
        /// <summary>
        /// 接收方报文ID
        /// 接收方返回，接收方医保区划代码(6)+时间(14)+流水号(10)时间格式：yyyyMMddHHmmss

        /// </summary>
        public string inf_refmsgid { get; set; }
        /// <summary>
        /// 接收报文时间
        /// 格式：yyyyMMddHHmmssSSS
        /// </summary>
        public string refmsg_time { get; set; }

        /// <summary>
        /// 响应报文时间
        /// 格式：yyyyMMddHHmmssSSS
        /// </summary>
        public string respond_time { get; set; }
        public string enctype { get; set; }
        public string signtype { get; set; }
        /// <summary>
        /// 错误信息
        /// 交易失败状态下，业务返回的错误信息
        /// </summary>
        public string err_msg { get; set; }

        /// <summary>
        /// 交易输出
        /// </summary>
        public object output { get; set; }
        /// <summary>
        /// 获取业务内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public T GetOutput<T>()
        {
            if (output == null)
            {
                return default(T);
            }
            if (output.GetType() == typeof(string))
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(output.ToString());
            }
            else
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Newtonsoft.Json.JsonConvert.SerializeObject(output));
            }
        }
    }
}
