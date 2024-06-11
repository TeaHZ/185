using System;
using System.Collections.Generic;
using System.IO;
using BusinessInterface;
using CommonModel;
using Newtonsoft.Json;
using PasS.Base.Lib;
namespace ZZJ_Report.BUS
{
    internal class GETLISREPORT
    {
        public static string B_GETLISREPORT(string json_in)
        {
            WriteLog("ZZJ_ReportAPI", "ReceiveData", json_in);
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                Dictionary<string, object> dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(json_in);
                if (!dic.ContainsKey("HOS_ID") || FormatHelper.GetStr(dic["HOS_ID"]) == "")
                {
                    dataReturn.Code = ConstData.CodeDefine.Parameter_Define_Out;
                    dataReturn.Msg = "HOS_ID为必传且不能为空";
                    goto EndPoint;
                }
                string out_data = GlobalVar.CallOtherBus(json_in, FormatHelper.GetStr(dic["HOS_ID"]), "ZZJ_Report", "0001").BusData;
                return out_data;
            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
                dataReturn.Param = ex.ToString();
            }
        EndPoint:
            json_out =   JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }

        protected static void WriteLog(string type, string className, string content)
        {
            string path = "";
            try
            {
                // path = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\MySpringlog";
                path = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "PasSLog", "ZzjLog");
            }
            catch (Exception ex)
            {
                //   path = HttpContent.Current.Server.MapPath("MySpringlog");
            }

            if (!Directory.Exists(path))//如果日志目录不存在就创建
            {
                Directory.CreateDirectory(path);
            }

