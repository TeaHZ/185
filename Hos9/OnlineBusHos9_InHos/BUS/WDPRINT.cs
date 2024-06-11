using System;
using System.Collections.Generic;
using System.Text;
using CommonModel;
using Newtonsoft.Json;

namespace OnlineBusHos9_InHos.BUS
{
    class WDPRINT
    {
        public static string B_WDPRINT(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";

            Model.WDPRINT_M.WDPRINT_IN _in = JsonConvert.DeserializeObject<Model.WDPRINT_M.WDPRINT_IN>(json_in);
            Model.WDPRINT_M.WDPRINT_OUT _out = new Model.WDPRINT_M.WDPRINT_OUT();


            
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}
