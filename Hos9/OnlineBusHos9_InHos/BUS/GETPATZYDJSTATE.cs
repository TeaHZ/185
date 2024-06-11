using System;
using System.Collections.Generic;
using System.Text;
using CommonModel;
using Newtonsoft.Json;

namespace OnlineBusHos9_InHos.BUS
{
    class GETPATZYDJSTATE
    {
        public static string B_GETPATZYDJSTATE(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            Model.GETPATZYDJSTATE_M.GETPATZYDJSTATE_IN _in = JsonConvert.DeserializeObject<Model.GETPATZYDJSTATE_M.GETPATZYDJSTATE_IN>(json_in);
            Model.GETPATZYDJSTATE_M.GETPATZYDJSTATE_OUT _out = new Model.GETPATZYDJSTATE_M.GETPATZYDJSTATE_OUT();
            
            
      
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}
