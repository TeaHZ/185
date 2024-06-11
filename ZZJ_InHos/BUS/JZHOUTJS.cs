using System;
using System.Collections.Generic;
using CommonModel;
using Newtonsoft.Json;
namespace ZZJ_InHos.BUS
{
    internal class JZHOUTJS
    {
        public static string B_JZHOUTJS(string json_in)
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
                string out_data = GlobalVar.CallOtherBus(json_in, FormatHelper.GetStr(dic["HOS_ID"]), "ZZJ_InHos", "0008").BusData;
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
        //public static string B_JZHOUTJS_b(string json_in)
        //{
        //    DataReturn dataReturn = new DataReturn();
        //    string json_out = "";
        //    try
        //    {
        //        JZHOUTJS_M.JZHOUTJS_IN _in = JsonConvert.DeserializeObject<JZHOUTJS_M.JZHOUTJS_IN>(json_in);
        //        JZHOUTJS_M.JZHOUTJS_OUT _out = new JZHOUTJS_M.JZHOUTJS_OUT();
        //        XmlDocument doc = QHXmlMode.GetBaseXml("JZHOUTJS", "1");
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", string.IsNullOrEmpty(_in.HOS_ID) ? "" : _in.HOS_ID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", string.IsNullOrEmpty(_in.LTERMINAL_SN) ? "" : _in.LTERMINAL_SN.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "USER_ID", string.IsNullOrEmpty(_in.USER_ID) ? "" : _in.USER_ID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_PAT_ID", string.IsNullOrEmpty(_in.HOS_PAT_ID) ? "" : _in.HOS_PAT_ID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_NO", string.IsNullOrEmpty(_in.HOS_NO) ? "" : _in.HOS_NO.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_ID", string.IsNullOrEmpty(_in.HOS_PAT_ID) ? "" : _in.HOS_PAT_ID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DJ_ID", string.IsNullOrEmpty(_in.DJ_ID) ? "" : _in.DJ_ID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DJ_NO", string.IsNullOrEmpty(_in.DJ_NO) ? "" : _in.DJ_NO.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YJJ", string.IsNullOrEmpty(_in.JE_YJJ) ? "" : _in.JE_YJJ.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "ZFY", string.IsNullOrEmpty(_in.JE_ALL) ? "" : _in.JE_ALL.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YB_JE", string.IsNullOrEmpty(_in.YB_PAY) ? "" : _in.YB_PAY.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "REFUND_JE", string.IsNullOrEmpty(_in.CASH_JE) ? "" : _in.CASH_JE.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TYPE", string.IsNullOrEmpty(_in.DEAL_TYPE) ? "" : _in.DEAL_TYPE.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "QUERYID", string.IsNullOrEmpty(_in.QUERYID) ? "" : _in.QUERYID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "USER_NAME", string.IsNullOrEmpty(_in.LTERMINAL_SN) ? "" : _in.LTERMINAL_SN.Trim());

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
        //            _out.HOS_PAY_SN = dtrev.Columns.Contains("DJ_ID") ? dtrev.Rows[0]["DJ_ID"].ToString() : "";
        //            _out.RCPT_NO = dtrev.Columns.Contains("DJ_NO") ? dtrev.Rows[0]["DJ_NO"].ToString() : "";
        //            try
        //            {
        //                DataTable dtrefund = ds.Tables["REFUND_JE"];
        //                _out.REFUNDLIST = new List<JZHOUTJS_M.REFUND>();
        //                foreach (DataRow dr in dtrefund.Rows)
        //                {
        //                    _out.HOS_PAY_SN = dtrev.Columns.Contains("DJ_ID") ? dtrev.Rows[0]["DJ_ID"].ToString() : "";
        //                    _out.RCPT_NO = dtrev.Columns.Contains("DJ_NO") ? dtrev.Rows[0]["DJ_NO"].ToString() : "";

        //                    JZHOUTJS_M.REFUND refund = new JZHOUTJS_M.REFUND();
        //                    refund.JE = dtrefund.Columns.Contains("JE") ? dr["JE"].ToString() : ""; ;
        //                    refund.QUERYID = dtrefund.Columns.Contains("QUERYID") ? dr["QUERYID"].ToString() : "";
        //                    refund.DEAL_TYPE = dtrefund.Columns.Contains("DEAL_TYPE") ? dr["DEAL_TYPE"].ToString() : "";
        //                    _out.REFUNDLIST.Add(refund);
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