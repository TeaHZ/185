using CommonModel;
using EncryptionKey;
using Newtonsoft.Json;
using OnlineBusHos9_Tran.Model;

using System;
using System.Collections.Generic;

namespace OnlineBusHos9_Tran.BUS
{
    internal class GETORDERSTATUS
    {
        public static string B_GETORDERSTATUS(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETORDERSTATUS_M.GETORDERSTATUS_IN _in = JsonConvert.DeserializeObject<GETORDERSTATUS_M.GETORDERSTATUS_IN>(json_in);
                GETORDERSTATUS_M.GETORDERSTATUS_OUT _out = new GETORDERSTATUS_M.GETORDERSTATUS_OUT();

                //SLBModels.GETORDERSTATUS.busdata busdata=new SLBModels.GETORDERSTATUS.busdata()
                //{
                //    HOS_ID= _in.HOS_ID,
                //    COMM_SN=_in.QUERYID,
                //    PAY_TYPE= _in.DEAL_TYPE == "3" ? "R4" : _in.DEAL_TYPE,
                //};
                //string rtndata;

                //bool suc = new SLBHelper().CallSLB("PBusPay", "GETORDERSTATUS", JsonConvert.SerializeObject(busdata), out rtndata);

                //if (suc)
                //{
                //    SLBModels.GETORDERSTATUS.outbody outbody = JsonConvert.DeserializeObject<SLBModels.GETORDERSTATUS.outbody>(rtndata);

                //    if (outbody.CLBZ!="0")
                //    {
                //        dataReturn.Code = 2;

                //        dataReturn.Msg = outbody.CLJG;
                //        return JsonConvert.SerializeObject(dataReturn);

                //    }
                //    _out.STATUS = outbody.STATUS;
                //    dataReturn.Param = JsonConvert.SerializeObject(_out);

                //}
                //else
                //{
                //    _out.STATUS = "0";
                //}


                string appId = "2022081925029210";
                string key = "539130e1b1fdb52093bb072a67e3c62a";


                if (_in.TYPE == "4")
                {
                    appId = "2022081925029211";

                }
                else if (_in.TYPE == "5")
                {
                    appId = "2022081925029211";
                }


                HerenModels.Query.bizContent bizContent = new HerenModels.Query.bizContent()
                {
                    appTradeNo = _in.QUERYID
                };
                string bizcontentJson = JsonConvert.SerializeObject(bizContent);



                string signPlain = appId + _in.QUERYID + key;
                string sign = MD5Helper.Md5(signPlain).ToLower();




                Dictionary<string, string> @params = new Dictionary<string, string>
                    {
                        { "appId", appId },
                        { "method", "uniform.trade.query" },
                        { "sign", sign },

                        { "bizContent", bizcontentJson }
                    };

                var response = HerenHelper.SendTrade(@params);

                HerenModels.Query.Response rsp = JsonConvert.DeserializeObject<HerenModels.Query.Response>(response.Result);

                if (rsp.tradeStatus == "TRADE_SUCCESS")
                {
                    _out.STATUS = "1";
                }
             
                else if(rsp.tradeStatus== "TRADE_CLOSED")
                {
                    _out.STATUS = "2";//异常，停止轮询
                }
                else //if (rsp.tradeStatus == "WAIT_BUYER_PAY")
                {
                    _out.STATUS = "0";
                }

                _out.APPID = rsp.appId;
                _out.DEFRAYNO = rsp.defrayNo;
                _out.CHANNELTRADENO = rsp.channelTradeNo;

                dataReturn.Param = Newtonsoft.Json.JsonConvert.SerializeObject(_out);
            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = ex.Message;
            }
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}