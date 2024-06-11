using CommonModel;
using Hos185_His.Models;
using Hos185_His.Models.Report;
using Newtonsoft.Json;
using OnlineBusHos185_Report.Model;
using System;
using System.Collections.Generic;
using System.Xml;

namespace OnlineBusHos185_Report.BUS
{
    internal class GETLISREPORT
    {
        public static string B_GETLISREPORT(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETLISREPORT_M.GETLISREPORT_IN _in = JsonConvert.DeserializeObject<GETLISREPORT_M.GETLISREPORT_IN>(json_in);
                GETLISREPORT_M.GETLISREPORT_OUT _out = new GETLISREPORT_M.GETLISREPORT_OUT();
                XmlDocument doc = null;

                var db = new DbMySQLZZJ().Client;
                int LISREPORTMOUS = 3;
                SqlSugarModel.SysConfig model = db.Queryable<SqlSugarModel.SysConfig>().Where(t => t.HOS_ID == _in.HOS_ID && t.config_key == "LISREPORTMOUS").First();
                if (model != null)
                {
                    LISREPORTMOUS = FormatHelper.GetInt(model.config_value);
                }


                inspectionCheckList checkList = new inspectionCheckList()
                {
                    bdate = DateTime.Now.AddMonths(-1* LISREPORTMOUS).ToString("yyyyMMdd"),
                    checkType = "test",
                    edate = DateTime.Now.ToString("yyyyMMdd"),
                    idCard = _in.SFZ_NO,
                    idCardType = "01",////证件类型 01:身份证 06:护照 08:港澳台居民来往内地通行证
                    medCardSource = "",//就诊来源 src_mz-门诊 src_zy-住院,查询门诊时传src_mz，查询住院时传src_zy，查询住院时传src_tj，不传则查询所有
                    name = "",
                    inspectType="",

                    blh = _in.YLCARD_NO
                }
                ;
                string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(checkList);

                Output<List<inspectionCheckListData>> output
          = GlobalVar.CallAPI<List<inspectionCheckListData>>("/hislispacs/inspectionCheckListXTBYXL", jsonstr);

                if (output.code != 0)
                {
                    dataReturn.Code = output.code;
                    dataReturn.Msg = output.message;
                    return JsonConvert.SerializeObject(dataReturn);
                }
                try
                {
                    List<GETLISREPORT_M.LisReport> listreprot = new List<GETLISREPORT_M.LisReport>();
                    foreach (var dr in output.data)
                    {
                        foreach (var item in dr.list)
                        {
                            GETLISREPORT_M.LisReport lis = new GETLISREPORT_M.LisReport();
                            if (item.eleReportStatus != "1")
                            {
                                continue;
                            }

                            lis.REPORT_SN = Base64Helper.Base64Encode(item.inspectNo);
                            lis.REPORT_TYPE = item.itemName;
                            lis.TEST_REPORT_SOURCE = item.testReportSource;
                            lis.REPORT_DATE = dr.inspectDate;
                            lis.REPORT_ZL = "";

                            lis.PRINT_FLAG = item.printState;
                            lis.PRINT_TIME = "";
                            lis.TEST_DATE = dr.inspectDate;
                            lis.TEST_DOC_NAME = "";
                            lis.TEST_DEPT_NAME = "";
                            lis.AUDIT_DATE = dr.inspectDate;
                            lis.AUDIT_DOC_NAME = "";
                            lis.AUDIT_FLAG = "1";
                            lis.MACHINE = "";

                            listreprot.Add(lis);
                        }
                    }
                    _out.LISREPORT = listreprot;
                    _out.REPORT_ALL_NUM = listreprot.Count.ToString();
                    dataReturn.Code = 0;
                    dataReturn.Msg = "SUCCESS";
                    dataReturn.Param = JsonConvert.SerializeObject(_out);
                }
                catch (Exception ex)
                {
                    dataReturn.Code = 5;
                    dataReturn.Msg = ex.Message;
                }
            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
                dataReturn.Param = ex.ToString();
            }
            return JsonConvert.SerializeObject(dataReturn);
        }
    }
}