using CommonModel;
using Hos185_His.Models;
using Hos185_His.Models.Report;
using Newtonsoft.Json;
using OnlineBusHos185_Report.Model;

using System;
using System.Collections.Generic;

namespace OnlineBusHos185_Report.BUS
{
    internal class GETPATHOLOGYREPORT
    {
        public static string B_GETPATHOLOGYEPORT(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            string jsonstr = "";
            DateTime d1= DateTime.Parse("2024-4-1");
            DateTime d2 = DateTime.Now.AddMonths(-12);

      

            try
            {
                GETPATHOLOGYREPORT_M.GETPATHOLOGYREPORT_IN _in = JsonConvert.DeserializeObject<GETPATHOLOGYREPORT_M.GETPATHOLOGYREPORT_IN>(json_in);
                GETPATHOLOGYREPORT_M.GETPATHOLOGYREPORT_OUT _out = new GETPATHOLOGYREPORT_M.GETPATHOLOGYREPORT_OUT();

                var db = new DbMySQLZZJ().Client;
                int binglimous = 3;
                SqlSugarModel.SysConfig model = db.Queryable<SqlSugarModel.SysConfig>().Where(t => t.HOS_ID == _in.HOS_ID && t.config_key == "binglimous").First();
                if (model != null)
                {
                    binglimous = FormatHelper.GetInt(model.config_value);
                }

                if (_in.TYPE == "inspecttyp_xindian")
                {
                    Hos185_His.Models.Report.inspectionCheckList checkList
                        = new Hos185_His.Models.Report.inspectionCheckList()
                        {
                            //bdate = DateTime.Now.AddMonths(-12).ToString("yyyyMMdd"),开始时间在4.1前
                            bdate = (d1>d2?d1:d2).ToString("yyyyMMdd"),
                            checkType = "check",
                            edate = DateTime.Now.ToString("yyyyMMdd"),
                            idCard = _in.SFZ_NO,
                            idCardType = "01",////证件类型 01:身份证 06:护照 08:港澳台居民来往内地通行证
                            medCardSource = "",//就诊来源 src_mz-门诊 src_zy-住院,查询门诊时传src_mz，查询住院时传src_zy，查询住院时传src_tj，不传则查询所有
                            name = "",
                            blh = _in.YLCARD_NO,
                            inspectType = "inspecttyp_xindian"
                        }
                    ;
                    jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(checkList);
                }

                else
                {
                    Hos185_His.Models.Report.inspectionCheckList checkList
                        = new Hos185_His.Models.Report.inspectionCheckList()
                        {
                            bdate = DateTime.Now.AddMonths(-3).ToString("yyyyMMdd"),
                            checkType = "check",
                            edate = DateTime.Now.ToString("yyyyMMdd"),
                            idCard = _in.SFZ_NO,
                            idCardType = "01",////证件类型 01:身份证 06:护照 08:港澳台居民来往内地通行证
                            medCardSource = "",//就诊来源 src_mz-门诊 src_zy-住院,查询门诊时传src_mz，查询住院时传src_zy，查询住院时传src_tj，不传则查询所有
                            name = "",
                            blh = _in.YLCARD_NO,
                            inspectType = "inspecttyp_bingli"
                        }
                    ;
                    jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(checkList);
                }

                Output<List<inspectionCheckListData>> output
          = GlobalVar.CallAPI<List<inspectionCheckListData>>("/hislispacs/inspectionCheckListXTBYXL", jsonstr);

                try
                {
                    dataReturn.Code = output.code;
                    dataReturn.Msg = output.message;

                    if (output.code != 0)
                    {
                        return JsonConvert.SerializeObject(dataReturn);
                    }

                    _out.REPORT_ALL_NUM = output.data.Count.ToString();
                    _out.REPORT_PRINT_NUM = output.data.Count.ToString(); //dtpatholotyreport.Select("IsAllowPrint=1").Length.ToString();
                    _out.REPORT_AUDIT_NUM = output.data.Count.ToString();
                    _out.PATHOLOGYREPORT = new List<GETPATHOLOGYREPORT_M.PATHOLOGYREPORT>();
                    foreach (var dr in output.data)
                    {
                        foreach (var item in dr.list)
                        {
                            //if (item.inspectType != "inspecttyp_bingli")
                            //{
                            //    continue;
                            //}

                            if (item.eleReportStatus != "1")
                            {
                                continue;
                            }
                            GETPATHOLOGYREPORT_M.PATHOLOGYREPORT pathology = new GETPATHOLOGYREPORT_M.PATHOLOGYREPORT
                            {
                                REPORT_SN = item.inspectNo,
                                REPORT_TYPE = item.blReportType,
                                REPORT_DATE = item.inspectDate,
                                REPORT_NAME = item.itemName,
                                PRINT_FLAG = item.printState,
                                PRINT_TIME = "",
                                CHECK_DATE = "",
                                CHECK_DOC_NAME = "",
                                CHECK_DEPT_NAME = "",
                                APPLY_DATE = "",
                                APPLY_DEPT_NAME = "",
                                APPLY_DOC_NAME = "",
                                AUDIT_DATE = "",
                                AUDIT_DOC_NAME = "",
                                AUDIT_FLAG = "1",
                                RESULT = "",
                                FINAL_REPORT = "",
                                NOTE = "",
                                DATA_TYPE = "",//url //dtpatholotyreport.Columns.Contains("DATA_TYPE") ? FormatHelper.GetStr(dr["DATA_TYPE"]) : "1";
                                REPORTDATA = "",
                                PARAMETERS = "parameters",
                                PAT_NAME = "",
                            };
                            _out.PATHOLOGYREPORT.Add(pathology);
                        }
                    }
                    dataReturn.Code = 0;
                    dataReturn.Msg = "SUCCESS";
                    dataReturn.Param = JsonConvert.SerializeObject(_out);
                }
                catch (Exception ex)
                {
                    dataReturn.Code = 5;
                    dataReturn.Msg = "解析HIS出参失败,请检查HIS出参是否正确";
                    dataReturn.Param = ex.ToString();
                }
            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
            }

            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}