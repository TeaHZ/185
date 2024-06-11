using CommonModel;
using Hos185_His.Models.MZ;
using Newtonsoft.Json;
using OnlineBusHos185_YYGH.Model;
using System;
using System.Collections.Generic;

namespace OnlineBusHos185_YYGH.BUS
{
    public class GETREGISTERLIST
    {
        public static string B_GETREGISTERLIST(string json_in)
        {
            DataReturn dataReturn = new DataReturn();

            GETREGISTERLIST_M.GETREGISTERLIST_IN _in = JsonConvert.DeserializeObject<GETREGISTERLIST_M.GETREGISTERLIST_IN>(json_in);
            GETREGISTERLIST_M.GETREGISTERLIST_OUT _out = new GETREGISTERLIST_M.GETREGISTERLIST_OUT();

            Hos185_His.Models.MZ.GETAPPOINTMENTTAKENUMBERINFO reglist = new GETAPPOINTMENTTAKENUMBERINFO()
            {
                cardNo = _in.HOSPATID,  //医院内部就诊卡号,唯一
                idCardNo = _in.SFZ_NO, //证件号
                idCardType = "01",  //证件类型
                phoneNo = "",  //手机号
                seeDateStartDate = DateTime.Now.ToString("yyyy-MM-dd 08:00:00"),  //预约看诊开始日期 yyyy-MM-dd hh24:mi:ss
                seeDateStartEnd = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 23:59:59"),  //预约看诊结束日期 yyyy-MM-dd hh24:mi:ss
                state = "1"  //预约状态 0 取消 1 预约未挂号 2 已挂号 多个以#分割
            };

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(reglist);

            Hos185_His.Models.Output<List<GETAPPOINTMENTTAKENUMBERINFODATA>> output
      = GlobalVar.CallAPI<List<GETAPPOINTMENTTAKENUMBERINFODATA>>("/hisbooking/appointment/takeNumber", jsonstr);

            dataReturn.Code = output.code;
            dataReturn.Msg = output.message;
            if (output.code != 0)
            {
                return JsonConvert.SerializeObject(dataReturn);
            }

            List<GETREGISTERLIST_M.APPT> apptlist = new List<GETREGISTERLIST_M.APPT>();

            foreach (var item in output.data)
            {
                string APPT_TYPE = "";
                switch (item.state)
                {
                    case "0":
                        APPT_TYPE = "5";
                        break;

                    case "1":
                        APPT_TYPE = "0";
                        break;

                    case "2":
                        APPT_TYPE = "1";
                        break;

                    default:
                        break;
                }

                string[] times = item.periodSeeTime.Split('-');

                string PERIOD_START = item.seeDate.Trim().Substring(0, 10) + " " + times[0].Trim();
                string PERIOD_END = item.seeDate.Trim().Substring(0, 10) + " " + times[1].Trim();
                string SCH_TIME = item.noonName;

                if (DateTime.Parse(PERIOD_START).Date != DateTime.Now.Date)
                {
                    continue;
                }

                GETREGISTERLIST_M.APPT appt = new GETREGISTERLIST_M.APPT()
                {
                    HOS_SN = item.apointMentCode,
                    APPT_PAY = item.totalFee.ToString(),
                    JEALL = item.totalFee.ToString(),
                    APPT_ORDER = "",
                    APPT_TIME = item.appointMentOperDate,
                    APPT_PLACE = item.seeAddress,
                    YLCARD_TYPE = "",
                    DEPT_CODE = item.deptCode,
                    DEPT_NAME = item.deptName,
                    DEPT_INTRO = "",
                    DEPT_ORDER = item.numNo,
                    DEPT_TYPE = "",
                    DEPT_ADDRESS = item.seeAddress,
                    DOC_NO = item.doctCode,
                    DOC_NAME = item.doctName,
                    GH_FEE = item.regFee.ToString(),
                    ZL_FEE = item.diagFee.ToString(),
                    ALL_FEE = item.totalFee.ToString(),
                    SCH_TYPE = item.schemaType.Trim() == "1" ? "2" : "1",//排班类型 1专家 0普通
                    SCH_DATE = item.seeDate.Substring(0, 10),
                    SCH_TIME = SCH_TIME,
                    PERIOD_START = PERIOD_START,
                    PERIOD_END = PERIOD_END,
                    REGISTER_TYPE = item.regLevelCode,
                    REGISTER_TYPE_NAME = item.regLevelName,
                    APPT_TYPE = APPT_TYPE,
                    SCH_ID = item.schemaId
                };

                apptlist.Add(appt);
            }

            _out.APPTLIST = apptlist;

            dataReturn.Param = Newtonsoft.Json.JsonConvert.SerializeObject(_out);
            return JsonConvert.SerializeObject(dataReturn);
        }
    }
}