using System;
using System.Collections.Generic;
using System.Text;
using CommonModel;
using Newtonsoft.Json;

namespace OnlineBusHos9_InHos.BUS
{
    class ZYDJSAVE
    {
        public static string B_ZYDJSAVE(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";

            Model.ZYDJSAVE_M.ZYDJSAVE_IN _in = JsonConvert.DeserializeObject<Model.ZYDJSAVE_M.ZYDJSAVE_IN>(json_in);
            Model.ZYDJSAVE_M.ZYDJSAVE_OUT _out = new Model.ZYDJSAVE_M.ZYDJSAVE_OUT();


            
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}
