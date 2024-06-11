using System;
using System.Collections.Generic;
using BusinessInterface;
using CommonModel;
using Newtonsoft.Json;
using PasS.Base.Lib;
namespace ZZJ_OutHos.BUS
{
    internal class GETOUTFEENOPAYMX
    {
        public static string B_GETOUTFEENOPAYMX(string json_in)
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
                string out_data = GlobalVar.CallOtherBus(json_in, FormatHelper.GetStr(dic["HOS_ID"]), "ZZJ_OutHos", "0002").BusData;
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
        //public static string B_GETOUTFEENOPAYMX_b(string json_in)
        //{
        //    DataReturn dataReturn = new DataReturn();
        //    string json_out = "";
        //    try
        //    {
        //        GETOUTFEENOPAYMX_M.GETOUTFEENOPAYMX_IN _in = JsonConvert.DeserializeObject<GETOUTFEENOPAYMX_M.GETOUTFEENOPAYMX_IN>(json_in);
        //        GETOUTFEENOPAYMX_M.GETOUTFEENOPAYMX_OUT _out = new GETOUTFEENOPAYMX_M.GETOUTFEENOPAYMX_OUT();
        //        XmlDocument doc = QHXmlMode.GetBaseXml("GETOUTFEENOPAYMX", "1");
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", string.IsNullOrEmpty(_in.HOS_ID) ? "" : _in.HOS_ID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", string.IsNullOrEmpty(_in.LTERMINAL_SN) ? "" : _in.LTERMINAL_SN.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "USER_ID", string.IsNullOrEmpty(_in.USER_ID) ? "" : _in.USER_ID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", string.IsNullOrEmpty(_in.HOS_SN) ? "" : _in.HOS_SN.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "OPT_SN", string.IsNullOrEmpty(_in.OPT_SN) ? "" : _in.OPT_SN.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PRE_NO", string.IsNullOrEmpty(_in.PRE_NO) ? "" : _in.PRE_NO.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "BARCODE", string.IsNullOrEmpty(_in.HOSPATID) ? "" : _in.HOSPATID.Trim());

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
        //                dataReturn.Msg = dtrev.Rows[0]["CLJG"].ToString();
        //                dataReturn.Param =   JsonConvert.SerializeObject(_out);
        //                goto EndPoint;
        //            }
        //            _out.JEALL = dtrev.Columns.Contains("JEALL") ?FormatHelper.GetStr(dtrev.Rows[0]["JEALL"]) : "";

