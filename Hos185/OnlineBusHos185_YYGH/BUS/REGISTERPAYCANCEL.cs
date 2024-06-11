using CommonModel;
using Hos185_His.Models.MZ;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos185_YYGH.Model;

using System;
using System.Collections.Generic;

namespace OnlineBusHos185_YYGH.BUS
{
    class REGISTERPAYCANCEL
    {
        public static string B_REGISTERPAYCANCEL(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                REGISTERPAYCANCEL_M.REGISTERPAYCANCEL_IN _in = JsonConvert.DeserializeObject<REGISTERPAYCANCEL_M.REGISTERPAYCANCEL_IN>(json_in);
                REGISTERPAYCANCEL_M.REGISTERPAYCANCEL_OUT _out = new REGISTERPAYCANCEL_M.REGISTERPAYCANCEL_OUT();

                dataReturn.Code = 0;
                dataReturn.Msg = "success";

                if (string.IsNullOrEmpty(_in.HOS_SN))
                {

                    json_out = JsonConvert.SerializeObject(dataReturn);
                    return json_out;

                }


                string jsonstr = string.Format("apointMentCode={0}", _in.HOS_SN);



                //application/x-www-form-urlencoded
                Dictionary<string, string> header = new Dictionary<string, string>();
                header.Add("operCode", _in.USER_ID);

                Hos185_His.Models.Output<JObject> outputappt
= GlobalVar.CallAPIForm<JObject>("/hisbooking/appointment/cancel", jsonstr, "application/x-www-form-urlencoded", header);
                dataReturn.Code =0;
                dataReturn.Msg = outputappt.message;

              
     
            }
            catch
            {
               
            }

            return JsonConvert.SerializeObject(dataReturn);
        }
    }
}
