using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_YYGH.HISModels;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using static OnlineBusHos9_YYGH.Model.GETSCHDATE_M;

namespace OnlineBusHos9_YYGH.BUS
{
    internal class GETSCHDATE
    {
        public static string B_GETSCHDATE(string json_in)
        {
            DataReturn dataReturn = new DataReturn();

            try
            {
                Model.GETSCHDATE_M.GETSCHDATE_IN _in = JsonConvert.DeserializeObject<Model.GETSCHDATE_M.GETSCHDATE_IN>(json_in);
                Model.GETSCHDATE_M.GETSCHDATE_OUT _out = new Model.GETSCHDATE_M.GETSCHDATE_OUT();

                HISModels.T2001.input input = new T2001.input()
                {
                    hospitalId = "320282466455146",//  医院编码
                    keShiDM = _in.DEPT_CODE,//科室代码
                    yeWuLX = "2001",//业务类型
                    shiFouCXXL = "0",
                    //1:keShiDM必传,传1表示根据keShiDM查询其下属的科室小类
                    //0:若科室代码为空，则查询所有科室大类,不为空,则根据keShiDM查询医生排班
                    menZhenLX = "0",//  门诊类型 默认0
                };

                PushServiceResult<List<T2001.data>> result = HerenHelper<List<T2001.data>>.pushService("2001-QHZZJ", JsonConvert.SerializeObject(input));

                if (result.code != 1)
                {
                    dataReturn.Code = 6;
                    dataReturn.Msg = result.msg;

                    return JsonConvert.SerializeObject(dataReturn);
                }

                var query = from data in result.data
                            where data.tingZhenBZ == 0
                            group data by new { data.paiBanRQ, data.shiFouZJ }
      into newdata
                            select new
                            {
                                newdata.Key.shiFouZJ,
                                newdata.Key.paiBanRQ,
                            };

                List<SCHLIST> SCHDEPTLIST = new List<SCHLIST>();
                List<SCHLIST> SCHDOCLIST = new List<SCHLIST>();

                foreach (var item in query)
                {
                    SCHLIST sch = new SCHLIST();

                    sch.SCH_DATE = item.paiBanRQ;

                    sch.WEEK_DAY = DateTime.Parse(item.paiBanRQ).ToString("ddd", new CultureInfo("zh-CN"));

                    if (item.shiFouZJ == 1)
                    {
                        SCHDOCLIST.Add(sch);
                    }
                    else
                    {
                        SCHDEPTLIST.Add(sch);
                    }
                }

                _out.SCHDEPTLIST = SCHDEPTLIST;
                _out.SCHDOCLIST = SCHDOCLIST;
                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
                dataReturn.Param = JsonConvert.SerializeObject(_out);
            }
            catch (Exception ex)
            {
                dataReturn.Code = 7;
                dataReturn.Msg = ex.Message;
            }
            return JsonConvert.SerializeObject(dataReturn);
        }
    }
}