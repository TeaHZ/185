using System;
using System.Collections.Generic;
using CommonModel;
using Newtonsoft.Json;
namespace ZZJ_InHos.BUS
{
    internal class SAVEINPATYJJ
    {
        public static string B_SAVEINPATYJJ(string json_in)
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
                string out_data = GlobalVar.CallOtherBus(json_in, FormatHelper.GetStr(dic["HOS_ID"]), "ZZJ_InHos", "0004").BusData;
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
        //public static string B_SAVEINPATYJJ_b(string json_in)
        //{
        //    DataReturn dataReturn = new DataReturn();
        //    string json_out = "";
        //    try
        //    {
        //        SAVEINPATYJJ_M.SAVEINPATYJJ_IN _in = JsonConvert.DeserializeObject<SAVEINPATYJJ_M.SAVEINPATYJJ_IN>(json_in);
        //        SAVEINPATYJJ_M.SAVEINPATYJJ_OUT _out = new SAVEINPATYJJ_M.SAVEINPATYJJ_OUT();

        //        string his_rtnxml = "";
        //        if (GlobalVar.DoBussiness == "0")
        //        {
        //            XmlDocument doc = QHXmlMode.GetBaseXml("SAVEINPATYJJ", "1");
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", string.IsNullOrEmpty(_in.HOS_ID) ? "" : _in.HOS_ID.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", string.IsNullOrEmpty(_in.LTERMINAL_SN) ? "" : _in.LTERMINAL_SN.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "USER_ID", string.IsNullOrEmpty(_in.USER_ID) ? "" : _in.USER_ID.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_NO", string.IsNullOrEmpty(_in.HOS_NO) ? "" : _in.HOS_NO.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_PAT_ID", string.IsNullOrEmpty(_in.HOS_PAT_ID) ? "" : _in.HOS_PAT_ID.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CASH_JE", string.IsNullOrEmpty(_in.CASH_JE) ? "" : _in.CASH_JE.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TYPE", string.IsNullOrEmpty(_in.DEAL_TYPE) ? "" : _in.DEAL_TYPE.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_STATES", string.IsNullOrEmpty(_in.DEAL_STATES) ? "" : _in.DEAL_STATES.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TIME", string.IsNullOrEmpty(_in.DEAL_TIME) ? "" : _in.DEAL_TIME.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "QUERYID", string.IsNullOrEmpty(_in.QUERYID) ? "" : _in.QUERYID.Trim());
        //            string inxml = doc.InnerXml;
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
        //                    goto EndPoint;
        //                }
        //                _out.HOS_PAT_ID = dtrev.Columns.Contains("HOS_PAT_ID") ? dtrev.Rows[0]["HOS_PAT_ID"].ToString() : "";
        //                _out.JE_PAY = dtrev.Columns.Contains("JE_PAY") ? dtrev.Rows[0]["JE_PAY"].ToString() : "";
        //                _out.JE_REMAIN = dtrev.Columns.Contains("JE_REMAIN") ? dtrev.Rows[0]["JE_REMAIN"].ToString() : "";
        //                _out.CASH_JE = dtrev.Columns.Contains("CASH_JE") ? dtrev.Rows[0]["CASH_JE"].ToString() : "";
        //                _out.HOS_PAY_SN = dtrev.Columns.Contains("HOS_PAY_SN") ? dtrev.Rows[0]["HOS_PAY_SN"].ToString() : "";
        //                _out.CASH_JE = dtrev.Columns.Contains("CASH_JE") ? dtrev.Rows[0]["CASH_JE"].ToString() : "";
        //                _out.HOS_PAY_SN = dtrev.Columns.Contains("HOS_PAY_SN") ? dtrev.Rows[0]["HOS_PAY_SN"].ToString() : "";

        //                dataReturn.Code = 0;
        //                dataReturn.Msg = "SUCCESS";
        //                dataReturn.Param =   JsonConvert.SerializeObject(_out);

