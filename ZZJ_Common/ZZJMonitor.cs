using CommonModel;
using Newtonsoft.Json;
using Soft.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;

namespace ZZJ_Common
{
    public class ZZJMonitor
    {
        //private static string user_id = "JSQH";
        //private static string key = "8478CEFB711D4C00294CBD9BD76D4DFD";
        private static string user_id = "JSQH";
        private static string key = "8478CEFB711D4C00294CBD9BD76D4DFD";

        public static string GetQueryUrl
        {
            get
            {
                XmlDocument doc = new XmlDocument();
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "bin", "MonitorUrl.config");

                doc.Load(path);
                DataSet ds = XMLHelper.X_GetXmlData(doc, "configuration/appSettings");//请求的数据包


                string url = ds.Tables[0].Select("key='MonitorUrl'")[0]["Value"].ToString();//GetHisServerUrlConfig(HOS_ID);//获取服务地址

                return url;
            }

        }

        public static string Invoke(string json_in)
        {
            try
            {
                //ZZJ_CommonAPI.WriteLog("info", "ZZJMonitor", "SendData*****" + json_in);
                //var out_data = GlobalVar.CallOtherBus(json_in, "QHZZJMONITORAPI", "").BusData;
                var out_data = "";
                string pherText = AESExample.AESEncrypt(json_in, key);

                string md5 = AESExample.Encode(pherText, key);

                Root root = new Root();
                root.Param = pherText;

                root.user_id = user_id;
                root.sign = md5;
                root.SubBusID = "RECEIVENEWDATA";

                //GlobalVar.CallOtherBus(Newtonsoft.Json.JsonConvert.SerializeObject(root))

                var http = new HttpClient(GetQueryUrl + "/SLB/MySpringAES/QHZZJMONITORAPI");
                //var http = new HttpClient(GetQueryUrl + "/SLB/MySpringAES/QHMONITORAPI");
                int status = http.SendJson(JsonConvert.SerializeObject(root), Encoding.UTF8, out out_data);

                //ZZJ_CommonAPI.WriteLog("info", "ZZJMonitor", "ReceiveData*****" + out_data);
                return out_data;


            }
            catch (Exception ex)
            {

                //ZZJ_CommonAPI.WriteLog("Error", "ZZJMonitor", "Error*****" + ex.ToString());
                return ex.ToString();
            }
        }
        class Root
        {
            /// <summary>
            /// 
            /// </summary>
            public string Param { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string user_id { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string sign { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string TID { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string CTag { get; set; }

            /// <summary>
            /// 子模块ID
            /// </summary>
            public string SubBusID { get; set; }



        }
    }
}