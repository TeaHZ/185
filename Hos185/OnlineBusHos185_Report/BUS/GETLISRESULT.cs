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
using PasS.Base.Lib;
using static OnlineBusHos185_Report.Model.GETLISRESULT_M;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace OnlineBusHos185_Report.BUS
{
    class GETLISRESULT
    {
        public static string B_GETLISRESULT(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETLISRESULT_M.GETLISRESULT_IN _in = JsonConvert.DeserializeObject<GETLISRESULT_M.GETLISRESULT_IN>(json_in);
                GETLISRESULT_M.GETLISRESULT_OUT _out = new GETLISRESULT_M.GETLISRESULT_OUT();

                List<ReportData> reportData = new List<ReportData>();
                if (_in.TEST_REPORT_SOURCE.ToUpper()== "WINEX")//来源是卫宁检验系统的报告
                {
                    JObject jin = new JObject
                    {
                        { "reportId", Base64Helper.Base64Decode(_in.REPORT_SN) }
                    };
                    Output<List< winex_getFileStream>> output_winex
= GlobalVar.CallAPI< List<winex_getFileStream>>("/hislispacs/winning/getFileStream", jin.ToString());

                    if (output_winex.code != 0)
                    {

                        dataReturn.Code = 5;
                        dataReturn.Msg = output_winex.message;
                        return JsonConvert.SerializeObject(dataReturn);


                    }
                    foreach (var item in output_winex.data)
                    {
                        string base64head = item.pdfbase64str.Substring(0, 3);


                        string reportdatatype = "";

                        //获取不同文件的文件头前3个字作为判断依据
                        //fileHeader.set("/9j", "JPG")
                        //fileHeader.set("iVB", "PNG")
                        //fileHeader.set("Qk0", "BMP")
                        //fileHeader.set("SUk", "TIFF")
                        //fileHeader.set("JVB", "PDF")
                        //fileHeader.set("UEs", "OFD")
                        switch (base64head)
                        {
                            case "JVB":
                                reportdatatype = "1";
                                break;
                            case "iVB":
                            case "/9j":

                                reportdatatype = "7";
                                break;
                            default:
                                break;
                        }
                        ReportData report = new ReportData()
                        {
                            DATA_TYPE = reportdatatype,
                            REPORTDATA = item.pdfbase64str

                        };
                        reportData.Add(report);
                    }
                }
                else
                {
                    downloadFile download = new downloadFile()
                    {

                        filePath = "",
                        inspectType = "testType",//检验检查列表返回的inspectType   检查类型枚举   InspectTypeEnum 
                        reportId = Base64Helper.Base64Decode(_in.REPORT_SN),//检验检查列表返回的报告id  inspectNo              
                        visitType = ""
                    };


                    string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(download);

                    Output<downloadFileData> output
              = GlobalVar.CallAPI<downloadFileData>("/medicaloss/file/downloadFile", jsonstr);


                    if (output.code != 0)
                    {

                        dataReturn.Code = 5;
                        dataReturn.Msg = output.message;
                        return JsonConvert.SerializeObject(dataReturn);


                    }

      
                    foreach (var item in output.data.fileBase64List)
                    {
                        string base64head = item.Substring(0, 3);


                        string reportdatatype = "";

                        //获取不同文件的文件头前3个字作为判断依据
                        //fileHeader.set("/9j", "JPG")
                        //fileHeader.set("iVB", "PNG")
                        //fileHeader.set("Qk0", "BMP")
                        //fileHeader.set("SUk", "TIFF")
                        //fileHeader.set("JVB", "PDF")
                        //fileHeader.set("UEs", "OFD")
                        switch (base64head)
                        {
                            case "JVB":
                                reportdatatype = "1";
                                break;
                            case "iVB":
                            case "/9j":

                                reportdatatype = "7";
                                break;
                            default:
                                break;
                        }
                        ReportData report = new ReportData()
                        {
                            DATA_TYPE = reportdatatype,
                            REPORTDATA = item

                        };
                        reportData.Add(report);
                    }
                }








                _out.REPORTLIST= reportData;
                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
                dataReturn.Param = JsonConvert.SerializeObject(_out);

            }
            catch (Exception ex)
            {
                dataReturn.Code = 5;
                dataReturn.Msg = "解析HIS出参失败,请检查HIS出参是否正确";

            }


            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;

        }
    }
}
