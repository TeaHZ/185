using CommonModel;
using Hos185_His.Models.OriginPay;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos185_Tran.Model;

using System;

namespace OnlineBusHos185_Tran.BUS
{
    class PAYCOMMIT
    {
        public static string B_PAYCOMMIT(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            try
            {
                PAYCANCEL_M.PAYCANCEL_IN _in = JsonConvert.DeserializeObject<PAYCANCEL_M.PAYCANCEL_IN>(json_in);
                PAYCANCEL_M.PAYCANCEL_OUT _out = new PAYCANCEL_M.PAYCANCEL_OUT();



                P0601 p0601 = new P0601()
                {

                    outTradeNo = _in.QUERYID,
                    transactionId = "",
                    confirmDate = DateTime.Now.ToString(" yyyy-mm-dd HH:mm:ss"),
                    confirmState = "fail",// 确认状态 success 确认成功 fail 失败
                    receiptNo = ""
                };


                string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(p0601);


                Hos185_His.Models.Output<JObject> output
                          = GlobalVar.CallAPI<JObject>("/platformpayment/pay/confirmPay", jsonstr);

                dataReturn = new DataReturn() { Code = output.code, Msg = output.message };
                if (output.code == 0)
                {
                }

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
