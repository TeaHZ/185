using CommonModel;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBusHos9_InHos.BUS
{
    public class GETPATHOSNO
    {
        public static string B_GETPATHOSNO(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";

            Model.GETPATHOSNO.In _in = JsonConvert.DeserializeObject<Model.GETPATHOSNO.In>(json_in);
            Model.GETPATHOSNO.Out _out = new Model.GETPATHOSNO.Out();
            

            HISModels.PAT012.Body input = new HISModels.PAT012.Body()
            {
                zhengJianHM = _in.SFZ_NO
            };

            QueryServiceResult result = HerenHelper<string>.QueryService("PAT012-QHZZJ", input);



            if (result.Head.TradeStatus != "AA")
            {
                dataReturn.Code = 2;
                dataReturn.Msg = result.Head.TradeMessage;

                return JsonConvert.SerializeObject(dataReturn);
                ;
            }
            List<HISModels.PAT012.Data> datas = JsonConvert.DeserializeObject<List<HISModels.PAT012.Data>>(JsonConvert.SerializeObject(result.Body));

            //var inpatinfo = datas.Find(x => x.AdtStatus == "在院");
            var inpatinfo = datas[0];

            if (inpatinfo==null)
            {
                dataReturn.Code = 2;
                dataReturn.Msg = "没有查询到在院记录";
                json_out = JsonConvert.SerializeObject(dataReturn);
                return json_out;

            }
            _out.HOS_NO = inpatinfo.VisitNo;
            _out.HOSPATID = inpatinfo.PatientId;


            dataReturn.Param = JsonConvert.SerializeObject(_out);
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;

        }

    }
}
