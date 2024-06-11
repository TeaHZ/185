using System;
using System.Collections.Generic;
using System.Text.Json;
using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_InHos.HISModels;

namespace OnlineBusHos9_InHos.BUS
{

    internal class GETPATYDJSD
    {
        public static string B_GETPATYDJSD(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            Model.GETPATYDJSD_M.GETPATYDJSD_IN _in = JsonConvert.DeserializeObject<Model.GETPATYDJSD_M.GETPATYDJSD_IN>(json_in);
            Model.GETPATYDJSD_M.GETPATYDJSD_OUT _out = new Model.GETPATYDJSD_M.GETPATYDJSD_OUT();

            HISModels.T5301.Input input = new HISModels.T5301.Input()
            {
                patientId = _in.HOSPATID,
                visitNo = _in.HOS_NO
            };

            PushServiceResult<T5301.Data> result = HerenHelper<T5301.Data>.pushService("5301-QHZZJ", JsonConvert.SerializeObject(input));
          
            if (result.code != 1)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = result.msg;

                return JsonConvert.SerializeObject(dataReturn);
            }

            if (string.IsNullOrEmpty(result.data.base64))
            {
                dataReturn.Code = 7;
                dataReturn.Msg = "异地单为空";

                return JsonConvert.SerializeObject(dataReturn);
            }

            _out.HOS_ID = _in.HOS_ID;
            _out.FILE_TYPE = "1";
            _out.FILE_DATA = result.data.base64;

            dataReturn.Code = 0;
            dataReturn.Msg = "success";
            dataReturn.Param = JsonConvert.SerializeObject(_out);
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }

    }
}