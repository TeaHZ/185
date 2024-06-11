using System;
using System.Collections.Generic;
using CommonModel;
using Newtonsoft.Json;
namespace ZZJ_InHos.BUS
{
    internal class GETPATZYDJDATA
    {
        public static string B_GETPATZYDJDATA(string json_in)
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
                string out_data = GlobalVar.CallOtherBus(json_in, FormatHelper.GetStr(dic["HOS_ID"]), "ZZJ_InHos", "0010").BusData;
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
        //public static string B_GETPATZYDJDATA_b(string json_in)
        //{
        //    DataReturn dataReturn = new DataReturn();
        //    string json_out = "";
        //    try
        //    {
        //        GETPATZYDJDATA_M.GETPATZYDJDATA_IN _in = JsonConvert.DeserializeObject<GETPATZYDJDATA_M.GETPATZYDJDATA_IN>(json_in);
        //        GETPATZYDJDATA_M.GETPATZYDJDATA_OUT _out = new GETPATZYDJDATA_M.GETPATZYDJDATA_OUT();
        //        XmlDocument doc = QHXmlMode.GetBaseXml("GETPATZYDJDATA", "1");
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", string.IsNullOrEmpty(_in.HOS_ID) ? "" : _in.HOS_ID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", string.IsNullOrEmpty(_in.LTERMINAL_SN) ? "" : _in.LTERMINAL_SN.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "USER_ID", string.IsNullOrEmpty(_in.USER_ID) ? "" : _in.USER_ID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_TYPE", string.IsNullOrEmpty(_in.YLCARD_TYPE) ? "" : _in.YLCARD_TYPE.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_NO", string.IsNullOrEmpty(_in.YLCARD_NO) ? "" : _in.YLCARD_NO.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", string.IsNullOrEmpty(_in.SFZ_NO) ? "" : _in.SFZ_NO.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "TYPE", string.IsNullOrEmpty(_in.TYPE) ? "" : _in.TYPE.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_NAME", "");

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
        //            DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
        //            if (dtrev.Rows[0]["CLBZ"].ToString() != "0")
        //            {
        //                dataReturn.Code = 1;
        //                dataReturn.Msg = dtrev.Columns.Contains("CLJG") ? dtrev.Rows[0]["CLJG"].ToString() : "";
        //                goto EndPoint;
        //            }
        //            _out.JZ_NAME = dtrev.Columns.Contains("JZ_NAME") ? dtrev.Rows[0]["JZ_NAME"].ToString() : "";
        //            _out.PAT_TYPE = dtrev.Columns.Contains("PAT_TYPE") ? dtrev.Rows[0]["PAT_TYPE"].ToString() : "";
        //            _out.HOS_NO = dtrev.Columns.Contains("HSP_NO") ? dtrev.Rows[0]["HSP_NO"].ToString() : "";
        //            _out.HOS_ORDER = dtrev.Columns.Contains("HSP_ORDER") ? dtrev.Rows[0]["HSP_ORDER"].ToString() : "";
        //            _out.UTB_IN_NAME = dtrev.Columns.Contains("UTB_IN_NAME") ? dtrev.Rows[0]["UTB_IN_NAME"].ToString() : "";
        //            _out.UTB_IN_ID = dtrev.Columns.Contains("UTB_IN_ID") ? dtrev.Rows[0]["UTB_IN_ID"].ToString() : "";
        //            _out.HIN_DATE = dtrev.Columns.Contains("HIN_DATE") ? dtrev.Rows[0]["HIN_DATE"].ToString() : "";
        //            _out.MZ_DIS_NAME = dtrev.Columns.Contains("MZ_DIS_NAME") ? dtrev.Rows[0]["MZ_DIS_NAME"].ToString() : "";
        //            _out.UTZ_IN_NAME = dtrev.Columns.Contains("UTZ_IN_NAME") ? dtrev.Rows[0]["UTZ_IN_NAME"].ToString() : "";
        //            _out.HST_CODE = dtrev.Columns.Contains("HST_CODE") ? dtrev.Rows[0]["HST_CODE"].ToString() : "";
        //            _out.HIN_WAY = dtrev.Columns.Contains("HIN_WAY") ? dtrev.Rows[0]["HIN_WAY"].ToString() : "";
        //            _out.MZYS_NAME = dtrev.Columns.Contains("MZYS_NAME") ? dtrev.Rows[0]["MZYS_NAME"].ToString() : "";
        //            _out.MZYSMAN_ID = dtrev.Columns.Contains("MZYSMAN_ID") ? dtrev.Rows[0]["MZYSMAN_ID"].ToString() : "";
        //            _out.DOC_NO = dtrev.Columns.Contains("DOC_NO") ? dtrev.Rows[0]["DOC_NO"].ToString() : "";
        //            _out.PAT_NAME = dtrev.Columns.Contains("PAT_NAME") ? dtrev.Rows[0]["PAT_NAME"].ToString() : "";
        //            _out.CARD_TYPE = dtrev.Columns.Contains("CARD_TYPE") ? dtrev.Rows[0]["CARD_TYPE"].ToString() : "";
        //            _out.CARD_NO = dtrev.Columns.Contains("CARD_NO") ? dtrev.Rows[0]["CARD_NO"].ToString() : "";
        //            _out.SEX = dtrev.Columns.Contains("SEX") ? dtrev.Rows[0]["SEX"].ToString() : "";
        //            _out.PAT_TEL = dtrev.Columns.Contains("PAT_TEL") ? dtrev.Rows[0]["PAT_TEL"].ToString() : "";
        //            _out.BIRTHDAY = dtrev.Columns.Contains("BIRTHDAY") ? dtrev.Rows[0]["BIRTHDAY"].ToString() : "";
        //            _out.AGE = dtrev.Columns.Contains("AGE") ? dtrev.Rows[0]["AGE"].ToString() : "";
        //            _out.MRG = dtrev.Columns.Contains("MRG") ? dtrev.Rows[0]["MRG"].ToString() : "";
        //            _out.OCCUPATION = dtrev.Columns.Contains("OCCUPATION") ? dtrev.Rows[0]["OCCUPATION"].ToString() : "";
        //            _out.BPOS = dtrev.Columns.Contains("BPOS") ? dtrev.Rows[0]["BPOS"].ToString() : "";
        //            _out.BIRTH_ADDR = dtrev.Columns.Contains("BIRTH_ADDR") ? dtrev.Rows[0]["BIRTH_ADDR"].ToString() : "";
        //            _out.NATION = dtrev.Columns.Contains("NATION") ? dtrev.Rows[0]["NATION"].ToString() : "";
        //            _out.NOW_REGION = dtrev.Columns.Contains("NOW_REGION") ? dtrev.Rows[0]["NOW_REGION"].ToString() : "";
        //            _out.NOW_ADDR = dtrev.Columns.Contains("NOW_ADDR") ? dtrev.Rows[0]["NOW_ADDR"].ToString() : "";
        //            _out.EDUCATION = dtrev.Columns.Contains("EDUCATION") ? dtrev.Rows[0]["EDUCATION"].ToString() : "";
        //            _out.UNIT_NAME = dtrev.Columns.Contains("UNIT_NAME") ? dtrev.Rows[0]["UNIT_NAME"].ToString() : "";
        //            _out.HK_REGION = dtrev.Columns.Contains("HK_REGION") ? dtrev.Rows[0]["HK_REGION"].ToString() : "";
        //            _out.HK_ADDR = dtrev.Columns.Contains("HK_ADDR") ? dtrev.Rows[0]["HK_ADDR"].ToString() : "";
        //            _out.REL_NAME = dtrev.Columns.Contains("REL_NAME") ? dtrev.Rows[0]["REL_NAME"].ToString() : "";
        //            _out.REL_TYPE = dtrev.Columns.Contains("REL_TYPE") ? dtrev.Rows[0]["REL_TYPE"].ToString() : "";
        //            _out.REL_TELE = dtrev.Columns.Contains("REL_TELE") ? dtrev.Rows[0]["REL_TELE"].ToString() : "";
        //            _out.REL_REGION = dtrev.Columns.Contains("REL_REGION") ? dtrev.Rows[0]["REL_REGION"].ToString() : "";
        //            _out.REL_ADDR = dtrev.Columns.Contains("REL_ADDR") ? dtrev.Rows[0]["REL_ADDR"].ToString() : "";
        //            _out.HIN_TIME = dtrev.Columns.Contains("HIN_TIME") ? dtrev.Rows[0]["HIN_TIME"].ToString() : "";
        //            _out.HOS_PAT_ID = dtrev.Columns.Contains("HOS_PAT_ID") ? dtrev.Rows[0]["HOS_PAT_ID"].ToString() : "";
        //            _out.DIS_NO = dtrev.Columns.Contains("DIS_NO") ? dtrev.Rows[0]["DIS_NO"].ToString() : "";
        //            _out.DIS_NAME = dtrev.Columns.Contains("DIS_NAME") ? dtrev.Rows[0]["DIS_NAME"].ToString() : "";

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