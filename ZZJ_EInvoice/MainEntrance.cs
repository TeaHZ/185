using BusinessInterface;
using CommonModel;
using Newtonsoft.Json;
using PasS.Base.Lib;
using System;
using System.IO;

namespace ZZJ_EInvoice
{
    public class ZZJ_EInvoiceAPI : ProcessingBusinessAsyncResultByte
    {
        public override bool ProcessingBusiness(int CCN, SLBInfoHeadBusS sLBInfoHeadBusS, byte[] In, out byte[] Out)
        {
            DateTime inTime = DateTime.Now;
            string OutBusinessInfo = "";
            string InBusinessInfo = ""; string BUSID = CCN.ToString(); string SUB_BUSNAME = "";
            try
            {
                InBusinessInfo = base.GetStrData(In);
                switch (CCN.ToString().Substring(CCN.ToString().Length - 4))
                {
                    case "0001"://获取电子票据列表
                        SUB_BUSNAME = "GetHisIssueBySfzno";
                        OutBusinessInfo = BUS.GetHisIssueBySfzno_B.B_GetHisIssueBySfzno(InBusinessInfo);
                        break;

                    case "0002":// 电子票据文件下载
                        SUB_BUSNAME = "EInvoiceDownload";
                        OutBusinessInfo = BUS.ZZJ_EInvoiceDownload_B.B_EInvoiceDownload(InBusinessInfo);
                        break;

                    case "0003"://电子票据打印回传
                        SUB_BUSNAME = "UpdatePrintStatus";
                        OutBusinessInfo = BUS.UpdatePrintStatus_B.B_UpdatePrintStatus(InBusinessInfo);
                        break;

                    case "0004"://电子票据打印回传
                        SUB_BUSNAME = "EinvoiceIssue";
                        OutBusinessInfo = BUS.EInvoiceIssue_B.B_EInvoiceIssue(InBusinessInfo);
                        break;

                    default:
                        DataReturn dataReturn = new DataReturn();
                        dataReturn.Code = 1;
                        dataReturn.Msg = "未匹配到此业务类型";
                        OutBusinessInfo =   JsonConvert.SerializeObject(dataReturn);
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonModel.DataReturn dataReturn = new CommonModel.DataReturn();
                dataReturn.Code = ConstData.CodeDefine.BusError;
                dataReturn.Msg = ex.Message;
                OutBusinessInfo =   JsonConvert.SerializeObject(dataReturn);
            }
            finally
            {
                Log.Core.Model.ModLogQHZZJ logzzj = new Log.Core.Model.ModLogQHZZJ();
                logzzj.BUS = BUSID;
                logzzj.BUS_NAME = "ZZJ_EInvoice";
                logzzj.SUB_BUSNAME = SUB_BUSNAME;
                logzzj.InTime = inTime;
                logzzj.InData = InBusinessInfo;
                logzzj.OutTime = DateTime.Now;
                logzzj.OutData = OutBusinessInfo;
                new Log.Core.MySQLDAL.DalLogQHZZJ().Add(logzzj);
            }
            //WriteLog("ZZJ_EInvoiceAPI", "outData", OutBusinessInfo);
            Out = base.GetByte(OutBusinessInfo);
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