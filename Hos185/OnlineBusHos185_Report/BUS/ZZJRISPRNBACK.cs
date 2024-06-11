using CommonModel;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using OnlineBusHos185_Report.Model;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Hos185_His.Models;

namespace OnlineBusHos185_Report.BUS
{
    class ZZJRISPRNBACK
    {
        public static string B_ZZJRISPRNBACK(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                ZZJRISPRNBACK_M.ZZJRISPRNBACK_IN _in = JsonConvert.DeserializeObject<ZZJRISPRNBACK_M.ZZJRISPRNBACK_IN>(json_in);
                ZZJRISPRNBACK_M.ZZJRISPRNBACK_OUT _out = new ZZJRISPRNBACK_M.ZZJRISPRNBACK_OUT();
                XmlDocument doc = QHXmlMode.GetBaseXml("ZZJRISPRNBACK", "1");
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", string.IsNullOrEmpty(_in.HOS_ID) ? "" : _in.HOS_ID.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", string.IsNullOrEmpty(_in.LTERMINAL_SN) ? "" : _in.LTERMINAL_SN.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "USER_ID", string.IsNullOrEmpty(_in.USER_ID) ? "" : _in.USER_ID.Trim());
                XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "REPORT_SN", string.IsNullOrEmpty(_in.REPORT_SN) ? "" : _in.REPORT_SN.Trim());

      
     
                JObject jobj = new JObject();
                jobj.Add("inspectNo", _in.REPORT_SN);
                jobj.Add("inspectType", _in.REPORT_ZL);

                Output<object> mzoutput
          = GlobalVar.CallAPI<object>("/hislispacs/tech/updatePrintStatus", jobj.ToString());
                #region 不管成功失败,记录打印,用于计数
                try
                {
                    StringBuilder str_reportmx = new StringBuilder();
                    str_reportmx.Append("insert into reportmx(HOS_ID,TYPE,HOS_SN,lTERMINAL_SN,USER_ID,NOW) values (");
                    str_reportmx.Append("@HOS_ID,@TYPE,@HOS_SN,@lTERMINAL_SN,@USER_ID,@NOW);");
                    MySqlParameter[] parameters =
                    {
                    new MySqlParameter("@HOS_ID",MySqlDbType.VarChar,20),
                    new MySqlParameter("@TYPE",MySqlDbType.VarChar,30),
                    new MySqlParameter("@HOS_SN",MySqlDbType.VarChar,100),
                    new MySqlParameter("@lTERMINAL_SN",MySqlDbType.VarChar,30),
                    new MySqlParameter("@USER_ID",MySqlDbType.VarChar,30),
                    new MySqlParameter("@NOW",MySqlDbType.DateTime)
                    };
                    parameters[0].Value = _in.HOS_ID;
                    parameters[1].Value = "检查报告";
                    parameters[2].Value = _in.REPORT_SN;
                    parameters[3].Value = _in.LTERMINAL_SN;
                    parameters[4].Value = _in.USER_ID;
                    parameters[5].Value = DateTime.Now;
                    DB.Core.DbHelperMySQLZZJ.ExecuteSql(str_reportmx.ToString(), parameters);
                }
                catch (Exception ex)
                {
                    Log.Core.Model.ModSqlError logsql = new Log.Core.Model.ModSqlError();
                    logsql.TYPE = "检查";
                    logsql.EXCEPTION = ex.ToString();
                    logsql.time = DateTime.Now;
                    new Log.Core.MySQLDAL.DalSqlERRROR().Add(logsql);
                }
                #endregion
                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
                goto EndPoint;
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
                        dataReturn.Msg = dtrev.Columns.Contains("CLJG") ? dtrev.Rows[0]["CLJG"].ToString() : "";
                        dataReturn.Param = JsonConvert.SerializeObject(_out);
                        goto EndPoint;
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
