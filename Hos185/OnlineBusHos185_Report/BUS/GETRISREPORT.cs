using CommonModel;
using Hos185_His.Models;
using Hos185_His.Models.Report;
using Newtonsoft.Json;
using OnlineBusHos185_Report.Model;
using System;
using System.Collections.Generic;

namespace OnlineBusHos185_Report.BUS
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

                Hos185_His.Models.Report.inspectionCheckList checkList
                    = new Hos185_His.Models.Report.inspectionCheckList()
                    {
                        bdate = DateTime.Now.AddMonths(-3).ToString("yyyyMMdd"),
                        checkType = "check",
                        edate = DateTime.Now.ToString("yyyyMMdd"),
                        idCard = _in.SFZ_NO,
                        idCardType = "01",////证件类型 01:身份证 06:护照 08:港澳台居民来往内地通行证
                        medCardSource = "",//就诊来源 src_mz-门诊 src_zy-住院,查询门诊时传src_mz，查询住院时传src_zy，查询住院时传src_tj，不传则查询所有
                        name = ""
                    }
                ;
                string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(checkList);

                Output<List<inspectionCheckListData>> output
          = GlobalVar.CallAPI<List<inspectionCheckListData>>("/hislispacs/inspectionCheckListXTBYXL", jsonstr);

                try
                {
                    _out.REPORT_ALL_NUM = output.data.Count.ToString();
                    _out.RISREPORT = new List<GETRISREPORT_M.RISREPORT>();
                    foreach (var dr in output.data)
                    {
                        GETRISREPORT_M.RISREPORT lis = new GETRISREPORT_M.RISREPORT();
                        foreach (var item in dr.list)
                        {
                            lis.REPORT_SN = item.inspectNo;
                            lis.REPORT_ZL = item.inspectType;
                            lis.REPORT_TYPE = item.itemName;
                            lis.REPORT_DATE = dr.inspectDate;
                            lis.PRINT_FLAG = item.printState;
                            lis.PRINT_TIME = "";
                            lis.REPORT_DATE = dr.inspectDate;
                            lis.REPORT_DOC_NAME = "";
                            lis.APPLY_DEPT_NAME = dr.inspectDate;
                            lis.AUDIT_DATE = dr.inspectDate;
                            lis.AUDIT_DOC_NAME = dr.inspectDate;
                            lis.AUDIT_FLAG = "1";
                            lis.NOTE = "";
                          

                            _out.RISREPORT.Add(lis);
                        }
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