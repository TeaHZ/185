using CommonModel;
using EncryptionKey;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos9_OutHos.HISModels;

using System;
using System.Collections.Generic;

namespace OnlineBusHos9_OutHos.BUS
{
    internal class OUTFEEPAYSAVE
    {
        public static string B_OUTFEEPAYSAVE(string json_in)
        {
            return Business(json_in);
        }

        public static string Business(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                Model.OUTFEEPAYSAVE_M.OUTFEEPAYSAVE_IN _in = JsonConvert.DeserializeObject<Model.OUTFEEPAYSAVE_M.OUTFEEPAYSAVE_IN>(json_in);
                Model.OUTFEEPAYSAVE_M.OUTFEEPAYSAVE_OUT _out = new Model.OUTFEEPAYSAVE_M.OUTFEEPAYSAVE_OUT();
                Dictionary<string, string> dic_filter = PubFunc.Get_Filter(FormatHelper.GetStr(_in.FILTER));

                //读卡方式 1-医保卡 2-电子社保卡 3-人脸识别 4-电子健康卡 5- 电子医保凭证
                string duKaFS = "1";

                switch (_in.MDTRT_CERT_TYPE)
                {
                    case "01":
                        duKaFS = "5";
                        break;

                    case "03":
                        duKaFS = "1";
                        break;

                    default:
                        break;
                }

                string laiyuan = "";
                if (!string.IsNullOrEmpty(_in.QUERYID))
                {
                    string dealflag = _in.QUERYID.Substring(0, 1);
                    //支付方式 1-支付宝 2-微信 3-云闪付 4-银联卡 空-现金
                    switch (dealflag)
                    {
                        case "A":
                            laiyuan = "1";
                            _in.DEAL_TYPE = "2";

                            break;

                        case "W":
                            laiyuan = "2";
                            _in.DEAL_TYPE = "1";

                            break;

                        case "U":
                            laiyuan = "3";
                            _in.DEAL_TYPE = "3";

                            break;

                        default:
                            break;
                    }
                }

                T5119.input input = new T5119.input()
                {
                    liuShuiHao = _in.PRE_NO,//  就诊流水号       同待缴费就诊记录id
                    bingRenXM = "",//  病人姓名        商户流水号，此处为平台支付流水号
                    xingBie = "",// 病人性别
                    zongJinE = _in.JEALL,//    总金额     总金额
                    jiuZhenKH = "",//  就诊卡号        因院内没有就诊卡，可不传
                    hospitalId = "320282466455146",//  医院ID        320282466455146
                    yiBaoJE = _in.MEDFEE_SUMAMT,// 医保金额        医保金额
                    appID = _in.APPID,//  支付平台ID
                    shangHuDDH = _in.QUERYID, //  订单号
                    dingDanHao = _in.DEFRAYNO, // 第三方流水号
                    channelTradeNo = _in.CHANNELTRADENO,
                    yeWuLX = "5119",// 业务类型        业务类型（5119：通知医院结算成功）
                    yiYuanYWLX = "1",//  医院业务类型      医院业务类型，默认为1  ( 1 ：门诊缴费；  2：住 院预缴金充值)
                    jiuZhenKLX = "1",// 就诊卡类型       默认1

                    laiYuan = laiyuan,//支付来源        1 微信 2 支付宝 3 APP 4 医保 5银联商务
                    sheBeiBH = _in.LTERMINAL_SN,//   设备编号        同操作人即可
                    bingRenID = _in.HOSPATID,//  病人ID        病人档案号
                    //作废 jinE = _in.CASH_JE,//   自费金额        自费金额
                    zhengJianHM = _in.SFZ_NO,//证件号码        证件号码
                    duKaFS = duKaFS,// 读卡方式        1-医保卡 2-电子社保卡 3-人脸识别 4-电子健康卡 5-电子 医保凭证6-身份证(脱卡支付时使用)
                    bingRenLX = _in.YLCARD_TYPE == "2" ? "2" : "1",//  病人类型        病人类型（1：自费；2：医保；）
                    ziFeiJE = _in.CASH_JE,//自费金额        自费金额

                    jiuZhenJLID = _in.PRE_NO,// 就诊记录ID
                    caoZuoRId = _in.USER_ID,//  操作人工号
                    yiBaoXX = "",// 医保信息        读医保卡返回的原始信息
                    yiBaoData = "",//  医保交互数据      duKaFS=1时 社会保障卡卡号|pBusiCardInfo duKaFS=5时 电子凭证令牌， duKaFS=6时 医保1101返回的output 内容
                };

                PushServiceResult<T5119.data> result = HerenHelper<T5119.data>.pushService("5119-QHZZJ", JsonConvert.SerializeObject(input));

                if (result.code != 1)
                {
                    dataReturn.Code = 6;
                    dataReturn.Msg = result.msg;
                    if (decimal.Parse(_in.CASH_JE) != 0m)
                    {
                        if (!Paycancel("2", "0", _in.QUERYID, _in.CASH_JE))
                        {
                            dataReturn.Msg += "自动退款失败";
                        }
                    }    
                    return JsonConvert.SerializeObject(dataReturn);
                }

               
                try
                {
                    _out.HOS_PAY_SN = result.data.shouFeiId;
                    _out.HOS_REG_SN = result.data.daoZhenID;
                    _out.RCPT_NO = result.data.shouFeiId ;
                    _out.OPT_SN = _in.QUERYID;
                    _out.RCPT_URL = "https://h5.eheren.com/scan/yixing/invoice?pid=" + _in.HOSPATID;
                    _out.GUID_INFO = result.data.daoZhenXX;

                    if (_in.JEALL != (result.data.zongJinE).ToString() && _in.JEALL == (result.data.ziFeiJE).ToString())
                    {
                        _out.JEALL = (result.data.zongJinE).ToString() + "(临床试验)";//TODO

                        _out.CASH_JE = result.data.ziFeiJE;

                        _out.MEDFEE_SUMAMT = FormatHelper.GetStr(result.data.yiBaoJE);
                        _out.ACCT_PAY = FormatHelper.GetStr(result.data.geRenZHZF);
                        _out.PSN_CASH_PAY = FormatHelper.GetStr(result.data.ziFeiJE) + "(临床试验)";
                        _out.FUND_PAY_SUMAMT = FormatHelper.GetStr(result.data.yiBaoTCZF);
                        _out.OTH_PAY = FormatHelper.GetStr(result.data.qiTaZF);
                        _out.BALC = FormatHelper.GetStr(result.data.zhangHuYE);
                        _out.ACCT_MULAID_PAY = FormatHelper.GetStr(result.data.gongJiJinZF);
                    }
                    else
                    {
                        _out.JEALL = (result.data.zongJinE).ToString();//TODO  _out.JEALL = result.data.zongJinE

                        _out.CASH_JE = result.data.ziFeiJE;

                        _out.MEDFEE_SUMAMT = FormatHelper.GetStr(result.data.yiBaoJE);
                        _out.ACCT_PAY = FormatHelper.GetStr(result.data.geRenZHZF);
                        _out.PSN_CASH_PAY = FormatHelper.GetStr(result.data.ziFeiJE);
                        _out.FUND_PAY_SUMAMT = FormatHelper.GetStr(result.data.yiBaoTCZF);
                        _out.OTH_PAY = FormatHelper.GetStr(result.data.qiTaZF);
                        _out.BALC = FormatHelper.GetStr(result.data.zhangHuYE);
                        _out.ACCT_MULAID_PAY = FormatHelper.GetStr(result.data.gongJiJinZF);
                    }


                    dataReturn.Code = 0;
                    dataReturn.Msg = "SUCCESS";
                    dataReturn.Param = JsonConvert.SerializeObject(_out);

                    #region 平台数据保存

                    try
                    {
                        var db = new DbMySQLZZJ().Client;

                        SqlSugarModel.OptPayLock modelPayLock = db.Queryable<SqlSugarModel.OptPayLock>().Where(t => t.PAY_ID == _in.PAY_ID).Single();
                        SqlSugarModel.OptPay Modelopt_pay = new SqlSugarModel.OptPay();

                        Modelopt_pay.PAY_ID = _in.PAY_ID;
                        Modelopt_pay.HOS_ID = _in.HOS_ID;
                        Modelopt_pay.PAT_ID = modelPayLock.PAT_ID;
                        Modelopt_pay.PAT_NAME = modelPayLock.PAT_NAME;
                        Modelopt_pay.SFZ_NO = modelPayLock.SFZ_NO;
                        Modelopt_pay.YLCARD_TYPE = FormatHelper.GetInt(modelPayLock.YLCARD_TYPE);
                        Modelopt_pay.YLCARD_NO = modelPayLock.YLCARD_NO;
                        Modelopt_pay.HOSPATID = _in.HOSPATID;

                        Modelopt_pay.DEPT_CODE = modelPayLock.DEPT_CODE;
                        Modelopt_pay.DEPT_NAME = modelPayLock.DEPT_NAME;
                        Modelopt_pay.DOC_NO = modelPayLock.DOC_NO;
                        Modelopt_pay.DOC_NAME = modelPayLock.DOC_NAME;
                        Modelopt_pay.CHARGE_TYPE = "";
                        Modelopt_pay.QUERYID = _in.QUERYID;
                        Modelopt_pay.DEAL_TYPE = _in.DEAL_TYPE;
                        Modelopt_pay.SUM_JE = modelPayLock.SUM_JE;
                        Modelopt_pay.CASH_JE = FormatHelper.GetDecimal(_in.CASH_JE);
                        Modelopt_pay.ACCT_JE = 0;
                        Modelopt_pay.FUND_JE = Modelopt_pay.SUM_JE - Modelopt_pay.CASH_JE;
                        Modelopt_pay.OTHER_JE = 0;
                        Modelopt_pay.HOS_REG_SN = _out.HOS_PAY_SN;
                        Modelopt_pay.HOS_SN = _in.HOS_SN;
                        Modelopt_pay.OPT_SN = _out.OPT_SN;
                        Modelopt_pay.PRE_NO = _in.PRE_NO;
                        Modelopt_pay.RCPT_NO = _out.RCPT_NO;
                        Modelopt_pay.HOS_PAY_SN = _out.HOS_PAY_SN;
                        Modelopt_pay.DJ_DATE = DateTime.Now.Date;
                        Modelopt_pay.DJ_TIME = DateTime.Now.ToString("HH:mm:ss");

                        Modelopt_pay.USER_ID = _in.USER_ID;
                        Modelopt_pay.lTERMINAL_SN = _in.LTERMINAL_SN;
                        Modelopt_pay.SOURCE = "ZZJ";

                        try
                        {
                            db.BeginTran();
                            List<SqlSugarModel.OptPayMx> Modelopt_pay_mxs = db.Queryable<SqlSugarModel.OptPayMx>().AS("opt_pay_mx_lock").Where(t => t.PAY_ID == _in.PAY_ID).ToList();

                            db.Insertable(Modelopt_pay).ExecuteCommand();
                            db.Insertable<SqlSugarModel.OptPayMx>(Modelopt_pay_mxs).ExecuteCommand();
                            db.CommitTran();
                        }
                        catch (Exception ex)
                        {
                            db.RollbackTran();

                            SqlSugarModel.Sqlerror sqlerror = new SqlSugarModel.Sqlerror();
                            sqlerror.TYPE = "OUTFEEPAYSAVE";
                            sqlerror.Exception = ex.Message;
                            sqlerror.DateTime = DateTime.Now;
                            LogHelper.SaveSqlerror(sqlerror);
                        }
                    }
                    catch (Exception ex)
                    {
                        SqlSugarModel.Sqlerror sqlerror = new SqlSugarModel.Sqlerror();
                        sqlerror.TYPE = "OUTFEEPAYSAVE";
                        sqlerror.Exception = ex.Message;
                        sqlerror.DateTime = DateTime.Now;
                        LogHelper.SaveSqlerror(sqlerror);
                    }

                    #endregion 平台数据保存
                }
                catch (Exception ex)
                {
                    dataReturn.Code = 5;
                    dataReturn.Msg = "解析HIS出参失败,请检查HIS出参是否正确";
                    dataReturn.Param = ex.Message;
                }
            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
                dataReturn.Param = ex.Message;
            }
        EndPoint:
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }

