using CommonModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos9_Report.Model;

using System;
using System.Collections.Generic;

namespace OnlineBusHos9_Report.BUS
{
    internal class GETRISREPORT
    {
        public static string B_GETRISREPORT(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETRISREPORT_M.GETRISREPORT_IN _in = JsonConvert.DeserializeObject<GETRISREPORT_M.GETRISREPORT_IN>(json_in);
                GETRISREPORT_M.GETRISREPORT_OUT _out = new GETRISREPORT_M.GETRISREPORT_OUT();

                _out.HIS_RTNXML = "";

                if (_in.YLCARD_TYPE == "1")
                {
                    _in.SFZ_NO = _in.YLCARD_NO;
                }

                JObject jquery = new JObject
                {
                    { "zhengJianHM", _in.SFZ_NO },
                    {"Jclx","0" }
                };

                QueryServiceResult result = HerenHelper.QueryService("EXAM005-QHZZJ", jquery);

                if (result.Head.TradeStatus != "AA")
                {
                    dataReturn.Code = 5;
                    dataReturn.Msg = result.Head.TradeMessage;
                    return JsonConvert.SerializeObject(dataReturn);
                }

                try
                {
                    List<HRReport> reports = result.Body;

                    _out.REPORT_ALL_NUM = reports.ToString();
                    _out.RISREPORT = new List<GETRISREPORT_M.RISREPORT>();
                    foreach (var dr in reports)
                    {
                        GETRISREPORT_M.RISREPORT lis = new GETRISREPORT_M.RISREPORT();
                        lis.REPORT_SN = dr.ReportId;
                        lis.REPORT_TYPE = dr.ExamClass;
                        lis.REPORT_DATE = dr.ExamDateTime;
                        lis.PRINT_FLAG = "0";
                        lis.PRINT_TIME = "";

                        lis.REPORT_DOC_NAME = "";
                        lis.APPLY_DEPT_NAME = "";
                        lis.AUDIT_DATE = dr.ReportDateTime;
                        lis.AUDIT_DOC_NAME = "";
                        lis.AUDIT_FLAG = dr.Reporttype;
                        lis.NOTE = "";
                        lis.DATA_TYPE = "2";
                        lis.REPORTDATA = dr.UrlPath;

                        _out.RISREPORT.Add(lis);
                    }

                    dataReturn.Code = 0;
                    dataReturn.Msg = "SUCCESS";
                    dataReturn.Param = JsonConvert.SerializeObject(_out);
                }
                catch (Exception ex)
                {
                    GlobalVar.WriteLog("GETLISREPORT", "GETLISREPORT", ex.ToString());
                    dataReturn.Code = 5;
                    dataReturn.Msg = "解析HIS出参失败,请检查HIS出参是否正确";
                }
            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
            }
EndPoint:
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}