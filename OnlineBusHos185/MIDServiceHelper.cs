using Hos185_His.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using File = System.IO.File;

namespace OnlineBusHos185
{
    public class MIDServiceHelper
    {
        private bool istest = false;

        public Output<T> CallServiceAPI<T>(string apiroute, string inputjson, string appid = "MYNJ", string appkey = "MYNJ")
        {
            try
            {
                DateTime InTime = DateTime.Now;

                XmlDocument doc = new XmlDocument();
                string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "bin", "OnlineBusHos185.dll.config");
                doc.Load(path);
                DataSet ds = XMLHelper.X_GetXmlData(doc, "configuration/appSettings");//请求的数据包

                string baseurl = ds.Tables[0].Rows[0]["value"].ToString().Trim();

                long timestamp = GetTimeStamp();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseurl + apiroute);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Headers.Add("HospitalCode", "tkxlglyy");
                request.Headers.Add("AppId", appid);
                request.Headers.Add("SecretKey", Signatue(appid, appkey, timestamp));
                request.Headers.Add("RequestTime", timestamp.ToString());
                using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
                {
                    dataStream.Write(inputjson);
                    dataStream.Close();
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1)
                {
                    encoding = "UTF-8"; //默认编码
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                string output = reader.ReadToEnd();

                XmlDocument doclog = RtnSucXml(apiroute, "4");

                XMLHelper.X_XmlInsertNode(doclog, "ROOT/BODY", "HOS_ID", "185");
                XMLHelper.X_XmlInsertNode(doclog, "ROOT/BODY", "input", inputjson);
                SaveLog(InTime, doclog.InnerXml, DateTime.Now, output);

                JObject jobj = JObject.Parse(output);

                if (jobj["data"].ToString() == "")
                {
                    Output<T> outrooterror = new Output<T>()
                    {
                        code = int.Parse(jobj["code"].ToString()),
                        message = jobj["message"].ToString(),
                        statusCode = int.Parse(jobj["statusCode"].ToString()),
                    };

                    return outrooterror;
                }

                Output<T> outroot = Newtonsoft.Json.JsonConvert.DeserializeObject<Output<T>>(output);

                return outroot;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Output<T> CallServiceAPIForm<T>(string apiroute, string poststring, string contenttype, Dictionary<string, string> headerParams, string appid = "MYNJ", string appkey = "MYNJ")
        {
            try
            {
                DateTime InTime = DateTime.Now;

                XmlDocument doc = new XmlDocument();
                string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "bin", "OnlineBusHos185.dll.config");
                doc.Load(path);
                DataSet ds = XMLHelper.X_GetXmlData(doc, "configuration/appSettings");//请求的数据包

                string baseurl = ds.Tables[0].Rows[0]["value"].ToString().Trim();

                long timestamp = GetTimeStamp();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(baseurl + apiroute);
                request.Method = "POST";
                request.ContentType = contenttype;
                request.Headers.Add("HospitalCode", "tkxlglyy");
                request.Headers.Add("AppId", appid);
                request.Headers.Add("SecretKey", Signatue(appid, appkey, timestamp));
                request.Headers.Add("RequestTime", timestamp.ToString());

                foreach (var item in headerParams)
                {
                    request.Headers.Add(item.Key, item.Value);
                }

                // Convert the post string to a byte array
                byte[] bytedata = System.Text.Encoding.UTF8.GetBytes(poststring);
                request.ContentLength = bytedata.Length;

                // Create the stream
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytedata, 0, bytedata.Length);
                requestStream.Close();

                // Get the response from remote server
                HttpWebResponse httpWebResponse = (HttpWebResponse)request.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                using (StreamReader reader = new StreamReader(responseStream, System.Text.Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        sb.Append(line);
                    }
                }

                string output = sb.ToString();
                XmlDocument doclog = RtnSucXml(apiroute, "4");

                XMLHelper.X_XmlInsertNode(doclog, "ROOT/BODY", "HOS_ID", "185");
                XMLHelper.X_XmlInsertNode(doclog, "ROOT/BODY", "input", poststring);
                SaveLog(InTime, doclog.InnerXml, DateTime.Now, output);

                Output<T> outroot = Newtonsoft.Json.JsonConvert.DeserializeObject<Output<T>>(output);

                return outroot;
            }
            catch (Exception ex)
            {
                WriteLog("", apiroute + "「异常」", ex.ToString());
                return null;
            }
        }

        private static string Signatue(string appId, string appKey, long timestamp)
        {
            string secretKey = Md5Base64(appId + appKey + timestamp);
            return secretKey;
        }

        private static string Md5Base64(string inStr)
        {
            using (var md5 = MD5.Create())
            {
                byte[] md5hash = md5.ComputeHash(Encoding.UTF8.GetBytes(inStr));
                string md5hash_base64 = Convert.ToBase64String(md5hash);
                return md5hash_base64;
            }
        }

        public static long GetTimeStamp()
        {
            long timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds(); ;
            return timestamp;
        }

        private static void SaveLog(DateTime intime, string inxml, DateTime outTime, string outxml)
        {
            Log.Helper.Model.ModLogHos logHos = new Log.Helper.Model.ModLogHos();
            logHos.inTime = intime;
            logHos.inXml = inxml;
            logHos.outTime = outTime;
            logHos.outXml = outxml;
            Log.Helper.LogHelper.Addlog(logHos);
        }

        private static XmlDocument RtnSucXml(string type, string czlx)
        {
            XmlDocument rtndoc = null;
            rtndoc = XMLHelper.X_CreateXmlDocument("ROOT");
            XMLHelper.X_XmlInsertNode(rtndoc, "ROOT", "HEADER");
            XMLHelper.X_XmlInsertNode(rtndoc, "ROOT/HEADER", "TYPE", type);
            XMLHelper.X_XmlInsertNode(rtndoc, "ROOT/HEADER", "CZLX", czlx);
            XMLHelper.X_XmlInsertNode(rtndoc, "ROOT", "BODY");
            XMLHelper.X_XmlInsertNode(rtndoc, "ROOT/BODY", "CLBZ", "0");//只有0表示成功
            XMLHelper.X_XmlInsertNode(rtndoc, "ROOT/BODY", "CLJG", "Success");
            return rtndoc;
        }

        protected static void WriteLog(string type, string className, string content)
        {
            string path = "";
            try
            {
                // path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\MySpringlog";
                path = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "PasSLog", "tkLog");
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
                string filename = path + "/" + DateTime.Now.ToString("yyyyMMdd") + type.Replace('|', ':') + ".log";//用日期对日志文件命名
                //创建或打开日志文件，向日志文件末尾追加记录
                StreamWriter mySw = File.AppendText(filename);

                //向日志文件写入内容
                string write_content = className + ": " + content;
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
    }
}