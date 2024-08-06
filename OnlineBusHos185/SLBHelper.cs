using Newtonsoft.Json;
using OnlineBusHos185;
using Soft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// SLBHelper 的摘要说明
/// </summary>
public class SLBHelper
{
    public static bool CallSLBService(string slbURL, string SubBusID, string BusData, out string RtnData)
    {
        try
        {

            string user_id = "JSQH";
            string key = "8478CEFB711D4C00294CBD9BD76D4DFD";

            string pherText = AESExample.Encrypt(BusData, key);
            string md5 = CommonFunction.Encode(pherText, key);
                //AESExample.Encode(pherText, key);
            Request request = new Request();
            request.Param = pherText;
            request.user_id = user_id;
            request.sign = md5;
            request.SubBusID = SubBusID;
            var http = new HttpClient(slbURL);
            string out_data = "";
            int status = http.SendJson(JsonConvert.SerializeObject(request), Encoding.UTF8, out out_data);
            if (status == 200)
            {
                Response response = JsonConvert.DeserializeObject<Response>(out_data);
                if (response.ReslutCode != "1")
                {
                    RtnData = response.ResultMessage;
                    return false;
                }
                else
                {
                    RtnData = AESExample.Decrypt(response.Param, key);
                    return true;
                }
            }
            else
            {
                RtnData = "调用SLB服务异常,status:" + status.ToString();
                return false;
            }
        }
        catch (Exception ex)
        {
            RtnData = "处理过程出错：" + ex.Message;
            return false;
        }
    }
 

    private class Request
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

    private class Response
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