using System;
using System.Collections.Generic;
using CommonModel;
using Newtonsoft.Json;
namespace ZZJ_InHos.BUS
{
    /// <summary>
    /// 获取病人住院登记标识
    /// </summary>
    internal class GETPATZYDJSTATE
    {
        public static string B_GETPATZYDJSTATE(string json_in)
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
                string out_data = GlobalVar.CallOtherBus(json_in, FormatHelper.GetStr(dic["HOS_ID"]), "ZZJ_InHos", "0009").BusData;
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
        //public static string B_GETPATZYDJSTATE_b(string json_in)
        //{
        //    DataReturn dataReturn = new DataReturn();
        //    string json_out = "";
        //    try
        //    {
        //        GETPATZYDJSTATE_M.GETPATZYDJSTATE_IN _in = JsonConvert.DeserializeObject<GETPATZYDJSTATE_M.GETPATZYDJSTATE_IN>(json_in);
        //        GETPATZYDJSTATE_M.GETPATZYDJSTATE_OUT _out = new GETPATZYDJSTATE_M.GETPATZYDJSTATE_OUT();
        //        XmlDocument doc = QHXmlMode.GetBaseXml("GETPATZYDJSTATE", "1");
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", string.IsNullOrEmpty(_in.HOS_ID) ? "" : _in.HOS_ID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", string.IsNullOrEmpty(_in.LTERMINAL_SN) ? "" : _in.LTERMINAL_SN.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "USER_ID", string.IsNullOrEmpty(_in.USER_ID) ? "" : _in.USER_ID.Trim());

        //        DataTable dtpat = new DataTable();
        //        dtpat.Columns.Add("PAT_NAME", typeof(string));
        //        dtpat.Columns.Add("PAT_ID", typeof(string));
        //        dtpat.Columns.Add("SFZ_NO", typeof(string));
        //        dtpat.Columns.Add("YLCARD_TYPE", typeof(string));
        //        dtpat.Columns.Add("YLCARD_NO", typeof(string));
        //        DataRow dr = dtpat.NewRow();
        //        dr["PAT_NAME"] = "";
        //        dr["PAT_ID"] = "";
        //        dr["SFZ_NO"] = string.IsNullOrEmpty(_in.SFZ_NO) ? "" : _in.SFZ_NO.Trim();
        //        dr["YLCARD_TYPE"] = string.IsNullOrEmpty(_in.YLCARD_TYPE) ? "" : _in.YLCARD_TYPE.Trim();
        //        dr["YLCARD_NO"] = string.IsNullOrEmpty(_in.YLCARD_NO) ? "" : _in.YLCARD_NO.Trim();
        //        dtpat.Rows.Add(dr);
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PATLIST");
        //        XMLHelper.X_XmlInsertTable(doc, "ROOT/BODY/PATLIST", dtpat, "PAT");
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
        //        }
        //        else if (GlobalVar.DoBussiness == "1")
        //        {
        //            if (!GlobalVar.CALLSERVICE(_in.HOS_ID, inxml, ref his_rtnxml))
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
        //                goto EndPoint;
        //            }

        //            try
        //            {
        //               DataTable  dtitem = ds.Tables["PAT"];
        //               if(dtitem.Rows.Count>0)
        //                {
        //                    _out.ZY_PAT_TYPE = dtitem.Columns.Contains("ZY_PAT_TYPE") ? dtitem.Rows[0]["ZY_PAT_TYPE"].ToString() : "";
        //                }
        //            }
        //            catch
        //            {
        //                dataReturn.Code = 5;
        //                dataReturn.Msg = "解析HIS出参失败,未找到PATLIST节点,请检查HIS出参";
        //                goto EndPoint;
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