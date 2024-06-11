using BusinessInterface;
using CommonModel;
using Newtonsoft.Json;
using PasS.Base.Lib;

using System;
using System.IO;

namespace OnlineBusHos185_YYGH
{
    class PBusHos185_YYGH : ProcessingBusinessAsyncResult
    {
        public override bool ProcessingBusiness(SLBBusinessInfo InBusinessInfo, out SLBBusinessInfo OutBusinessInfo)
        {

            OutBusinessInfo = new SLBBusinessInfo(InBusinessInfo);
            GlobalVar.Init();//初始化静态变量
            try
            {
                string name = InBusinessInfo.SubBusID;
                switch (name)//CCN.ToString().Substring(CCN.ToString().Length - 4)
                {
                    case "0001"://获取医院科室信息列表
                        OutBusinessInfo.BusData = BUS.GETHOSPDEPT.B_GETHOSPDEPT(InBusinessInfo.BusData);
                        break;
                    case "0002"://获取医生科室排班数据
                        OutBusinessInfo.BusData = BUS.GETSCHINFO.B_GETSCHINFO(InBusinessInfo.BusData);
                        break;
                    case "0003"://预约挂号保存
                        OutBusinessInfo.BusData = BUS.REGISTERAPPTSAVE.B_REGISTERAPPTSAVE(InBusinessInfo.BusData);
                        break;
                    case "0004"://预约(实时)挂号支付
                        OutBusinessInfo.BusData = BUS.REGISTERPAYSAVE.B_REGISTERPAYSAVE(InBusinessInfo.BusData);
                        break;
                    case "0005"://预约取消(含支付)
                        OutBusinessInfo.BusData = BUS.REGISTERPAYCANCEL.B_REGISTERPAYCANCEL(InBusinessInfo.BusData);
                        break;
                    case "0006"://获取指定医院科室(专家)日期排班时间段
                        OutBusinessInfo.BusData = BUS.GETSCHPERIOD.B_GETSCHPERIOD(InBusinessInfo.BusData);
                        break;
                    case "0007"://获取可预约日期
                        OutBusinessInfo.BusData = BUS.GETSCHDATE.B_GETSCHDATE(InBusinessInfo.BusData);
                        break;

                    case "0010"://获取预约记录
                        OutBusinessInfo.BusData = BUS.GETREGISTERLIST.B_GETREGISTERLIST(InBusinessInfo.BusData);
                        break;
                    //case "0008"://获取日期上下午时段排班
                    //    OutBusinessInfo.BusData = BUS.GETSCHTIME.B_GETSCHTIME(InBusinessInfo.BusData);
                    //    break;
                    case "0012"://获取预约记录
                        OutBusinessInfo.BusData = BUS.GETEMERGENCYLIST.B_GETEMERGENCYLIST(InBusinessInfo.BusData);
                        break;
                    default:
                        DataReturn dataReturn = new DataReturn();
                        dataReturn.Code = 1;
                        dataReturn.Msg = "未匹配到此业务类型";
                        OutBusinessInfo.BusData = JsonConvert.SerializeObject(dataReturn);
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
            finally
            {
                WriteLog("OnlineBusHos185_YYGH", "outData", OutBusinessInfo.BusData);
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
            catch 
            {

            }
        }
    }
}
