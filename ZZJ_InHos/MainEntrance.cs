using BusinessInterface;
using CommonModel;
using Newtonsoft.Json;
using PasS.Base.Lib;
using System;
using System.IO;

namespace ZZJ_InHos
{
    /// <summary>
    /// 测试两数相加
    /// </summary>
    public class ZZJ_InHosAPI : ProcessingBusinessAsyncResultByte
    {
        public override bool ProcessingBusiness(int CCN, SLBInfoHeadBusS sLBInfoHeadBusS, byte[] In, out byte[] Out)
        {
            DateTime inTime = DateTime.Now;
            string OutBusinessInfo = "";
            string InBusinessInfo = ""; string BUSID = CCN.ToString(); string SUB_BUSNAME = "";
            try
            {
                //WriteLog("ZZJ_InHosHead", "ReceiveData",   JsonConvert.SerializeObject(sLBInfoHeadBusS));
                InBusinessInfo = base.GetStrData(In);
                InBusinessInfo = System.Web.HttpUtility.UrlDecode(InBusinessInfo);
                //WriteLog("ZZJ_InHosAPI", "ReceiveData", InBusinessInfo);
                switch (CCN.ToString().Substring(CCN.ToString().Length - 4))
                {
                    case "0001"://获取病人住院号
                        SUB_BUSNAME = "GETPATHOSNO";
                        OutBusinessInfo = BUS.GETPATHOSNO.B_GETPATHOSNO(InBusinessInfo);
                        break;

                    case "0002":// 通过住院获取病人信息
                        SUB_BUSNAME = "GETPATHOSINFO";
                        OutBusinessInfo = BUS.GETPATHOSINFO.B_GETHOSPATINFO(InBusinessInfo);
                        break;

                    case "0003":// 通过住院号获取病人基本信息
                        SUB_BUSNAME = "GETPATINFBYHOSNO";
                        OutBusinessInfo = BUS.GETPATINFBYHOSNO.B_GETPATINFBYHOSNO(InBusinessInfo);
                        break;

                    case "0004"://缴纳预交金
                        SUB_BUSNAME = "SAVEINPATYJJ";
                        OutBusinessInfo = BUS.SAVEINPATYJJ.B_SAVEINPATYJJ(InBusinessInfo);
                        break;

                    case "0005"://获取住院病人每日清单
                        SUB_BUSNAME = "GETHOSDAILY";
                        OutBusinessInfo = BUS.GETHOSDAILY.B_GETHOSDAILY(InBusinessInfo);
                        break;

                    case "0006"://判断病人是否可以出院
                        SUB_BUSNAME = "CANPATOUTJS";
                        OutBusinessInfo = BUS.CANPATOUTJS.B_CANPATOUTJS(InBusinessInfo);
                        break;

                    case "0007"://出院预结算
                        SUB_BUSNAME = "JZHOUTYJS";
                        OutBusinessInfo = BUS.JZHOUTYJS.B_JZHOUTYJS(InBusinessInfo);
                        break;

                    case "0008"://出院结算
                        SUB_BUSNAME = "JZHOUTJS";
                        OutBusinessInfo = BUS.JZHOUTJS.B_JZHOUTJS(InBusinessInfo);
                        break;

                    case "0009"://获取病人住院登记标识
                        SUB_BUSNAME = "GETPATZYDJSTATE";
                        OutBusinessInfo = BUS.GETPATZYDJSTATE.B_GETPATZYDJSTATE(InBusinessInfo);
                        break;

                    case "0010"://获取病人住院登记数据
                        SUB_BUSNAME = "GETPATZYDJDATA";
                        OutBusinessInfo = BUS.GETPATZYDJDATA.B_GETPATZYDJDATA(InBusinessInfo);
                        break;

                    case "0011"://住院登记保存
                        SUB_BUSNAME = "ZYDJSAVE";
                        OutBusinessInfo = BUS.ZYDJSAVE.B_ZYDJSAVE(InBusinessInfo);
                        break;
                    case "0012"://腕带打印回传
                        SUB_BUSNAME = "WDPRINT";
                        OutBusinessInfo = BUS.WDPRINT.B_WDPRINT(InBusinessInfo);
                        break;
                    case "0016"://获取自助机住院结算清单
                        SUB_BUSNAME = "GETPATZYQD";
                        OutBusinessInfo = BUS.GETPATZYQD.B_GETPATZYQD(InBusinessInfo);
                        break;
                    case "0017"://获取自助机医保跨省异地患者住院结算单
                        SUB_BUSNAME = "GETPATYDJSD";
                        OutBusinessInfo = BUS.GETPATYDJSD.B_GETPATYDJSD(InBusinessInfo);
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
                logzzj.BUS_NAME = "ZZJ_InHos";
                logzzj.SUB_BUSNAME = SUB_BUSNAME;
                logzzj.InTime = inTime;
                logzzj.InData = InBusinessInfo;
                logzzj.OutTime = DateTime.Now;
                logzzj.OutData = OutBusinessInfo;
                new Log.Core.MySQLDAL.DalLogQHZZJ().Add(logzzj);
            }
            //WriteLog("ZZJ_InHosAPI", "outData", OutBusinessInfo);
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