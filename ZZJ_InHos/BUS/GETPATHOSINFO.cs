using CommonModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ZZJ_InHos.BUS
{
    internal class GETPATHOSINFO
    {
        public static string B_GETHOSPATINFO(string json_in)
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
                string out_data = GlobalVar.CallOtherBus(json_in, FormatHelper.GetStr(dic["HOS_ID"]), "ZZJ_InHos", "0002").BusData;
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
        //public static string B_GETHOSPATINFO_b(string json_in)
        //{
        //    DataReturn dataReturn = new DataReturn();
        //    string json_out = "";
        //    try
        //    {
        //        GETPATHOSINFO_M.GETPATHOSINFO_IN _in = JsonConvert.DeserializeObject<GETPATHOSINFO_M.GETPATHOSINFO_IN>(json_in);
        //        GETPATHOSINFO_M.GETPATHOSINFO_OUT _out = new GETPATHOSINFO_M.GETPATHOSINFO_OUT();
        //        XmlDocument doc = QHXmlMode.GetBaseXml("GETPATHOSINFO", "1");
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", string.IsNullOrEmpty(_in.HOS_ID) ? "" : _in.HOS_ID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", string.IsNullOrEmpty(_in.LTERMINAL_SN) ? "" : _in.LTERMINAL_SN.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "USER_ID", string.IsNullOrEmpty(_in.USER_ID) ? "" : _in.USER_ID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_NO", string.IsNullOrEmpty(_in.HOS_NO) ? "" : _in.HOS_NO.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_PAT_ID", string.IsNullOrEmpty(_in.HOS_PAT_ID) ? "" : _in.HOS_PAT_ID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", string.IsNullOrEmpty(_in.SFZ_NO) ? "" : _in.SFZ_NO.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_NAME", string.IsNullOrEmpty(_in.PAT_NAME) ? "" : _in.PAT_NAME.Trim());

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
        //                dataReturn.Msg =dtrev.Columns.Contains("CLJG")?dtrev.Rows[0]["CLJG"].ToString():"";
        //                dataReturn.Param =   JsonConvert.SerializeObject(_out);
        //                goto EndPoint;
        //            }
        //            _out.HOS_NO =_in.HOS_NO ;
        //            _out.HOS_PAT_ID = _in.HOS_PAT_ID;
        //            _out.JE_PAY =dtrev.Columns.Contains("JE_PAY")?dtrev.Rows[0]["JE_PAY"].ToString():"";
        //            _out.JE_YET = dtrev.Columns.Contains("JE_YET") ? dtrev.Rows[0]["JE_YET"].ToString() : "";
        //            _out.JE_REMAIN = dtrev.Columns.Contains("JE_REMAIN") ? dtrev.Rows[0]["JE_REMAIN"].ToString() : "";
        //            _out.CAN_PAY = dtrev.Columns.Contains("CAN_PAY") ? dtrev.Rows[0]["CAN_PAY"].ToString() : "";
        //            _out.PAYLIST = new List<GETPATHOSINFO_M.PAY>();
        //            DataTable dtpay = new DataTable();
        //            try
        //            {
        //                dtpay = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/PAYLIST").Tables[0];
        //            }
        //            catch
        //            {
        //                dataReturn.Code = 5;
        //                dataReturn.Msg = "解析HIS出参失败,未找到PAYLIST节点,请检查HIS出参";
        //                goto EndPoint;
        //            }
        //            foreach (DataRow dr in dtpay.Rows)
        //            {
        //                GETPATHOSINFO_M.PAY pay = new GETPATHOSINFO_M.PAY();
        //                pay.HIN_TIME=dtpay.Columns.Contains("HIN_TIME") ? dr["HIN_TIME"].ToString() : "";
        //                pay.JE_NOTE = dtpay.Columns.Contains("JE_NOTE") ? dr["JE_NOTE"].ToString() : "";
        //                pay.JE = dtpay.Columns.Contains("JE") ? dr["JE"].ToString() : "";
        //                _out.PAYLIST.Add(pay);
        //            }
        //            _out.FEELIST = new List<GETPATHOSINFO_M.FEE>();
        //            DataTable dtfee = new DataTable();
        //            try
        //            {
        //                dtfee = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/FEELIST").Tables[0];
        //            }
        //            catch
        //            {
        //                dataReturn.Code = 5;
        //                dataReturn.Msg = "解析HIS出参失败,未找到FEELIST节点,请检查HIS出参";
        //                goto EndPoint;
        //            }
        //            foreach (DataRow dr in dtfee.Rows)
        //            {
        //                GETPATHOSINFO_M.FEE fee = new GETPATHOSINFO_M.FEE();
        //                fee.FEE_NOTE = dtfee.Columns.Contains("FEE_NOTE") ? dr["FEE_NOTE"].ToString() : "";
        //                fee.JE = dtfee.Columns.Contains("JE") ? dr["JE"].ToString() : "";
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
        //    EndPoint:
        //    json_out =   JsonConvert.SerializeObject(dataReturn);
        //    return json_out;

        //}
        #endregion
    }
}