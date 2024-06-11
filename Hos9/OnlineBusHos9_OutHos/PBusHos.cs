using BusinessInterface;
using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_OutHos.BUS;
using PasS.Base.Lib;

using System;
using System.IO;

namespace OnlineBusHos9_OutHos
{
    internal class PBusHos9_OutHos : ProcessingBusinessAsyncResult
    {
        public override bool ProcessingBusiness(SLBBusinessInfo InBusinessInfo, out SLBBusinessInfo OutBusinessInfo)
        {
            OutBusinessInfo = new SLBBusinessInfo(InBusinessInfo);
            try
            {
                string name = InBusinessInfo.SubBusID;
                switch (name)//CCN.ToString().Substring(CCN.ToString().Length - 4)
                {
                    case "0001"://获取待处方缴费列表
                        OutBusinessInfo.BusData = BUS.GETOUTFEENOPAY.B_GETOUTFEENOPAY(InBusinessInfo.BusData);
                        break;

                    case "0002"://获取费用明细
                        OutBusinessInfo.BusData = BUS.GETOUTFEENOPAYMX.B_GETOUTFEENOPAYMX(InBusinessInfo.BusData);
                        break;

                    case "0003"://诊间支付锁定
                        OutBusinessInfo.BusData = BUS.OUTFEEPAYLOCK.B_OUTFEEPAYLOCK(InBusinessInfo.BusData);
                        break;

                    case "0004"://诊间支付保存
                        OutBusinessInfo.BusData = BUS.OUTFEEPAYSAVE.B_OUTFEEPAYSAVE(InBusinessInfo.BusData);
                        break; 

                    case "0011"://获取自助开单列表
                        OutBusinessInfo.BusData = BUS.GETSELFBILLINGLIST.B_GETSELFBILLINGLIST(InBusinessInfo.BusData);
                        break;

                    case "0012"://自助开单保存
                        OutBusinessInfo.BusData = BUS.SAVESELFBILLING.B_SAVESELFBILLING(InBusinessInfo.BusData);
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
            //WriteLog("OnlineBusHos968_OutHosAPI", "outData", OutBusinessInfo.BusData);
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