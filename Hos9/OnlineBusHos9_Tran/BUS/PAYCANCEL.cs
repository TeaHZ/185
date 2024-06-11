using CommonModel;
using EncryptionKey;
using Newtonsoft.Json;
using OnlineBusHos9_Tran.Model;

using System;
using System.Collections.Generic;

namespace OnlineBusHos9_Tran.BUS
{
    internal class PAYCANCEL
    {
        public static string B_PAYCANCEL(string json_in)
        {

            return WXZFBTF.B_WXZFBTF(json_in);

            DataReturn dataReturn = new DataReturn();
            try
            {
                PAYCANCEL_M.PAYCANCEL_IN _in = JsonConvert.DeserializeObject<PAYCANCEL_M.PAYCANCEL_IN>(json_in);
                PAYCANCEL_M.PAYCANCEL_OUT _out = new PAYCANCEL_M.PAYCANCEL_OUT();

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

                HerenModels.Cancel.bizContent bizContent = new HerenModels.Cancel.bizContent()
                {
                    appTradeNo = _in.QUERYID
                };
                string bizcontentJson = JsonConvert.SerializeObject(bizContent);



                string signPlain = appId + _in.QUERYID + key;
                string sign = MD5Helper.Md5(signPlain).ToLower();




                Dictionary<string, string> @params = new Dictionary<string, string>
                    {
                        { "appId", appId },
                        { "method", "uniform.trade.cancel" },
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