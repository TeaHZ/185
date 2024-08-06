using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Soft.Common;
using System.Data;
using System.Text;

namespace OnlineBusHos185
{
    public class SAVEHOSPRE
    {
        public static bool SLBSendToPlatform(string json, string RequestURL)
        {
            try
            {
                string user_id = "JSQH";
                string key = "8478CEFB711D4C00294CBD9BD76D4DFD";

                string pherText = AESExampleOld.AESEncrypt(json, key);
                string md5 = Md5Helper.Encode(pherText, key);
                //AESExampleOld.Encode(pherText, key);
                string result = "";
                SLBRoot slbroot = new SLBRoot();
                slbroot.Param = pherText;
                //root.TID = "";
                //root.CTag = "";
                slbroot.user_id = user_id;
                slbroot.sign = md5;
                string in_data = JsonHelper.SerializeObject(slbroot);
                string out_data = "";
                var http = new HttpClient(RequestURL);
                int status = http.SendJson(in_data, Encoding.UTF8, out out_data);
                if (status == 200)
                {
                    slbroot = JsonHelper.DeserializeJsonToObject<SLBRoot>(out_data);
                    if (slbroot.ReslutCode == "1")
                    {
                        result = AESExampleOld.DecryptByKEY(FormatHelper.GetStr(slbroot.Param), key);
                    }
                    else
                    {
                        return false;
                    }
                }
                JObject jobject = (JObject)JsonConvert.DeserializeObject(result);
                string CLBZ = FormatHelper.GetStr(jobject["ROOT"]["BODY"]["CLBZ"]);
                if (CLBZ == "0")
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
    public class HEADER
    {
        /// <summary>
        /// 
        /// </summary>
        public string MODULE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CZLX { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string source { get; set; }

    }



    public class BODY
    {
        /// <summary>
        /// 医院ID
        /// </summary>
        public string HOS_ID { get; set; }

        /// <summary>
        /// 模块ID
        /// </summary>
        public string MODULE_ID { get; set; }
    }



    public class ROOT
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

    public class Root
    {
        /// <summary>
        /// 
        /// </summary>
        public ROOT ROOT { get; set; }

    }

    public class SendYunBody
    {
        public string HOS_ID { get; set; }

        public string InXml { get; set; }
    }

    public class SAVEHOSPREINFOBODY
    {
        public string HOS_ID { get; set; }
        public string DOC_NO { get; set; }
        public string HOS_SN { get; set; }
        public string PAT_NAME { get; set; }
        public string DIS_ID { get; set; }
        public string DIS_NAME { get; set; }
        public string BQ { get; set; }

        public string PRE_NO { get; set; }
        public string OPT_SN { get; set; }
        public DAMEDLIST DAMEDLIST { get; set; }
        public SQD_LIST SQD_LIST { get; set; }
    }
    public class OFFLINEPREUNDO
    {
        public string HOS_SN { get; set; }
        public string PAT_NAME { get; set; }
        public string HOS_ID { get; set; }
        public string PRE_NO { get; set; }
    }

    public class SAVEPRECHECKRESULT
    {
        public string HOS_ID { get; set; }
        public string SF_TYPE { get; set; }
        public string HOS_SN { get; set; }
        public string SF_MAN_ID { get; set; }
        public string SF_MAN_NAME { get; set; }
        public string SF_SFZ_NO { get; set; }
        public string SF_TIME { get; set; }
        public string SF_RESULT { get; set; }
    }

    public class DAMEDLIST
    {

        public decimal MED_JEALL { get; set; }
        public DataTable DAMED { get; set; }
    }
    public class SQD_LIST
    {
        public DataTable SQD { get; set; }
    }

    class SLBRoot
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

        public string ReslutCode { get; set; }

        public string ResultMessage { get; set; }

    }
}
