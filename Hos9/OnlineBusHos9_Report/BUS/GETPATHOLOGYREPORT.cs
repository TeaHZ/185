using CommonModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos9_Report.Model;

using System;
using System.Collections.Generic;

namespace OnlineBusHos9_Report.BUS
{
    internal class GETPATHOLOGYREPORT
    {
        public static string B_GETPATHOLOGYEPORT(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETPATHOLOGYREPORT_M.GETPATHOLOGYREPORT_IN _in = JsonConvert.DeserializeObject<GETPATHOLOGYREPORT_M.GETPATHOLOGYREPORT_IN>(json_in);
                GETPATHOLOGYREPORT_M.GETPATHOLOGYREPORT_OUT _out = new GETPATHOLOGYREPORT_M.GETPATHOLOGYREPORT_OUT();

                try
                {
                    _out.HIS_RTNXML = "";

                    if (_in.YLCARD_TYPE == "1")
                    {
                        _in.SFZ_NO = _in.YLCARD_NO;
                    }

                    JObject jquery = new JObject
                        {
                            { "zhengJianHM", _in.SFZ_NO },
                            {"Jclx","1" }
                        };

                    QueryServiceResult result = HerenHelper.QueryService("EXAM005-QHZZJ", jquery);

                    if (result.Head.TradeStatus != "AA")
                    {
                        return null;
                    }
                    List<HRReport> reports = JsonConvert.DeserializeObject<List<HRReport>>(result.Body.ToString());

                    _out.REPORT_ALL_NUM = reports.Count.ToString();
                    _out.REPORT_PRINT_NUM = reports.Count.ToString();
                    _out.REPORT_AUDIT_NUM = reports.Count.ToString();
                    _out.PATHOLOGYREPORT = new List<GETPATHOLOGYREPORT_M.PATHOLOGYREPORT>();

                    foreach (var item in reports)
                    {
                        GETPATHOLOGYREPORT_M.PATHOLOGYREPORT pathology = new GETPATHOLOGYREPORT_M.PATHOLOGYREPORT
                        {
                            REPORT_SN = item.ReportId,
                            REPORT_TYPE = item.ExamClass,
                            REPORT_DATE = item.ReportDateTime,
                            REPORT_NAME = item.ExamItemName,
                            PRINT_FLAG = "0",
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
                            DATA_TYPE = "2",//url //dtpatholotyreport.Columns.Contains("DATA_TYPE") ? FormatHelper.GetStr(dr["DATA_TYPE"]) : "1";
                            REPORTDATA = item.UrlPath,
                            PARAMETERS = "parameters",
                            PAT_NAME = "",
                        };
                        _out.PATHOLOGYREPORT.Add(pathology);
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