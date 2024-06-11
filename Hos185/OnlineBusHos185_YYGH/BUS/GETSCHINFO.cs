using CommonModel;
using Hos185_His.Models.MZ;
using Newtonsoft.Json;
using OnlineBusHos185_YYGH.Model;

using System;
using System.Collections.Generic;

namespace OnlineBusHos185_YYGH.BUS
{
    internal class GETSCHINFO
    {
        public static string B_GETSCHINFO(string json_in)
        {
            return DoBusiness(json_in);
        }

        public static string DoBusiness(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETSCHINFO_M.GETSCHINFO_IN _in = JsonConvert.DeserializeObject<GETSCHINFO_M.GETSCHINFO_IN>(json_in);
                GETSCHINFO_M.GETSCHINFO_OUT _out = new GETSCHINFO_M.GETSCHINFO_OUT();

                Hos185_His.Models.MZ.GETSCHINFO getschinfodept = new Hos185_His.Models.MZ.GETSCHINFO()
                {
                    deptCode = _in.DEPT_CODE, //科室编号
                    doctCode = _in.DOC_NO, //医⽣编号
                    isTh = "1", //是否停号 1未停 2已停
                    isTy = "1", //是否停约 0停约 1未停约
                    noonCodeStr = "", //午别编码,多个以#分割
                    pactCode = "", //合同编号
                    reglevlCodeStr = "", //号别编码,多个以#分割
                    schemaId = "", //排班序号
                    schemaType = "0", //排班类型 1专家 0普通
                    seeEndDate = _in.SCH_DATE, //看诊结束⽇期 yyyy-MM-dd
                    seeStartDate = _in.SCH_DATE, //看诊开始⽇期 yyyy-MM-dd
                    sourceType = "XCYY", //号源类别 XCYY=""线下 XCGG=""12320 OLYY=""线上(互联⽹在线问诊)
                    validFlag = "1"  //是否停诊=""0 停诊 1或空 正常 2全部
                };

                Hos185_His.Models.MZ.GETSCHINFO getschinfodoc = new Hos185_His.Models.MZ.GETSCHINFO()
                {
                    deptCode = "", //科室编号
                    doctCode = "", //医⽣编号
                    isTh = "1", //是否停号 1未停 2已停
                    isTy = "1", //是否停约 0停约 1未停约
                    noonCodeStr = "", //午别编码,多个以#分割
                    pactCode = "", //合同编号
                    reglevlCodeStr = "", //号别编码,多个以#分割
                    schemaId = "", //排班序号
                    schemaType = "1", //排班类型 1专家 0普通
                    seeEndDate = _in.SCH_DATE, //看诊结束⽇期 yyyy-MM-dd
                    seeStartDate = _in.SCH_DATE, //看诊开始⽇期 yyyy-MM-dd
                    sourceType = "XCYY", //号源类别 XCYY=""线下 XCGG=""12320 OLYY=""线上(互联⽹在线问诊)
                    validFlag = "1"  //是否停诊=""0 停诊 1或空 正常 2全部
                };

                string jsonstrdept = Newtonsoft.Json.JsonConvert.SerializeObject(getschinfodept);
                string jsonstrdoc = Newtonsoft.Json.JsonConvert.SerializeObject(getschinfodoc);

                Hos185_His.Models.Output<List<GETSCHINFODATA>> outputdept
          = GlobalVar.CallAPI<List<GETSCHINFODATA>>("/hisbooking/schema/schemaInfo", jsonstrdept);
                Hos185_His.Models.Output<List<GETSCHINFODATA>> outputdoc
= GlobalVar.CallAPI<List<GETSCHINFODATA>>("/hisbooking/schema/schemaInfo", jsonstrdoc);

                _out.DEPTLIST = new List<GETSCHINFO_M.DEPT>();

                _out.DOCLIST = new List<GETSCHINFO_M.DOC>();
                if (outputdept.code == 0)
                {
                    List<GETSCHINFODATA> schlist = outputdept.data;

                    foreach (GETSCHINFODATA sch in schlist)
                    {
                        if (sch.seeDate == DateTime.Now.ToString("yyyy-MM-dd"))
                        {
                            var tp3 = DateTime.Parse(sch.seeStartTime).TimeOfDay;
                            var tp2 = DateTime.Parse(sch.seeEndTime).TimeOfDay;
                            var tp1 = DateTime.Now.TimeOfDay;
                            if (TimeSpan.Compare(tp1, tp2) == 1)
                            {
                                continue;
                            }

                            //var tp = tp3 - tp1;

                            
                            //if (tp.TotalMinutes>60)
                            //{
                            //    continue;
                            //}
                        }

                        //普通号
                        GETSCHINFO_M.DEPT dept = new GETSCHINFO_M.DEPT()
                        {
                            DEPT_CODE = sch.deptCode,
                            DEPT_NAME = sch.deptName,

                            DOC_NO = sch.doctCode,
                            DOC_NAME = sch.doctName,

                            GH_FEE = sch.regFee.ToString(),
                            ZL_FEE = sch.treatfee.ToString(),

                            SCH_TYPE = "1",
                            SCH_DATE = sch.seeDate,
                            SCH_TIME = sch.noonName,
                            PERIOD_START = sch.seeStartTime,
                            PERIOD_END = sch.seeEndTime,
                            CAN_WAIT = "1",

                            REGISTER_TYPE = sch.reglevlCode + "|" + sch.noonCode,//（王丹那边就不用改了）modi by wyq 2023 01 06
                            REGISTER_TYPE_NAME = sch.reglevlName,
                            STATUS = "",
                            COUNT_REM = sch.numremain.ToString(),
                            YB_CODE = "",
                            PRO_TITLE = sch.proTitleName,
                            SCH_ID = sch.schemaId
                        };

                        _out.DEPTLIST.Add(dept);
                    }
                }

                if (outputdoc.code == 0)
                {
                    List<GETSCHINFODATA> schlist = outputdoc.data;

                    foreach (GETSCHINFODATA sch in schlist)
                    {
                        if (sch.deptCode.Trim() != _in.DEPT_CODE.Trim())
                        {
                            continue;
                        }
                        if (sch.seeDate == DateTime.Now.ToString("yyyy-MM-dd"))
                        {
                            var tp3 = DateTime.Parse(sch.seeStartTime).TimeOfDay;
                            var tp2 = DateTime.Parse(sch.seeEndTime).TimeOfDay;
                            var tp1 = DateTime.Now.TimeOfDay;
                            if (TimeSpan.Compare(tp1, tp2) == 1)
                            {
                                continue;
                            }

                      
                        }

                        //专家号
                        GETSCHINFO_M.DOC doc = new GETSCHINFO_M.DOC()
                        {
                            DOC_NO = sch.doctCode,
                            DOC_NAME = sch.doctName,

                            GH_FEE = sch.regFee.ToString(),
                            ZL_FEE = sch.treatfee.ToString(),
                            SCH_TYPE = "2",
                            SCH_DATE = sch.seeDate,
                            SCH_TIME = sch.noonName,
                            PERIOD_START = sch.seeStartTime,
                            PERIOD_END = sch.seeEndTime,
                            CAN_WAIT = "1",
                            REGISTER_TYPE = sch.reglevlCode + "|" + sch.noonCode,//（王丹那边就不用改了）modi by wyq 2023 01 06
                            REGISTER_TYPE_NAME = sch.reglevlName,
                            STATUS = "",
                            COUNT_REM = sch.numremain.ToString(),
                            YB_CODE = "",
                            PRO_TITLE = sch.proTitleName,
                            SCH_ID = sch.schemaId
                        };
                        _out.DOCLIST.Add(doc);
                    }
                }
                dataReturn.Param = Newtonsoft.Json.JsonConvert.SerializeObject(_out);
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