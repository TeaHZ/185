using System;
using System.IO;
using System.Net;
using System.Text;

namespace OnlineBusHos185_Common.EHealthCard
{
    public class EHealthCardBus
    {
        /// <summary>
        /// 获取电子健康卡信息
        /// </summary>
        /// <param name="qrcode">二维码信息</param>
        /// <returns></returns>
        public static EHealthCard.qrCodeAnalysisResponse GetHealthCardInfo(string qrcode)
        {
            DateTime InTime = DateTime.Now;
            //计算时间戳，是指格林威治时间1970年01月01日00时00分00秒(北京时间1970年01月01日08时00分00秒)起至现在的总秒数，能够唯一地标识某一刻的时间
            long iTmp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            //固定值
            string appKey = "07c59ecff78e4273b16d10564da717da";
            string pack = "cn.ac.sec.doctor";

            string[] arr = { appKey, pack, "" + iTmp };
            System.Array.Sort(arr);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (String param in arr)
            {
                sb.Append(param);
            }

            string cl = sb.ToString();

            //MD5转码
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] s = md5.ComputeHash(System.Text.Encoding.ASCII.GetBytes(cl));
            string pwd = "";
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("X2");
            }

            //qrcode = "B6B5BD9DF3714114894865A58863235697FE66A2DEC14E96A8454CA642AD8349:1";
            //qrcode = "BFFD6474929A473EA034FE70E15696E3F1F920FFAD274CBB82CDE8C5BB6AFE9E:1";//潘路强
            //qrcode = "6D65E91EAFE8444AAB213A2F53030E878B83345C16D94E50ABAABF1090B53DDA:1";//杜邦

            EHealthCard.QrCodeAnalysisRequest data = new EHealthCard.QrCodeAnalysisRequest();
            data.appSign = pwd;
            data.timestamp = iTmp;
            data.pack = "cn.ac.sec.doctor";
            data.qrCode = qrcode;
            //string RequestPara = JsonConvert.SerializeObject(data);
            string RequestPara = string.Format("appSign={0}&timestamp={1}&pack={2}&qrCode={3}", pwd, iTmp, "cn.ac.sec.doctor", qrcode);
            /*
    {
    "data":{
        "virtualCardNum":"5420120170600000002",
        "realname":"李明",
        "idNumber":"110108188305065433",
        "cellphone":"15833064659",
        "gender":1,
        "pin":1234,
        "addr":"无锡?XX 区 XX 街道"
    },
    "code":0,
    "msg":""
    }
             */
            // 创建WebRequest调用对象
            string Url = System.Configuration.ConfigurationManager.AppSettings["EHealthCard_URL"] + "/hospital/v2/user/qrCodeAnalysis";

            WebRequest request = HttpWebRequest.Create(Url);
            // 数据编码为键值对
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            //request.Timeout = 800;//请求超时时间
            // 将调用数据转换为字节数组
            byte[] buf = Encoding.GetEncoding("UTF-8").GetBytes(RequestPara);
            // 设置HTTP头，提交的数据长度
            request.ContentLength = buf.Length;
            // 写入参数内容
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(buf, 0, buf.Length);
                requestStream.Close();

            }
            // 调用返回内容
            string ReturnVal = "";
            // 发起调用操作
            WebResponse response = request.GetResponse();
            // 响应数据流
            Stream stream = response.GetResponseStream();
            // 以UTF-8编码转换为StreamReader
            using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("UTF-8")))
            {
                ReturnVal = reader.ReadToEnd();
            }
            DateTime OutTime = DateTime.Now;
            //Log.Helper.LogHelper.SaveLogZZJ("qrCodeAnalysis", InTime, RequestPara, OutTime, ReturnVal);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<EHealthCard.qrCodeAnalysisResponse>(ReturnVal);
        }
    }
}
