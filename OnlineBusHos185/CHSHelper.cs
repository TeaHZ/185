using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;

/// <summary>
/// CHSHelper 的摘要说明
/// </summary>
public class CHSHelper
{
    public CHSHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public class InputRoot
    {
        /// <summary>
        /// 医院编号
        /// </summary>
        public string HOS_ID { get; set; }


        /// <summary>
        /// 交易编号
        /// </summary>
        public string infno { get; set; }
        /// <summary>
        /// 发送方报文ID 
        /// 说明：定点医药机构编号(12)+时间(14)+顺序号(4)时间格式：yyyyMMddHHmmss
        /// </summary>
        public string msgid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string insuplc_admdvs { get; set; }

        /// <summary>
        /// 交易输入
        /// </summary>
        public string InData { get; set; }
        /// <summary>
        /// 获取业务内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public T GetInData<T>()
        {
            if (InData == null)
            {
                return default(T);
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(InData);
        }
    }

    public class OutputRoot
    {

        public OutputRoot()
        {
        }

        /// <summary>
        /// 0成功 
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 返回应答描述
        /// </summary>
        public string Msg { get; set; }

        public string chsInput { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string chsOutput { get; set; }

        /// <summary>
        /// 获取业务内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public T GetChsOutput<T>()
        {
            if (chsOutput == null)
            {
                return default(T);
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(chsOutput);
        }

    }

    public static bool Post(InputRoot inputRoot,ref OutputRoot outputRoot, ref string msg) 
    {
        DateTime InTime = DateTime.Now;


        XmlDocument doc = new XmlDocument();
        string path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "bin", "OnlineBusHos185.dll.config");
        doc.Load(path);
        DataSet ds = XMLHelper.X_GetXmlData(doc, "configuration/appSettings");//请求的数据包


        string chsurl = ds.Tables[0].Rows[2]["value"].ToString().Trim();


        string SubBusID = "";
        string BusData = Newtonsoft.Json.JsonConvert.SerializeObject(inputRoot);
        string RtnData = "";
        bool flag= SLBHelper.CallSLBService(chsurl, SubBusID, BusData,out RtnData);
        if (!flag) 
        {
            msg = RtnData;
            return flag;
        }
        outputRoot = Newtonsoft.Json.JsonConvert.DeserializeObject<OutputRoot>(RtnData);
        SaveLog(InTime, BusData, DateTime.Now, RtnData);//保存his接口日志
        return flag;
    }

    /// <summary>
    /// 保存调用HIS的日志
    /// </summary>
    /// <param name="intime"></param>
    /// <param name="inxml"></param>
    /// <param name="outTimem"></param>
    /// <param name="outxml"></param>
    private static void SaveLog(DateTime intime, string inxml, DateTime outTime, string outxml)
    {
        Log.Helper.Model.ModLogHos logHos = new Log.Helper.Model.ModLogHos();
        logHos.inTime = intime;
        logHos.inXml = inxml;
        logHos.outTime = outTime;
        logHos.outXml = outxml;
        Log.Helper.LogHelper.Addlog(logHos);
    }
}