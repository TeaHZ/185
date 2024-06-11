using BusinessInterface;
using Newtonsoft.Json;
using PasS.Base.Lib;
using System;
using System.Collections.Generic;
using System.IO;

namespace ZZJ_Common
{
    /// <summary>
    /// 测试两数相加
    /// </summary>
    public class ZZJ_CommonAPI : ProcessingBusinessAsyncResultByte
    {
        public override bool ProcessingBusiness(int CCN, SLBInfoHeadBusS sLBInfoHeadBusS, byte[] In, out byte[] Out)
        {
            DateTime inTime = DateTime.Now;
            string OutBusinessInfo = "";
            string InBusinessInfo = "";
            string BUSID = CCN.ToString();
            string SUB_BUSNAME = "";

            InBusinessInfo = base.GetStrData(In);
   
            try
            {
                //WriteLog("ZZJ_CommonHead", "ReceiveData",   JsonConvert.SerializeObject(sLBInfoHeadBusS));
                InBusinessInfo = base.GetStrData(In);
                //InBusinessInfo = System.Web.HttpUtility.UrlDecode(InBusinessInfo);
                //WriteLog("ZZJ_CommonAPI", "ReceiveData", InBusinessInfo);
                switch (CCN.ToString().Substring(CCN.ToString().Length - 4))
                {
                    case "0001"://获取病人基础信息
                        SUB_BUSNAME = "GETPATINFO";
                        OutBusinessInfo = BUS.GETPATINFO.B_GETPATINFO(InBusinessInfo);
                        break;

                    case "0002"://保存病人建档信息
                        SUB_BUSNAME = "GETPATRECORD";
                        OutBusinessInfo = BUS.GETPATRECORD.B_GETPATRECORD(InBusinessInfo);
                        break;

                    case "0005"://凭条打印
                        SUB_BUSNAME = "TICKETREPRINT";
                        OutBusinessInfo = BUS.TICKETREPRINT.B_TICKETREPRINT(InBusinessInfo);
                        break;

                    case "0006"://凭条打印
                        SUB_BUSNAME = "GETGOODSLIST";
                        OutBusinessInfo = BUS.GETGOODSLIST.B_GETGOODSLIST(InBusinessInfo);
                        break;
                    case "0009"://自助机传过来的大屏实时列表
                        SUB_BUSNAME = "ZZJMonitor";
                        OutBusinessInfo = ZZJMonitor.Invoke(InBusinessInfo);
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
                logzzj.BUS_NAME = "ZZJ_Common";
                logzzj.SUB_BUSNAME = SUB_BUSNAME;
                logzzj.InTime = inTime;
                logzzj.InData = InBusinessInfo;
                logzzj.OutTime = DateTime.Now;
                logzzj.OutData = OutBusinessInfo;
                new Log.Core.MySQLDAL.DalLogQHZZJ().Add(logzzj);
            }
            //WriteLog("ZZJ_CommonAPI", "outData", OutBusinessInfo);
            //OutBusinessInfo = System.Web.HttpUtility.UrlEncode(OutBusinessInfo);
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