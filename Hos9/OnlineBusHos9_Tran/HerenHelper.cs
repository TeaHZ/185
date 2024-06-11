
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBusHos9_Tran
{
    public class HerenHelper
    {

        static string HerenHIS = ConfigurationManager.AppSettings["HerenHIS"];
        static string HerenZF = ConfigurationManager.AppSettings["HerenZF"];

        public static async Task<string> SendTrade(Dictionary<string, string> @params)
        {
            DateTime itime = DateTime.Now;//logdb
            string TradeCode ="tran";
            var client = new System.Net.Http.HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, HerenZF);
            var collection = new List<KeyValuePair<string, string>>();
            foreach (var item in @params)
            {
                collection.Add(item);
            }

            var content = new FormUrlEncodedContent(collection);

            //WriteLog("Heren", "支付「请求」", JsonConvert.SerializeObject(@params));

            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string re = await response.Content.ReadAsStringAsync();
            //WriteLog("Heren", "支付「响应」", re);

            DateTime otime = DateTime.Now;

            WriteLogdb(TradeCode, JsonConvert.SerializeObject(@params), itime, re, otime);

            return re;
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

    }
}
