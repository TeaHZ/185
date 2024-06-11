using System;
using System.IO;
using BusinessInterface;
using CommonModel;
using Newtonsoft.Json;
using PasS.Base.Lib;
namespace ZZJ_Report
{
    internal class ZZJ_ReportAPI : ProcessingBusinessAsyncResultByte
    {
        public override bool ProcessingBusiness(int CCN, SLBInfoHeadBusS sLBInfoHeadBusS, byte[] In, out byte[] Out)
        {
            DateTime inTime = DateTime.Now;
            string OutBusinessInfo = "";
            string InBusinessInfo = ""; string BUSID = CCN.ToString(); string SUB_BUSNAME = "";
            try
            {
                InBusinessInfo = base.GetStrData(In);
                InBusinessInfo = System.Web.HttpUtility.UrlDecode(InBusinessInfo);
                switch (CCN.ToString().Substring(CCN.ToString().Length - 4))
                {
                    case "0001"://获取患者检验报告列表
                        SUB_BUSNAME = "GETLISREPORT";
                        OutBusinessInfo = BUS.GETLISREPORT.B_GETLISREPORT(InBusinessInfo);
                        break;

                    case "0002":// 查询检验报告明细
                        SUB_BUSNAME = "GETLISRESULT";
                        OutBusinessInfo = BUS.GETLISRESULT.B_GETLISRESULT(InBusinessInfo);
                        break;

                    case "0003":// 检查报告打印回传
                        SUB_BUSNAME = "ZZJLISPRNBACK";
                        OutBusinessInfo = BUS.ZZJLISPRNBACK.B_ZZJLISPRNBACK(InBusinessInfo);
                        break;

                    case "0004"://获取患者检查报告列表
                        SUB_BUSNAME = "GETRISREPORT";
                        OutBusinessInfo = BUS.GETRISREPORT.B_GETRISREPORT(InBusinessInfo);
                        break;

                    case "0005":// 查询检查报告明细
                        SUB_BUSNAME = "GETRISRESULT";
                        OutBusinessInfo = BUS.GETRISRESULT.B_GETRISRESULT(InBusinessInfo);
                        break;

                    case "0006":// 检查报告打印回传
                        SUB_BUSNAME = "ZZJRISPRNBACK";
                        OutBusinessInfo = BUS.ZZJRISPRNBACK.B_ZZJRISPRNBACK(InBusinessInfo);
                        break;

                    case "0007"://获取患者检查报告列表
                        SUB_BUSNAME = "GETPATHOLOGYREPORT";
                        OutBusinessInfo = BUS.GETPATHOLOGYREPORT.B_GETPATHOLOGYREPORT(InBusinessInfo);
                        break;

                    case "0008":// 查询病理报告明细
                        SUB_BUSNAME = "GETPATHOLOGYRESULT";
                        OutBusinessInfo = BUS.GETPATHOLOGYRESULT.B_GETPATHOLOGYRESULT(InBusinessInfo);
                        break;

                    case "0009":// 病理报告打印回传
                        SUB_BUSNAME = "ZZJPATHOLOGYPRNBACK";
                        OutBusinessInfo = BUS.ZZJPATHOLOGYPRNBACK.B_ZZJPATHOLOGYPRNBACK(InBusinessInfo);
                        break;

                    case "0010":// 病历列表
                        SUB_BUSNAME = "GETMEDICALRECORD";
                        OutBusinessInfo = BUS.GETMEDICALRECORD.B_GETMEDICALRECORD(InBusinessInfo);
                        break;

                    case "0011":// 病历明细
                        SUB_BUSNAME = "GETMEDICALRESULT";

                        OutBusinessInfo = BUS.GETMEDICALRECORD.B_GETMEDICALRESULT(InBusinessInfo);
                        break;

                    case "0012":// 病历列表打印回传
                        SUB_BUSNAME = "ZZJMEDICALPRNBACK";

                        OutBusinessInfo = BUS.GETMEDICALRECORD.B_ZZJMEDICALPRNBACK(InBusinessInfo);
                        break;

                    case "0013":// 医学证明列表
                        SUB_BUSNAME = "GETMEDCERTIFICATE";
                        OutBusinessInfo = BUS.GETMEDCERTIFICATE.B_GETMEDCERTIFICATE(InBusinessInfo);
                        break;
                    case "0014":// 医学证明明细
                        SUB_BUSNAME = "GETMEDCERTIFICATEDATA";
                        OutBusinessInfo = BUS.GETMEDCERTIFICATE.B_GETMEDCERTIFICATEDATA(InBusinessInfo);
                        break;
                    case "0015":// 医学证明回传 his没这个接口
                        SUB_BUSNAME = "ZZJMEDICALEVIDENCEPRNBACK";
                        OutBusinessInfo = BUS.GETMEDCERTIFICATE.B_ZZJMEDICALEVIDENCEPRNBACK(InBusinessInfo);
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
                logzzj.BUS_NAME = "ZZJ_Report";
                logzzj.SUB_BUSNAME = SUB_BUSNAME;
                logzzj.InTime = inTime;
                logzzj.InData = InBusinessInfo;
                logzzj.OutTime = DateTime.Now;
                logzzj.OutData = OutBusinessInfo;
                new Log.Core.MySQLDAL.DalLogQHZZJ().Add(logzzj);
            }
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