        //            try
        //            {
        //                DataTable dtmed = ds.Tables["DAMED"];
        //                _out.DAMEDLIST = new List<GETOUTFEENOPAYMX_M.MED>();
        //                foreach (DataRow dr in dtmed.Rows)
        //                {
        //                    GETOUTFEENOPAYMX_M.MED med = new GETOUTFEENOPAYMX_M.MED();
        //                    med.PRENO = dtmed.Columns.Contains("PRENO") ? dr["PRENO"].ToString() : "";
        //                    med.DATIME = dtmed.Columns.Contains("DATIME") ? dr["DATIME"].ToString() : "";
        //                    med.DAID = dtmed.Columns.Contains("DAID") ? dr["DAID"].ToString() : "";
        //                    med.MED_ID = dtmed.Columns.Contains("MED_ID") ? dr["MED_ID"].ToString() : "";
        //                    med.MED_NAME = dtmed.Columns.Contains("MED_NAME") ? dr["MED_NAME"].ToString() : "";
        //                    med.MED_GG = dtmed.Columns.Contains("MED_GG") ? dr["MED_GG"].ToString() : "";
        //                    med.GROUPID = dtmed.Columns.Contains("GROUPID") ? dr["GROUPID"].ToString() : "";
        //                    med.USAGE = dtmed.Columns.Contains("USAGE") ? dr["USAGE"].ToString() : "";
        //                    med.AUT_NAME = dtmed.Columns.Contains("AUT_NAME") ? dr["AUT_NAME"].ToString() : "";
        //                    med.CAMT = dtmed.Columns.Contains("CAMT") ? dr["CAMT"].ToString() : "";
        //                    med.AUT_NAMEALL = dtmed.Columns.Contains("AUT_NAMEALL") ? dr["AUT_NAMEALL"].ToString() : "";
        //                    med.CAMTALL = dtmed.Columns.Contains("CAMTALL") ? dr["CAMTALL"].ToString() : "";
        //                    med.TIMES = dtmed.Columns.Contains("TIMES") ? dr["TIMES"].ToString() : "";
        //                    med.PRICE = dtmed.Columns.Contains("PRICE") ? dr["PRICE"].ToString() : "";
        //                    med.AMOUNT = dtmed.Columns.Contains("AMOUNT") ? dr["AMOUNT"].ToString() : "";
        //                    med.YB_CODE = dtmed.Columns.Contains("YB_CODE") ? dr["YB_CODE"].ToString() : "";
        //                    med.YB_CODE_GJM = dtmed.Columns.Contains("YB_CODE_GJM") ? dr["YB_CODE_GJM"].ToString() : "";
        //                    med.IS_QX = dtmed.Columns.Contains("IS_QX") ? dr["IS_QX"].ToString() : "";
        //                    med.MINAUT_FLAG = dtmed.Columns.Contains("MINAUT_FLAG") ? dr["MINAUT_FLAG"].ToString() : "";
        //                    _out.DAMEDLIST.Add(med);
        //                }
        //            }
        //            catch
        //            {
        //            }
        //            try
        //            {
        //                DataTable dtchkt = ds.Tables["DACHKT"];
        //                _out.DACHKTLIST = new List<GETOUTFEENOPAYMX_M.CHKT>();
        //                foreach (DataRow dr in dtchkt.Rows)
        //                {
        //                    GETOUTFEENOPAYMX_M.CHKT chkt = new GETOUTFEENOPAYMX_M.CHKT();
        //                    chkt.DATIME = dtchkt.Columns.Contains("DATIME") ? dr["DATIME"].ToString() : "";
        //                    chkt.DAID = dtchkt.Columns.Contains("DAID") ? dr["DAID"].ToString() : "";
        //                    chkt.CHKIT_ID = dtchkt.Columns.Contains("CHKIT_ID") ? dr["CHKIT_ID"].ToString() : "";
        //                    chkt.CHKIT_NAME = dtchkt.Columns.Contains("CHKIT_NAME") ? dr["CHKIT_NAME"].ToString() : "";
        //                    chkt.AUT_NAME = dtchkt.Columns.Contains("AUT_NAME") ? dr["AUT_NAME"].ToString() : "";
        //                    chkt.CAMTALL = dtchkt.Columns.Contains("CAMTALL") ? dr["CAMTALL"].ToString() : "";
        //                    chkt.PRICE = dtchkt.Columns.Contains("PRICE") ? dr["PRICE"].ToString() : "";
        //                    chkt.AMOUNT = dtchkt.Columns.Contains("AMOUNT") ? dr["AMOUNT"].ToString() : "";
        //                    chkt.YB_CODE = dtchkt.Columns.Contains("YB_CODE") ? dr["YB_CODE"].ToString() : "";
        //                    chkt.YB_CODE_GJM = dtchkt.Columns.Contains("YB_CODE_GJM") ? dr["YB_CODE_GJM"].ToString() : "";
        //                    chkt.IS_QX = dtchkt.Columns.Contains("IS_QX") ? dr["IS_QX"].ToString() : "";
        //                    chkt.MINAUT_FLAG = dtchkt.Columns.Contains("MINAUT_FLAG") ? dr["MINAUT_FLAG"].ToString() : "";
        //                    chkt.FEE_TYPE = dtchkt.Columns.Contains("FEE_TYPE") ? dr["FEE_TYPE"].ToString() : "";
        //                    _out.DACHKTLIST.Add(chkt);
        //                }
        //            }
        //            catch
        //            {
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