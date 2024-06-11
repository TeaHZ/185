using CommonModel;
using EncryptionKey;
using Newtonsoft.Json;
using OnlineBusHos9_Tran.Model;

using System;
using System.Collections.Generic;

namespace OnlineBusHos9_Tran.BUS
{
    internal class WXZFBTF
    {
        public static string B_WXZFBTF(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            try
            {
                WXZFBTF_M.WXZFBTF_IN _in = JsonConvert.DeserializeObject<WXZFBTF_M.WXZFBTF_IN>(json_in);
                WXZFBTF_M.WXZFBTF_OUT _out = new WXZFBTF_M.WXZFBTF_OUT();


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
                string Refundid = "R"+NewIdHelper.NewOrderId20 + "-" + _in.HOS_ID;

                HerenModels.Refund.bizContent bizContent = new HerenModels.Refund.bizContent()
                {
                    appTradeNo = _in.QUERYID,//  String  40  应用交易号
                    appRefundNo = Refundid,// String  40  应用退款号
                    refundFee = _in.CASH_JE,//    Price   10  退款金额
                    returnUrl = "",//    String  200 同步返回页面
                    refundReason = "正常退款",//   String  200 退款原因
                };
                string bizcontentJson = JsonConvert.SerializeObject(bizContent);



                string signPlain = appId + _in.QUERYID +Refundid+_in.CASH_JE+ key;
                string sign = MD5Helper.Md5(signPlain).ToLower();




                Dictionary<string, string> @params = new Dictionary<string, string>
                    {
                        { "appId", appId },
                        { "method", "uniform.trade.refund" },
                        { "sign", sign },

                        { "bizContent", bizcontentJson }
                    };

                var response = HerenHelper.SendTrade(@params);


            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
            }
            string json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}