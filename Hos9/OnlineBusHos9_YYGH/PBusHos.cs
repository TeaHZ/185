using BusinessInterface;
using CommonModel;
using Newtonsoft.Json;
using PasS.Base.Lib;

using System;
using System.IO;

namespace OnlineBusHos9_YYGH
{
    class PBusHos9_YYGH : ProcessingBusinessAsyncResult
    {
        public override bool ProcessingBusiness(SLBBusinessInfo InBusinessInfo, out SLBBusinessInfo OutBusinessInfo)
        {

            OutBusinessInfo = new SLBBusinessInfo(InBusinessInfo);
            try
            {
                string name = InBusinessInfo.SubBusID;
                switch (name)//CCN.ToString().Substring(CCN.ToString().Length - 4)
                {
                    case "0001"://获取医院科室信息列表
                        OutBusinessInfo.BusData = BUS.GETHOSPDEPT2.B_GETHOSPDEPT2(InBusinessInfo.BusData);
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
