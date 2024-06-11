using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_YYGH.HISModels;
using OnlineBusHos9_YYGH.Model;

using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBusHos9_YYGH.BUS
{
    internal class GETHOSPDEPT
    {
        public static string B_GETHOSPDEPT(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETHOSPDEPT_M.GETHOSPDEPT_IN _in = JsonConvert.DeserializeObject<GETHOSPDEPT_M.GETHOSPDEPT_IN>(json_in);
                GETHOSPDEPT_M.GETHOSPDEPT_OUT _out = new GETHOSPDEPT_M.GETHOSPDEPT_OUT();

                _out.DEPTLIST = new List<GETHOSPDEPT_M.DEPT>();

                if (_in.FILT_TYPE == "01")
                {


                    PushServiceResult<List<dept>> result = RedisDataHelper.GetT2001("");


                    if (result.code != 1)
                    {
                        dataReturn.Code = 6;
                        dataReturn.Msg = result.msg;

                        return JsonConvert.SerializeObject(dataReturn);
                    }

                    var query = from data in result.data
                                where data.tingZhenBZ == 0
                                group data by new { data.keShiDLDM, data.keShiDLMC }
                                into newdata
                                select new
                                {
                                    DEPT_CODE = newdata.Key.keShiDLDM,
                                    DEPT_NAME = newdata.Key.keShiDLMC,
                                    DEPT_ADDRESS = "",
                                    DEPT_INTRO = "",
                                    DEPT_URL = "",
                                    DEPT_ORDER = "",
                                    DEPT_TYPE = "",
                                };

                    foreach (var item in query)
                    {
                        GETHOSPDEPT_M.DEPT dept = new GETHOSPDEPT_M.DEPT();
                        dept.DEPT_CODE = item.DEPT_CODE;
                        dept.DEPT_NAME = item.DEPT_NAME;
                        dept.DEPT_INTRO = item.DEPT_INTRO;
                        dept.DEPT_URL = item.DEPT_URL;
                        dept.DEPT_ORDER = item.DEPT_ORDER;
                        dept.DEPT_TYPE = item.DEPT_TYPE;
                        dept.DEPT_ADDRESS = item.DEPT_ADDRESS;
                        _out.DEPTLIST.Add(dept);
                    }
                }
                else
                {


                    PushServiceResult<List<dept>> result = RedisDataHelper.GetT2001(_in.FILT_VALUE);

                    if (result.code != 1)
                    {
                        dataReturn.Code = 6;
                        dataReturn.Msg = result.msg;

                        return JsonConvert.SerializeObject(dataReturn);
                    }

                    var query = from data in result.data
                                where data.tingZhenBZ == 0
                                group data by new { data.keShiDM, data.keShiMC }
                                into newdata
                                select new
                                {
                                    DEPT_CODE = newdata.Key.keShiDM,
                                    DEPT_NAME = newdata.Key.keShiMC,
                                    DEPT_ADDRESS = "",
                                    DEPT_INTRO = "",
                                    DEPT_URL = "",
                                    DEPT_ORDER = "",
                                    DEPT_TYPE = "",
                                };

                    foreach (var item in query)
                    {
                        GETHOSPDEPT_M.DEPT dept = new GETHOSPDEPT_M.DEPT();
                        dept.DEPT_CODE = item.DEPT_CODE;
                        dept.DEPT_NAME = item.DEPT_NAME;
                        dept.DEPT_INTRO = item.DEPT_INTRO;
                        dept.DEPT_URL = item.DEPT_URL;
                        dept.DEPT_ORDER = item.DEPT_ORDER;
                        dept.DEPT_TYPE = item.DEPT_TYPE;
                        dept.DEPT_ADDRESS = item.DEPT_ADDRESS;
                        _out.DEPTLIST.Add(dept);
                    }
                }

                dataReturn.Param = JsonConvert.SerializeObject(_out);
            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常" + ex.Message;
            }
            return JsonConvert.SerializeObject(dataReturn);
        }
    }
}