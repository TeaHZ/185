using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using ZZJ_Tran.Model;
using BusinessInterface;
using CommonModel;
using Newtonsoft.Json;
using PasS.Base.Lib;
using Alipay.AopSdk.F2FPay.Domain;
using Soft.Common;
using Alipay.AopSdk.F2FPay.Business;

namespace ZZJ_Tran.BUS
{
    internal class WXZFBTF
    {
        public static string B_WXZFBTF(string json_in)
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
                if (!dic.ContainsKey("DEAL_TYPE") || FormatHelper.GetStr(dic["DEAL_TYPE"]) == "")
                {
                    dataReturn.Code = ConstData.CodeDefine.Parameter_Define_Out;
                    dataReturn.Msg = "DEAL_TYPE为必传且不能为空";
                    goto EndPoint;
                }
                if (!dic.ContainsKey("CASH_JE") || FormatHelper.GetDecimal(dic["CASH_JE"]) <= 0)
                {
                    dataReturn.Code = ConstData.CodeDefine.Parameter_Define_Out;
                    dataReturn.Msg = "CASH_JE为必传且大于零";
                    goto EndPoint;
                }
                string out_data = GlobalVar.CallOtherBus(json_in, FormatHelper.GetStr(dic["HOS_ID"]), "ZZJ_Tran", "0003").BusData;
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

