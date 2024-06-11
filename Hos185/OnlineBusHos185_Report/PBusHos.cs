using BusinessInterface;
using CommonModel;
using Newtonsoft.Json;
using PasS.Base.Lib;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace OnlineBusHos185_Report
{
    class PBusHos185_Report: ProcessingBusinessAsyncResult
    {
        public override bool ProcessingBusiness(SLBBusinessInfo InBusinessInfo, out SLBBusinessInfo OutBusinessInfo)
        {
            OutBusinessInfo = new SLBBusinessInfo(InBusinessInfo);

            try
            {
                string name = InBusinessInfo.SubBusID;
                switch (name)//CCN.ToString().Substring(CCN.ToString().Length - 4)
                {
                    case "0001"://获取患者检验报告列表
                        OutBusinessInfo.BusData = BUS.GETLISREPORT.B_GETLISREPORT(InBusinessInfo.BusData);
                        break;
                    case "0002":// 查询检验报告明细
                        OutBusinessInfo.BusData = BUS.GETLISRESULT.B_GETLISRESULT(InBusinessInfo.BusData);
                        break;
                    case "0003":// 检验报告打印回传
                        OutBusinessInfo.BusData = BUS.ZZJLISPRNBACK.B_ZZJLISPRNBACK(InBusinessInfo.BusData);
                        break;
                    case "0004"://获取患者检查报告列表
                        OutBusinessInfo.BusData = BUS.GETRISREPORT.B_GETRISREPORT(InBusinessInfo.BusData);
                        break;
                    case "0005":// 查询检查报告明细
                        OutBusinessInfo.BusData = BUS.GETRISRESULT.B_GETRISRESULT(InBusinessInfo.BusData);
                        break;
                    case "0006":// 检查报告打印回传
                        OutBusinessInfo.BusData = BUS.ZZJRISPRNBACK.B_ZZJRISPRNBACK(InBusinessInfo.BusData);
                        break;
                    case "0007"://获取患者病理报告列表
                        OutBusinessInfo.BusData = BUS.GETPATHOLOGYREPORT.B_GETPATHOLOGYEPORT(InBusinessInfo.BusData);
                        break;
                    case "0008":// 查询病理报告明细
                        OutBusinessInfo.BusData = BUS.GETPATHOLOGYRESULT.B_GETPATHOLOGYRESULT(InBusinessInfo.BusData);
                        break;
                    case "0009":// 病理报告打印回传
                        OutBusinessInfo.BusData = BUS.ZZJPATHOLOGYPRNBACK.B_ZZJPATHOLOGYPRNBACK(InBusinessInfo.BusData);
                        break;
                    case "0010"://查询病历报告列表 （TYPE：GETMEDICALRECORD）
                        OutBusinessInfo.BusData = BUS.GETMEDICALRECORD.B_GETMEDICALRECORD(InBusinessInfo.BusData);
                        break;
                    case "0011"://查询病历报告明细 （TYPE：GETMEDICALRESULT）
                        OutBusinessInfo.BusData = BUS.GETMEDICALRECORD.B_GETMEDICALRESULT(InBusinessInfo.BusData);
                        break;
                    case "0012"://病历报告打印回传 （TYPE：ZZJMEDICALPRNBACK）
                        OutBusinessInfo.BusData = BUS.GETMEDICALRECORD.B_ZZJMEDICALPRNBACK(InBusinessInfo.BusData);

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

    }
}