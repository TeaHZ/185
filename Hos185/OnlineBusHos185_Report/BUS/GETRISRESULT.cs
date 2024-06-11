using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using OnlineBusHos185_Report.Model;
using CommonModel;

using System;
using Hos185_His.Models.Report;
using Hos185_His.Models;
using Newtonsoft.Json;

namespace OnlineBusHos185_Report.BUS
{
    class GETRISRESULT
    {
        public static string B_GETRISRESULT(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETRISRESULT_M.GETRISRESULT_IN _in = JsonConvert.DeserializeObject<GETRISRESULT_M.GETRISRESULT_IN>(json_in);
                GETRISRESULT_M.GETRISRESULT_OUT _out = new GETRISRESULT_M.GETRISRESULT_OUT();
                Hos185_His.Models.Report.downloadFile download = new Hos185_His.Models.Report.downloadFile()
                {

                    filePath = "",
                    inspectType = "inspecttyp_pacs",//检验检查列表返回的inspectType   检查类型枚举   InspectTypeEnum 
                    reportId = _in.REPORT_SN,//检验检查列表返回的报告id  inspectNo              
                    visitType = ""
                };


                string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(download);

                Output<downloadFileData> output
          = GlobalVar.CallAPI<downloadFileData>("/medicaloss/file/downloadFile", jsonstr);


                if (output.code != 0)
                {

                    dataReturn.Code = 5;
                    dataReturn.Msg = "解析HIS出参失败,请检查HIS出参是否正确";
                    return JsonConvert.SerializeObject(dataReturn);


                }
                _out.DATA_TYPE = "1";
                _out.REPORTDATA = output.data.fileBase64String;


                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
                dataReturn.Param = JsonConvert.SerializeObject(_out);
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
