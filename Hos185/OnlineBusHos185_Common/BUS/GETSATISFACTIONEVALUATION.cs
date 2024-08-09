using CommonModel;
using Hos185_His;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OnlineBusHos185_Common.BUS
{
    public class GETSATISFACTIONEVALUATION
    {
        public static string B_GETSATISFACTIONEVALUATION(string json_in)
        {
            return Business(json_in);
        }

        public static string Business(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";

            try
            {
                Model.GETSATISFACTIONEVALUATION_M.GETSATISFACTIONEVALUATION_IN _in = JsonConvert.DeserializeObject<Model.GETSATISFACTIONEVALUATION_M.GETSATISFACTIONEVALUATION_IN>(json_in);
                Model.GETSATISFACTIONEVALUATION_M.GETSATISFACTIONEVALUATION_OUT _out = new Model.GETSATISFACTIONEVALUATION_M.GETSATISFACTIONEVALUATION_OUT();

            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
                dataReturn.Param = ex.Message;
            }


            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;

        }
    }
}