        //            }
        //            catch (Exception ex)
        //            {
        //                dataReturn.Code = 5;
        //                dataReturn.Msg = "解析HIS出参失败,请检查HIS出参是否正确";
        //                goto EndPoint;
        //            }
        //        }
        //        else if (GlobalVar.DoBussiness == "1")
        //        {
        //            DataTable dtpat = new Plat.BLL.BaseFunction().GetList_ZZJ("pat_info", "SFZ_NO='" +_in.SFZ_NO + "'", "PAT_ID", "PAT_NAME");
        //            if (dtpat.Rows.Count == 0)
        //            {
        //                dataReturn.Code = 1;
        //                dataReturn.Msg = "病人还未建档";
        //                goto EndPoint;
        //            }
        //            string PAT_NAME = dtpat.Rows[0]["PAT_NAME"].ToString().Trim();
        //            int PAT_ID = int.Parse(dtpat.Rows[0]["PAT_ID"].ToString().Trim());
        //            DataTable dtpat_card = new Plat.BLL.BaseFunction().GetList_ZZJ("pat_card_bind", "PAT_ID='" + PAT_ID + "' and YLCARTD_TYPE='4'", "YLCARD_NO");
        //            if (dtpat_card.Rows.Count == 0)
        //            {
        //                Plat.Model.pat_card_bind bind = new Plat.Model.pat_card_bind();
        //                bind.HOS_ID = _in.HOS_ID;
        //                bind.PAT_ID = Convert.ToInt32(PAT_ID);
        //                bind.YLCARTD_TYPE = Convert.ToInt32(4); //多次建档
        //                bind.YLCARD_NO =_in.HOS_PAT_ID;
        //                bind.MARK_BIND = 1;
        //                bind.BAND_TIME = DateTime.Now;
        //                new Plat.BLL.pat_card_bind().Add(bind);
        //            }
        //            XmlDocument doc = QHXmlMode.GetBaseXml("SAVEINPATYJJ", "1");
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", string.IsNullOrEmpty(_in.HOS_ID) ? "" : _in.HOS_ID.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "LTERMINAL_SN", string.IsNullOrEmpty(_in.LTERMINAL_SN) ? "" : _in.LTERMINAL_SN.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_NO", string.IsNullOrEmpty(_in.HOS_NO) ? "" : _in.HOS_NO.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_PAT_ID", string.IsNullOrEmpty(_in.HOS_PAT_ID) ? "" : _in.HOS_PAT_ID.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CASH_JE", string.IsNullOrEmpty(_in.CASH_JE) ? "" : _in.CASH_JE.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TYPE", string.IsNullOrEmpty(_in.DEAL_TYPE) ? "" : _in.DEAL_TYPE.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_STATES", string.IsNullOrEmpty(_in.DEAL_STATES) ? "" : _in.DEAL_STATES.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TIME", string.IsNullOrEmpty(_in.DEAL_TIME) ? "" : _in.DEAL_TIME.Trim());
        //            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "QUERYID", string.IsNullOrEmpty(_in.QUERYID) ? "" : _in.QUERYID.Trim());
        //            string inxml = doc.InnerXml;
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
        //                _out.HOS_PAT_ID = dtrev.Columns.Contains("HOS_PAT_ID") ? dtrev.Rows[0]["HOS_PAT_ID"].ToString() : "";
        //                _out.JE_PAY = dtrev.Columns.Contains("JE_PAY") ? dtrev.Rows[0]["JE_PAY"].ToString() : "";
        //                _out.JE_REMAIN = dtrev.Columns.Contains("JE_REMAIN") ? dtrev.Rows[0]["JE_REMAIN"].ToString() : "";
        //                _out.CASH_JE = dtrev.Columns.Contains("CASH_JE") ? dtrev.Rows[0]["CASH_JE"].ToString() : "";
        //                _out.HOS_PAY_SN = dtrev.Columns.Contains("HOS_PAY_SN") ? dtrev.Rows[0]["HOS_PAY_SN"].ToString() : "";
        //                #region 保存业务数据
        //                Plat.Model.pat_prepay modelpat_prepay = new Plat.Model.pat_prepay();
        //                Plat.BLL.pat_prepay BLLpat_prepay = new Plat.BLL.pat_prepay();
        //                int yjj_id = 0;//预约标识
        //                if (!new Plat.BLL.BaseFunction().GetSysIdBase_ZZJ("pat_prepay", out yjj_id))
        //                {
        //                    string s = DateTime.Now.Ticks.ToString();
        //                    yjj_id = Convert.ToInt32(s.Substring(s.Length - 6));
        //                }
        //                #endregion
        //                dataReturn.Code = 0;
        //                dataReturn.Msg = "SUCCESS";
        //                dataReturn.Param =   JsonConvert.SerializeObject(_out);
        //                modelpat_prepay.PAY_ID = yjj_id;
        //                modelpat_prepay.HOS_ID = _in.HOS_ID;
        //                modelpat_prepay.HOS_PAT_ID = _in.HOS_PAT_ID;
        //                modelpat_prepay.HOS_PAY_SN = dtrev.Rows[0]["HOS_PAY_SN"].ToString().Trim();
        //                modelpat_prepay.PAT_ID = PAT_ID;
        //                modelpat_prepay.USER_ID = _in.USER_ID;
        //                modelpat_prepay.CASH_JE =FormatHelper.GetDecimal(_in.CASH_JE);
        //                modelpat_prepay.DJ_TIME = DateTime.Parse(_in.DEAL_TIME);
        //                modelpat_prepay.lTERMINAL_SN = _in.LTERMINAL_SN;
        //                //现金交易记录表
        //                Plat.Model.pay_info modelpay_info = new Plat.Model.pay_info();
        //                Plat.BLL.pay_info BLLpay_info = new Plat.BLL.pay_info();

