using BusinessInterface;
using Newtonsoft.Json;
using PasS.Base.Lib;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OnlineBusHos9_Tran
{
    /// <summary>
    /// 测试两数相加
    /// </summary>
    public class PBusHos9_Tran : ProcessingBusinessAsyncResult 
    {
        public override bool ProcessingBusiness(SLBBusinessInfo InBusinessInfo, out SLBBusinessInfo OutBusinessInfo)
        {
            OutBusinessInfo = new SLBBusinessInfo(InBusinessInfo);
            try
            {
                string name =InBusinessInfo.SubBusID;
                switch (name)
                {
                    case "0001"://获取支付二维码
                        OutBusinessInfo.BusData = BUS.GETQRCODE.B_GETQRCODE(InBusinessInfo.BusData);
                        break;
                    case "0002"://查询订单状态
                        OutBusinessInfo.BusData = BUS.GETORDERSTATUS.B_GETORDERSTATUS(InBusinessInfo.BusData);
                        break;
                    case "0003"://微信支付宝退费
                        OutBusinessInfo.BusData= BUS.WXZFBTF.B_WXZFBTF(InBusinessInfo.BusData);
                        break;
                    case "0004"://扫码被动支付
                        OutBusinessInfo.BusData =  BUS.GETPASSIVEPAY.B_GETPASSIVEPAY(InBusinessInfo.BusData);
                        break;
                    case "0005"://订单取消
                        OutBusinessInfo.BusData = BUS.PAYCANCEL.B_PAYCANCEL(InBusinessInfo.BusData);
                        break;

                    case "0006"://业务确认提交
                        OutBusinessInfo.BusData = BUS.PAYCOMMIT.B_PAYCOMMIT(InBusinessInfo.BusData);
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