        private static bool Paycancel(string op, string type, string queryid, string cash_je)
        {
            string appId = "2022081925029210";
            string key = "539130e1b1fdb52093bb072a67e3c62a";

            if (type == "4" || type == "5")
            {
                appId = "2022081925029211";
            }
            string Refundid = "R" + NewIdHelper.NewOrderId20 + "-9";

            JObject jobj = new JObject();
            jobj.Add("appTradeNo", queryid);

            if (op == "2")
            {
                jobj.Add("appRefundNo", Refundid);
                jobj.Add("refundFee", cash_je);//    Price   10  退款金额
                jobj.Add("returnUrl", "");//    String  200 同步返回页面
                jobj.Add("refundReason", "正常退款");//   String  200 退款原因
            }

            string bizcontentJson = JsonConvert.SerializeObject(jobj);

            if (op == "1")
            {
                string signPlain = appId + queryid + key;
                string sign = MD5Helper.Md5(signPlain).ToLower();

                Dictionary<string, string> @params = new Dictionary<string, string>
                    {
                        { "appId", appId },
                        { "method", "uniform.trade.cancel" },
                        { "sign", sign },
                        { "bizContent", bizcontentJson }
                    };

                var response = HerenHelper<string>.SendTrade(@params);

                JObject jrtn = JObject.Parse(response.Result);

                if (jrtn["status"].ToString() != "CANCLED_SUCCESS")
                {
                    return false;
                }
            }
            else if (op == "2")
            {
                string signPlain = appId + queryid + Refundid + cash_je + key;
                string sign = MD5Helper.Md5(signPlain).ToLower();

                Dictionary<string, string> @params = new Dictionary<string, string>
                    {
                        { "appId", appId },
                        { "method", "uniform.trade.refund" },
                        { "sign", sign },

                        { "bizContent", bizcontentJson }
                    };

                var response = HerenHelper<string>.SendTrade(@params);

                JObject jrtn = JObject.Parse(response.Result);

                if (jrtn["status"].ToString() != "REFUND_SUCCESS")
                {
                    return false;
                }
            }

            return true;
        }
    }
}