        //                int pinfo_id = 0;
        //                if (!new Plat.BLL.BaseFunction().GetSysIdBase("pay_info", out pinfo_id))
        //                {
        //                    string s = DateTime.Now.Ticks.ToString();
        //                    pinfo_id = Convert.ToInt32(s.Substring(s.Length - 6));
        //                }
        //                //现金交易记录表
        //                modelpay_info.PAT_ID = PAT_ID;
        //                modelpay_info.PAY_ID = pinfo_id.ToString();
        //                modelpay_info.HOS_ID = _in.HOS_ID;
        //                modelpay_info.USER_ID = _in.USER_ID;
        //                modelpay_info.BIZ_TYPE = 3;
        //                modelpay_info.BIZ_SN = yjj_id.ToString();
        //                modelpay_info.CASH_JE =FormatHelper.GetDecimal(_in.CASH_JE);
        //                modelpay_info.SFZ_NO = _in.SFZ_NO;
        //                modelpay_info.DJ_DATE = _in.DEAL_TIME.Substring(0, 10);
        //                modelpay_info.DJ_TIME = _in.DEAL_TIME.Substring(11, 8);
        //                modelpay_info.DEAL_TYPE = _in.DEAL_TYPE;
        //                modelpay_info.DEAL_STATES = _in.DEAL_STATES;
        //                modelpay_info.lTERMINAL_SN = _in.LTERMINAL_SN;
        //                //支付宝
        //                Plat.Model.pay_info_zfb modelpay_info_zfb = null;
        //                if (_in.DEAL_TYPE == "2")
        //                {
        //                    DataTable dtTran = new Plat.BLL.BaseFunction().GetList_ZZJ("ifps_tran", "comm_sn='" + _in.QUERYID + "'", "MerId");
        //                    modelpay_info_zfb = new Plat.Model.pay_info_zfb();
        //                    modelpay_info_zfb.PAY_ID = pinfo_id.ToString();
        //                    modelpay_info_zfb.SELLER_ID = dtTran.Rows.Count > 0 ? dtTran.Rows[0]["MerId"].ToString().Trim() : "";
        //                    modelpay_info_zfb.BIZ_TYPE = 3;
        //                    modelpay_info_zfb.BIZ_SN = yjj_id.ToString();
        //                    modelpay_info_zfb.JE = FormatHelper.GetDecimal(_in.CASH_JE);
        //                    modelpay_info_zfb.DEAL_STATES = _in.DEAL_STATES;
        //                    modelpay_info_zfb.DEAL_TIME = DateTime.Parse(_in.DEAL_TIME);
        //                    modelpay_info_zfb.COMM_SN = _in.QUERYID;
        //                    modelpay_info_zfb.lTERMINAL_SN = _in.LTERMINAL_SN;
        //                    modelpay_info_zfb.TXN_TYPE = "01";
        //                }
        //                //微信
        //                Plat.Model.pay_info_wc modelpay_info_wc = null;
        //                if (_in.DEAL_TYPE == "1")
        //                {
        //                    DataTable dtTran = new Plat.BLL.BaseFunction().GetList_ZZJ("ifps_tran", "comm_sn='" + _in.QUERYID + "'", "MerId", "transaction_id");

        //                    modelpay_info_wc = new Plat.Model.pay_info_wc();
        //                    modelpay_info_wc.PAY_ID = pinfo_id.ToString();
        //                    modelpay_info_wc.WECHAT = "";
        //                    modelpay_info_wc.PAY_TYPE = "";
        //                    modelpay_info_wc.BIZ_TYPE = 1;
        //                    modelpay_info_wc.BIZ_SN = yjj_id.ToString();
        //                    modelpay_info_wc.COMM_SN = _in.QUERYID;
        //                    modelpay_info_wc.JE =FormatHelper.GetDecimal(_in.CASH_JE);
        //                    modelpay_info_wc.COMM_NAME = dtTran.Rows.Count > 0 ? dtTran.Rows[0]["MerId"].ToString().Trim() : "";
        //                    modelpay_info_wc.DEAL_STATES = "2";
        //                    modelpay_info_wc.DEAL_TIME = DateTime.Now;
        //                    modelpay_info_wc.DEAL_SN = dtTran.Rows.Count > 0 ? dtTran.Rows[0]["transaction_id"].ToString().Trim() : "";
        //                    modelpay_info_wc.TNX_TYPE = "01";
        //                    modelpay_info_wc.lTERMINAL_SN = _in.LTERMINAL_SN;
        //                }
        //                if (!BLLpat_prepay.AddByTran_ZZJ(modelpat_prepay, modelpay_info, modelpay_info_zfb, modelpay_info_wc, null, null, null))
        //                {
        //                }

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
        //EndPoint:
        //    json_out =   JsonConvert.SerializeObject(dataReturn);
        //    return json_out;

        //}
        #endregion
    }
}