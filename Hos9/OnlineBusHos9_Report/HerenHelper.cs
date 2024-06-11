using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace OnlineBusHos9_Report
{
    public static class HerenHelper
    {
        private static string HerenHIS = ConfigurationManager.AppSettings["HerenHIS"];
        private static string HerenZF = ConfigurationManager.AppSettings["HerenZF"];
        private static string RMLIS = ConfigurationManager.AppSettings["RMLIS"];

        public static PushServiceResult pushService(string TradeCode, string pInput)
        {
            Hashtable hs = new Hashtable
                {
                    { "DataType", "JSON" },
                    { "TradeCode", TradeCode},
                    { "pInput", pInput }
                };

            WriteLog("Heren", TradeCode + "「请求」", pInput);

            string rtnstr = WebServiceHelper.QuerySoapWebService(HerenHIS, "pushService", hs).InnerText;
            //WriteLog("Heren", TradeCode + "「响应」", rtnstr);
            WriteLog("Heren", TradeCode + "「响应」", "成功，报文省略");
            
            PushServiceResult result = JsonConvert.DeserializeObject<PushServiceResult>(rtnstr);

            return result;
        }

        public static QueryServiceResult QueryService(string TradeCode, object Tradebody)
        {
            QueryServiceInput input = new QueryServiceInput
            {
                Head = new QueryServiceInputHead() { TradeCode = TradeCode },
                Body = Tradebody
            };

            string pInput = JsonConvert.SerializeObject(input);

            Hashtable hs = new Hashtable
                {
                    { "DataType", "JSON" },
                    { "pInput", pInput }
                };

            WriteLog("Heren", TradeCode + "「请求」", pInput);

            string rtnstr = WebServiceHelper.QuerySoapWebService(HerenHIS, "queryService", hs).InnerText;

            JObject jrtn = JObject.Parse(rtnstr);

            if (jrtn["Head"]["TradeStatus"].ToString() != "AA")
            {
                QueryServiceResult error = new QueryServiceResult()
                {
                    Head = new QueryServiceResultHead() { TradeStatus = jrtn["Head"]["TradeStatus"].ToString(), TradeMessage = jrtn["Head"]["TradeMessage"].ToString() }
                };

                return error;
            }

            WriteLog("Heren", TradeCode + "「响应」", rtnstr);
            var result = JsonConvert.DeserializeObject<QueryServiceResult>(rtnstr);

            return result;
        }

        public static RMResult LisReportService(string Method, string inprmjson)
        {
            Hashtable hs = new Hashtable
                {
                    { "inprmjson", inprmjson }
                };

            WriteLog("Heren", Method + "「请求」", inprmjson);

            string rtnstr = WebServiceHelper.QuerySoapWebService(RMLIS, Method, hs).InnerText;
            //WriteLog("Heren", Method + "「响应」", rtnstr);
            WriteLog("Heren", Method + "「响应」", "成功，报文省略");
            return JsonConvert.DeserializeObject<RMResult>(rtnstr);
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
    }

    public class QueryServiceInput
    {
        /// <summary>
        ///
        /// </summary>
        public QueryServiceInputHead Head { get; set; }

        /// <summary>
        ///
        /// </summary>
        public object Body { get; set; }
    }

    public class QueryServiceInputHead
    {
        /// <summary>
        ///
        /// </summary>
        public string TradeCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string TradeTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        /// <summary>
        ///
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string BranchCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string HospitalCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string SystemCode { get; set; } = "QHZZJ";

        /// <summary>
        ///
        /// </summary>
        public string HipKeyNo { get; set; } = "e4d303f20f4ade45e0ab76fb5e6f48b6";
    }

    public class QueryServiceResult
    {
        /// <summary>
        ///
        /// </summary>
        public QueryServiceResultHead Head { get; set; }

        /// <summary>
        ///
        /// </summary>
        public List<HRReport> Body { get; set; }
    }

    public class QueryServiceResultHead
    {
        /// <summary>
        ///
        /// </summary>
        public string TradeCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string TradeTime { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string TradeStatus { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        public string TradeMessage { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string BranchCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string HospitalCode { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string SystemCode { get; set; }
    }

    public class HRReport
    {
        /// <summary>
        /// 放射
        /// </summary>
        public string ExamClass { get; set; }

        /// <summary>
        /// 泌尿系直接增强CT+二维/三维重建
        /// </summary>
        public string ExamItemName { get; set; }

        /// <summary>
        /// 陈广
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string UrlPath { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string VisitNo { get; set; }

        public string ReportId { get; set; }
        public string ExamDateTime { get; set; }
        public string ReportDateTime { get; set; }

        /// <summary>
        /// 1 已审核，0未审核
        /// </summary>
        public string Reporttype { get; set; }
        /// <summary>
        /// 1：存在托收费用（需要卡住不给打印报告），0：无托收费用
        /// </summary>
        public string TsStatus { get; set; }
        
    }

    public class RMReport
    {
        /// <summary>
        /// 报告单号
        /// </summary>
        public string ReportID { get; set; }

        /// <summary>
        /// 条码号
        /// </summary>
        public string Barcode { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string PatName { get; set; }

        /// <summary>
        /// 病人号
        /// </summary>
        public string Pat_NO { get; set; }

        /// <summary>
        /// 申请科室
        /// </summary>
        public string Req_Dept { get; set; }

        /// <summary>
        /// 申请医生
        /// </summary>
        public string Req_Doc { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public string Req_Date { get; set; }

        /// <summary>
        /// 申请项目（多个项目，以逗号拼接）
        /// </summary>
        public string Req_Items { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public string Rechk_Dt { get; set; }

        /// <summary>
        /// 审核医生
        /// </summary>
        public string Rechk_User { get; set; }

        /// <summary>
        /// 报告状态（限制打印，已打印，可打印，正在检验）
        /// </summary>
        public string ReportState { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string PDFBase64 { get; set; }
    }

    public class RMResult
    {
        /// <summary>
        /// 结果状态码，1 成功，0 失败
        /// </summary>
        public string ResultCode { get; set; }

        /// <summary>
        /// 结果文本描述
        /// </summary>
        public string ResultMsg { get; set; }

        /// <summary>
        ///
        /// </summary>
        public List<RMReport> Reports { get; set; }
    }


    public class RMReportBack
    {
        /// <summary>
        /// 报告单号
        /// </summary>
        public string ReportID { get; set; }

        /// <summary>
        /// 打印状态（1.已打印）
        /// </summary>
        public string PrintSatate { get; set; }
    }

    public class RMPrintBack
    {
        /// <summary>
        ///
        /// </summary>
        public List<RMReportBack> Reports { get; set; }
    }

    public class PushServiceResult
    {
        public int code { get; set; }

        /// <summary>
        ///
        /// </summary>
        public object data { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        public string msg { get; set; }
    }


    public class medicalreport
    {
        /// <summary>
        ///
        /// </summary>
        public string note { get; set; }

        /// <summary>
        /// 凌左明
        /// </summary>
        public string prport_DOC_NAME { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string REPORT_DATE { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string data_TYPE { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string print_TIME { get; set; }

        /// <summary>
        /// 全科医学科门诊(普内)
        /// </summary>
        public string prport_DEPT_NAME { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string PRINT_TIME { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string report_NAME { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string report_DATE { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string print_FLAG { get; set; }

        /// <summary>
        /// 周玉英
        /// </summary>
        public string pat_NAME { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string report_SN { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string report_TYPE { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string reportdata { get; set; }
    }

    public class HRMedicalRecord
    {
        public string report_PRINT_NUM { get; set; }
        public string report_AUDIT_NUM { get; set; }
        public string report_ALL_NUM { get; set; }
        public string parameters { get; set; }

        public List<medicalreport> medicalreport { get; set; }
    }

    public class HRMedicalResult
    {
        /// <summary>
        /// 
        /// </summary>
        public int data_TYPE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string parameters { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string reportdata { get; set; }
    }

}