using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_YYGH.HISModels;
using OnlineBusHos9_YYGH.Model;

using System;
using System.Collections.Generic;

namespace OnlineBusHos9_YYGH.BUS
{
    internal class GETSCHPERIOD
    {
        public static string B_GETSCHPERIOD(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETSCHPERIOD_M.GETSCHPERIOD_IN _in = JsonConvert.DeserializeObject<GETSCHPERIOD_M.GETSCHPERIOD_IN>(json_in);
                GETSCHPERIOD_M.GETSCHPERIOD_OUT _out = new GETSCHPERIOD_M.GETSCHPERIOD_OUT();


        

                


                HISModels.T2002.input t2002 = new T2002.input()
                {
                    yiShengDM = _in.DOC_NO,//  医生代码
                    guaHaoLB = "",//  挂号类别
                    zhuanJiaGHLB = "",//   专家判别挂号类别
                    jiuZhenRQ =_in.SCH_DATE,//  就诊时间
                    keShiDM = _in.DEPT_CODE,// 科室代码
                    hospitalId = "320282466455146",//  医院id
                    guaHaoBC=_in.SCH_TIME=="上午"?"1":"2",
                    yeWuLX = "2002",//  业务类型
                };


                PushServiceResult<List<T2002.data>> result = HerenHelper<List<T2002.data>>.pushService("2002-QHZZJ", JsonConvert.SerializeObject(t2002));

                if (result.code != 1)
                {
                    dataReturn.Code = 6;
                    dataReturn.Msg = result.msg;

                    return JsonConvert.SerializeObject(dataReturn);
                }


                if (result.data.Count==0)
                {
                    dataReturn.Code = 2;
                    dataReturn.Msg = "无可用号源";
                    return JsonConvert.SerializeObject(dataReturn);
                }
                _out.PERIODLIST = new List<GETSCHPERIOD_M.PERIOD>();
                foreach (var item in result.data)//这里是一个list？
                {
                    
                    foreach (var haoyuan in item.haoYuans)
                    {
                        if (decimal.Parse( haoyuan.shengYuHYS)<=0)
                        {
                            continue;
                        }

                        string PERIOD_START = haoyuan.jiuZhenSJFW.Split('-')[0];
                        string PERIOD_END = haoyuan.jiuZhenSJFW.Split('-')[1];
                        GETSCHPERIOD_M.PERIOD period = new GETSCHPERIOD_M.PERIOD();
                        period.PERIOD_START = PERIOD_START;
                        period.PERIOD_END = PERIOD_END;
                        period.COUNT_REM = haoyuan.shengYuHYS;
                   
                        period.SCH_ID = item.dangTianPBID+"|"+item.yiZhouPBID;
                        period.PERIOD_ID = haoyuan.guaHaoXH;
                        _out.PERIODLIST.Add(period);
                    }
                }
               

                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
                dataReturn.Param = JsonConvert.SerializeObject(_out);
            }
            catch
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
            }

            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}