using System;
using System.Collections.Generic;
using CommonModel;
using Newtonsoft.Json;
namespace ZZJ_InHos.BUS
{
    internal class GETPATINFBYHOSNO
    {
        public static string B_GETPATINFBYHOSNO(string json_in)
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
                string out_data = GlobalVar.CallOtherBus(json_in, FormatHelper.GetStr(dic["HOS_ID"]), "ZZJ_InHos", "0003").BusData;
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

        #region
        //public static string B_GETPATINFBYHOSNO_b(string json_in)
        //{
        //    DataReturn dataReturn = new DataReturn();
        //    string json_out = "";
        //    try
        //    {
        //        GETPATINFBYHOSNO_M.GETPATINFBYHOSNO_IN _in = JsonConvert.DeserializeObject<GETPATINFBYHOSNO_M.GETPATINFBYHOSNO_IN>(json_in);
        //        GETPATINFBYHOSNO_M.GETPATINFBYHOSNO_OUT _out = new GETPATINFBYHOSNO_M.GETPATINFBYHOSNO_OUT();
        //        XmlDocument doc = QHXmlMode.GetBaseXml("GETPATINFBYHOSNO", "1");
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", string.IsNullOrEmpty(_in.HOS_ID) ? "" : _in.HOS_ID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", string.IsNullOrEmpty(_in.LTERMINAL_SN) ? "" : _in.LTERMINAL_SN.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "USER_ID", string.IsNullOrEmpty(_in.USER_ID) ? "" : _in.USER_ID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_NO", string.IsNullOrEmpty(_in.HOS_NO) ? "" : _in.HOS_NO.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", string.IsNullOrEmpty(_in.SFZ_NO) ? "" : _in.SFZ_NO.Trim());
        //        string inxml = doc.InnerXml;
        //        string his_rtnxml = "";
        //        if (GlobalVar.DoBussiness == "0")
        //        {
        //            if (!GlobalVar.CALLSERVICE(_in.HOS_ID, inxml, ref his_rtnxml))
        //            {
        //                dataReturn.Code = 1;
        //                dataReturn.Msg = his_rtnxml;
        //                goto EndPoint;
        //            }
        //            _out.HIS_RTNXML = his_rtnxml;
        //            try
        //            {
        //                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(his_rtnxml);
        //                DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
        //                if (dtrev.Rows[0]["CLBZ"].ToString() != "0")
        //                {
        //                    dataReturn.Code = 1;
        //                    dataReturn.Msg = dtrev.Rows[0]["CLJG"].ToString();
        //                    dataReturn.Param =   JsonConvert.SerializeObject(_out);
        //                    goto EndPoint;
        //                }
        //                _out.HOS_NO = dtrev.Columns.Contains("HOS_NO") ? dtrev.Rows[0]["HOS_NO"].ToString() : "";
        //                _out.HOS_PAT_ID = dtrev.Columns.Contains("HOS_PAT_ID") ? dtrev.Rows[0]["HOS_PAT_ID"].ToString() : "";
        //                _out.PAT_NAME = dtrev.Columns.Contains("PAT_NAME") ? dtrev.Rows[0]["PAT_NAME"].ToString() : "";
        //                _out.SEX = dtrev.Columns.Contains("SEX") ? dtrev.Rows[0]["SEX"].ToString() : "";
        //                _out.HIN_TIME = dtrev.Columns.Contains("HIN_TIME") ? dtrev.Rows[0]["HIN_TIME"].ToString() : "";
        //                _out.SFZ_NO = dtrev.Columns.Contains("SFZ_NO") ? dtrev.Rows[0]["SFZ_NO"].ToString() : "";
        //                _out.BED_NO = dtrev.Columns.Contains("BED_NO") ? dtrev.Rows[0]["BED_NO"].ToString() : "";
        //                dataReturn.Code = 0;
        //                dataReturn.Msg = "SUCCESS";
        //                dataReturn.Param =   JsonConvert.SerializeObject(_out);

        //            }
        //            catch (Exception ex)
        //            {
        //                dataReturn.Code = 5;
        //                dataReturn.Msg = "解析HIS出参失败,请检查HIS出参是否正确";

