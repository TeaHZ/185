using BusinessInterface;
using CommonModel;
using Newtonsoft.Json;
using PasS.Base.Lib;

using System;

namespace OnlineBusHos185_Common
{
    class PBusHos185_Common : ProcessingBusinessAsyncResult
    {
        public override bool ProcessingBusiness(SLBBusinessInfo InBusinessInfo, out SLBBusinessInfo OutBusinessInfo)
        {
            OutBusinessInfo = new SLBBusinessInfo(InBusinessInfo);
            try
            {
                string name = InBusinessInfo.SubBusID;
                switch (name)//CCN.ToString().Substring(CCN.ToString().Length - 4)
                {
                    case "0001"://获取病人基础信息
                        OutBusinessInfo.BusData = BUS.GETPATINFO.B_GETPATINFO(InBusinessInfo.BusData);
                        break;
                    case "0002"://保存病人建档信息
                        OutBusinessInfo.BusData = BUS.GETPATRECORD.B_GETPATRECORD(InBusinessInfo.BusData);
                        break;
                    case "0005"://凭条打印
                        OutBusinessInfo.BusData = BUS.TICKETREPRINT.B_TICKETREPRINT(InBusinessInfo.BusData);
                        break;
                    case "0013"://获取评价
                        OutBusinessInfo.BusData = BUS.GETSATISFACTIONEVALUATION.B_GETSATISFACTIONEVALUATION(InBusinessInfo.BusData);
                        break;
                    case "0014"://保存评价
                        OutBusinessInfo.BusData = BUS.SAVESATISFACTIONEVALUATION.B_SAVESATISFACTIONEVALUATION(InBusinessInfo.BusData);
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
            //WriteLog("OnlineBusHos968_OutHosAPI", "inData",Newtonsoft.Json.JsonConvert.SerializeObject(InBusinessInfo));
            //WriteLog("OnlineBusHos968_OutHosAPI", "outData",  OutBusinessInfo.BusData);
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
