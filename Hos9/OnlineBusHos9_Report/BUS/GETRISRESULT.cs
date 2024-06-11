using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_Report.Model;

using System;

namespace OnlineBusHos9_Report.BUS
{
    internal class GETRISRESULT
    {
        public static string B_GETRISRESULT(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETRISRESULT_M.GETRISRESULT_IN _in = JsonConvert.DeserializeObject<GETRISRESULT_M.GETRISRESULT_IN>(json_in);
                GETRISRESULT_M.GETRISRESULT_OUT _out = new GETRISRESULT_M.GETRISRESULT_OUT();

                //_out.DATA_TYPE = "1";
                //_out.REPORTDATA = output.data.fileBase64String;

                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
                dataReturn.Param = JsonConvert.SerializeObject(_out);
            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
            }

            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}