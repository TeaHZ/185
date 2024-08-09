using CommonModel;
using Hos185_His;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OnlineBusHos185_Common.BUS
{
    public class SAVESATISFACTIONEVALUATION
    {
        public static string B_SAVESATISFACTIONEVALUATION(string json_in)
        {
            return Business(json_in);
        }

        public static string Business(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";

            try
            {
                Model.SAVESATISFACTIONEVALUATION_M.SAVESATISFACTIONEVALUATION_IN _in = JsonConvert.DeserializeObject<Model.SAVESATISFACTIONEVALUATION_M.SAVESATISFACTIONEVALUATION_IN>(json_in);
                Model.SAVESATISFACTIONEVALUATION_M.SAVESATISFACTIONEVALUATION_OUT _out = new Model.SAVESATISFACTIONEVALUATION_M.SAVESATISFACTIONEVALUATION_OUT();





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
