using Alipay.AopSdk.Core.Response;
using Alipay.AopSdk.F2FPay.Business;
using Alipay.AopSdk.F2FPay.Domain;
using CommonModel;
using Newtonsoft.Json;
using Soft.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using ZZJ_Tran.Model;

namespace ZZJ_Tran.BUS
{
    internal class GETQRCODE
    {
        public static string B_GETQRCODE(string json_in)
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
                string out_data = GlobalVar.CallOtherBus(json_in, FormatHelper.GetStr(dic["HOS_ID"]), "ZZJ_Tran", "0001").BusData;
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

        public static string B_GETQRCODE_b(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                GETQRCODE_M.GETQRCODE_IN _in = JsonConvert.DeserializeObject<GETQRCODE_M.GETQRCODE_IN>(json_in);
                GETQRCODE_M.GETQRCODE_OUT _out = new GETQRCODE_M.GETQRCODE_OUT();
                string his_rtnxml = "";
                if (GlobalVar.DoBussiness == "0")
                {
                    XmlDocument doc = QHXmlMode.GetBaseXml("GETQRCODE", "1");
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", string.IsNullOrEmpty(_in.HOS_ID) ? "" : _in.HOS_ID.Trim());
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "lTERMINAL_SN", string.IsNullOrEmpty(_in.LTERMINAL_SN) ? "" : _in.LTERMINAL_SN.Trim());
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "USER_ID", string.IsNullOrEmpty(_in.USER_ID) ? "" : _in.USER_ID.Trim());
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CASH_JE", string.IsNullOrEmpty(_in.CASH_JE) ? "" : _in.CASH_JE.Trim());
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEAL_TYPE", string.IsNullOrEmpty(_in.DEAL_TYPE) ? "" : _in.DEAL_TYPE.Trim());
                    XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "TYPE", string.IsNullOrEmpty(_in.TYPE) ? "" : _in.TYPE.Trim());
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
                        _out.QUERYID = dtrev.Columns.Contains("QUERYID") ? FormatHelper.GetStr(dtrev.Rows[0]["QUERYID"]) : "";
                        _out.QRCODE = dtrev.Columns.Contains("QRCODE") ? FormatHelper.GetStr(dtrev.Rows[0]["QRCODE"]) : "";
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
                    string Service_name = _in.DEAL_TYPE == "1" ? "WXPAYPRECREATE" : "ALIPAYPRECREATE";
                    string TYPE_NAME = "";
                    if (_in.TYPE == "0")
                    {
                        TYPE_NAME = "Z预约挂号费" + "|" + _in.SFZ_NO + "|" + _in.LTERMINAL_SN + "&1";
                    }
                    else if (_in.TYPE == "1")
                    {
                        TYPE_NAME = "Z门诊收费" + "|" + _in.SFZ_NO + "|" + _in.LTERMINAL_SN + "&2";
                    }
                    else if (_in.TYPE == "4")
                    {
                        TYPE_NAME = "Z预交金收费" + "|" + _in.SFZ_NO + "|" + _in.LTERMINAL_SN + "&3";
                    }
                    else if (_in.TYPE == "5")
                    {
                        TYPE_NAME = "Z出院结算" + "|" + _in.SFZ_NO + "|" + _in.LTERMINAL_SN + "&4";
                    }
                    else if (_in.TYPE == "2")
                    {
                        TYPE_NAME = "Z病历本" + "|" + _in.SFZ_NO + "|" + _in.LTERMINAL_SN + "&5";
                    }
                    string out_trade_no = NewIdHelper.NewOrderId20 + "-" + _in.HOS_ID;
                    if (GlobalVar.Linux == "0")
                    {
                        if (_in.DEAL_TYPE == "2")//支付宝
                        {
                            AlipayTradePrecreateContentBuilder builder = new AlipayTradePrecreateContentBuilder();
                            builder.out_trade_no = out_trade_no;
                            builder.total_amount = FormatHelper.GetDecimal(_in.CASH_JE).ToString("0.00");
                            builder.discountable_amount = FormatHelper.GetDecimal(_in.CASH_JE).ToString("0.00");
                            builder.undiscountable_amount = "0";
                            builder.operator_id = "F2F";//商户操作员编号
                            builder.store_id = "F2F";//商户门店编号
                            builder.subject = TYPE_NAME;
                            builder.time_expire = System.DateTime.Now.AddMinutes(FormatHelper.GetStr(_in.TIME_EXPIRE) == "" ? 2 : int.Parse(FormatHelper.GetStr(_in.TIME_EXPIRE))).ToString("yyyy-MM-dd HH:mm:ss");
                            builder.body = TYPE_NAME;
                            builder.store_id = "test store id";    //商户门店编号
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
                                    dataReturn.Msg = AESExample.Decrypt(InDataRe.Body, Key);
                                    goto EndPoint;
                                }
                            }
                            AlipayF2FPrecreateResult F2FPayResult = InDataRe.GetBusinessData<AlipayF2FPrecreateResult>(Key);
                            _out.HIS_RTNXML = F2FPayResult.response.Body;
                            if (F2FPayResult.Status.ToString() == "SUCCESS")
                            {
                                #region 保存alipay_tran表数据

                                Plat.Model.alipay_tran tran = new Plat.Model.alipay_tran();
                                tran.batch_no = "";
                                tran.body = TYPE_NAME;
                                tran.buyer_email = "";
                                tran.buyer_id = "";
                                tran.COMM_SN = out_trade_no;
                                tran.COMM_MAIN = out_trade_no;
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
                                tran.subject = TYPE_NAME;
                                tran.trade_code = "01";
                                tran.trade_message = "";
                                tran.TRADE_NO = "";
                                tran.TRADE_STATUS = "";
                                tran.TXN_TYPE = "03";
                                tran.USER_ID = _in.USER_ID;
                                tran.lTERMINAL_SN = _in.LTERMINAL_SN;
                                tran.PAT_NAME = _in.PAT_NAME;
                                tran.SFZ_NO = _in.SFZ_NO;
                                tran.HOSPATID = _in.HOSPATID;
                                new Plat.BLL.alipay_tran().Add(tran);

                                #endregion 保存alipay_tran表数据

                                _out.QRCODE = F2FPayResult.response.QrCode;
                                _out.QUERYID = out_trade_no;
                            }
                            else
                            {
                                dataReturn.Code = 1;
                                dataReturn.Msg = F2FPayResult.response.SubMsg;
                                goto EndPoint;
                            }
                            dataReturn.Code = 0;
                            dataReturn.Msg = "SUCCESS";
                            dataReturn.Param =   JsonConvert.SerializeObject(_out);
                        }
                        else if (_in.DEAL_TYPE == "1")//微信
                        {
                            ZZJ_Tran.F2FPAY.Windows.WxPayTradePrecreateBuilder builder = new ZZJ_Tran.F2FPAY.Windows.WxPayTradePrecreateBuilder();
                            builder.out_trade_no = out_trade_no;
                            builder.total_fee = FormatHelper.GetDecimal(_in.CASH_JE);
                            builder.in_body = TYPE_NAME;
                            builder.time_expire = System.DateTime.Now.AddMinutes(FormatHelper.GetStr(_in.TIME_EXPIRE) == "" ? 2 : int.Parse(FormatHelper.GetStr(_in.TIME_EXPIRE))).ToString("yyyyMMddHHmmss");
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
                                    dataReturn.Msg = AESExample.Decrypt(InDataRe.Body, Key);
                                    goto EndPoint;
                                }
                            }
                            ZZJ_Tran.F2FPAY.Windows.WxPayTradePrecreateBuilder F2FPayResult = InDataRe.GetBusinessData<ZZJ_Tran.F2FPAY.Windows.WxPayTradePrecreateBuilder>(Key);
                            _out.HIS_RTNXML = F2FPayResult.DateRe;
                            if (F2FPayResult.result_code.ToString() == "SUCCESS")
                            {
                                #region 保存wechat_tran表数据

                                try
                                {
                                    Plat.Model.wechat_tran wet_tran = new Plat.Model.wechat_tran();
                                    wet_tran.appid = "";
                                    wet_tran.AT_result_code = "";
                                    wet_tran.AT_TIME = "";
                                    wet_tran.body = TYPE_NAME;
                                    wet_tran.COMM_MAIN = out_trade_no;
                                    wet_tran.COMM_SN = out_trade_no;
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
                                    wet_tran.TXN_TYPE = "03";
                                    wet_tran.USER_ID = _in.USER_ID;
                                    wet_tran.lTERMINAL_SN = _in.LTERMINAL_SN;
                                    wet_tran.PAT_NAME = _in.PAT_NAME;
                                    wet_tran.SFZ_NO = _in.SFZ_NO;
                                    wet_tran.HOSPATID = _in.HOSPATID;
                                    new Plat.BLL.wechat_tran().Add(wet_tran);
                                }
                                catch
                                { }
                                #endregion 保存wechat_tran表数据

                                _out.QRCODE = F2FPayResult.code_url;
                                _out.QUERYID = out_trade_no;
                            }
                            else
                            {
                                dataReturn.Code = 1;
                                dataReturn.Msg = F2FPayResult.return_msg;
                                goto EndPoint;
                            }

                            dataReturn.Code = 0;
                            dataReturn.Msg = "SUCCESS";
                            dataReturn.Param =   JsonConvert.SerializeObject(_out);
                        }
                    }
                    else if (GlobalVar.Linux == "1")
                    {
                        if (_in.DEAL_TYPE == "2")//支付宝
                        {
                            F2FPAY.Linux.AlipayTradePrecreateContentBuilder builder = new F2FPAY.Linux.AlipayTradePrecreateContentBuilder();
                            builder.out_trade_no = out_trade_no;
                            builder.total_amount = FormatHelper.GetDecimal(_in.CASH_JE).ToString("0.00");
                            builder.discountable_amount = FormatHelper.GetDecimal(_in.CASH_JE).ToString("0.00");
                            builder.undiscountable_amount = "0";
                            builder.operator_id = "F2F";//商户门店编号
                            builder.subject = TYPE_NAME;
                            builder.time_expire = System.DateTime.Now.AddMinutes(FormatHelper.GetStr(_in.TIME_EXPIRE) == "" ? 2 : int.Parse(FormatHelper.GetStr(_in.TIME_EXPIRE))).ToString("yyyy-MM-dd HH:mm:ss"); ;
                            builder.body = TYPE_NAME;
                            builder.COMM_HIS = "";
                            builder.store_id = "test store id";    //商户门店编号
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
                                    dataReturn.Msg = AESExample.Decrypt(InDataRe.Body, Key);
                                    goto EndPoint;
                                }
                            }
                            AlipayTradePrecreateResponse F2FPayResult = InDataRe.GetBusinessData<AlipayTradePrecreateResponse>(Key);
                            if (F2FPayResult.Code.ToString() == "10000")
                            {
                                #region 保存alipay_tran数据

                                Plat.Model.alipay_tran tran = new Plat.Model.alipay_tran();
                                tran.batch_no = "";
                                tran.body = TYPE_NAME;
                                tran.buyer_email = "";
                                tran.buyer_id = "";
                                tran.COMM_SN = out_trade_no;
                                tran.COMM_MAIN = out_trade_no;
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
                                tran.subject = TYPE_NAME;
                                tran.trade_code = "01";
                                tran.trade_message = "";
                                tran.TRADE_NO = "";
                                tran.TRADE_STATUS = "";
                                tran.TXN_TYPE = "03";
                                tran.USER_ID = _in.USER_ID;
                                tran.lTERMINAL_SN = _in.LTERMINAL_SN;
                                tran.PAT_NAME = _in.PAT_NAME;
                                tran.SFZ_NO = _in.SFZ_NO;
                                tran.HOSPATID = _in.HOSPATID;
                                new Plat.BLL.alipay_tran().Add(tran);

                                #endregion 保存alipay_tran数据

                                _out.QRCODE = F2FPayResult.QrCode;
                                _out.QUERYID = F2FPayResult.OutTradeNo;
                            }
                            else
                            {
                                dataReturn.Code = 1;
                                dataReturn.Msg = F2FPayResult.SubMsg;
                                goto EndPoint;
                            }
                            dataReturn.Code = 0;
                            dataReturn.Msg = "SUCCESS";
                            dataReturn.Param =   JsonConvert.SerializeObject(_out);
                        }
                        else if (_in.DEAL_TYPE == "1")//微信
                        {
                            F2FPAY.Linux.WxPayTradePrecreateBuilder builder = new F2FPAY.Linux.WxPayTradePrecreateBuilder();
                            builder.out_trade_no = out_trade_no;
                            builder.total_fee = FormatHelper.GetDecimal(_in.CASH_JE);
                            builder.in_body = TYPE_NAME;
                            builder.time_expire = System.DateTime.Now.AddMinutes(FormatHelper.GetStr(_in.TIME_EXPIRE) == "" ? 2 : int.Parse(FormatHelper.GetStr(_in.TIME_EXPIRE))).ToString("yyyyMMddHHmmss"); ;
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
                                    dataReturn.Msg = AESExample.Decrypt(InDataRe.Body, Key);
                                    goto EndPoint;
                                }
                            }
                            F2FPAY.Linux.WxPayTradePrecreateBuilder F2FPayResult = InDataRe.GetBusinessData<ZZJ_Tran.F2FPAY.Linux.WxPayTradePrecreateBuilder>(Key);
                            _out.HIS_RTNXML = F2FPayResult.DateRe;
                            if (F2FPayResult.result_code.ToString() == "SUCCESS")
                            {
                                #region 保存wechat_tran表数据

                                Plat.Model.wechat_tran wet_tran = new Plat.Model.wechat_tran();
                                wet_tran.appid = "";
                                wet_tran.AT_result_code = "";
                                wet_tran.AT_TIME = "";
                                wet_tran.body = TYPE_NAME;
                                wet_tran.COMM_MAIN = out_trade_no;
                                wet_tran.COMM_SN = out_trade_no;
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
                                wet_tran.TXN_TYPE = "03";
                                wet_tran.USER_ID = _in.USER_ID;
                                wet_tran.lTERMINAL_SN = _in.LTERMINAL_SN;
                                wet_tran.PAT_NAME = _in.PAT_NAME;
                                wet_tran.SFZ_NO = _in.SFZ_NO;
                                wet_tran.HOSPATID = _in.HOSPATID;
                                try
                                {
                                    new Plat.BLL.wechat_tran().Add(wet_tran);
                                }
                                catch
                                {
                                }

                                #endregion 保存wechat_tran表数据

                                _out.QRCODE = F2FPayResult.code_url;
                                _out.QUERYID = out_trade_no;
                            }
                            else
                            {
                                dataReturn.Code = 1;
                                dataReturn.Msg = F2FPayResult.return_msg;
                                goto EndPoint;
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