        public static string B_WXZFBTF_b(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                WXZFBTF_M.WXZFBTF_IN _in = JsonConvert.DeserializeObject<WXZFBTF_M.WXZFBTF_IN>(json_in);
                WXZFBTF_M.WXZFBTF_OUT _out = new WXZFBTF_M.WXZFBTF_OUT();
                string his_rtnxml = "";
                if (GlobalVar.DoBussiness == "0")
                {
                    XmlDocument doc = QHXmlMode.GetBaseXml("WXZFBTF", "1");
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", string.IsNullOrEmpty(_in.HOS_ID) ? "" : _in.HOS_ID.Trim());
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", string.IsNullOrEmpty(_in.LTERMINAL_SN) ? "" : _in.LTERMINAL_SN.Trim());
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "USER_ID", string.IsNullOrEmpty(_in.USER_ID) ? "" : _in.USER_ID.Trim());
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CASH_JE", string.IsNullOrEmpty(_in.CASH_JE) ? "" : _in.CASH_JE.Trim());
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TYPE", string.IsNullOrEmpty(_in.DEAL_TYPE) ? "" : _in.DEAL_TYPE.Trim());
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "QUERYID", string.IsNullOrEmpty(_in.QUERYID) ? "" : _in.QUERYID.Trim());
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "REASON", string.IsNullOrEmpty(_in.REASON) ? "" : _in.REASON.Trim());
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_NAME", string.IsNullOrEmpty(_in.PAT_NAME) ? "" : _in.PAT_NAME.Trim());
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", string.IsNullOrEmpty(_in.SFZ_NO) ? "" : _in.SFZ_NO.Trim());
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "BARCODE", string.IsNullOrEmpty(_in.HOSPATID) ? "" : _in.HOSPATID.Trim());
                    string inxml = doc.InnerXml;

                    if (!GlobalVar.CALLSERVICE(_in.HOS_ID, inxml, ref his_rtnxml))
                    {
                        dataReturn.Code = 1;
                        dataReturn.Msg = his_rtnxml;
                        goto EndPoint;
                    }
                    _out.HIS_RTNXML = his_rtnxml;
                    try
                    {
                        XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(his_rtnxml);
                        DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                        if (dtrev.Rows[0]["CLBZ"].ToString() != "0")
                        {
                            dataReturn.Code = 1;
                            dataReturn.Msg = dtrev.Rows[0]["CLJG"].ToString();
                            dataReturn.Param =   JsonConvert.SerializeObject(_out);
                            goto EndPoint;
                        }
                        _out.STATUS = dtrev.Columns.Contains("STATUS") ? FormatHelper.GetStr(dtrev.Rows[0]["STATUS"]) : "";
                        dataReturn.Code = 0;
                        dataReturn.Msg = "SUCCESS";
                        dataReturn.Param =   JsonConvert.SerializeObject(_out);
                    }
                    catch (Exception ex)
                    {
                        dataReturn.Code = 5;
                        dataReturn.Msg = "解析HIS出参失败,请检查HIS出参是否正确";
                    }
                }
                else if (GlobalVar.DoBussiness == "1")
                {
                    string Key = EncryptionKey.KeyData.AESKEY(_in.HOS_ID);
                    string Service_name = _in.DEAL_TYPE == "1" ? "WXPAYREFUND" : "ALIPAYTradeRefund";
                    if (GlobalVar.Linux == "0")
                    {
                        if (_in.DEAL_TYPE == "2")//支付宝
                        {
                            AlipayTradeRefundContentBuilder builder = new AlipayTradeRefundContentBuilder();
                            builder.out_trade_no = _in.QUERYID;
                            //退款请求单号保持唯一性。
                            builder.out_request_no = NewIdHelper.NewOrderId20 + "-" + _in.HOS_ID;
                            //out_request_no = builder.out_request_no;
                            builder.refund_amount = FormatHelper.GetDecimal(_in.CASH_JE).ToString("0.00");
                            builder.refund_reason = FormatHelper.GetStr(_in.REASON) == "" ? "refund reason" : FormatHelper.GetStr(_in.REASON);
                            InteractiveData InDataRe = GlobalVar.F2FPay(_in.HOS_ID, builder, Service_name);
                            if (InDataRe.Code < 0)
                            {
                                if (InDataRe.Code == -2)
                                {
                                    dataReturn.Code = 1;
                                    dataReturn.Msg = InDataRe.Msg;
                                    goto EndPoint;
                                }
                                else
                                {
                                    dataReturn.Code = 1;
                                    dataReturn.Msg = AESExample.Decrypt(InDataRe.Body, Key); ;
                                    goto EndPoint;
                                }
                            }
                            AlipayF2FRefundResult F2FRefundResult = InDataRe.GetBusinessData<AlipayF2FRefundResult>(Key);
                            _out.HIS_RTNXML = F2FRefundResult.response.Body;
                            if (F2FRefundResult.Status.ToString() == "SUCCESS")
                            {
                                #region 保存alipay_tran表数据
                                Plat.Model.alipay_tran tran = new Plat.BLL.alipay_tran().GetModel(_in.QUERYID);
                                if (tran == null)
                                {
                                    tran = new Plat.Model.alipay_tran();
                                    tran.batch_no = "";
                                    tran.body = "";
                                    tran.buyer_email = "";
                                    tran.buyer_id = "";
                                    tran.COMM_SN = builder.out_request_no;
                                    tran.COMM_MAIN = builder.out_trade_no;
                                    tran.error_code = "";
                                    tran.error_message = "";
                                    tran.gmt_create = DateTime.Now;
                                    tran.gmt_payment = DateTime.Now;
                                    tran.gmt_refund = DateTime.Now;
                                    tran.JE = FormatHelper.GetDecimal(_in.CASH_JE);
                                    tran.notify_id = "";
                                    tran.notify_time = DateTime.Now;
                                    tran.notify_type = "";
                                    tran.payment_type = "3";
                                    tran.refund_status = "";
                                    tran.seller_email = "";
                                    tran.seller_id = "";
                                    tran.subject = "";
                                    tran.trade_code = "01";
                                    tran.trade_message = "";
                                    tran.TRADE_NO = "";
                                    tran.TRADE_STATUS = "";
                                    tran.TXN_TYPE = "02";
                                    tran.USER_ID = _in.USER_ID;
                                    tran.lTERMINAL_SN = _in.LTERMINAL_SN;
                                    tran.PAT_NAME = _in.PAT_NAME;
                                    tran.SFZ_NO = _in.SFZ_NO;
                                    tran.HOSPATID = _in.HOSPATID;
                                }
                                else
                                {
                                    tran.TXN_TYPE = "02";
                                    tran.COMM_MAIN = builder.out_trade_no;
                                    tran.COMM_SN = builder.out_request_no;
                                    tran.USER_ID = _in.USER_ID;
                                    tran.lTERMINAL_SN = _in.LTERMINAL_SN;
                                    tran.PAT_NAME = _in.PAT_NAME;
                                    tran.SFZ_NO = _in.SFZ_NO;
                                    tran.HOSPATID = _in.HOSPATID;
                                }

                                new Plat.BLL.alipay_tran().Add(tran);
                                #endregion 保存alipay_tran表数据
                                _out.STATUS = "1";
                                _out.OUT_REQUEST_NO = builder.out_request_no;
                            }
                            else
                            {
                                _out.STATUS = "0";
                                _out.OUT_REQUEST_NO = builder.out_request_no;
                            }
                            dataReturn.Code = 0;
                            dataReturn.Msg = "SUCCESS";
                            dataReturn.Param =   JsonConvert.SerializeObject(_out);
                        }
                        else if (_in.DEAL_TYPE == "1")//微信
                        {
                            ZZJ_Tran.F2FPAY.Windows.WxPayTradeRefundBuilder builder = new ZZJ_Tran.F2FPAY.Windows.WxPayTradeRefundBuilder();
                            DateTime time_start = DateTime.Now;
                            builder.out_trade_no = _in.QUERYID;
                            builder.out_refund_no = NewIdHelper.NewOrderId20 + "-" + _in.HOS_ID;
                            builder.refund_fee = FormatHelper.GetDecimal(_in.CASH_JE);
                            InteractiveData InDataRe = GlobalVar.F2FPay(_in.HOS_ID, builder, Service_name);
                            if (InDataRe.Code < 0)
                            {
                                if (InDataRe.Code == -2)
                                {
                                    dataReturn.Code = 1;
                                    dataReturn.Msg = InDataRe.Msg;
                                    goto EndPoint;
                                }
                                else
                                {
                                    dataReturn.Code = 1;
                                    dataReturn.Msg = AESExample.Decrypt(InDataRe.Body, Key); ;
                                    goto EndPoint;
                                }
                            }
                            ZZJ_Tran.F2FPAY.Windows.WxPayTradeRefundBuilder F2FPayResult = InDataRe.GetBusinessData<ZZJ_Tran.F2FPAY.Windows.WxPayTradeRefundBuilder>(Key);
                            _out.HIS_RTNXML = F2FPayResult.DateRe;
                            if (F2FPayResult.result_code == "SUCCESS")
                            {
                                #region
                                Plat.Model.wechat_tran wet_tran = new Plat.BLL.wechat_tran().GetModel(_in.QUERYID);
                                if (wet_tran == null)
                                {
                                    wet_tran = new Plat.Model.wechat_tran();
                                    wet_tran.appid = "";
                                    wet_tran.AT_result_code = "";
                                    wet_tran.AT_TIME = "";
                                    wet_tran.body = "";
                                    wet_tran.COMM_MAIN = builder.out_trade_no;
                                    wet_tran.COMM_SN = builder.out_refund_no;
                                    wet_tran.currency_type = "";
                                    wet_tran.device_info = "";
                                    wet_tran.error_code = "";
                                    wet_tran.error_message = "";
                                    wet_tran.JE = FormatHelper.GetDecimal(_in.CASH_JE);
                                    wet_tran.mch_id = "";
                                    wet_tran.nonce_str = "";
                                    wet_tran.openid = "";
                                    wet_tran.prepay_id = "";
                                    wet_tran.refund_channe = "";
                                    wet_tran.refund_recv_accout = "";
                                    wet_tran.request_back_time = DateTime.Now;
                                    wet_tran.return_code = "";
                                    wet_tran.spbill_create_ip = "";
                                    wet_tran.time_end = DateTime.Now;
                                    wet_tran.time_start = DateTime.Now;
                                    wet_tran.trade_code = "01";
                                    wet_tran.trade_message = "";
                                    wet_tran.trade_type = "";
                                    wet_tran.transaction_id = "";
                                    wet_tran.TXN_TYPE = "02";
                                    wet_tran.USER_ID = _in.USER_ID;
                                    wet_tran.lTERMINAL_SN = _in.LTERMINAL_SN;
                                    wet_tran.PAT_NAME = _in.PAT_NAME;
                                    wet_tran.SFZ_NO = _in.SFZ_NO;
                                    wet_tran.HOSPATID = _in.HOSPATID;
                                }
                                else
                                {
                                    wet_tran.TXN_TYPE = "02";
                                    wet_tran.COMM_MAIN = builder.out_trade_no;
                                    wet_tran.COMM_SN = builder.out_refund_no;
                                    wet_tran.USER_ID = _in.USER_ID;
                                    wet_tran.lTERMINAL_SN = _in.LTERMINAL_SN;
                                    wet_tran.PAT_NAME = _in.PAT_NAME;
                                    wet_tran.SFZ_NO = _in.SFZ_NO;
                                    wet_tran.HOSPATID = _in.HOSPATID;
                                }
                                new Plat.BLL.wechat_tran().Add(wet_tran);
                                #endregion
                                _out.STATUS = "1";
                                _out.OUT_REQUEST_NO = builder.out_refund_no;
                            }
                            else
                            {
                                _out.STATUS = "0";
                                _out.OUT_REQUEST_NO = builder.out_refund_no;
                            }
                            dataReturn.Code = 0;
                            dataReturn.Msg = "SUCCESS";
                            dataReturn.Param =   JsonConvert.SerializeObject(_out);
                        }
                    }
                    else
                    {
                        if (_in.DEAL_TYPE == "2")//支付宝
                        {
                            AlipayTradeRefundContentBuilder builder = new AlipayTradeRefundContentBuilder();
                            builder.out_trade_no = _in.QUERYID;
                            //退款请求单号保持唯一性。
                            builder.out_request_no = NewIdHelper.NewOrderId20 + "-" + _in.HOS_ID;
                            //out_request_no = builder.out_request_no;
                            builder.refund_amount = FormatHelper.GetDecimal(_in.CASH_JE).ToString("0.00");
                            builder.refund_reason = FormatHelper.GetStr(_in.REASON) == "" ? "refund reason" : FormatHelper.GetStr(_in.REASON);
                            InteractiveData InDataRe = GlobalVar.F2FPay_Linux(_in.HOS_ID, builder, Service_name);
                            if (InDataRe.Code < 0)
                            {
                                if (InDataRe.Code == -2)
                                {
                                    dataReturn.Code = 1;
                                    dataReturn.Msg = InDataRe.Msg;
                                    goto EndPoint;
                                }
                                else
                                {
                                    dataReturn.Code = 1;
                                    dataReturn.Msg = AESExample.Decrypt(InDataRe.Body, Key); ;
                                    goto EndPoint;
                                }
                            }
                            F2FPAY.Linux.AlipayTradeRefund F2FRefundResult = InDataRe.GetBusinessData<F2FPAY.Linux.AlipayTradeRefund>(Key);
                            _out.HIS_RTNXML = F2FRefundResult.Body;
                            if (F2FRefundResult.Code == "10000")
                            {
                                #region 保存alipay_tran表数据
                                Plat.Model.alipay_tran tran = new Plat.BLL.alipay_tran().GetModel(_in.QUERYID);
                                if (tran == null)
                                {
                                    tran = new Plat.Model.alipay_tran();
                                    tran.batch_no = "";
                                    tran.body = "";
                                    tran.buyer_email = "";
                                    tran.buyer_id = "";
                                    tran.COMM_SN = builder.out_request_no;
                                    tran.COMM_MAIN = builder.out_trade_no;
                                    tran.error_code = "";
                                    tran.error_message = "";
                                    tran.gmt_create = DateTime.Now;
                                    tran.gmt_payment = DateTime.Now;
                                    tran.gmt_refund = DateTime.Now;
                                    tran.JE = FormatHelper.GetDecimal(_in.CASH_JE);
                                    tran.notify_id = "";
                                    tran.notify_time = DateTime.Now;
                                    tran.notify_type = "";
                                    tran.payment_type = "3";
                                    tran.refund_status = "";
                                    tran.seller_email = "";
                                    tran.seller_id = "";
                                    tran.subject = "";
                                    tran.trade_code = "01";
                                    tran.trade_message = "";
                                    tran.TRADE_NO = "";
                                    tran.TRADE_STATUS = "";
                                    tran.TXN_TYPE = "02";
                                    tran.USER_ID = _in.USER_ID;
                                    tran.lTERMINAL_SN = _in.LTERMINAL_SN;
                                    tran.PAT_NAME = _in.PAT_NAME;
                                    tran.SFZ_NO = _in.SFZ_NO;
                                    tran.HOSPATID = _in.HOSPATID;
                                }
                                else
                                {
                                    tran.TXN_TYPE = "02";
                                    tran.COMM_MAIN = builder.out_trade_no;
                                    tran.COMM_SN = builder.out_request_no;
                                    tran.USER_ID = _in.USER_ID;
                                    tran.lTERMINAL_SN = _in.LTERMINAL_SN;
                                    tran.PAT_NAME = _in.PAT_NAME;
                                    tran.SFZ_NO = _in.SFZ_NO;
                                    tran.HOSPATID = _in.HOSPATID;
                                }

                                new Plat.BLL.alipay_tran().Add(tran);
                                #endregion
                                _out.STATUS = "1";
                                _out.OUT_REQUEST_NO = builder.out_request_no;
                            }
                            else
                            {
                                _out.STATUS = "0";
                                _out.OUT_REQUEST_NO = builder.out_request_no;
                            }
                            dataReturn.Code = 0;
                            dataReturn.Msg = "SUCCESS";
                            dataReturn.Param =   JsonConvert.SerializeObject(_out);
                        }
                        else if (_in.DEAL_TYPE == "1")//微信
                        {
                            F2FPAY.Linux.WxPayTradeRefundBuilder builder = new F2FPAY.Linux.WxPayTradeRefundBuilder();
                            DateTime time_start = DateTime.Now;
                            builder.out_trade_no = _in.QUERYID;
                            builder.out_refund_no = NewIdHelper.NewOrderId20 + "-" + _in.HOS_ID;
                            builder.refund_fee = FormatHelper.GetDecimal(_in.CASH_JE);
                            InteractiveData InDataRe = GlobalVar.F2FPay_Linux(_in.HOS_ID, builder, Service_name);
                            if (InDataRe.Code < 0)
                            {
                                if (InDataRe.Code == -2)
                                {
                                    dataReturn.Code = 1;
                                    dataReturn.Msg = InDataRe.Msg;
                                    goto EndPoint;
                                }
                                else
                                {
                                    dataReturn.Code = 1;
                                    dataReturn.Msg = AESExample.Decrypt(InDataRe.Body, Key); ;
                                    goto EndPoint;
                                }
                            }
                            F2FPAY.Linux.WxPayTradeRefundBuilder F2FPayResult = InDataRe.GetBusinessData<F2FPAY.Linux.WxPayTradeRefundBuilder>(Key);
                            _out.HIS_RTNXML = F2FPayResult.DateRe;
                            if (F2FPayResult.result_code == "SUCCESS")
                            {
                                #region
                                Plat.Model.wechat_tran wet_tran = new Plat.BLL.wechat_tran().GetModel(_in.QUERYID);
                                if (wet_tran == null)
                                {
                                    wet_tran = new Plat.Model.wechat_tran();
                                    wet_tran.appid = "";
                                    wet_tran.AT_result_code = "";
                                    wet_tran.AT_TIME = "";
                                    wet_tran.body = "";
                                    wet_tran.COMM_MAIN = builder.out_trade_no;
                                    wet_tran.COMM_SN = builder.out_refund_no;
                                    wet_tran.currency_type = "";
                                    wet_tran.device_info = "";
                                    wet_tran.error_code = "";
                                    wet_tran.error_message = "";
                                    wet_tran.JE = FormatHelper.GetDecimal(_in.CASH_JE);
                                    wet_tran.mch_id = "";
                                    wet_tran.nonce_str = "";
                                    wet_tran.openid = "";
                                    wet_tran.prepay_id = "";
                                    wet_tran.refund_channe = "";
                                    wet_tran.refund_recv_accout = "";
                                    wet_tran.request_back_time = DateTime.Now;
                                    wet_tran.return_code = "";
                                    wet_tran.spbill_create_ip = "";
                                    wet_tran.time_end = DateTime.Now;
                                    wet_tran.time_start = DateTime.Now;
                                    wet_tran.trade_code = "01";
                                    wet_tran.trade_message = "";
                                    wet_tran.trade_type = "";
                                    wet_tran.transaction_id = "";
                                    wet_tran.TXN_TYPE = "02";
                                    wet_tran.USER_ID = _in.USER_ID;
                                    wet_tran.lTERMINAL_SN = _in.LTERMINAL_SN;
                                    wet_tran.PAT_NAME = _in.PAT_NAME;
                                    wet_tran.SFZ_NO = _in.SFZ_NO;
                                    wet_tran.HOSPATID = _in.HOSPATID;
                                }
                                else
                                {
                                    wet_tran.TXN_TYPE = "02";
                                    wet_tran.COMM_MAIN = builder.out_trade_no;
                                    wet_tran.COMM_SN = builder.out_refund_no;
                                    wet_tran.USER_ID = _in.USER_ID;
                                    wet_tran.lTERMINAL_SN = _in.LTERMINAL_SN;
                                    wet_tran.PAT_NAME = _in.PAT_NAME;
                                    wet_tran.SFZ_NO = _in.SFZ_NO;
                                    wet_tran.HOSPATID = _in.HOSPATID;
                                }
                                new Plat.BLL.wechat_tran().Add(wet_tran);
                                #endregion
                                _out.STATUS = "1";
                                _out.OUT_REQUEST_NO = builder.out_refund_no;
                            }
                            else
                            {
                                _out.STATUS = "0";
                                _out.OUT_REQUEST_NO = builder.out_refund_no;
                            }
                            dataReturn.Code = 0;
                            dataReturn.Msg = "SUCCESS";
                            dataReturn.Param =   JsonConvert.SerializeObject(_out);
                        }
                    }
                }
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
    }
}