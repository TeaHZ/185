using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_YYGH.HISModels;
using OnlineBusHos9_YYGH.Model;

using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBusHos9_YYGH.BUS
{
    /// <summary>
    /// 三级科室目录
    /// </summary>
    public class GETHOSPDEPT2
    {
        public static string B_GETHOSPDEPT2(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETHOSPDEPT_M.GETHOSPDEPT_IN _in = JsonConvert.DeserializeObject<GETHOSPDEPT_M.GETHOSPDEPT_IN>(json_in);
                GETHOSPDEPT_M.GETHOSPDEPT_OUT _out = new GETHOSPDEPT_M.GETHOSPDEPT_OUT();

                _out.DEPTLIST = new List<GETHOSPDEPT_M.DEPT>();
                //1级
                if (_in.FILT_TYPE == "01")
                {
                    List<ZZJ001.DEPT> result = RedisDataHelper.GetZZJ001();
                    //List<string> excludedCores = new List<string> { "A01", "A02", "A03" };
                    //List<ZZJ001.DEPT> result = result1.Where(dept => !excludedCores.Contains(dept.DeptType)).ToList();


                    //string s = "A01|A02|A03|A014|A04";

                    //List<string> DBresult = s?.Split('|').ToList();
                    //List<ZZJ001.DEPT> result1 = RedisDataHelper.GetZZJ001();
                    //List<string> excludedCores = DBresult;
                    //List<ZZJ001.DEPT> result = result1.Where(dept => !excludedCores.Contains(dept.DeptType)).ToList();

                    //List<ZZJ001.DEPT> result = RedisDataHelper.GetZZJ001();

                    var query = from data in result
                                group data by new { data.DeptName, data.DeptType }
                                into newdata
                                select new
                                {
                                    DEPT_CODE = newdata.Key.DeptType,
                                    DEPT_NAME = newdata.Key.DeptName,
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
                else//2级
                {
                    List<ZZJ001.DEPT> result = RedisDataHelper.GetZZJ001();
                    //筛选2级
                    var data2 = result.FindAll(x => x.DeptType == _in.FILT_VALUE);

                    var query = from data in data2
                                group data by new { data.ClinicDept, data.ClinicDeptName }
                                into newdata
                                select new
                                {
                                    DEPT_CODE = newdata.Key.ClinicDept,
                                    DEPT_NAME = newdata.Key.ClinicDeptName,
                                    DEPT_ADDRESS = "",
                                    DEPT_INTRO = "",
                                    DEPT_URL = "",
                                    DEPT_ORDER = "",
                                    DEPT_TYPE = "",
                                };

                    foreach (var item in query)
                    {//处理2级
                        GETHOSPDEPT_M.DEPT dept = new GETHOSPDEPT_M.DEPT();
                        dept.DEPT_CODE = item.DEPT_CODE;
                        dept.DEPT_NAME = item.DEPT_NAME;
                        dept.DEPT_INTRO = item.DEPT_INTRO;
                        dept.DEPT_URL = item.DEPT_URL;
                        dept.DEPT_ORDER = item.DEPT_ORDER;
                        dept.DEPT_TYPE = item.DEPT_TYPE;
                        dept.DEPT_ADDRESS = item.DEPT_ADDRESS;

                        List<GETHOSPDEPT_M.DEPT> dept3s = new List<GETHOSPDEPT_M.DEPT>();
                        //筛选3级
                        var data3 = result.FindAll(x => x.ClinicDept == item.DEPT_CODE);

                        var query3= from data in data3
                                    group data by new { data.SpecialClinicDept, data.SpecialClinicName }
                                into newdata
                                    select new
                                    {
                                        DEPT_CODE = newdata.Key.SpecialClinicDept,
                                        DEPT_NAME = newdata.Key.SpecialClinicName,
                                        DEPT_ADDRESS = "",
                                        DEPT_INTRO = "",
                                        DEPT_URL = "",
                                        DEPT_ORDER = "",
                                        DEPT_TYPE = "",
                                    };
                        foreach (var item3 in query3)
                        {//处理3级
                            GETHOSPDEPT_M.DEPT dept3 = new GETHOSPDEPT_M.DEPT();
                            dept3.DEPT_CODE = item3.DEPT_CODE;
                            dept3.DEPT_NAME = item3.DEPT_NAME;
                            dept3.DEPT_INTRO = "";
                            dept3.DEPT_URL = "";
                            dept3.DEPT_ORDER = "";
                            dept3.DEPT_TYPE = "";
                            dept3.DEPT_ADDRESS = "";
                            dept3s.Add(dept3);
                        }
                        dept.CHILDREN = dept3s;

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