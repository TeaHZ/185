using CommonModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using ZZJ_YYGH.Model;

namespace ZZJ_YYGH.BUS
{
    internal class GETSCHINFO
    {
        public static string B_GETSCHINFO(string json_in)
        {
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
                string out_data = GlobalVar.CallOtherBus(json_in, FormatHelper.GetStr(dic["HOS_ID"]), "ZZJ_YYGH", "0002").BusData;
                return out_data;
            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
            }
        EndPoint:
            json_out =   JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }

        public static string B_GETSCHINFO_B(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETSCHINFO_M.GETSCHINFO_IN _in = JsonConvert.DeserializeObject<GETSCHINFO_M.GETSCHINFO_IN>(json_in);
                GETSCHINFO_M.GETSCHINFO_OUT _out = new GETSCHINFO_M.GETSCHINFO_OUT();
                XmlDocument doc = QHXmlMode.GetBaseXml("GETSCHINFO", "1");
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", string.IsNullOrEmpty(_in.HOS_ID) ? "" : _in.HOS_ID.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", string.IsNullOrEmpty(_in.LTERMINAL_SN) ? "" : _in.LTERMINAL_SN.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "USER_ID", string.IsNullOrEmpty(_in.USER_ID) ? "" : _in.USER_ID.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEPT_CODE", string.IsNullOrEmpty(_in.DEPT_CODE) ? "" : _in.DEPT_CODE.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DOC_NO", string.IsNullOrEmpty(_in.DOC_NO) ? "" : _in.DOC_NO.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SCH_DATE", string.IsNullOrEmpty(_in.SCH_DATE) ? "" : _in.SCH_DATE.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_TYPE", string.IsNullOrEmpty(_in.YLCARD_TYPE) ? "" : _in.YLCARD_TYPE.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_NO", string.IsNullOrEmpty(_in.YLCARD_NO) ? "" : _in.YLCARD_NO.Trim());

                string inxml = doc.InnerXml;
                string his_rtnxml = "";
                if (GlobalVar.DoBussiness == "0")
                {
                    if (!GlobalVar.CALLSERVICE(_in.HOS_ID, inxml, ref his_rtnxml))
                    {
                        dataReturn.Code = 1;
                        dataReturn.Msg = his_rtnxml;
                        goto EndPoint;
                    }
                }
                else if (GlobalVar.DoBussiness == "1")
                {
                    if (!GlobalVar.CALLSERVICE(_in.HOS_ID, inxml, ref his_rtnxml))
                    {
                        dataReturn.Code = 1;
                        dataReturn.Msg = his_rtnxml;
                        goto EndPoint;
                    }
                }

                _out.HIS_RTNXML = his_rtnxml;
                try
                {
                    XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(his_rtnxml);
                    DataSet ds = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY");
                    DataTable dtrev = ds.Tables[0];
                    if (dtrev.Rows[0]["CLBZ"].ToString() != "0")
                    {
                        dataReturn.Code = 1;
                        dataReturn.Msg = dtrev.Rows[0]["CLJG"].ToString();
                        dataReturn.Param =   JsonConvert.SerializeObject(_out);
                        goto EndPoint;
                    }
                    try
                    {
                        DataTable dtdept = ds.Tables["DEPT"];
                        _out.DEPTLIST = new List<GETSCHINFO_M.DEPT>();
                        foreach (DataRow dr in dtdept.Rows)
                        {
                            GETSCHINFO_M.DEPT dept = new GETSCHINFO_M.DEPT();
                            dept.DEPT_CODE = dtdept.Columns.Contains("DEPT_CODE") ? dr["DEPT_CODE"].ToString() : "";
                            dept.DEPT_NAME = dtdept.Columns.Contains("DEPT_NAME") ? dr["DEPT_NAME"].ToString() : "";
                            dept.DOC_NO = dtdept.Columns.Contains("DOC_NO") ? dr["DOC_NO"].ToString() : "";
                            dept.DOC_NAME = dtdept.Columns.Contains("DOC_NAME") ? dr["DOC_NAME"].ToString() : "";
                            dept.GH_FEE = dtdept.Columns.Contains("GH_FEE") ? dr["GH_FEE"].ToString() : "";
                            dept.ZL_FEE = dtdept.Columns.Contains("ZL_FEE") ? dr["ZL_FEE"].ToString() : "";
                            dept.SCH_TYPE = dtdept.Columns.Contains("SCH_TYPE") ? dr["SCH_TYPE"].ToString() : "";
                            dept.SCH_DATE = dtdept.Columns.Contains("SCH_DATE") ? dr["SCH_DATE"].ToString() : "";
                            dept.SCH_TIME = dtdept.Columns.Contains("SCH_TIME") ? dr["SCH_TIME"].ToString() : "";
                            dept.PERIOD_START = dtdept.Columns.Contains("PERIOD_START") ? dr["PERIOD_START"].ToString() : "";
                            dept.PERIOD_END = dtdept.Columns.Contains("PERIOD_END") ? dr["PERIOD_END"].ToString() : "";
                            dept.CAN_WAIT = dtdept.Columns.Contains("CAN_WAIT") ? dr["CAN_WAIT"].ToString() : "";
                            dept.REGISTER_TYPE = dtdept.Columns.Contains("REGISTER_TYPE") ? dr["REGISTER_TYPE"].ToString() : "";
                            dept.REGISTER_TYPE_NAME = dtdept.Columns.Contains("REGISTER_TYPE_NAME") ? dr["REGISTER_TYPE_NAME"].ToString() : "";
                            dept.STATUS = dtdept.Columns.Contains("STATUS") ? dr["STATUS"].ToString() : "";
                            dept.COUNT_REM = dtdept.Columns.Contains("COUNT_REM") ? dr["COUNT_REM"].ToString() : "";
                            dept.YB_CODE = dtdept.Columns.Contains("YB_CODE") ? dr["YB_CODE"].ToString() : "";
                            dept.PRO_TITLE = dtdept.Columns.Contains("PRO_TITLE") ? dr["PRO_TITLE"].ToString() : "";

                            _out.DEPTLIST.Add(dept);
                        }
                    }
                    catch
                    {
                    }
                    try
                    {
                        DataTable dtdoc = ds.Tables["DOC"];
                        _out.DOCLIST = new List<GETSCHINFO_M.DOC>();
                        foreach (DataRow dr in dtdoc.Rows)
                        {
                            GETSCHINFO_M.DOC doctor = new GETSCHINFO_M.DOC();
                            doctor.DOC_NO = dtdoc.Columns.Contains("DOC_NO") ? dr["DOC_NO"].ToString() : "";
                            doctor.DOC_NAME = dtdoc.Columns.Contains("DOC_NAME") ? dr["DOC_NAME"].ToString() : "";
                            doctor.GH_FEE = dtdoc.Columns.Contains("GH_FEE") ? dr["GH_FEE"].ToString() : "";
                            doctor.ZL_FEE = dtdoc.Columns.Contains("ZL_FEE") ? dr["ZL_FEE"].ToString() : "";
                            doctor.SCH_TYPE = dtdoc.Columns.Contains("SCH_TYPE") ? dr["SCH_TYPE"].ToString() : "";
                            doctor.SCH_DATE = dtdoc.Columns.Contains("SCH_DATE") ? dr["SCH_DATE"].ToString() : "";
                            doctor.SCH_TIME = dtdoc.Columns.Contains("SCH_TIME") ? dr["SCH_TIME"].ToString() : "";
                            doctor.PERIOD_START = dtdoc.Columns.Contains("PERIOD_START") ? dr["PERIOD_START"].ToString() : "";
                            doctor.PERIOD_END = dtdoc.Columns.Contains("PERIOD_END") ? dr["PERIOD_END"].ToString() : "";
                            doctor.CAN_WAIT = dtdoc.Columns.Contains("CAN_WAIT") ? dr["CAN_WAIT"].ToString() : "";

                            doctor.REGISTER_TYPE = dtdoc.Columns.Contains("REGISTER_TYPE") ? dr["REGISTER_TYPE"].ToString() : "";
                            doctor.REGISTER_TYPE_NAME = dtdoc.Columns.Contains("REGISTER_TYPE_NAME") ? dr["REGISTER_TYPE_NAME"].ToString() : "";
                            doctor.STATUS = dtdoc.Columns.Contains("STATUS") ? dr["STATUS"].ToString() : "";
                            doctor.COUNT_REM = dtdoc.Columns.Contains("COUNT_REM") ? dr["COUNT_REM"].ToString() : "";
                            doctor.YB_CODE = dtdoc.Columns.Contains("YB_CODE") ? dr["YB_CODE"].ToString() : "";
                            doctor.PRO_TITLE = dtdoc.Columns.Contains("PRO_TITLE") ? dr["PRO_TITLE"].ToString() : "";

                            _out.DOCLIST.Add(doctor);
                        }
                    }
                    catch
                    {
                    }

                    dataReturn.Code = 0;
                    dataReturn.Msg = "SUCCESS";
                    dataReturn.Param =   JsonConvert.SerializeObject(_out);
                }
                catch (Exception ex)
                {
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
            json_out =   JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}