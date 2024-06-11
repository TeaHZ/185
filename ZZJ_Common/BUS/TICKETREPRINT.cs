using CommonModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ZZJ_Common.BUS
{
    internal class TICKETREPRINT
    {
        public static string B_TICKETREPRINT(string json_in)
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
                string out_data = GlobalVar.CallOtherBus(json_in, FormatHelper.GetStr(dic["HOS_ID"]), "ZZJ_Common", "0005").BusData;
                return out_data;
            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
                dataReturn.Param = ex.ToString();
            }
        EndPoint:
            json_out =   JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }

        #region
        //public static string B_TICKETREPRINT_B(string json_in)
        //{
        //    DataReturn dataReturn = new DataReturn();
        //    string json_out = "";
        //    try
        //    {
        //        Model.TICKETREPRINT.TICKETREPRINT_IN _in = JsonConvert.DeserializeObject<Model.TICKETREPRINT.TICKETREPRINT_IN>(json_in);
        //        Model.TICKETREPRINT.TICKETREPRINT_OUT _out = new Model.TICKETREPRINT.TICKETREPRINT_OUT();
        //        XmlDocument doc = new XmlDocument() ;
        //        if (!string.IsNullOrEmpty(_in.TYPE) && _in.TYPE == "3")
        //        {
        //            doc = QHXmlMode.GetBaseXml("TICKETREPRINT", "1");
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", string.IsNullOrEmpty(_in.HOS_ID) ? "" : _in.HOS_ID.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", string.IsNullOrEmpty(_in.LTERMINAL_SN) ? "" : _in.LTERMINAL_SN.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "USER_ID", string.IsNullOrEmpty(_in.USER_ID) ? "" : _in.USER_ID.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "TEXT", string.IsNullOrEmpty(_in.TEXT) ? "" : _in.TEXT.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", string.IsNullOrEmpty(_in.SFZ_NO) ? "" : _in.SFZ_NO.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "TYPE", string.IsNullOrEmpty(_in.TYPE) ? "" : _in.TYPE.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PT_TYPE", string.IsNullOrEmpty(_in.PT_TYPE) ? "" : _in.PT_TYPE.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DJ_ID", string.IsNullOrEmpty(_in.DJ_ID) ? "" : _in.DJ_ID.Trim());
        //        }
        //        else
        //        {
        //            doc = QHXmlMode.GetBaseXml("REPRINTBACK", "1");
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", string.IsNullOrEmpty(_in.HOS_ID) ? "" : _in.HOS_ID.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", string.IsNullOrEmpty(_in.LTERMINAL_SN) ? "" : _in.LTERMINAL_SN.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "USER_ID", string.IsNullOrEmpty(_in.USER_ID) ? "" : _in.USER_ID.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DJ_ID", string.IsNullOrEmpty(_in.DJ_ID) ? "" : _in.DJ_ID.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PT_TYPE", string.IsNullOrEmpty(_in.PT_TYPE) ? "" : _in.PT_TYPE.Trim());
        //        }
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
        //                dataReturn.Msg = dtrev.Rows[0]["CLJG"].ToString();
        //                dataReturn.Param =   JsonConvert.SerializeObject(_out);
        //                goto EndPoint;
        //            }
        //            if (_in.TYPE == "2")
        //            {
        //                DataTable dtitem = new DataTable();
        //                try
        //                {
        //                    dtitem = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/ITEMLIST").Tables[0];
        //                }
        //                catch
        //                {
        //                    dataReturn.Code = 5;
        //                    dataReturn.Msg = "解析出参失败,未找到ITEMLIST节点,请检查出参";
        //                    goto EndPoint;
        //                }
        //                _out.ITEMLIST = new List<Model.TICKETREPRINT.ITEM>();
        //                foreach (DataRow dr in dtitem.Rows)
        //                {
        //                    Model.TICKETREPRINT.ITEM item = new Model.TICKETREPRINT.ITEM();
        //                    item.CAN_PRINT =dtitem.Columns.Contains("CAN_PRINT")?FormatHelper.GetStr(dr["CAN_PRINT"]):"";
        //                    item.DJ_ID = dtitem.Columns.Contains("DJ_ID") ?FormatHelper.GetStr(dr["DJ_ID"]):"";
        //                    item.TEXT = dtitem.Columns.Contains("TEXT") ?FormatHelper.GetStr(dr["TEXT"]):"";
        //                    item.PRINT_TIMES = dtitem.Columns.Contains("PRINT_TIMES") ?FormatHelper.GetStr(dr["PRINT_TIMES"]):"";
        //                    _out.ITEMLIST.Add(item);
        //                }
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