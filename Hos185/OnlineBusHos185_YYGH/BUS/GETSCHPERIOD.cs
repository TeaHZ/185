using CommonModel;
using Hos185_His.Models.MZ;
using Newtonsoft.Json;
using OnlineBusHos185_YYGH.Model;

using System;
using System.Collections.Generic;

namespace OnlineBusHos185_YYGH.BUS
{
    class GETSCHPERIOD
    {
        public static string B_GETSCHPERIOD(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETSCHPERIOD_M.GETSCHPERIOD_IN _in = JsonConvert.DeserializeObject<GETSCHPERIOD_M.GETSCHPERIOD_IN>(json_in);
                GETSCHPERIOD_M.GETSCHPERIOD_OUT _out = new GETSCHPERIOD_M.GETSCHPERIOD_OUT();


                string beginTime = "";
                string endTime = "";

                string[] schinfo = _in.REGISTER_TYPE.Split('|');


                Hos185_His.Models.MZ.GETSCHPERIOD getschperiod = new Hos185_His.Models.MZ.GETSCHPERIOD()
                {
                    beginTime = DateTime.Now.AddMinutes(15).ToString("yyyy-MM-dd HH:mm:ss"), //看诊开始⽇期yyyy-MM-dd HH:mm:ss
                    deptCode = _in.DEPT_CODE, //科室Code
                    doctCode = _in.DOC_NO, //医⽣code
                    doctName = "", //医⽣姓名（⽀持模糊查询）
                    endTime = endTime, //看诊结束⽇期yyyy-MM-dd HH:mm:ss
                    noonCode = schinfo[1], //午别code
                    regLevelCode = schinfo[0], //挂号级别code 
                    schemaIdList = new List<int>() { _in.SCH_ID }, //排班序号
                    sourceType = "XCYY" //号源类别 XCYY:线下 XCGG:12320 OLYY:线上(互联⽹在线问诊)
                };


                string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(getschperiod);

                Hos185_His.Models.Output<List<GETSCHPERIODDATA>> output
          = GlobalVar.CallAPI<List<GETSCHPERIODDATA>>("/hisbooking/schema/schemaDaypartInfo", jsonstr);

                try
                {


                    _out.PERIODLIST = new List<GETSCHPERIOD_M.PERIOD>();
                    foreach (var dr in output.data)
                    {

                        var times1 = DateTime.Now - DateTime.Parse(dr.beginTime.ToString());
                        var times2 = DateTime.Now- DateTime.Parse(dr.endTime.ToString())  ;

                        if (times2.TotalMinutes>0)
                        {
                            continue;
                        }

                        if (dr.numremain==0)
                        {
                            continue;
                        }

                        //if (times1.TotalMinutes<0)
                        //{
                        //    continue;
                        //}

                        GETSCHPERIOD_M.PERIOD period = new GETSCHPERIOD_M.PERIOD();
                        period.PERIOD_START =DateTime.Parse( dr.beginTime).TimeOfDay.ToString();
                        period.PERIOD_END = DateTime.Parse(dr.endTime).TimeOfDay.ToString();
                        period.COUNT_REM = dr.numremain.ToString();
                        period.REGISTER_TYPE = dr.regLevelCode;
                        period.SCH_ID = dr.schemaId;
                        period.PERIOD_ID = dr.darpartId;
                        _out.PERIODLIST.Add(period);
                    }


                    dataReturn.Code = 0;
                    dataReturn.Msg = "SUCCESS";
                    dataReturn.Param = JsonConvert.SerializeObject(_out);

                }
                catch
                {
                    dataReturn.Code = 5;
                    dataReturn.Msg = "解析HIS出参失败,请检查HIS出参是否正确";
                }
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
