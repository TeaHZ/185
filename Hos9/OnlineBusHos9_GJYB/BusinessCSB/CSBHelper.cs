using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace OnlineBusHos9_GJYB
{
    public class CSBHelper
    {

        /// <summary>
        /// 生成医保请求的root节点
        /// </summary>
        /// <param name="HOS_ID"></param>
        /// <param name="infno"></param>
        /// <param name="opter_no"></param>
        /// <param name="insuplc_admdvs"></param>
        /// <param name="jsonobj"></param>
        /// <param name="inputRoot"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static bool CreateInputRoot(string HOS_ID, string infno, string msgid, string opter_no, string insuplc_admdvs, object jsonobj, ref Models.InputRoot inputRoot, ref string msg)
        {
            try
            {
                string opter_id = FormatHelper.GetStr(HOS_ID) + "-" + FormatHelper.GetStr(opter_no);
                //从缓存取
                Model.hospital hospital = RedisDataHelper.GetRedisHospital(HOS_ID, "");
                Model.hos_opter hos_opter = RedisDataHelper.GetRedishos_opter(HOS_ID, opter_no);
                if (hospital == null)
                {
                    msg = "没有找到" + HOS_ID + "对应的医疗机构信息！";
                    return false;
                }
                if (hos_opter == null)
                {
                    //注册操作员
                    hos_opter = new Model.hos_opter();
                    hos_opter.HOS_ID = HOS_ID;
                    hos_opter.opter_no = opter_no;
                    hos_opter.opter_name = opter_no;
                    hos_opter.opter_type = 1;
                    hos_opter.vali_flag = "1";
                    new BLL.hos_opter().Add(hos_opter);
                    //msg = "没有找到" + opter_no + "对应的操作员信息，请先注册！";
                    //return false;
                }

                if (string.IsNullOrEmpty(hos_opter.ip))
                {
                    hos_opter.ip = "127.0.0.1";
                }
                if (string.IsNullOrEmpty(hos_opter.mac))
                {
                    hos_opter.mac = "00:00:00:00:00:00";
                }
                string sign_no = hos_opter.sign_no;
                if (infno != "9001" && infno != "9002")//非签到签退交易的
                {
                    if (string.IsNullOrEmpty(sign_no) || hos_opter.sign_date != DateTime.Now.ToString("yyyy-MM-dd"))
                    {
                        //需要进行签到
                        if (!SignIn(HOS_ID, opter_no, hos_opter.ip, hos_opter.mac, ref sign_no, ref msg))
                        {
                            return false;
                        }
                    }
                }
                if (string.IsNullOrEmpty(msgid))
                {
                    // 发送方报文ID
                    msgid = FormatHelper.GetStr(hospital.fixmedins_code) + DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(0, 9999).ToString("0000");
                }
                inputRoot = new Models.InputRoot()
                {
                    infno = infno,// 交易编号
                    msgid = msgid,// 发送方报文ID
                    mdtrtarea_admvs = FormatHelper.GetStr(hospital.mdtrtarea_admvs),// 就医地医保区划
                    insuplc_admdvs = insuplc_admdvs,// 参保地医保区划
                    recer_sys_code = hospital.recer_sys_code,// 接收方系统代码:0023
                    dev_no = FormatHelper.GetStr(hospital.dev_no),// 设备编号
                    dev_safe_info = FormatHelper.GetStr(hospital.dev_safe_info),//   设备安全信息
                    cainfo = FormatHelper.GetStr(hospital.cainfo),// 数字签名信息
                    signtype = "SM2",//   签名类型
                    infver = hospital.infver,// 接口版本号 "V1.0"
                    opter_type = FormatHelper.GetStr(hos_opter.opter_type),//  经办人类别
                    opter = FormatHelper.GetStr(hos_opter.opter_no),// 经办人
                    opter_name = FormatHelper.GetStr(hos_opter.opter_name),// 经办人姓名
                    inf_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),//  交易时间
                    fixmedins_code = FormatHelper.GetStr(hospital.fixmedins_code),//  定点医药机构编号
                    fixmedins_name = FormatHelper.GetStr(hospital.fixmedins_name),// 定点医药机构名称
                    sign_no = sign_no,//交易签到流水号
                    fixmedins_soft_fcty = hospital.fixmedins_soft_fcty,// "江苏启航开创软件有限公司",
                    input = jsonobj
                };
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }
        private static readonly object locksign = new object();
        /// <summary>
        /// 医保签到(内部使用)
        /// </summary>
        /// <param name="HOS_ID"></param>
        /// <param name="opter_no"></param>
        /// <param name="sign_no"></param>
        /// <param name="errormessage"></param>
        /// <returns></returns>
        internal static bool SignIn(string HOS_ID, string opter_no, string ip, string mac, ref string sign_no, ref string errormessage)
        {
            try
            {
                Monitor.Enter(locksign);
                {
                    BLL.hos_opter hos_Opter = new BLL.hos_opter();
                    Model.hos_opter modelhos_Opter = hos_Opter.GetModel(HOS_ID, opter_no);
                    if (modelhos_Opter == null)
                    {
                        errormessage = "没有操作员信息";
                        return false;
                    }
                    if (modelhos_Opter.sign_date == DateTime.Now.ToString("yyyy-MM-dd"))
                    {
                        sign_no = modelhos_Opter.sign_no;
                        return true;
                    }

                    Models.T9001.SignIn t9001 = new Models.T9001.SignIn()
                    {
                        ip = ip,
                        mac = mac,
                        opter_no = opter_no
                    };
                    Models.T9001.Root root = new Models.T9001.Root();
                    root.signIn = t9001;

                    object output = "";
                    Models.InputRoot inputRoot = new Models.InputRoot();
                    string msg = "";
                    bool flag = CreateInputRoot(HOS_ID, "9001", "", opter_no, "", root, ref inputRoot, ref msg);
                    if (!flag)
                    {
                        errormessage = msg;
                        return false;
                    }
                    Models.OutputRoot outPutRoot = new Models.OutputRoot();
                    flag = CallCSBService(inputRoot, out outPutRoot);
                    if (outPutRoot.infcode != "0")
                    {
                        errormessage = "签到失败：" + outPutRoot.err_msg;
                        return false;
                    }
                    Models.RT9001.Root rt9001 = outPutRoot.GetOutput<Models.RT9001.Root>();

                    flag = BusDataSave.T9001(HOS_ID, false, inputRoot, root, rt9001);
                    return flag;
                }
            }
            catch (Exception ex)
            {
                errormessage = "执行异常:" + ex.Message;
                return false;
            }
            finally
            {
                Monitor.Exit(locksign);
            }
        }



        /// <summary>
        /// 调用医保（医保参数）
        /// </summary>
        /// <param name="inputRoot"></param>
        /// <param name="outPutRoot"></param>
        /// <returns></returns>
        public static bool CallCSBService(Models.InputRoot inputRoot, out Models.OutputRoot outPutRoot)
        {
            Model.hospital hospital = RedisDataHelper.GetRedisHospital("", inputRoot.fixmedins_code);
            DateTime inTime = DateTime.Now;
            bool result = false;
            string outputjson = string.Empty;
            string inputjson = JsonConvert.SerializeObject(inputRoot);
            //outPutRoot = new Models.OutputRoot();
            /*
            string urlzf = "http://223.68.162.50:8091/CSB/";
            outputjson = HttpHelper.PostFunction(urlzf, inputjson);
            outPutRoot = JsonConvert.DeserializeObject<Models.OutputRoot>(outputjson);
            */
            try
            {


                //long timestamp = Convert.ToInt64((DateNow - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))).TotalSeconds);
                long timeStamp = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
                string api_timestamp = timeStamp.ToString();
                //签名
                Dictionary<string, object[]> formParamDict = new Dictionary<string, object[]>();
                string signature = sign(hospital.api_name, hospital.api_version, timeStamp, hospital.api_access_key, hospital.api_secretKey, formParamDict, null);
                //调用
                string url = hospital.api_uri;//http://10.72.3.0:8086/CSB

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 120 * 1000;
                request.Method = "post";
                request.ContentType = "application/json;charset=utf-8";
                request.Headers.Add("_api_timestamp", api_timestamp);
                request.Headers.Add("_api_name", hospital.api_name);
                request.Headers.Add("_api_version", hospital.api_version);
                request.Headers.Add("_api_access_key", hospital.api_access_key);
                request.Headers.Add("_api_signature", signature);
                #region 宿迁
                //request.ContentType = "application/json";
                //long currentTicks = DateTime.Now.Ticks;
                //DateTime dtFrom = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                //string api_timestamp = ((currentTicks - dtFrom.Ticks) / 10000).ToString();
                //request.Headers.Add("_api_timestamp", api_timestamp);
                //request.Headers.Add("_api_name", hospital.api_name);
                ////签名计算（按理说这里应该是参数排序）
                //Dictionary<string, string> paramdic = new Dictionary<string, string>();
                //paramdic.Add("_api_access_key", hospital.api_access_key);
                //paramdic.Add("_api_name", hospital.api_name);
                //paramdic.Add("_api_timestamp", api_timestamp);
                //paramdic.Add("_api_version", hospital.api_version);
                //Dictionary<string, string> dicSign = paramdic.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
                ////加密串拼接
                //string param = string.Join("&", dicSign.Select(x => x.Key + "=" + x.Value).ToArray());
                //HMAC m = HMAC.Create("HMACSHA1");
                //m.Key = Encoding.UTF8.GetBytes(hospital.api_secretKey);
                //byte[] signData = Encoding.UTF8.GetBytes(param);
                //byte[] finalData = m.ComputeHash(signData);
                //signature = Convert.ToBase64String(finalData);
                //request.Headers.Add("_api_signature", signature);
                //request.Headers.Add("_api_version", hospital.api_version);
                //request.Headers.Add("_api_access_key", hospital.api_access_key);
                #endregion


                //填充POST数据
                byte[] bytesRequestData = Encoding.UTF8.GetBytes(inputjson);
                request.ContentLength = bytesRequestData.Length;
                using (Stream requestStream = request.GetRequestStream())
                {
                    //获得请求流
                    requestStream.Write(bytesRequestData, 0, bytesRequestData.Length);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //下载交易
                if (response.ContentType.Contains("application/octet-stream"))//                if (inputRoot.infno == "9102")
                {
                    //通过此交易下载【1301 - 1319】、【5204】、【3202】交易生成的文件。
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "download");
                    string filepath = Path.Combine(path, inputRoot.infno + "_" + inputRoot.msgid + ".zip");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    FileStream fs = new FileStream(filepath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);

                    Stream stream = response.GetResponseStream();
                    //创建本地文件写入流
                    byte[] bArr = new byte[1024];
                    int iTotalSize = 0;
                    int size = stream.Read(bArr, 0, (int)bArr.Length);
                    while (size > 0)
                    {
                        iTotalSize += size;
                        fs.Write(bArr, 0, size);
                        size = stream.Read(bArr, 0, (int)bArr.Length);
                    }
                    fs.Close();
                    stream.Close();

                    outPutRoot = new Models.OutputRoot();
                    outPutRoot.infcode = "0";
                    outPutRoot.inf_refmsgid = inputRoot.msgid;
                    outPutRoot.refmsg_time = inTime.ToString("yyyyMMddHHmmssfff");
                    outPutRoot.respond_time = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    byte[] bytes = File.ReadAllBytes(filepath);

                    fsDownloadOut fsDownloadOut = new fsDownloadOut();
                    fsDownloadOut.filebytes = bytes;

                    fsDownloadResponse fsDownloadResponse = new fsDownloadResponse();
                    fsDownloadResponse.fsDownloadOut = fsDownloadOut;
                    outPutRoot.output = fsDownloadResponse;//
                }
                else
                {
                    string encoding = response.ContentEncoding;
                    if (encoding == null || encoding.Length < 1)
                    {
                        encoding = "UTF-8"; //默认编码  
                    }
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                    outputjson = reader.ReadToEnd();

                    outPutRoot = JsonConvert.DeserializeObject<Models.OutputRoot>(outputjson);
                }
                result = true;
            }
            catch (Exception ex)
            {
                outPutRoot = new Models.OutputRoot();
                outPutRoot.infcode = "-2";
                outPutRoot.inf_refmsgid = inputRoot.msgid;
                outPutRoot.refmsg_time = inTime.ToString("yyyyMMddHHmmssfff");
                outPutRoot.respond_time = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                outPutRoot.err_msg = ex.Message;
                outPutRoot.output = "";
                result = false;
                //记录错误日志
                outputjson = FormatHelper.GetStr(outputjson) + "  平台报错内容：" + ex.Message;
            }

            DateTime outTime = DateTime.Now;


            try
            {
                insurlog insurlog = new insurlog
                {
                    msgid = inputRoot.msgid,
                    fixmedins_code = inputRoot.fixmedins_code,
                    infno = inputRoot.infno,
                    InTime = inTime,
                    InData = inputjson,
                    OutTime = outTime,
                    OutData = outputjson,
                    OutCode = outPutRoot.infcode,
                    sign_no = inputRoot.sign_no
                };
                LogHelper.SaveInsurLog(insurlog);
            }
            catch (Exception ex)
            {
                outPutRoot = new Models.OutputRoot();
                outPutRoot.infcode = "-2";
                outPutRoot.inf_refmsgid = inputRoot.msgid;
                outPutRoot.refmsg_time = inTime.ToString("yyyyMMddHHmmssfff");
                outPutRoot.respond_time = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                outPutRoot.err_msg = ex.Message;
                outPutRoot.output = "";
                result = false;
                //记录错误日志
                outputjson = FormatHelper.GetStr(outputjson) + "  平台报错内容：" + ex.Message;
            }
            return result;
        }

        public class fsDownloadResponse
        {
            public fsDownloadOut fsDownloadOut { get; set; }

        }
        /// <summary>
        /// 文件下载出参
        /// </summary>
        public class fsDownloadOut
        {
            public byte[] filebytes { get; set; }
        }
        /// <summary>
        /// 本方法生成http请求的csb签名值。
        /// 调用csb服务时，需要在httpheader中增加以下几个头信息：
        /// _api_name: csb服务名
        /// _api_version: csb服务版本号
        /// _api_access_key: csb上的凭证ak
        /// _api_timestamp: 时间戳
        /// _api_signature: 本方法返回的签名串
        /// </summary>
        /// <param name="apiName">csb服务名</param>
        /// <param name="apiVersion">csb服务版本号</param>
        /// <param name="timeStamp">时间戳</param>
        /// <param name="accessKey">csb上的凭证ak</param>
        /// <param name="secretKey">csb上凭证的sk</param>
        /// <param name="formParamDict">form表单提交的参数列表(各参数值是还未urlEncoding编码的原始业务参数值)。如果是form提交，请使用 Content-Type= application/x-www-form-urlencoded </param>
        /// <param name="body">非form表单方式提交的请求内容，目前没有参与签名计算</param>
        /// <returns>签名串。</returns>
        internal static string sign(string apiName, string apiVersion, long timeStamp, string accessKey, string secretKey, Dictionary<string, object[]> formParamDict, object body)
        {
            Dictionary<string, object[]> newDict = new Dictionary<string, object[]>();
            if (formParamDict != null)
            {
                foreach (KeyValuePair<string, object[]> pair in formParamDict)
                {
                    newDict.Add(pair.Key, pair.Value);
                }
            }

            //设置csb要求的头参数
            newDict.Add("_api_name", new String[] { apiName });
            newDict.Add("_api_version", new String[] { apiVersion });
            newDict.Add("_api_access_key", new String[] { accessKey });
            newDict.Add("_api_timestamp", new object[] { timeStamp });

            //对所有参数进行排序
            var sortedDict = from pair in newDict orderby pair.Key select pair;
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, object[]> pair in sortedDict)
            {
                foreach (object obj in pair.Value)
                {
                    builder.Append(string.Format("{0}={1}&", pair.Key, obj));
                }
            }
            string str = builder.ToString();
            if (str.EndsWith("&"))
            {
                str = str.Substring(0, str.Length - 1); //去掉最后一个多余的 & 符号
            }
            System.Security.Cryptography.HMACSHA1 hmacsha = new System.Security.Cryptography.HMACSHA1
            {
                Key = Encoding.UTF8.GetBytes(secretKey)
            };
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(hmacsha.ComputeHash(bytes));
        }
    }
}