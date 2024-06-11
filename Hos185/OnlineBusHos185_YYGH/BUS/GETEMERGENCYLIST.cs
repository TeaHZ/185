using CommonModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos185_YYGH.Model;
using System;
using System.Collections.Generic;
using System.Text;
using OnlineBusHos185_YYGH.Models.MZ;

namespace OnlineBusHos185_YYGH.BUS
{

    public class GETEMERGENCYLIST
    {
        public static string B_GETEMERGENCYLIST(string json_in)
        {
            DataReturn dataReturn = new DataReturn();

            GETEMERGENCYLIST_M.GETEMERGENCYLIST_IN _in = JsonConvert.DeserializeObject<GETEMERGENCYLIST_M.GETEMERGENCYLIST_IN>(json_in);
            GETEMERGENCYLIST_M.GETEMERGENCYLIST_OUT _out = new GETEMERGENCYLIST_M.GETEMERGENCYLIST_OUT();

            JObject getpreCheckNo = new JObject();
            getpreCheckNo.Add("patientId", _in.HOSPATID);
            getpreCheckNo.Add("patientName", _in.PAT_NAME);

            Hos185_His.Models.Output<List<EMERGENCYDATA>> output
= GlobalVar.CallAPI<List<EMERGENCYDATA>>("/hisbooking/preDiagnosis/emergency/get", getpreCheckNo.ToString());

            dataReturn.Code = output.code;
            dataReturn.Msg = output.message;
            if (output.code != 0)
            {

                return JsonConvert.SerializeObject(dataReturn);

            }

            List<GETEMERGENCYLIST_M.APPT> apptlist = new List<GETEMERGENCYLIST_M.APPT>();

            foreach (var item in output.data)
            {


                string APPT_TYPE = "0";


                GETEMERGENCYLIST_M.APPT appt = new GETEMERGENCYLIST_M.APPT()
                {
                    HOS_SN = item.preCheckNo,
                    APPT_PAY = "",
                    JEALL = "",
                    APPT_ORDER = "",
                    APPT_TIME = "",
                    APPT_PLACE = "",
                    YLCARD_TYPE = "",
                    DEPT_CODE = item.preCheckDeptCode,
                    DEPT_NAME = item.preCheckDeptName,
                    DEPT_INTRO = "",
                    DEPT_ORDER = "",
                    DEPT_TYPE = "",
                    DEPT_ADDRESS = "",
                    DOC_NO = "",
                    DOC_NAME = "",
                    GH_FEE = "",
                    ZL_FEE = "",
                    ALL_FEE = "",
                    SCH_TYPE = "1",
                    SCH_DATE = DateTime.Now.ToString("yyyy-MM-dd"),
                    SCH_TIME = item.emergencyLevelName,
                    PERIOD_START = "00:00:00",
                    PERIOD_END = "23:59:59",
                    REGISTER_TYPE = "",
                    REGISTER_TYPE_NAME = "",
                    APPT_TYPE = APPT_TYPE,
                    SCH_ID = "",
                    EMERGENCYLEVELCODE=item.emergencyLevelCode,
                    EMERGENCYLEVELNAME= item.emergencyLevelName
                };

                apptlist.Add(appt);


            }


            _out.APPTLIST = apptlist;

            dataReturn.Param = Newtonsoft.Json.JsonConvert.SerializeObject(_out);
            return JsonConvert.SerializeObject(dataReturn);
        }

    }

}
