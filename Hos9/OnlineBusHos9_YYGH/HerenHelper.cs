using Newtonsoft.Json;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnlineBusHos9_YYGH
{
    public static class HerenHelper<T>
    {
        private static string HerenHIS = ConfigurationManager.AppSettings["HerenHIS"];
        private static string HerenZF = ConfigurationManager.AppSettings["HerenZF"];

        public static PushServiceResult<T> pushService(string TradeCode, string pInput)
        {
            DateTime itime = DateTime.Now;

            Hashtable hs = new Hashtable
                {
                    { "DataType", "JSON" },
                    { "TradeCode", TradeCode},
                    { "pInput", pInput }
                };

            //WriteLog("Heren", TradeCode + "「请求」", pInput);

            string rtnstr = WebServiceHelper.QuerySoapWebService(HerenHIS, "pushService", hs).InnerText;
            DateTime otime = DateTime.Now;
            //WriteLog("Heren", TradeCode + "「响应」", rtnstr);
            if (TradeCode == "2001-QHZZJ" || TradeCode == "2002-QHZZJ")
            {
                WriteLog("Heren", TradeCode + "「响应」", "成功，报文省略");
            }
            else
            {
                //WriteLog("Heren", TradeCode + "「响应」", rtnstr);

                WriteLogdb(TradeCode, pInput, itime, rtnstr, otime);

            }


            PushServiceResult<T> result = JsonConvert.DeserializeObject<PushServiceResult<T>>(rtnstr);

            return result;
        }
        public static QueryServiceResult<T> QueryService(string TradeCode, object Tradebody)
        {
            QueryServiceInput input = new QueryServiceInput
            {
                Head = new QueryServiceInputHead() { TradeCode = TradeCode },
                Body = Tradebody
            };

            string pInput = JsonConvert.SerializeObject(input);

            Hashtable hs = new Hashtable
                {
                    { "DataType", "JSON" },
                    { "pInput", pInput }
                };

            WriteLog("Heren", "HerenHIS", HerenHIS);
            WriteLog("Heren", TradeCode + "「请求」", pInput);
            string rtnstr = WebServiceHelper.QuerySoapWebService(HerenHIS, "queryService", hs).InnerText;
            WriteLog("Heren", TradeCode + "「响应」", rtnstr);
            //if (TradeCode == "ZZJ001-QHZZJ")
            //{
            //    WriteLog("Heren", TradeCode + "「响应」", "成功，报文省略");
            //}
            //else
            //{
            //    WriteLog("Heren", TradeCode + "「响应」", rtnstr);

            //}


            var result = JsonConvert.DeserializeObject<QueryServiceResult<T>>(rtnstr);


            return result;
        }

        private static void WriteLog(string type, string className, string content)
        {
            string path = "";
            try
            {
                // path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\MySpringlog";
                path = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "PasSLog", type + "log");
            }
            catch (Exception ex)
            {
                //   path = HttpContent.Current.Server.MapPath("MySpringlog");
            }

            if (!Directory.Exists(path))//如果日志目录不存在就创建
            {
                Directory.CreateDirectory(path);
            }

            try
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");//获取当前系统时间
                string filename = path + "/" + DateTime.Now.ToString("yyyyMMdd") + ".log";//用日期对日志文件命名
                //创建或打开日志文件，向日志文件末尾追加记录
                StreamWriter mySw = File.AppendText(filename);

                //向日志文件写入内容
                string write_content = className + ":\r\n " + content;
                mySw.WriteLine(time + " " + type);
                mySw.WriteLine(write_content);
                mySw.WriteLine("");
                //关闭日志文件
                mySw.Close();
            }
            catch (Exception ex)
            {
            }
        }
        private static void WriteLogdb(string bustype, string input, DateTime intime, string output, DateTime outtime)
        {
            #region 日志

            SqlSugarModel.Loghos loghos = new SqlSugarModel.Loghos()
            {
                HOS_ID = "9",
                UID = Guid.NewGuid().ToString(),//todo:ddd
                InTime = intime,
                OutTime = outtime,
                InXml = input,
                OutXml = output,
                TYPE = bustype

            };
            LogHelper.SaveLogHos(loghos);

            #endregion 日志
        }

        public static async Task<string> SendTrade(Dictionary<string, string> @params)
        {
            DateTime itime = DateTime.Now;
            string TradeCode = "tran";
            var client = new System.Net.Http.HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, HerenZF);
            var collection = new List<KeyValuePair<string, string>>();
            foreach (var item in @params)
            {
                collection.Add(item);
            }

            var content = new FormUrlEncodedContent(collection);

            WriteLog("Heren", "支付「请求」", JsonConvert.SerializeObject(@params));

            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string re = await response.Content.ReadAsStringAsync();
            WriteLog("Heren", "支付「响应」", re);
            DateTime otime = DateTime.Now;

            WriteLogdb(TradeCode, JsonConvert.SerializeObject(@params), itime, re, otime);

            return re;
        }
    }

    public class QueryServiceInput
    {
        /// <summary>
        /// 
        /// </summary>
        public QueryServiceInputHead Head { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object Body { get; set; }
    }
    public class QueryServiceInputHead
    {
        /// <summary>
        /// 
        /// </summary>
        public string TradeCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TradeTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        /// <summary>
        /// 
        /// </summary>
        public string TradeNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BranchCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string HospitalCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SystemCode { get; set; } = "QHZZJ";

        /// <summary>
        /// 
        /// </summary>
        public string HipKeyNo { get; set; } = "e4d303f20f4ade45e0ab76fb5e6f48b6";

    }
    public class QueryServiceResult<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public QueryServiceResultHead Head { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public T Body { get; set; }
    }

    public class QueryServiceResultHead
    {
        /// <summary>
        /// 
        /// </summary>
        public string TradeCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TradeTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TradeNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SessionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TradeStatus { get; set; }
        /// <summary>
        /// 成功
        /// </summary>
        public string TradeMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BranchCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string HospitalCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SystemCode { get; set; }
    }
    public class PushServiceResult<T>
    {
        public int code { get; set; }

        /// <summary>
        ///
        /// </summary>
        public T data { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        public string msg { get; set; }
    }
}