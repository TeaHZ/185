using Hos185_His.Models;
using Hos185_His;
using Log.Core.Model;

using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using Soft.Common;

namespace OnlineBusHos185_EInvoice
{
    internal class GlobalVar
    {
        public static string DoBussiness = GetConfig("DoBussiness");

        public static string callmode = GetConfig("callmode");

        public static string posturl = GetConfig("url");

        public static string parameter = GetConfig("parameters");

        public static string use_encryption = GetConfig("use_encryption");

        public static string MethodName = GetConfig("MethodName");

        public static string GetConfig(string configname)
        {
            XmlDocument docini = new XmlDocument();
            docini.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "OnlineBusHos185_EInvoice.dll.config"));
            DataSet ds = XMLHelper.X_GetXmlData(docini, "configuration/appSettings");//请求的数据包
            DataRow[] dr = ds.Tables[0].Select("key='" + configname + "'");
            if (dr.Length > 0)
            {
                return dr[0]["value"].ToString();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// POST入参存入hashtable
        /// </summary>
        /// <param name="inxml">入参</param>
        /// <param name="hos_id">医院ID</param>
        /// <param name="para">POST参数</param>
        /// <returns></returns>
        public static Hashtable GetHashTable(string inxml, string hos_id, string para, string use_encryption)
        {
            try
            {
                Hashtable hashtable = new Hashtable();
                if (use_encryption == "1")
                {
                    string secretkey = "";
                    secretkey = EncryptionKey.KeyData.AESKEY(hos_id);
                    string encryxml = AESExample.AESEncrypt(inxml, secretkey);
                    string signature = EncryptionKey.MD5Helper.Md5(encryxml + secretkey);
                    string[] items = para.Split('^');
                    string[] _showids = items[0].Split('|');
                    string[] _shownames = items[1].Split('|');

                    if (_showids[0] == "1")
                    {
                        hashtable.Add(_shownames[0], encryxml);
                    }
                    if (_showids[1] == "1")
                    {
                        hashtable.Add(_shownames[1], hos_id);
                    }
                    if (_showids[2] == "1")
                    {
                        hashtable.Add(_shownames[2], signature);
                    }
                }
                else
                {
                    string[] items = para.Split('^');
                    string[] _showids = items[0].Split('|');
                    string[] _shownames = items[1].Split('|');
                    if (_showids[0] == "1")
                    {
                        hashtable.Add(_shownames[0], inxml);
                    }
                    if (_showids[1] == "1")
                    {
                        hashtable.Add(_shownames[1], hos_id);
                    }
                    if (_showids[2] == "1")
                    {
                        hashtable.Add(_shownames[2], "");
                    }
                }
                return hashtable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public class indata
        {
            public string xmlstr { get; set; }
            public string user_id { get; set; }
            public string signature { get; set; }
        }

        public class outdata
        {
            public string outxml { get; set; }
        }

        public static bool CALLSERVICE(string HOS_ID, string inxml, ref string his_rtnxml)
        {
            DateTime intime = DateTime.Now;
            try
            {
                if (callmode == "0")//webservice
                {
                    Hashtable hashtable = new Hashtable();
                    hashtable = GlobalVar.GetHashTable(inxml, HOS_ID, parameter, use_encryption);
                    XmlDocument doc_sec = WebServiceHelper.QuerySoapWebService(posturl, GlobalVar.MethodName, hashtable);
                    his_rtnxml = doc_sec.InnerText;
                }
                else if (callmode == "1")//api
                {
                    string secretkey = EncryptionKey.KeyData.AESKEY(HOS_ID);
                    string encryxml = AESExample.AESEncrypt(inxml, secretkey);
                    string signature = EncryptionKey.MD5Helper.Md5(encryxml + secretkey);
                    indata apiin = new indata();
                    apiin.user_id = HOS_ID;
                    apiin.xmlstr = encryxml;
                    apiin.signature = signature;
                    var http = new HttpClient(posturl);
                    string out_data = "";
                    int status = http.SendJson(encryxml, Encoding.UTF8, out out_data);
                    if (status == 200)
                    {
                        outdata outdata = JsonConvert.DeserializeObject<outdata>(out_data);
                        his_rtnxml = outdata.outxml;
                    }
                    else
                    {
                        ModLogHosError modLogHos = new ModLogHosError();
                        modLogHos.inTime = intime;
                        modLogHos.inXml = inxml;
                        modLogHos.outTime = DateTime.Now;
                        modLogHos.outXml = out_data;
                        new Log.Core.MySQLDAL.DalLogHosError().Add(modLogHos);
                        his_rtnxml = out_data;
                        return false;
                    }
                }
                if (use_encryption == "1")
                {
                    string secretkey = EncryptionKey.KeyData.AESKEY(HOS_ID);
                    his_rtnxml = AESExample.AESDecrypt(his_rtnxml, secretkey);
                }
                if (DoBussiness == "1")
                {
                    ModLogHos modLogHos = new ModLogHos();
                    modLogHos.inTime = intime;
                    modLogHos.inXml = inxml;
                    modLogHos.outTime = DateTime.Now;
                    modLogHos.outXml = his_rtnxml;
                    new Log.Core.MySQLDAL.DalLogHos().Add(modLogHos);
                }
            }
            catch (Exception ex)
            {
                ModLogHosError modLogHos = new ModLogHosError();
                modLogHos.inTime = intime;
                modLogHos.inXml = inxml;
                modLogHos.outTime = DateTime.Now;
                modLogHos.outXml = his_rtnxml;
                new Log.Core.MySQLDAL.DalLogHosError().Add(modLogHos);
                his_rtnxml = ex.ToString();
                return false;
            }
            return true;
        }
        public static Output<T> CallAPI<T>(string routepath, string inputjson)
        {
            TKHelper main = new TKHelper();
            Output<T> output = main.CallServiceAPI<T>(routepath, inputjson);
            return output;
        }
        //public static bool CallService_Core(string HOS_ID, string inxml, ref string his_rtnxml)
        //{
        //    DateTime InTime = DateTime.Now;
        //    try
        //    {
        //        //string secretkey = "";
        //        //secretkey = EncryptionKey.KeyData.AESKEY(HOS_ID);
        //        //string encryxml = AESExample.AESEncrypt(inxml, secretkey);
        //        //string signature = EncryptionKey.MD5Helper.Md5(encryxml + secretkey);

        //        string webServiceUrl = GlobalVar.posturl;
        //        GlobalVar.WriteLog("GETHISISSUEBYSFZNO", "OnlineBusHos32_EInvoice", webServiceUrl);
        //        // 创建 HTTP 绑定对象
        //        var binding = new BasicHttpBinding();
        //        //设置最大传输接受数量
        //        binding.MaxReceivedMessageSize = 2147483647;
        //        // 根据 WebService 的 URL 构建终端点对象
        //        var endpoint = new EndpointAddress(webServiceUrl);

        //        // Elecinvoice.ServiceSoapClient client = new Elecinvoice.ServiceSoapClient(binding, endpoint);
        //        dzpdjservice.ServiceSoapClient client = new dzpdjservice.ServiceSoapClient(binding, endpoint);
        //        GlobalVar.WriteLog("GETHISISSUEBYSFZNO", "OnlineBusHos32_EInvoice", "服务链接成功");
        //        Task<string> response = client.BusinessElectInvoiceAsync(inxml);//(encryxml, HOS_ID, signature)

        //        his_rtnxml = response.Result;
        //        GlobalVar.WriteLog("GETHISISSUEBYSFZNO", "OnlineBusHos32_EInvoice", his_rtnxml);

        //        //if (use_encryption == "1")
        //        //{
        //        //    his_rtnxml = AESExample.AESDecrypt(his_rtnxml, secretkey);
        //        //}
        //        Log.Core.Model.ModLogHos modLogHos = new Log.Core.Model.ModLogHos();
        //        modLogHos.inTime = InTime;
        //        modLogHos.outTime = DateTime.Now;
        //        modLogHos.inXml = inxml;
        //        modLogHos.outXml = his_rtnxml;
        //        Log.Core.LogHelper.Addlog(modLogHos);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        GlobalVar.WriteLog("GETHISISSUEBYSFZNO", "OnlineBusHos32_EInvoice", ex.ToString());
        //        Log.Core.Model.ModLogHos modLogHos = new Log.Core.Model.ModLogHos();
        //        modLogHos.inTime = InTime;
        //        modLogHos.outTime = DateTime.Now;
        //        modLogHos.inXml = inxml;
        //        modLogHos.outXml = ex.ToString();
        //        Log.Core.LogHelper.Addlog(modLogHos);
        //        his_rtnxml = ex.ToString();
        //        return false;
        //    }
        //}

        //public static void WriteLog(string type, string className, string content)
        //{
        //    string path = "";
        //    try
        //    {
        //        // path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\MySpringlog";
        //        path = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "PasSLog", "ZzjLog");
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    if (!Directory.Exists(path))//如果日志目录不存在就创建
        //    {
        //        Directory.CreateDirectory(path);
        //    }

        //    try
        //    {
        //        string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");//获取当前系统时间
        //        string filename = path + "/" + DateTime.Now.ToString("yyyyMMdd") + type.Replace('|', ':') + ".log";//用日期对日志文件命名
        //        //创建或打开日志文件，向日志文件末尾追加记录
        //        StreamWriter mySw = File.AppendText(filename);

        //        //向日志文件写入内容
        //        string write_content = className + ": " + content;
        //        mySw.WriteLine(time + " " + type);
        //        mySw.WriteLine(write_content);
        //        mySw.WriteLine("");
        //        //关闭日志文件
        //        mySw.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
    }

    public class Root
    {
        /// <summary>
        ///
        /// </summary>
        public ROOT ROOT { get; set; }

        /// <summary>
        ///
        /// </summary>
        public object BODY { get; set; }
    }

    public class ROOT
    {
        public HEADER HEADER { get; set; }
    }

    public class Root_rtn
    {
        /// <summary>
        ///
        /// </summary>
        public ROOT_rtn ROOT { get; set; }
    }

    public class ROOT_rtn
    {
        /// <summary>
        ///
        /// </summary>
        public HEADER HEADER { get; set; }

        /// <summary>
        ///
        /// </summary>
        public object BODY { get; set; }
    }

    public class HEADER
    {
        public string MODULE { get; set; }
        public string CZLX { get; set; }
        public string TYPE { get; set; }
        public string SOURCE { get; set; }
    }
}