using BusinessInterface;
using CommonModel;
using Newtonsoft.Json;
using PasS.Base.Lib;
using System;
using System.Collections.Generic;
using System.IO;

namespace ZZJ_YYGH
{
    internal class ZZJ_YYGHAPI : ProcessingBusinessAsyncResultByte
    {
        public override bool ProcessingBusiness(int CCN, SLBInfoHeadBusS sLBInfoHeadBusS, byte[] In, out byte[] Out)
        {
            DateTime inTime = DateTime.Now;
            string OutBusinessInfo = "";
            string InBusinessInfo = ""; string BUSID = CCN.ToString(); string SUB_BUSNAME = "";

            InBusinessInfo = base.GetStrData(In);
        
            try
            {
                InBusinessInfo = base.GetStrData(In);
                switch (CCN.ToString().Substring(CCN.ToString().Length - 4))
                {
                    case "0001"://获取医院科室信息列表
                        SUB_BUSNAME = "GETHOSPDEPT";
                        OutBusinessInfo = BUS.GETHOSPDEPT.B_GETHOSPDEPT(InBusinessInfo);
                        break;

                    case "0002"://获取医生科室排班数据
                        SUB_BUSNAME = "GETSCHINFO";
                        OutBusinessInfo = BUS.GETSCHINFO.B_GETSCHINFO(InBusinessInfo);
                        break;

                    case "0003"://预约挂号保存
                        SUB_BUSNAME = "REGISTERAPPTSAVE";
                        OutBusinessInfo = BUS.REGISTERAPPTSAVE.B_REGISTERAPPTSAVE(InBusinessInfo);
                        break;

                    case "0004"://预约(实时)挂号支付
                        SUB_BUSNAME = "REGISTERPAYSAVE";
                        OutBusinessInfo = BUS.REGISTERPAYSAVE.B_REGISTERPAYSAVE(InBusinessInfo);
                        break;

                    case "0005"://预约取消(含支付)
                        SUB_BUSNAME = "REGISTERPAYCANCEL";
                        OutBusinessInfo = BUS.REGISTERPAYCANCEL.B_REGISTERPAYCANCEL(InBusinessInfo);
                        break;

                    case "0006"://获取指定医院科室(专家)日期排班时间段
                        SUB_BUSNAME = "GETSCHPERIOD";
                        OutBusinessInfo = BUS.GETSCHPERIOD.B_GETSCHPERIOD(InBusinessInfo);
                        break;

                    case "0007"://获取可预约日期
                        SUB_BUSNAME = "GETSCHDATE";
                        OutBusinessInfo = BUS.GETSCHDATE.B_GETSCHDATE(InBusinessInfo);
                        break;

                    case "0008"://获取可挂号时段
                        SUB_BUSNAME = "GETSCHTIME";
                        OutBusinessInfo = BUS.GETSCHTIME.B_GETSCHTIME(InBusinessInfo);
                        break;

                    case "0010"://获取预约记录
                        SUB_BUSNAME = "GETREGISTERLIST";

                        OutBusinessInfo = BUS.GETREGISTERLIST.B_GETREGISTERLIST(InBusinessInfo);
                        break;
                    case "0012"://急诊分诊转诊
                        SUB_BUSNAME = "GETEMERGENCYLIST ";

                        OutBusinessInfo = BUS.GETEMERGENCYLIST.B_GETEMERGENCYLIST(InBusinessInfo);
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
                logzzj.BUS_NAME = "ZZJ_YYGH";
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