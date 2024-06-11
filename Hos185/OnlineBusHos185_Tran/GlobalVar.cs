using Hos185_His.Models;
using Hos185_His;
using Log.Core.Model;
using Newtonsoft.Json;

using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;

namespace OnlineBusHos185_Tran
{
    class GlobalVar
    {

        public static string DoBussiness = ""; // GetConfig("DoBussiness");

        public static string callmode = ""; // GetConfig("callmode");

        public static string posturl = ""; // GetConfig("url");

        public static string parameter = ""; // GetConfig("parameters");

        public static string use_encryption = ""; // GetConfig("use_encryption");

        public static string MethodName = ""; // GetConfig("MethodName");

        public static string Linux = ""; // GetConfig("Linux");

        public static string F2FPAY_URL = ""; // GetConfig("F2FPAY");


        public static string GetConfig(string configname)
        {
            string key = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace + "_" + configname;
            Config config = null;
            try
            {
                config = DictionaryCacheHelper.GetCache(key, () => GetConfigClass(configname));
            }
            catch
            {
                config = DictionaryCacheHelper.UpdateCache(key, () => GetConfigClass(configname));
            }
            if (config == null)
            {
                return "";
            }
            else
            {
                TimeSpan ts = new TimeSpan();
                ts = DateTime.Now - config.Time;
                if (ts.Minutes >= 5)
                {
                    config = DictionaryCacheHelper.UpdateCache(key, () => GetConfigClass(configname));
                }
            }
            return config.Value;
        }

        public static Config GetConfigClass(string configname)
        {
            XmlDocument docini = new XmlDocument();
            docini.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace + ".dll.config"));
            DataSet ds = XMLHelper.X_GetXmlData(docini, "configuration/appSettings");//请求的数据包
            DataRow[] dr = ds.Tables[0].Select("key='" + configname + "'");
            if (dr.Length > 0)
            {
                Config C = new Config();
                C.Key = configname;
                C.Value = dr[0]["value"].ToString();
                C.Time = DateTime.Now;
                return C;
            }
            else
            {
                return null;
            }
        }



        public static void Init()
        {
            DoBussiness = GetConfig("DoBussiness");
            callmode = GetConfig("callmode");
            posturl = GetConfig("url");
            parameter = GetConfig("parameters");
            use_encryption = GetConfig("use_encryption");
            MethodName = GetConfig("MethodName");
            Linux = GetConfig("Linux");
            F2FPAY_URL = GetConfig("F2FPAY");
        }

        public static Output<T> CallAPI<T>(string routepath, string inputjson)
        {
            TKHelper main = new TKHelper();
            Hos185_His.Models.Output<T>
                output = main.CallServiceAPI<T>(routepath, inputjson);


            return output;


        }

    }

}