            try
            {
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");//获取当前系统时间
                string filename = path + "/" + DateTime.Now.ToString("yyyyMMdd") + type.Replace('|', ':') + ".log";//用日期对日志文件命名
                //创建或打开日志文件，向日志文件末尾追加记录
                StreamWriter mySw = File.AppendText(filename);

                //向日志文件写入内容
                string write_content = className + ": " + content;
                mySw.WriteLine(time + " " + type);
                mySw.WriteLine(write_content);
                mySw.WriteLine("");
                //关闭日志文件
                mySw.Close();
            }
            catch (Exception ex)
            {
            }
        }

        #region
        //public static string B_GETLISREPORT_b(string json_in)
        //{
        //    DataReturn dataReturn = new DataReturn();
        //    string json_out = "";
        //    try
        //    {
        //        GETLISREPORT_M.GETLISREPORT_IN _in = JsonConvert.DeserializeObject<GETLISREPORT_M.GETLISREPORT_IN>(json_in);
        //        GETLISREPORT_M.GETLISREPORT_OUT _out = new GETLISREPORT_M.GETLISREPORT_OUT();
        //        XmlDocument doc = QHXmlMode.GetBaseXml("GETLISREPORT_MYNJ", "1");
        //        //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", string.IsNullOrEmpty(_in.HOS_ID) ? "" : _in.HOS_ID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", "218");
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", string.IsNullOrEmpty(_in.LTERMINAL_SN) ? "" : _in.LTERMINAL_SN.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "USER_ID", string.IsNullOrEmpty(_in.USER_ID) ? "" : _in.USER_ID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_TYPE", string.IsNullOrEmpty(_in.YLCARD_TYPE) ? "" : _in.YLCARD_TYPE.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_NO", string.IsNullOrEmpty(_in.YLCARD_NO) ? "" : _in.YLCARD_NO.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", string.IsNullOrEmpty(_in.SFZ_NO) ? "" : _in.SFZ_NO.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "BARCODE", string.IsNullOrEmpty(_in.HOSPATID) ? _in.YLCARD_NO.Trim() : _in.HOSPATID.Trim());

        //        string inxml = doc.InnerXml;
        //        string his_rtnxml = "";
        //        if (GlobalVar.DoBussiness == "0")
        //        {
        //            if (!GlobalVar.CALLSERVICE("218", inxml, ref his_rtnxml))
        //            {
        //                dataReturn.Code = 1;
        //                dataReturn.Msg = his_rtnxml;
        //                goto EndPoint;
        //            }
        //        }
        //        else if (GlobalVar.DoBussiness == "1")
        //        {
        //            if (!GlobalVar.CALLSERVICE("218", inxml, ref his_rtnxml))
        //            {
        //                dataReturn.Code = 1;
        //                dataReturn.Msg = his_rtnxml;
        //                goto EndPoint;
        //            }
        //        }

        //        _out.HIS_RTNXML = his_rtnxml;
        //        try
        //        {
        //            XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(his_rtnxml);
        //            DataSet ds = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY");
        //            DataTable dtrev = ds.Tables[0];
        //            if (dtrev.Rows[0]["CLBZ"].ToString() != "0")
        //            {
        //                dataReturn.Code = 1;
        //                dataReturn.Msg = dtrev.Columns.Contains("CLJG") ? dtrev.Rows[0]["CLJG"].ToString() : "";
        //                dataReturn.Param =   JsonConvert.SerializeObject(_out);
        //                goto EndPoint;
        //            }
        //            DataTable dtlisreport= new DataTable();
        //            try
        //            {
        //                dtlisreport = ds.Tables["LISREPORT"];
        //            }
        //            catch
        //            {
        //                dataReturn.Code = 5;
        //                dataReturn.Msg = "解析HIS出参失败,未找到LISREPORT节点,请检查出参";
        //                goto EndPoint;
        //            }
        //            _out.REPORT_ALL_NUM = dtlisreport.Rows.Count.ToString();
        //            _out.REPORT_PRINT_NUM = dtlisreport.Rows.Count.ToString();

        //            _out.LISREPORT = new List<GETLISREPORT_M.LisReport>();
        //            foreach (DataRow dr in dtlisreport.Rows)
        //            {
        //                GETLISREPORT_M.LisReport lis = new GETLISREPORT_M.LisReport();
        //                lis.REPORT_SN = dtlisreport.Columns.Contains("REPORT_SN") ? FormatHelper.GetStr(dr["REPORT_SN"]) : "";
        //                lis.REPORT_TYPE = dtlisreport.Columns.Contains("REPORT_TYPE") ? FormatHelper.GetStr(dr["REPORT_TYPE"]) : "";
        //                lis.REPORT_DATE = dtlisreport.Columns.Contains("REPORT_DATE") ? FormatHelper.GetStr(dr["REPORT_DATE"]) : "";
        //                lis.PRINT_FLAG = dtlisreport.Columns.Contains("PRINT_FLAG") ? FormatHelper.GetStr(dr["PRINT_FLAG"]) : "";
        //                lis.PRINT_TIME = dtlisreport.Columns.Contains("PRINT_TIME") ? FormatHelper.GetStr(dr["PRINT_TIME"]) : "";
        //                lis.TEST_DATE = dtlisreport.Columns.Contains("TEST_DATE") ? FormatHelper.GetStr(dr["TEST_DATE"]) : "";
        //                lis.TEST_DOC_NAME = dtlisreport.Columns.Contains("TEST_DOC_NAME") ? FormatHelper.GetStr(dr["TEST_DOC_NAME"]) : "";
        //                lis.TEST_DEPT_NAME = dtlisreport.Columns.Contains("TEST_DEPT_NAME") ? FormatHelper.GetStr(dr["TEST_DEPT_NAME"]) : "";
        //                lis.AUDIT_DATE = dtlisreport.Columns.Contains("AUDIT_DATE") ? FormatHelper.GetStr(dr["AUDIT_DATE"]) : "";
        //                lis.AUDIT_DOC_NAME = dtlisreport.Columns.Contains("AUDIT_DOC_NAME") ? FormatHelper.GetStr(dr["AUDIT_DOC_NAME"]) : "";
        //                lis.DATA_TYPE = dtlisreport.Columns.Contains("DATA_TYPE") ? FormatHelper.GetStr(dr["DATA_TYPE"]) : "1";
        //                lis.REPORTDATA = dtlisreport.Columns.Contains("REPORTDATA") ? FormatHelper.GetStr(dr["REPORTDATA"]) : "";
        //                _out.LISREPORT.Add(lis);
        //            }
        //            dataReturn.Code = 0;
        //            dataReturn.Msg = "SUCCESS";
        //            dataReturn.Param =   JsonConvert.SerializeObject(_out);

        //        }
        //        catch (Exception ex)
        //        {
        //            dataReturn.Code = 5;
        //            dataReturn.Msg = "解析HIS出参失败,请检查HIS出参是否正确";

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        dataReturn.Code = 6;
        //        dataReturn.Msg = "程序处理异常";
        //    }
        //EndPoint:
        //    json_out =   JsonConvert.SerializeObject(dataReturn);
        //    return json_out;

        //}
        #endregion
    }
}