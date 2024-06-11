using System;
using System.Collections.Generic;
using System.Text;
using OnlineBusHos9_InHos.HISModels;
using CommonModel;
using Newtonsoft.Json;



namespace OnlineBusHos9_InHos.BUS
{
    class GETPATZYDJDATA
    {
        public static string B_GETPATZYDJDATA(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";

            Model.GETPATZYDJDATA_M.GETPATZYDJDATA_IN _in = JsonConvert.DeserializeObject<Model.GETPATZYDJDATA_M.GETPATZYDJDATA_IN>(json_in);
            Model.GETPATZYDJDATA_M.GETPATZYDJDATA_OUT _out = new Model.GETPATZYDJDATA_M.GETPATZYDJDATA_OUT();

       
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}
