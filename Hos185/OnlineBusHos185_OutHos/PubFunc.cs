
using Newtonsoft.Json;
using Soft.Common;
using SqlSugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace OnlineBusHos185_OutHos
{
    public class PubFunc
    {
        public static bool CALLSERVICE(string HOS_ID, string inxml, ref string his_rtnxml)
        {
            bool flag = false;

            DateTime intime = DateTime.Now;
           
            SqlSugarModel.Loghos log = new SqlSugarModel.Loghos();
            try
            {
                var hosconfig = GetHosServiceConfig(HOS_ID);
                if (hosconfig == null) 
                {
                    his_rtnxml = "未取到HIS接口配置数据[hos_service_config]";
                    flag = false;
                    goto TheEnd;
                }
                if (hosconfig.callmode == "0")//webservice
                {
                    Hashtable hashtable = new Hashtable();
                    hashtable = GetHashTable(inxml, HOS_ID, hosconfig.Params, hosconfig.use_encryption);
                    XmlDocument doc_sec = WebServiceHelper.QuerySoapWebService(hosconfig.Service_URL, hosconfig.MethodName, hashtable);
                    his_rtnxml = doc_sec.InnerText;
                }
                else if (hosconfig.callmode == "1")//api
                {

                    string secretkey = EncryptionKey.KeyData.AESKEY(HOS_ID);
                    string encryxml = AESExample.AESEncrypt(inxml, secretkey);
                    string signature = EncryptionKey.MD5Helper.Md5(encryxml + secretkey);
                    indata apiin = new indata();
                    apiin.user_id = HOS_ID;
                    apiin.xmlstr = encryxml;
                    apiin.signature = signature;
                    var http = new HttpClient(hosconfig.Service_URL);
                    string out_data = "";
                    int status = http.SendJson(Newtonsoft.Json.JsonConvert.SerializeObject(apiin), Encoding.UTF8, out out_data);
                    if (status == 200)
                    {
                        outdata outdata = JsonConvert.DeserializeObject<outdata>(out_data);
                        his_rtnxml = outdata.outxml;
                    }
                    else
                    {
                        his_rtnxml = out_data;
                        flag= false;
                        goto TheEnd;
                    }
                }
                if (hosconfig.use_encryption == "1")
                {
                    string secretkey = EncryptionKey.KeyData.AESKEY(HOS_ID);
                    his_rtnxml = AESExample.AESDecrypt(his_rtnxml, secretkey);
                }
                flag = true;
            }
            catch (Exception ex)
            {
                his_rtnxml = ex.ToString()+"   "+his_rtnxml ;
                flag = false;
            }
            TheEnd:
            log.InTime = intime;
            log.InXml = inxml;
            log.OutTime = DateTime.Now;
            log.OutXml = his_rtnxml;
            LogHelper.SaveLogHos(log);

            return flag;
        }


        public static bool GetSysID(string SYSIDNAME,out int SYSID) 
        {
            var db = new DbMySQLZZJ().Client;
            //支持output
            var SYSIDNAMEP = new SugarParameter("@SYSIDNAME", SYSIDNAME);
            var SYSIDP = new SugarParameter("@SYSID", null, true);
            db.Ado.UseStoredProcedure().GetDataTable("GETSYSIDBASE", SYSIDNAMEP, SYSIDP);
            SYSID = FormatHelper.GetInt(SYSIDP.Value);
            return true;
        }

        public static Dictionary<string, string> Get_Filter(string filter)
        {
            Dictionary<string, string> dic_filter = new Dictionary<string, string>();
            try
            {
                if (FormatHelper.GetStr(filter) != "")
                {
                    try
                    {
                        dic_filter = JsonConvert.DeserializeObject<Dictionary<string, string>>(filter);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            { }
            return dic_filter;
        }

        private static SqlSugarModel.HosServiceConfig GetHosServiceConfig(string HOS_ID) 
        {
            var db = new DbMySQLZZJ().Client;
            var config = db.Queryable<SqlSugarModel.HosServiceConfig>().InSingle(HOS_ID);
            return config;
        }

        /// <summary>
        /// POST入参存入hashtable
        /// </summary>
        /// <param name="inxml">入参</param>
        /// <param name="hos_id">医院ID</param>
        /// <param name="para">POST参数</param>
        /// <returns></returns>
        private static Hashtable GetHashTable(string inxml, string hos_id, string para, string use_encryption)
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


        private class indata
        {
            public string xmlstr { get; set; }
            public string user_id { get; set; }
            public string signature { get; set; }
        }
        private class outdata
        {
            public string outxml { get; set; }
        }

       
    }
}
