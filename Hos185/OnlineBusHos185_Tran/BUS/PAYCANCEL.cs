using CommonModel;
using Hos185_His.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos185_Tran.Model;

using System;

namespace OnlineBusHos185_Tran.BUS
{
    class PAYCANCEL
    {
        public static string B_PAYCANCEL(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            try
            {
                PAYCANCEL_M.PAYCANCEL_IN _in = JsonConvert.DeserializeObject<PAYCANCEL_M.PAYCANCEL_IN>(json_in);
                PAYCANCEL_M.PAYCANCEL_OUT _out = new PAYCANCEL_M.PAYCANCEL_OUT();


                Hos185_His.Models.OriginPay.P0701 p0701 = new Hos185_His.Models.OriginPay.P0701()
                {
                    transactionId=_in.QUERYID
                };

                string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(p0701);

                Output<JObject> output
          = GlobalVar.CallAPI<JObject>("/platformpayment/pay/closeOrder", jsonstr);

                dataReturn.Code = output.code;
                dataReturn.Msg = output.message;
             
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
