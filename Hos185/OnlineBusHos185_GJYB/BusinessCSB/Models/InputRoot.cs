namespace OnlineBusHos185_GJYB.Models
{
    public class InputRoot
    {

        /// <summary>
        /// 交易编号
        /// </summary>
        public string infno { get; set; }
        /// <summary>
        /// 发送方报文ID 
        /// 说明：定点医药机构编号(12)+时间(14)+顺序号(4)时间格式：yyyyMMddHHmmss
        /// </summary>
        public string msgid { get; set; }
        /// <summary>
        /// 就医地医保区划
        /// </summary>
        public string mdtrtarea_admvs { get; set; }
        /// <summary>
        /// 参保地医保区划
        /// 如果交易输入中含有人员编号，此项必填，可通过【1101】人员信息获取交易取得
        /// </summary>
        public string insuplc_admdvs { get; set; }
        /// <summary>
        /// 接收方系统代码
        /// </summary>
        public string recer_sys_code { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string dev_no { get; set; }
        /// <summary>
        /// 设备安全信息
        /// </summary>
        public string dev_safe_info { get; set; }
        /// <summary>
        /// 数字签名信息
        /// </summary>
        public string cainfo { get; set; }

        /// <summary>
        /// 签名类型
        /// </summary>
        public string signtype { get; set; }

        /// <summary>
        /// 接口版本号
        /// 例如：“V1.0”，版本号由医保下发通知。
        /// </summary>
        public string infver { get; set; }

        /// <summary>
        /// 经办人类别
        /// 1-经办人；2-自助终端；3-移动终端
        /// </summary>
        public string opter_type { get; set; }
        /// <summary>
        /// 经办人编号
        /// </summary>
        public string opter { get; set; }
        /// <summary>
        /// 经办人姓名
        /// </summary>
        public string opter_name { get; set; }

        /// <summary>
        /// 交易时间 
        /// 日期时间型的数据元（例如开始时间）格式为：yyyy-MM-dd HH:mm:ss ；日期型的数据元（例如开始日期）格式为：yyyy-MM-dd
        /// </summary>
        public string inf_time { get; set; }

        /// <summary>
        /// 定点医药机构编号
        /// </summary>
        public string fixmedins_code { get; set; }

        /// <summary>
        /// 定点医药机构名称
        /// </summary>
        public string fixmedins_name { get; set; }

        /// <summary>
        /// 交易签到流水号
        /// </summary>
        public string sign_no { get; set; }
        /// <summary>
        /// 定点医药机构软件厂商全称
        /// </summary>
        public string fixmedins_soft_fcty { get; set; }
        /// <summary>
        /// 交易输入
        /// </summary>
        public object input { get; set; }

        /// <summary>
        /// 获取业务内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public T GetInput<T>()
        {
            if (input == null)
            {
                return default(T);
            }
            if (input.GetType() == typeof(string))
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(input.ToString());
            }
            else
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Newtonsoft.Json.JsonConvert.SerializeObject(input));
            }
        }
    }





}
