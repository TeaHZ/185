using CommonModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ZZJ_InHos.BUS
{
    internal class GETHOSDAILY
    {
        public static string B_GETHOSDAILY(string json_in)
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
                string out_data = GlobalVar.CallOtherBus(json_in, FormatHelper.GetStr(dic["HOS_ID"]), "ZZJ_InHos", "0005").BusData;
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
        //public static string B_GETHOSDAILY_b(string json_in)
        //{
        //    DataReturn dataReturn = new DataReturn();
        //    string json_out = "";
        //    try
        //    {
        //        GETHOSDAILY_M.GETHOSDAILY_IN _in = JsonConvert.DeserializeObject<GETHOSDAILY_M.GETHOSDAILY_IN>(json_in);
        //        GETHOSDAILY_M.GETHOSDAILY_OUT _out = new GETHOSDAILY_M.GETHOSDAILY_OUT();
        //        XmlDocument doc = QHXmlMode.GetBaseXml("GETHOSDAILY", "1");
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", string.IsNullOrEmpty(_in.HOS_ID) ? "" : _in.HOS_ID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", string.IsNullOrEmpty(_in.LTERMINAL_SN) ? "" : _in.LTERMINAL_SN.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "USER_ID", string.IsNullOrEmpty(_in.USER_ID) ? "" : _in.USER_ID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_NO", string.IsNullOrEmpty(_in.HOS_NO) ? "" : _in.HOS_NO.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_PAT_ID", string.IsNullOrEmpty(_in.HOS_PAT_ID) ? "" : _in.HOS_PAT_ID.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "BEGIN_DATE", string.IsNullOrEmpty(_in.BEGIN_DATE) ? "" : _in.BEGIN_DATE.Trim());
        //        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "END_DATE", string.IsNullOrEmpty(_in.END_DATE) ? "" : _in.END_DATE.Trim());

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
        //                dataReturn.Param =   JsonConvert.SerializeObject(_out);
        //                goto EndPoint;
        //            }
        //            _out.JE_PAY = dtrev.Columns.Contains("JE_PAY") ? dtrev.Rows[0]["JE_PAY"].ToString() : "";
        //            _out.JE_YET = dtrev.Columns.Contains("JE_YET") ? dtrev.Rows[0]["JE_YET"].ToString() : "";
        //            _out.JE_REMAIN = dtrev.Columns.Contains("JE_REMAIN") ? dtrev.Rows[0]["JE_REMAIN"].ToString() : "";
        //            _out.JE_TODAY = dtrev.Columns.Contains("JE_TODAY") ? dtrev.Rows[0]["JE_TODAY"].ToString() : "";
        //            _out.BIGITEMLIST = new List<GETHOSDAILY_M.BIGITEM>();
        //            DataTable dtbigitem = new DataTable();
        //            try
        //            {
        //                dtbigitem = ds.Tables["BIGITEMLIST"];
        //            }
        //            catch
        //            {
        //                dataReturn.Code = 5;
        //                dataReturn.Msg = "解析HIS出参失败,未找到BIGITEMLIST节点,请检查HIS出参";
        //                goto EndPoint;
        //            }
        //            DataTable dtitem = new DataTable();
        //            try
        //            {
        //                dtitem = ds.Tables["ITEM"];
        //            }
        //            catch
        //            {
        //                dataReturn.Code = 5;
        //                dataReturn.Msg = "解析HIS出参失败,未找到ITEMLIST节点,请检查HIS出参";
        //                goto EndPoint;
        //            }
        //            foreach(DataRow dr in dtbigitem.Rows)
        //            {
        //                GETHOSDAILY_M.BIGITEM bigitem = new GETHOSDAILY_M.BIGITEM();
        //                bigitem.ITEM_NAME = dtbigitem.Columns.Contains("ITEM_NAME") ? FormatHelper.GetStr(dr["ITEM_NAME"]):"";
        //                bigitem.JE_ALL = dtbigitem.Columns.Contains("JE_ALL") ? FormatHelper.GetStr(dr["JE_ALL"]):"";
        //                bigitem.ITEMLIST = new List<GETHOSDAILY_M.ITEM>();
        //                DataRow[] dritem = dtitem.Select("ITEMLIST_Id='" + dr["BIGITEMLIST_Id"] + "'");
        //                for (int j = 0; j < dritem.Length; j++)
        //                {
        //                    GETHOSDAILY_M.ITEM item = new GETHOSDAILY_M.ITEM();
        //                    item.NAME = dtitem.Columns.Contains("ITEM_NAME") ? dritem[j]["ITEM_NAME"].ToString() : "";
        //                    item.GG = dtitem.Columns.Contains("GG") ? dritem[j]["GG"].ToString() : "";
        //                    item.AMOUNT = dtitem.Columns.Contains("AMOUNT") ? dritem[j]["AMOUNT"].ToString() : "";
        //                    item.CAMT = dtitem.Columns.Contains("CAMT") ? dritem[j]["CAMT"].ToString() : "";
        //                    item.JE = dtitem.Columns.Contains("JE") ? dritem[j]["JE"].ToString() : "";
        //                    item.JE_ALL = dtitem.Columns.Contains("JE_ALL") ? dritem[j]["JE_ALL"].ToString() : "";
        //                    item.DJ_DATE = dtitem.Columns.Contains("DJ_DATE") ? dritem[j]["DJ_DATE"].ToString() : "";
        //                    bigitem.ITEMLIST.Add(item);
        //                }
        //                _out.BIGITEMLIST.Add(bigitem);
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