using CommonModel;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using OnlineBusHos185_Report.Model;
using Hos185_His.Models.Report;
using Hos185_His.Models;
using Newtonsoft.Json;

namespace OnlineBusHos185_Report.BUS
{
    class GETPATHOLOGYRESULT
    {
        public static string B_GETPATHOLOGYRESULT(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            string jsonstr = "";
            try
            {
                GETPATHOLOGYRESULT_M.GETPATHOLOGYRESULT_IN _in = JsonConvert.DeserializeObject<GETPATHOLOGYRESULT_M.GETPATHOLOGYRESULT_IN>(json_in);
                GETPATHOLOGYRESULT_M.GETPATHOLOGYRESULT_OUT _out = new GETPATHOLOGYRESULT_M.GETPATHOLOGYRESULT_OUT();
                if (_in.TYPE == "inspecttyp_xindian")
                {
                    Hos185_His.Models.Report.downloadFile download = new Hos185_His.Models.Report.downloadFile()
                    {

                        filePath = "",
                        inspectType = "inspecttyp_xindian",//检验检查列表返回的inspectType   检查类型枚举   InspectTypeEnum 
                        reportId = _in.REPORT_SN,//检验检查列表返回的报告id  inspectNo              
                        visitType = ""
                    };


                    jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(download);
                }

                else
                {

                    {
                        Hos185_His.Models.Report.downloadFile download = new Hos185_His.Models.Report.downloadFile()
                        {

                            filePath = "",
                            inspectType = "inspecttyp_bingli",//检验检查列表返回的inspectType   检查类型枚举   InspectTypeEnum 
                            reportId = _in.REPORT_SN,//检验检查列表返回的报告id  inspectNo              
                            visitType = ""
                        };


                        jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(download);
                    }
                }

                Output<downloadFileData> output
          = GlobalVar.CallAPI<downloadFileData>("/medicaloss/file/downloadFile", jsonstr);


                _out.HIS_RTNXML = "";

                if (output.code != 0)
                {
                    dataReturn.Code = 5;
                    dataReturn.Msg = "您的病理报告还未完成，请稍后再试！";
                    if (_in.TYPE == "inspecttyp_xindian")
                    {
                        dataReturn.Msg = "您的心电报告还未完成，请稍后再试！";
                    }
                    return JsonConvert.SerializeObject(dataReturn);
                }
                try
                {



                    _out.REPORTDATA = output.data.fileBase64String;
                    string base64head = output.data.fileBase64String.Substring(0, 3);

                    switch (base64head)
                    {
                        case "JVB":
                            _out.DATA_TYPE = "1";
                            break;
                        case "iVB":
                        case "/9j":

                            _out.DATA_TYPE = "7";
                            break;
                        default:
                            break;
                    }
                    dataReturn.Code = 0;
                    dataReturn.Msg = "SUCCESS";
                    dataReturn.Param = JsonConvert.SerializeObject(_out);

                }
                catch (Exception ex)
                {
                    dataReturn.Code = 5;
                    dataReturn.Msg = "获取病理报告数据异常";


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
