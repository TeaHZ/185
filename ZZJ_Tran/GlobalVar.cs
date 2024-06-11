using BusinessInterface;
using CommonModel;
using DB.Core;
using Log.Core.Model;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using PasS.Base.Lib;
using Soft.Common;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

namespace ZZJ_Tran
{
    internal class GlobalVar
    {
        public static string DoBussiness = GetConfig("DoBussiness");

        public static string callmode = GetConfig("callmode");

        public static string posturl = GetConfig("url");

        public static string parameter = GetConfig("parameters");

        public static string use_encryption = GetConfig("use_encryption");

        public static string MethodName = GetConfig("MethodName");

        public static string Linux = GetConfig("Linux");

        public static string F2FPAY_URL = GetConfig("F2FPAY");

        public static string GetConfig(string configname)
        {
            XmlDocument docini = new XmlDocument();
            docini.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "ZZJ_Tran.dll.config"));
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

        public static InteractiveData F2FPay<T>(string Hos_id, T IndataT, string ServiceName)
        {
            string Key = EncryptionKey.KeyData.AESKEY(Hos_id);
            InteractiveData InData = new InteractiveData();
            InData.ServiceName = ServiceName;
            InData.SetBusinessData(IndataT, Key);

            InData.HOSID = Hos_id;
            InData.BuilderSignature(Key);
            string dd = InData.BuildJson();

            Hashtable hashtable = new Hashtable();
            hashtable.Add("strbuilder", InData.BuildJson());
            XmlDocument doc = WebServiceHelper.QuerySoapWebService(F2FPAY_URL, "F2Fpay", hashtable);
            string strRet = doc.InnerText;
            InteractiveData InDataRe = InteractiveData.FromJson(strRet);
            if (!InDataRe.CheckSignature(Key))
            {
                InDataRe.Msg = "验证服务器返回签名失败";
                InDataRe.Code = -2;
            }
            if (InDataRe.Code < 0)
            {
                return InDataRe;
            }
            return InDataRe;
        }

        public static InteractiveData F2FPay_Linux<T>(string Hos_id, T IndataT, string ServiceName)
        {
            string Key = EncryptionKey.KeyData.AESKEY(Hos_id);
            var http = new HttpClient(F2FPAY_URL + "PBusF2FHelper");
            InteractiveData InData = new InteractiveData();
            InData.ServiceName = ServiceName;
            InData.SetBusinessData(IndataT, Key);

            InData.HOSID = Hos_id;
            InData.BuilderSignature(Key);

            string pherText = AESExample.AESEncrypt(InData.BuildJson(), Key);
            string ss = InData.BuildJson();
            string md5 = AESExample.Encrypt(pherText, Key);
            Root root = new Root();
            root.Param = pherText;
            root.CTag = "测试001";
            root.TID = "3001" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            root.user_id = Hos_id;
            root.sign = md5;
            root.SubBusID = ServiceName;
            string strRet = "";
            string injson =   JsonConvert.SerializeObject(root);
            int status = http.SendJson(injson, Encoding.UTF8, out strRet);
            if (status == 200)
            {
                root = JsonConvert.DeserializeObject<Root>(strRet);
                if (root.ReslutCode == "1")
                {
                    strRet = AESExample.Decrypt(root.Param, Key);
                }
            }
            InteractiveData InDataRe = InteractiveData.FromJson(strRet);
            if (!InDataRe.CheckSignature(Key))
            {
                throw new Exception("验证服务器返回签名失败" + InDataRe.SubCode);
                InDataRe.Code = -2;
            }
            if (InDataRe.Code < 0)
            {
                throw new Exception(InDataRe.Msg + InDataRe.SubCode);
            }
            return InDataRe;
        }

        public static SLBBusinessInfo CallOtherBus(string data, string HOS_ID, string SLB_ID, string subSubId)
        {
            try
            {
                DateTime nowIn = DateTime.Now;
                SLBBusinessInfo OutSLBBOtherBus = new SLBBusinessInfo();

                string key = HOS_ID + "_" + SLB_ID;
                DataTable dtconfig = DictionaryCacheHelper.GetCache(key, () => GetSLBBusinessInfo(SLB_ID, HOS_ID));
                if (dtconfig.Rows.Count == 0)
                {
                    dtconfig = DictionaryCacheHelper.UpdateCache(key, () => GetSLBBusinessInfo(SLB_ID, HOS_ID));
                    if (dtconfig.Rows.Count == 0)
                    {
                        DataReturn dataReturn = new CommonModel.DataReturn();
                        dataReturn.Code = ConstData.CodeDefine.BusError;
                        dataReturn.Msg = "未配置模块对应院端服务";
                        OutSLBBOtherBus.BusData =   JsonConvert.SerializeObject(dataReturn);
                        goto EndPoint;
                    }
                }
                else
                {
                    TimeSpan ts = new TimeSpan();
                    ts = DateTime.Now - DateTime.Parse(dtconfig.Rows[0]["CURRENT_TIMESTAMP"].ToString());
                    if (ts.Minutes > 5)
                    {
                        dtconfig = DictionaryCacheHelper.UpdateCache(key, () => GetSLBBusinessInfo(SLB_ID, HOS_ID));
                    }
                }

                SLBBusinessInfo SLBBOtherBus = new SLBBusinessInfo();
                SLBBOtherBus.BusID = FormatHelper.GetStr(dtconfig.Rows[0]["BUS_ID"]);
                SLBBOtherBus.SubBusID = subSubId;
                SLBBOtherBus.BusData = data;

                bool result = BusServiceAdapter.Ipb_CallOtherBusiness(SLBBOtherBus, out OutSLBBOtherBus);

            EndPoint:
                Log.Core.Model.ModLogAPP modLogAPP = new Log.Core.Model.ModLogAPP();
                modLogAPP.inTime = nowIn;
                modLogAPP.outTime = DateTime.Now;
                modLogAPP.inXml = data;
                modLogAPP.outXml = OutSLBBOtherBus.BusData;
                Log.Core.LogHelper.Addlog(modLogAPP);
                return OutSLBBOtherBus;
            }
            catch (Exception ex)
            {
                Log.Core.Model.ModLogAPPError modLogAPPError = new Log.Core.Model.ModLogAPPError();
                modLogAPPError = new Log.Core.Model.ModLogAPPError();
                modLogAPPError.inTime = DateTime.Now;
                modLogAPPError.outTime = DateTime.Now;
                modLogAPPError.inXml = data;
                modLogAPPError.TYPE = "2020";
                modLogAPPError.outXml = ex.ToString();

                Log.Core.LogHelper.Addlog(modLogAPPError);
                return null;
            }
        }

        public static DataTable GetSLBBusinessInfo(string SLB_ID, string HOS_ID)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select *,CURRENT_TIMESTAMP from baccountSlbToHos where Slb_ID=@SLB_ID and HOS_ID=@HOS_ID");
            MySqlParameter[] parameters =
            {
                    new MySqlParameter("@SLB_ID", MySqlDbType.VarChar,20),
                    new MySqlParameter("@HOS_ID", MySqlDbType.VarChar,20)
                };
            parameters[0].Value = SLB_ID;
            parameters[1].Value = HOS_ID;
            DataTable dtconfig = DbHelperPlatZzjSQL.Query(sb.ToString(), parameters).Tables[0];
            return dtconfig;
        }
    }
}