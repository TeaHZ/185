﻿using BusinessInterface;
using CommonModel;
using Newtonsoft.Json;
using PasS.Base.Lib;

using System;
using System.IO;

namespace OnlineBusHos185_GJYB
{
    class PBusHos185_GJYB : ProcessingBusinessAsyncResult
    {
        public override bool ProcessingBusiness(SLBBusinessInfo InBusinessInfo, out SLBBusinessInfo OutBusinessInfo)
        {
            OutBusinessInfo = new SLBBusinessInfo(InBusinessInfo);
            GlobalVar.Init();//初始化静态变量
            try
            {
                string name = InBusinessInfo.SubBusID;
                switch (name)
                {
                    case "0001"://获取人员信息
                        OutBusinessInfo.BusData = BUS.GJYB_PSNQUERY.B_GJYB_PSNQUERY(InBusinessInfo.BusData);
                        break;
                    case "0002"://挂号预结算 
                        OutBusinessInfo.BusData = BUS.GJYB_REGTRY.B_GJYB_REGTRY(InBusinessInfo.BusData);
                        break;
                    case "0003":// 门诊缴费预结算 
                        OutBusinessInfo.BusData = BUS.GJYB_OUTPTRY.B_GJYB_OUTPTRY(InBusinessInfo.BusData);
                        break;
                    case "0004"://结算 
                        OutBusinessInfo.BusData = BUS.GJYB_SETTLE.B_GJYB_SETTLE(InBusinessInfo.BusData);
                        break;
                    case "0005"://退款
                        OutBusinessInfo.BusData = BUS.GJYB_REFUND.B_GJYB_REFUND(InBusinessInfo.BusData);
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
            }
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
