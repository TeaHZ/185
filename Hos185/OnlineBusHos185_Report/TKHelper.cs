using Hos185_His.Models;
using Log.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using File = System.IO.File;

namespace Hos185_His
{
    public class TKHelper
    {
        private string appid = ConfigurationManager.AppSettings["tkappid"];// DB.Core.ConfigHelper.GetConfiguration("tkappid");
        private string appkey = ConfigurationManager.AppSettings["tkappkey"];// DB.Core.ConfigHelper.GetConfiguration("tkappkey");
        private string baseurl = ConfigurationManager.AppSettings["tkbaseurl"];//DB.Core.ConfigHelper.GetConfiguration("tkbaseurl");

        //string baseurl = "http://10.81.46.217:8002";
        //string baseurl = "https://mid-service.tkxlglyy.com:38002";

        public Output<T> CallServiceAPI<T>(string apiroute, string inputjson)
        {
            try
            {
                DateTime InTime = DateTime.Now;

                string output = "";

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
                output = reader.ReadToEnd();

                #region 日志

                XmlDocument doclog = RtnSucXml(apiroute, "4");

                XMLHelper.X_XmlInsertNode(doclog, "ROOT/BODY", "HOS_ID", "185");
                XMLHelper.X_XmlInsertNode(doclog, "ROOT/BODY", "input", inputjson);
                SaveLog(InTime, doclog.InnerXml, DateTime.Now, output);

                #endregion 日志

                try
                {
                    Output<T> outroot = Newtonsoft.Json.JsonConvert.DeserializeObject<Output<T>>(output);

                    return outroot;
                }
                catch (Exception ex)
                {
                    JObject jobj = JObject.Parse(output);

                    Output<T> outrooterror = new Output<T>()
                    {
                        code = int.Parse(jobj["code"].ToString()),
                        message = jobj["message"].ToString() + "\r\n" + ex.Message,
                    };

                    return outrooterror;
                }
            }
            catch (Exception ex)
            {
                SaveLog(DateTime.Now, apiroute, DateTime.Now, ex.ToString());
                return null;
            }
        }

        public Output<T> CallServiceAPIForm<T>(string apiroute, string poststring, string contenttype, Dictionary<string, string> headerParams)
        {
            try
            {
                DateTime InTime = DateTime.Now;

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
                SaveLog(DateTime.Now, apiroute, DateTime.Now, ex.ToString());

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

        private static void SaveLog(DateTime intime, string inxml, DateTime outTime, string outxml)
        {
            Log.Core.Model.ModLogHos logHos = new Log.Core.Model.ModLogHos();
            logHos.inTime = intime;
            logHos.inXml = inxml;
            logHos.outTime = outTime;
            logHos.outXml = outxml;
            LogHelper.Addlog(logHos);
        }

    }
}