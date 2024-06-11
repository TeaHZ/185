using Newtonsoft.Json;
using Soft.Common;
using System;
using System.IO;
using System.Text;

namespace OnlineBusHos9_Tran
{
    public class SLBHelper
    {

        string baseurl = "http://192.168.17.211:9002/test/dy/SLB/MySpringAES";

        public bool CallSLB( string BusID, string SubBusID, string BusData, out string RtnData)
        {
            try
            {

                WriteLog("slb", SubBusID + "「请求」", BusData);

                string user_id = "zWnCRHiZfa";
                string key = "C7D0B26C80671A15ACB0BBB7B9D65B09";

                string pherText = AESExample.AESEncrypt(BusData, key);
                string md5 = AESExample.Encode(pherText, key);

                Request root = new Request();
                root.Param = pherText;
                root.user_id = user_id;
                root.sign = md5;
                root.SubBusID = SubBusID;
                var http = new QhHttpClient(baseurl + "/" + BusID);
                string out_data = "";
                int status = http.SendJson(JsonConvert.SerializeObject(root), Encoding.UTF8, out out_data);

                WriteLog("slb", SubBusID + "「响应」", out_data);

                if (status == 200)
                {
                    Response root_out = JsonConvert.DeserializeObject<Response>(out_data);
                    if (root_out.ReslutCode != "1")
                    {
                        RtnData = root_out.ResultMessage;
                        return false;
                    }
                    else
                    {
                        RtnData = AESExample.Decrypt(root_out.Param, key);
                        return true;
                    }
                }
                else
                {
                    RtnData = "调用SLB服务异常,status:" + status.ToString() + "  result:" + out_data;
                    return false;
                }
            }
            catch (Exception ex)
            {
                RtnData = "处理过程出错：" + ex.Message;
                return false;
            }
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

        public class Request
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
            /// 
            /// </summary>
            public string SubBusID { get; set; }

        }

        public class Response
        {
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
            public string ReslutCode { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string ResultMessage { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string Param { get; set; }

        }
    }


}
