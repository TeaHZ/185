using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos185_YYGH.Model;

using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;

namespace OnlineBusHos185_YYGH.BUS
{
    class GETSCHTIME
    {
        public static string B_GETSCHTIME(string json_in)
        {
            if (GlobalVar.DoBussiness == "0")
            {
                return UnDoBusiness(json_in);
            }
            else
            {
                return DoBusiness(json_in);
            }
        }

        public static string DoBusiness(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETSCHTIME_M.GETSCHTIME_IN _in = JsonConvert.DeserializeObject<GETSCHTIME_M.GETSCHTIME_IN>(json_in);
                GETSCHTIME_M.GETSCHTIME_OUT _out = new GETSCHTIME_M.GETSCHTIME_OUT();
                XmlDocument doc = QHXmlMode.GetBaseXml("GETSCHINFO", "1");
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", string.IsNullOrEmpty(_in.HOS_ID) ? "" : _in.HOS_ID.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", string.IsNullOrEmpty(_in.LTERMINAL_SN) ? "" : _in.LTERMINAL_SN.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "USER_ID", string.IsNullOrEmpty(_in.USER_ID) ? "" : _in.USER_ID.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEPT_CODE", string.IsNullOrEmpty(_in.DEPT_CODE) ? "" : _in.DEPT_CODE.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DOC_NO", string.IsNullOrEmpty(_in.DOC_NO) ? "" : _in.DOC_NO.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SCH_DATE", string.IsNullOrEmpty(_in.SCH_DATE) ? "" : _in.SCH_DATE.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_TYPE", "4");
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_NO", "");
                string inxml = doc.InnerXml;
                string his_rtnxml = "";


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
                        goto EndPoint;
                    }
                    DataTable dtdept = new DataTable(); DataTable dtdoc = new DataTable();
                    try
                    {
                        dtdept = ds.Tables["DEPT"];
                    }
                    catch
                    {
                    }
                    try
                    {
                        dtdoc = ds.Tables["DOC"];
                    }
                    catch
                    {
                    }
                    if (dtdept.Rows.Count > 0)
                    {
                        DataView dataView = dtdept.DefaultView;
                        if (_in.IS_JZGH == "0")
                        {
                            if (_in.SCH_DATE != "")
                            {
                                dataView.RowFilter = "SCH_TYPE=1  and  COUNT_REM>0 and status ='' and sch_date='" + _in.SCH_DATE + "'";
                            }
                            else
                            {
                                dataView.RowFilter = "SCH_TYPE=1   and  COUNT_REM>0 and status ='' ";
                            }
                        }
                        else
                        {
                            dataView.RowFilter = "SCH_TYPE=3";
                        }
                        dataView.Sort = "SCH_TIME";
                        dtdept = dataView.ToTable();
                    }
                    if (dtdoc.Rows.Count > 0)
                    {
                        DataView dataView = dtdoc.DefaultView;
                        if (_in.IS_JZGH == "0")
                        {
                            if (_in.SCH_DATE != "")
                            {
                                dataView.RowFilter = "SCH_TYPE=2  and  COUNT_REM>0 and status ='' and sch_date='" + _in.SCH_DATE + "'";
                            }
                            else
                            {
                                dataView.RowFilter = "SCH_TYPE=2  and  COUNT_REM>0 and status ='' ";
                            }
                        }
                        else
                        {
                            dataView.RowFilter = "SCH_TYPE=3  COUNT_REM>0";
                        }
                        dataView.Sort = "SCH_TIME";
                        dtdoc = dataView.ToTable();
                    }
                    if (dtdept.Rows.Count == 0 && dtdoc.Rows.Count == 0)
                    {
                        dataReturn.Code = 1;
                        dataReturn.Msg = "未获取到可用时段";
                        goto EndPoint;
                    }
                    _out.SCHTIMELIST = new List<GETSCHTIME_M.SCHTIME>();
                    foreach (DataRow dr in dtdept.Rows)
                    {
                        if (_out.SCHTIMELIST.FindIndex(x => x.SCH_TIME == FormatHelper.GetStr(dr["SCH_TIME"])) < 0)
                        {
                            GETSCHTIME_M.SCHTIME schtime = new GETSCHTIME_M.SCHTIME();
                            schtime.SCH_TIME = FormatHelper.GetStr(dr["SCH_TIME"]);
                            _out.SCHTIMELIST.Add(schtime);
                        }
                    }
                    foreach (DataRow dr in dtdoc.Rows)
                    {
                        if (_out.SCHTIMELIST.FindIndex(x => x.SCH_TIME == FormatHelper.GetStr(dr["SCH_TIME"])) < 0)
                        {
                            GETSCHTIME_M.SCHTIME schtime = new GETSCHTIME_M.SCHTIME();
                            schtime.SCH_TIME = FormatHelper.GetStr(dr["SCH_TIME"]);
                            _out.SCHTIMELIST.Add(schtime);
                        }
                    }
                    dataReturn.Code = 0;
                    dataReturn.Msg = "SUCCESS";
                    dataReturn.Param = JsonConvert.SerializeObject(_out);

                }
                catch 
                {
                    dataReturn.Code = 5;
                    dataReturn.Msg = "解析HIS出参失败,请检查HIS出参是否正确";
                }
            }
            catch 
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
            }
        EndPoint:
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;

        }
        public static string UnDoBusiness(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETSCHDATE_M.GETSCHDATE_IN _in = JsonConvert.DeserializeObject<GETSCHDATE_M.GETSCHDATE_IN>(json_in);
                GETSCHDATE_M.GETSCHDATE_OUT _out = new GETSCHDATE_M.GETSCHDATE_OUT();
                XmlDocument doc = QHXmlMode.GetBaseXml("GETSCHINFO", "1");
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", string.IsNullOrEmpty(_in.HOS_ID) ? "" : _in.HOS_ID.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", string.IsNullOrEmpty(_in.LTERMINAL_SN) ? "" : _in.LTERMINAL_SN.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "USER_ID", string.IsNullOrEmpty(_in.USER_ID) ? "" : _in.USER_ID.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEPT_CODE", string.IsNullOrEmpty(_in.DEPT_CODE) ? "" : _in.DEPT_CODE.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DOC_NO", string.IsNullOrEmpty(_in.DOC_NO) ? "" : _in.DOC_NO.Trim());

                if (_in.USE_TYPE == "09")
                {
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SCH_DATE", "");//DateTime.Now.ToString("yyyy-MM-dd")
                }
                else
                {
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SCH_DATE", "");
                }
                //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "USE_TYPE", string.IsNullOrEmpty(_in.USE_TYPE) ? "" : _in.USE_TYPE.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_TYPE", "4");
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_NO", "");
                string inxml = doc.InnerXml;
                string his_rtnxml = "";


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
                        dataReturn.Param = JsonConvert.SerializeObject(_out);
                        goto EndPoint;
                    }
                    try
                    {
                        DataTable dtdept = ds.Tables["DEPT"];
                        _out.SCHDEPTLIST = new List<GETSCHDATE_M.SCHLIST>();
                        foreach (DataRow dr in dtdept.Rows)
                        {
                            GETSCHDATE_M.SCHLIST dept = new GETSCHDATE_M.SCHLIST();
                            dept.SCH_DATE = dtdept.Columns.Contains("SCH_DATE") ? dr["SCH_DATE"].ToString() : "";
                            if (_out.SCHDEPTLIST.FindIndex(x => x.SCH_DATE.Equals(dept.SCH_DATE)) < 0)
                            {
                                _out.SCHDEPTLIST.Add(dept);
                            }
                        }
                    }
                    catch
                    {
                        //dataReturn.Code = 5;
                        //dataReturn.Msg = "解析HIS出参失败,未找到SCHDEPT节点,请检查HIS出参";
                        //goto EndPoint;
                    }
                    try
                    {
                        DataTable dtdoc = ds.Tables["DOC"];
                        _out.SCHDOCLIST = new List<GETSCHDATE_M.SCHLIST>();
                        foreach (DataRow dr in dtdoc.Rows)
                        {
                            GETSCHDATE_M.SCHLIST doct = new GETSCHDATE_M.SCHLIST();
                            doct.SCH_DATE = dtdoc.Columns.Contains("SCH_DATE") ? dr["SCH_DATE"].ToString() : "";
                            if (_out.SCHDOCLIST.FindIndex(x => x.SCH_DATE.Equals(doct.SCH_DATE)) < 0)
                            {
                                _out.SCHDOCLIST.Add(doct);
                            }
                        }
                    }
                    catch
                    {
                        //dataReturn.Code = 5;
                        //dataReturn.Msg = "解析HIS出参失败,未找到SCHDOC节点,请检查HIS出参";
                        //goto EndPoint;
                    }

                    dataReturn.Code = 0;
                    dataReturn.Msg = "SUCCESS";
                    dataReturn.Param = JsonConvert.SerializeObject(_out);

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
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;

        }
    }
}
