using Newtonsoft.Json;
using Soft.Common;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

namespace OnlineBusHos9_EInvoice
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
                
                }
            }
            catch (Exception ex)
            {
   
                return false;
            }
            return true;
        }
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