using System;
using System.Collections.Generic;
using CommonModel;
using Newtonsoft.Json;
namespace ZZJ_InHos.BUS
{
    /// <summary>
    /// 出院预结算
    /// </summary>
    internal class JZHOUTYJS
    {
        public static string B_JZHOUTYJS(string json_in)
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
                string out_data = GlobalVar.CallOtherBus(json_in, FormatHelper.GetStr(dic["HOS_ID"]), "ZZJ_InHos", "0007").BusData;
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
        //public static string B_JZHOUTYJS_b(string json_in)
        //{
        //    DataReturn dataReturn = new DataReturn();
        //    string json_out = "";
        //    try
        //    {
        //        JZHOUTYJS_M.JZHOUTYJS_IN _in = JsonConvert.DeserializeObject<JZHOUTYJS_M.JZHOUTYJS_IN>(json_in);
        //        JZHOUTYJS_M.JZHOUTYJS_OUT _out = new JZHOUTYJS_M.JZHOUTYJS_OUT();
        //        XmlDocument doc = QHXmlMode.GetBaseXml("JZHOUTYJS", "1");
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", string.IsNullOrEmpty(_in.HOS_ID) ? "" : _in.HOS_ID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", string.IsNullOrEmpty(_in.LTERMINAL_SN) ? "" : _in.LTERMINAL_SN.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "USER_ID", string.IsNullOrEmpty(_in.USER_ID) ? "" : _in.USER_ID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_PAT_ID", string.IsNullOrEmpty(_in.HOS_PAT_ID) ? "" : _in.HOS_PAT_ID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_NO", string.IsNullOrEmpty(_in.HOS_NO) ? "" : _in.HOS_NO.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_ID", string.IsNullOrEmpty(_in.HOS_PAT_ID) ? "" : _in.HOS_PAT_ID.Trim());

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
        //            _out.HIN_DATE = dtrev.Columns.Contains("HIN_DATE") ? dtrev.Rows[0]["HIN_DATE"].ToString().Replace(".","-") : "";
        //            _out.HOUT_DATE = dtrev.Columns.Contains("HOUT_DATE") ? dtrev.Rows[0]["HOUT_DATE"].ToString().Replace(".", "-") : "";
        //            _out.HIN_DAYS = dtrev.Columns.Contains("HIN_DAYS") ? dtrev.Rows[0]["HIN_DAYS"].ToString() : "";
        //            _out.JE_ALL = dtrev.Columns.Contains("ZFY") ? dtrev.Rows[0]["ZFY"].ToString() : "";
        //            _out.JE_YJJ = dtrev.Columns.Contains("YJJ") ? dtrev.Rows[0]["YJJ"].ToString() : "";
        //            _out.JE_REMAIN = dtrev.Columns.Contains("refund_JE") ? dtrev.Rows[0]["refund_JE"].ToString() : "";
        //            _out.DJ_ID = dtrev.Columns.Contains("DJ_ID") ? dtrev.Rows[0]["DJ_ID"].ToString() : "";
        //            _out.DJ_NO = dtrev.Columns.Contains("DJ_NO") ? dtrev.Rows[0]["DJ_NO"].ToString() : "";
        //            _out.YB_PAY = dtrev.Columns.Contains("YB_PAY") ? dtrev.Rows[0]["YB_PAY"].ToString() : "";
        //            _out.CASH_JE = dtrev.Columns.Contains("CASH_JE") ? dtrev.Rows[0]["CASH_JE"].ToString() : _out.JE_REMAIN;
        //            _out.TC_JE = dtrev.Columns.Contains("TC_JE") ? dtrev.Rows[0]["TC_JE"].ToString() : "";
        //            _out.DB_JE = dtrev.Columns.Contains("DB_JE") ? dtrev.Rows[0]["DB_JE"].ToString() : "";
        //            _out.ZH_JE = dtrev.Columns.Contains("ZH_JE") ? dtrev.Rows[0]["ZH_JE"].ToString() : "";
        //            _out.MZBZ_JE = dtrev.Columns.Contains("MZBZ_JE") ? dtrev.Rows[0]["MZBZ_JE"].ToString() : "";
        //            _out.GRZL_JE = dtrev.Columns.Contains("GRZL_JE") ? dtrev.Rows[0]["GRZL_JE"].ToString() : "";
        //            _out.YBPAY_MX = dtrev.Columns.Contains("YBPAY_MX") ? dtrev.Rows[0]["YBPAY_MX"].ToString() : "";

        //            DataTable dtinpatf2 = new DataTable();
        //            try
        //            {
        //                dtinpatf2 = ds.Tables["INPATF2"];
        //            }
        //            catch
        //            {
        //                dataReturn.Code = 5;
        //                dataReturn.Msg = "解析HIS出参失败,未找到INPATF2节点,请检查HIS出参";
        //                goto EndPoint;
        //            }
        //            _out.FEELIST = new List<JZHOUTYJS_M.FEE>();
        //            foreach (DataRow dr in dtinpatf2.Rows)
        //            {
        //                JZHOUTYJS_M.FEE fee = new JZHOUTYJS_M.FEE();
        //                fee.JE = dtinpatf2.Columns.Contains("JE") ? dr["JE"].ToString() : "";
        //                fee.FEE_NOTE= dtinpatf2.Columns.Contains("CODE_NAME") ? dr["CODE_NAME"].ToString() : "";
        //                _out.FEELIST.Add(fee);
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