using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_Report.Model;

using System;

namespace OnlineBusHos9_Report.BUS
{
    internal class GETPATHOLOGYRESULT
    {
        public static string B_GETPATHOLOGYRESULT(string json_in)
        {
            DataReturn dataReturn = new DataReturn();

            try
            {
                GETPATHOLOGYRESULT_M.GETPATHOLOGYRESULT_IN _in = JsonConvert.DeserializeObject<GETPATHOLOGYRESULT_M.GETPATHOLOGYRESULT_IN>(json_in);
                GETPATHOLOGYRESULT_M.GETPATHOLOGYRESULT_OUT _out = new GETPATHOLOGYRESULT_M.GETPATHOLOGYRESULT_OUT();

             
                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
                dataReturn.Param = JsonConvert.SerializeObject(_out);
            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
            }

            return JsonConvert.SerializeObject(dataReturn);
        
        }
    }
}