        //            }
        //        }
        //        else if (GlobalVar.DoBussiness == "1")
        //        {
        //            if (!GlobalVar.CALLSERVICE(_in.HOS_ID, inxml, ref his_rtnxml))
        //            {
        //                dataReturn.Code = 1;
        //                dataReturn.Msg = his_rtnxml;
        //                goto EndPoint;
        //            }
        //            _out.HIS_RTNXML = his_rtnxml;
        //            try
        //            {
        //                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(his_rtnxml);
        //                DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
        //                if (dtrev.Rows[0]["CLBZ"].ToString() != "0")
        //                {
        //                    dataReturn.Code = 1;
        //                    dataReturn.Msg = dtrev.Rows[0]["CLJG"].ToString();
        //                    dataReturn.Param =   JsonConvert.SerializeObject(_out);
        //                    goto EndPoint;
        //                }
        //                _out.HOS_NO = dtrev.Columns.Contains("HOS_NO") ? dtrev.Rows[0]["HOS_NO"].ToString() : "";
        //                _out.HOS_PAT_ID = dtrev.Columns.Contains("HOS_PAT_ID") ? dtrev.Rows[0]["HOS_PAT_ID"].ToString() : "";
        //                _out.PAT_NAME = dtrev.Columns.Contains("PAT_NAME") ? dtrev.Rows[0]["PAT_NAME"].ToString() : "";
        //                _out.SEX = dtrev.Columns.Contains("SEX") ? dtrev.Rows[0]["SEX"].ToString() : "";
        //                _out.HIN_TIME = dtrev.Columns.Contains("HIN_TIME") ? dtrev.Rows[0]["HIN_TIME"].ToString() : "";
        //                _out.SFZ_NO = dtrev.Columns.Contains("SFZ_NO") ? dtrev.Rows[0]["SFZ_NO"].ToString() : "";
        //                _out.BED_NO = dtrev.Columns.Contains("BED_NO") ? dtrev.Rows[0]["BED_NO"].ToString() : "";
        //                #region 业务数据保存
        //                DataTable dtPAT = new Plat.BLL.BaseFunction().GetList("pat_info", "sfz_no='" + dtrev.Rows[0]["sfz_no"].ToString().Trim() + "'", "PAT_ID");
        //                int pat_id;
        //                if (dtPAT.Rows.Count == 0)
        //                {
        //                    if (!new Plat.BLL.BaseFunction().GetSysIdBase("pat_info", out pat_id))
        //                    {
        //                        dataReturn.Code = 1;
        //                        dataReturn.Msg = "获取病人ID失败,请检查sysidbase表";
        //                        goto EndPoint ;
        //                    }
        //                    Plat.Model.pat_info pat_info = new Plat.Model.pat_info();
        //                    pat_info.ADDRESS = "";
        //                    pat_info.BIRTHDAY = "";
        //                    pat_info.CREATE_TIME = DateTime.Now;
        //                    pat_info.GUARDIAN_NAME = "";
        //                    pat_info.GUARDIAN_SFZ_NO = "";
        //                    pat_info.MARK_DEL = false;
        //                    pat_info.MOBILE_NO = "";
        //                    pat_info.NOTE = "";
        //                    pat_info.OPER_TIME = DateTime.Now;
        //                    pat_info.PAT_ID = pat_id;
        //                    pat_info.PAT_NAME = dtrev.Rows[0]["PAT_NAME"].ToString().Trim();
        //                    pat_info.SEX = dtrev.Rows[0]["SEX"].ToString().Trim();
        //                    pat_info.SFZ_NO = _in.SFZ_NO == "" ? dtrev.Rows[0]["SFZ_NO"].ToString().Trim() :_in.SFZ_NO;
        //                    pat_info.YB_CARDNO = "";

        //                    new Plat.BLL.pat_info().Add(pat_info);

        //                }

        //                #endregion
        //                dataReturn.Code = 0;
        //                dataReturn.Msg = "SUCCESS";
        //                dataReturn.Param =   JsonConvert.SerializeObject(_out);

        //            }
        //            catch (Exception ex)
        //            {
        //                dataReturn.Code = 5;
        //                dataReturn.Msg = "解析HIS出参失败,请检查HIS出参是否正确";

        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        dataReturn.Code = 6;
        //        dataReturn.Msg = "程序处理异常";
        //    }
        //    EndPoint:
        //    json_out =   JsonConvert.SerializeObject(dataReturn);
        //    return json_out;

        //}
        #endregion
    }
}