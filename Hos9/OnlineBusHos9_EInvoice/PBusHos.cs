using BusinessInterface;
using Newtonsoft.Json;
using PasS.Base.Lib;

using System;
using System.IO;

namespace OnlineBusHos9_EInvoice
{
    /// <summary>
    /// 测试两数相加
    /// </summary>
    public class PBusHos9_EInvoice : ProcessingBusinessAsyncResult
    {
        public override bool ProcessingBusiness(SLBBusinessInfo InBusinessInfo, out SLBBusinessInfo OutBusinessInfo)
        {
            OutBusinessInfo = new SLBBusinessInfo(InBusinessInfo);
            try
            {
                string name = InBusinessInfo.SubBusID;
                switch (name)
                {
                    case "0001"://获取电子票据列表
                        OutBusinessInfo.BusData = BUS.GetHisIssueBySfzno.B_GetHisIssueBySfzno(InBusinessInfo.BusData);
                        break;

                    case "0002":// 电子票据文件下载
                        OutBusinessInfo.BusData = BUS.EInvoiceDownload.B_EInvoiceDownload(InBusinessInfo.BusData);
                        break;

                    case "0003"://电子票据打印回传
                        OutBusinessInfo.BusData = BUS.UpdatePrintStatus.B_UpdatePrintStatus(InBusinessInfo.BusData);
                        break;

                    case "0004"://电子票据开立
                        OutBusinessInfo.BusData = BUS.EinvocieIssue.B_EinvocieIssue(InBusinessInfo.BusData);
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonModel.DataReturn dataReturn = new CommonModel.DataReturn();
                dataReturn.Code = ConstData.CodeDefine.BusError;
                dataReturn.Msg = ex.Message;
                OutBusinessInfo.BusData = JsonConvert.SerializeObject(dataReturn);
            }
            //OutBusinessInfo = System.Web.HttpUtility.UrlEncode(OutBusinessInfo);
            return true;
        }

        public override byte[] DefErrotReturn(int Code, string ErrorMsage)
        {
            CommonModel.DataReturn dataReturn = new CommonModel.DataReturn();
            dataReturn.Code = Code;
            dataReturn.Msg = ErrorMsage;
            return base.GetByte(dataReturn);
        }

        protected static void WriteLog(string type, string className, string content)
        {
            string path = "";
            try
            {
                // path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\MySpringlog";
                path = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "PasSLog", "ZzjLog");
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
                string filename = path + "/" + DateTime.Now.ToString("yyyyMMdd") + type.Replace('|', ':') + ".log";//用日期对日志文件命名
                //创建或打开日志文件，向日志文件末尾追加记录
                StreamWriter mySw = File.AppendText(filename);

                //向日志文件写入内容
                string write_content = className + ": " + content;
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
}