using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_YYGH.HISModels;
using OnlineBusHos9_YYGH.Model;

using System.Collections.Generic;

namespace OnlineBusHos9_YYGH.BUS
{
    public class GETREGISTERLIST
    {
        public static string B_GETREGISTERLIST(string json_in)
        {
            DataReturn dataReturn = new DataReturn();

            GETREGISTERLIST_M.GETREGISTERLIST_IN _in = JsonConvert.DeserializeObject<GETREGISTERLIST_M.GETREGISTERLIST_IN>(json_in);
            GETREGISTERLIST_M.GETREGISTERLIST_OUT _out = new GETREGISTERLIST_M.GETREGISTERLIST_OUT();



            T5203.input input = new T5203.input()
            {
                zhengJianHM = _in.SFZ_NO,//证件号码        
                yiBaoBH = "",//医保编号        医保编号
                yeWuLX = "5203",//业务类型     
                bingRenID = _in.HOSPATID,//  病人ID        
                bingRenXM = "",// 病人姓名
                hospitalId= "320282466455146"
            };


            PushServiceResult<List< T5203.data>> result = HerenHelper< List<T5203.data>>.pushService("5203-QHZZJ", JsonConvert.SerializeObject(input));
            
            if (result.code != 1)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = result.msg;

                return JsonConvert.SerializeObject(dataReturn);
            }
            List<GETREGISTERLIST_M.APPT> apptlist = new List<GETREGISTERLIST_M.APPT>();

            foreach (var item in result.data)
            {
                if (item.zhuangTai!="1")
                {
                    continue;
                }
                GETREGISTERLIST_M.APPT appt = new GETREGISTERLIST_M.APPT()
                {
                    HOS_SN = item.yuYueID,
                    APPT_PAY = item.zhenLiaoFei,
                    JEALL = item.heji,
                    APPT_ORDER = item.guaHaoXH,
                    APPT_TIME = item.shouFeiRQ,
                    APPT_PLACE = item.daoZhenXX,
                    YLCARD_TYPE = "",
                    DEPT_CODE = item.keShiDM,
                    DEPT_NAME = item.keShiMC,
                    DEPT_INTRO = "",
                    DEPT_ORDER = "",
                    DEPT_TYPE = "",
                    DEPT_ADDRESS = item.daoZhenXX,
                    DOC_NO = item.yiShengDM,
                    DOC_NAME = item.yiShengXM,
                    GH_FEE = "0.00",
                    ZL_FEE = item.zhenLiaoFei,
                    ALL_FEE =item.heji,
                    //SCH_TYPE = item.yiShengDM != "" ? "2" : "1",
                    SCH_TYPE = item.clinicTypeClass == "普通" ? "1" : "2",
                    SCH_DATE = item.jiuZhenRQ,
                    SCH_TIME = item.jiuZhenSJ,
                    PERIOD_START = "",
                    PERIOD_END = "",
                    REGISTER_TYPE = "",
                    REGISTER_TYPE_NAME = "",
                    APPT_TYPE = "0",
                    SCH_ID = ""
                    
                };

                apptlist.Add(appt);
            }


            _out.APPTLIST = apptlist;

            dataReturn.Param = Newtonsoft.Json.JsonConvert.SerializeObject(_out);
            return JsonConvert.SerializeObject(dataReturn);
        }
    }
}