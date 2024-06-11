using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_Tran.Model;

using System;

namespace OnlineBusHos9_Tran.BUS
{
    internal class PAYCOMMIT
    {
        public static string B_PAYCOMMIT(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            try
            {
                PAYCANCEL_M.PAYCANCEL_IN _in = JsonConvert.DeserializeObject<PAYCANCEL_M.PAYCANCEL_IN>(json_in);
                PAYCANCEL_M.PAYCANCEL_OUT _out = new PAYCANCEL_M.PAYCANCEL_OUT();
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