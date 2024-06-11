using CommonModel;
using Hos185_His.Models;
using Hos185_His.Models.MZ;
using Newtonsoft.Json;
using OnlineBusHos185_YYGH.Model;

using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBusHos185_YYGH.BUS
{
    class GETHOSPDEPT
    {
        public static string B_GETHOSPDEPT(string json_in)
        {

            return DoBusiness(json_in);

        }
        public static string DoBusiness(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETHOSPDEPT_M.GETHOSPDEPT_IN _in = JsonConvert.DeserializeObject<GETHOSPDEPT_M.GETHOSPDEPT_IN>(json_in);
                GETHOSPDEPT_M.GETHOSPDEPT_OUT _out = new GETHOSPDEPT_M.GETHOSPDEPT_OUT();

                string begindate = DateTime.Now.ToString("yyyy-MM-dd");
                string enddate = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");


                Hos185_His.Models.MZ.GETHOSPDEPT gethospdept = new Hos185_His.Models.MZ.GETHOSPDEPT()
                {
                    bookFlag = "0",    //科室是否可预约
                    branchCode = "", //院区编号
                    deptFlag = "", //科室列表
                    deptId = "",  //科室编号
                    deptName = "", //科室名称
                    deptType = "", //科室类型,多种科室类型以#分割
                    regdeptFlag = "1", //是否挂号科室 1是 0否 // 2024 02 29 修改 传 0 ，解决没有普通号的科室显示问题
                    validState = "1" //科室状态1在用0停用
                };


                string jsonstrdept = Newtonsoft.Json.JsonConvert.SerializeObject(gethospdept);

                Hos185_His.Models.Output<List<Hos185_His.Models.MZ.GETHOSPDEPTDATA>> outputdept
          = GlobalVar.CallAPI<List<GETHOSPDEPTDATA>>("/hisbasicinfo/dept/deptInfo", jsonstrdept);


                if (outputdept.code != 0)
                {

                    dataReturn.Code = outputdept.code;
                    dataReturn.Msg = outputdept.message;

                    json_out = JsonConvert.SerializeObject(dataReturn);
                    return json_out;

                }



                Hos185_His.Models.MZ.GETSCHINFO getschinfodept = new Hos185_His.Models.MZ.GETSCHINFO()
                {
                    deptCode = "", //科室编号
                    doctCode = "", //医⽣编号
                    isTh = "1", //是否停号 1未停 2已停
                    isTy = "1", //是否停约 0停约 1未停约
                    noonCodeStr = "", //午别编码,多个以#分割
                    pactCode = "", //合同编号
                    reglevlCodeStr = "", //号别编码,多个以#分割
                    schemaId = "", //排班序号
                    schemaType = "", //排班类型 1专家 0普通
                    seeEndDate = begindate, //看诊结束⽇期 yyyy-MM-dd
                    seeStartDate = begindate, //看诊开始⽇期 yyyy-MM-dd
                    sourceType = "XCYY", //号源类别 XCYY=""线下 XCGG=""12320 OLYY=""线上(互联⽹在线问诊)
                    validFlag = "1"  //是否停诊=""0 停诊 1或空 正常 2全部
                };




                string jsonstrschdept = Newtonsoft.Json.JsonConvert.SerializeObject(getschinfodept);

                Hos185_His.Models.Output<List<GETSCHINFODATA>> outputschdept
          = GlobalVar.CallAPI<List<GETSCHINFODATA>>("/hisbooking/schema/schemaInfo", jsonstrschdept);



                if (outputschdept.code == 0)
                {
                    var querydept = from dept in outputschdept.data.AsEnumerable()
                                        //where dept.numremain>0//没号不显示
                                    where (DateTime.Now - DateTime.Parse(dept.canRegEndTime.ToString())).TotalMinutes < 0//过时不显示

                                    group dept by new
                                    {
                                        DEPT_CODE = dept.deptCode,
                                        DEPT_NAME = dept.deptName,
                                        DEPT_INTRO = "",
                                        DEPT_URL = "",
                                        DEPT_ORDER = "",
                                        DEPT_TYPE = "",
                                        DEPT_ADDRESS = ""

                                    } into g
                                    select new
                                    {
                                        g.Key.DEPT_CODE,
                                        g.Key.DEPT_NAME,
                                        g.Key.DEPT_INTRO,
                                        g.Key.DEPT_URL,
                                        g.Key.DEPT_ORDER,
                                        g.Key.DEPT_TYPE,
                                        g.Key.DEPT_ADDRESS

                                    };


                    _out.DEPTLIST = new List<GETHOSPDEPT_M.DEPT>();
                    // 从排班接口返回的科室，需要能在科室返回内
                    //find用法，集合体 用x 来找对应需要的
                    foreach (var deptinfo in querydept)
                    {

                        if (outputdept.data.Find(x => x.deptId == deptinfo.DEPT_CODE) == null)
                        {
                            continue;
                        }

                        if (deptinfo.DEPT_CODE == "69031001")
                        {
                            continue;
                        }

                        if (_in.LTERMINAL_SN.ToUpper() == "ZZJ10"||_in.LTERMINAL_SN.ToUpper() == "ZZJ13")
                        {

                            string[] deptkids = { "07011002", "07011001", "07011006", "07011006" };



                            if (!deptkids.Contains(deptinfo.DEPT_CODE))
                            {
                                continue;
                            }
                        }


                        GETHOSPDEPT_M.DEPT dept = new GETHOSPDEPT_M.DEPT();
                        dept.DEPT_CODE = deptinfo.DEPT_CODE;
                        dept.DEPT_NAME = deptinfo.DEPT_NAME;
                        dept.DEPT_INTRO = deptinfo.DEPT_INTRO;
                        dept.DEPT_URL = "";
                        dept.DEPT_ORDER = deptinfo.DEPT_ORDER;
                        dept.DEPT_TYPE = deptinfo.DEPT_TYPE;
                        dept.DEPT_ADDRESS = deptinfo.DEPT_ADDRESS;
                        _out.DEPTLIST.Add(dept);
                    }

                    dataReturn.Param = JsonConvert.SerializeObject(_out);


                }
                else
                {
                    dataReturn.Code = outputschdept.code;
                    dataReturn.Msg = outputschdept.message;
                }




            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常" + ex.Message;
            }
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;

        }
    }
}
