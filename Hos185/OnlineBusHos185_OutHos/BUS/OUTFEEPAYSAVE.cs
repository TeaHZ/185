using CommonModel;
using Hos185_His.Models;
using Hos185_His.Models.OriginPay;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBusHos185_OutHos.BUS
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

                try
                {
                    Hos185_His.Models.MZ.MedicareInfo medicareInfo = new Hos185_His.Models.MZ.MedicareInfo()
                    {
                        balanceInParam = "", //医保结算⼊参
                        balanceNo = "", //医保结算单据号
                        balanceOutParam = "", //医保结算出参
                        patientDiseaseNo = "", //医保特殊病⼈疾病编码
                        patientType = "", //医保特殊病⼈类型例:⻔慢、⻔特等
                        preBalanceInParam = "", //医保预结算⼊参
                        preBalanceOutParam = "", //医保预结算出参
                        readCardOut = "", //医保读卡出参
                        registerInParam = "", //医保登记⼊参
                        registerNo = "", //医保登记号
                        registerOutParam = "" //医保登记出参
                    };

                    string transSerialNo = "";

                    if (decimal.Parse(_in.CASH_JE) != 0m)
                    {
                        #region 获取源启支付流水号

                        var db = new DbMySQLZZJ().Client;

                        var Hoshmpay = db.Queryable<SqlSugarModel.Hoshmpay>().Where(x => x.COMM_MAIN == _in.QUERYID).Where(x => x.TXN_TYPE == "01").ToList().FirstOrDefault();

                        if (Hoshmpay == null)
                        {
                            dataReturn.Code = 8;
                            dataReturn.Msg = "未能获取到有效的支付数据，请联系工作人员";

                            return JsonConvert.SerializeObject(dataReturn);
                        }

                        #endregion 获取源启支付流水号

                        transSerialNo = Hoshmpay.ThirdTradeNo;
                    }
                    string medicareParam = "";


                    if ((_in.YLCARD_TYPE == "2" || _in.YLCARD_TYPE == "6") && _in.SETTLE_TYPE != "1")
                    {


                        string TRAN_ID = _in.HOS_ID + "_02_" + _in.PAY_ID;
                        var dbinsur = new DbContext().Client;
                        SqlSugarModel.ChsTran chsTran = dbinsur.Queryable<SqlSugarModel.ChsTran>().Where(t => t.TRAN_ID == TRAN_ID).Single();
                        if (chsTran == null)
                        {
                            dataReturn.Code = 222;
                            dataReturn.Msg = "未能获取到有效的医保支付数据，请确认是否医保支付";
                            return JsonConvert.SerializeObject(dataReturn);
                        }

                        string in2201 = chsTran.chsInput2201;
                        string in2203 = chsTran.chsInput2203;
                        string in2204 = chsTran.chsInput2204;
                        string in2206 = chsTran.chsInput2206;
                        string in2207 = chsTran.chsInput2207;

                        string out2204 = chsTran.chsOutput2204;
                        string out2206 = chsTran.chsOutput2206;
                        string out2207 = chsTran.chsOutput2207;

                        JObject jin2207 = JObject.Parse(in2207);

                        JObject jzzj = new JObject();

                        JObject jybrc = new JObject();
                        jybrc.Add("in2201", "");
                        jybrc.Add("in2203", JObject.Parse(in2203));
                        jybrc.Add("in2204", "");
                        jybrc.Add("in2206", JObject.Parse(in2206));
                        jybrc.Add("in2207", jin2207["input"]);
                        jybrc.Add("out2203", "");
                        jybrc.Add("out2204", JObject.Parse(out2204));
                        jybrc.Add("out2206", "");
                        jybrc.Add("out2207", JObject.Parse(out2207));

                        JObject jexpContentinfo = new JObject();
                        jexpContentinfo.Add("in2207msgid", jin2207["msgid"].ToString());
                        jybrc.Add("expContentinfo", jexpContentinfo);

                        jzzj.Add("zzj", jybrc);

                        medicareParam = Base64Helper.Base64Encode(jzzj.ToString());
                    }

                    //是否涉及第三⽅⽀付 1是 0否
                    int existsThirdPay = 1;

                    int payNature = 1;
                    string hisPayMode = "51";

                    if (decimal.Parse(_in.CASH_JE) == 0m)
                    {
                        hisPayMode = "0";
                        existsThirdPay = 0;
                        payNature = 2;
                    }

                    Hos185_His.Models.MZ.ThirdPayInfo thirdPayInfo = new Hos185_His.Models.MZ.ThirdPayInfo()
                    {
                        appPayMode = "", //对应APP⽀付⽅式
                        bankNo = "", //银⾏名称
                        businessType = "2", //业务类型 1挂号 2收费 3发卡 4预交款
                        businessType2 = "3", //业务类型 1现场挂号 2预约挂号 3⻔诊收费 4住院预交⾦
                        cardNo = _in.HOSPATID, //医院内部就诊卡号,唯⼀
                        cashFee = decimal.Parse(_in.CASH_JE), //第三⽅⽀付⾦额
                        clinicCode = _in.HOS_SN, //挂号流⽔号
                        hisOrderNo = "", //HIS订单号
                        hisPayMode = hisPayMode, //对应HIS⽀付⽅式
                        intenetAddress = "", //互联⽹医院收件地址
                        intenetRecivePerson = "", //互联⽹医院收件⼈
                        intenetTelNo = "", //互联⽹医院收件电话
                        invoiceNo = "", //发票号
                        machineNo = _in.LTERMINAL_SN, //机器编号
                        merchantOrderNo = string.IsNullOrEmpty(_in.QUERYID) ? "" : _in.QUERYID, //商⼾订单号
                        operCode = _in.USER_ID, //操作员编号
                        payChannelNo = "", //⽀付渠道
                        powerTranID = "", //平台交易ID
                        powerTranType = "", //平台交易类型
                        powerTranUserCode = "", //平台交易⽤⼾
                        rrn = "",//对账流⽔号
                        terminalNo = "", //交易终端号
                        thirdOrderNo = string.IsNullOrEmpty(_in.QUERYID) ? "" : _in.QUERYID, //第三⽅订单号
                        thirdTradeInParam = "", //第三⽅交易⼊参参数
                        thirdTradeOutParam = "", //第三⽅交易出参参数
                        tradeTime = _in.DEAL_TIME, //交易时间 yyyy-mm-dd HH24:mi:ss
                        tranCardNum = "", //银⾏卡号
                        transSerialNo = transSerialNo //交易流⽔号 （必传）
                    };

                    string isYbPay = "0";
                    if (_in.YLCARD_TYPE == "2")
                    {
                        isYbPay = "1";
                    }

                    Hos185_His.Models.MZ.OUTFEEPAYSAVE paysave = new Hos185_His.Models.MZ.OUTFEEPAYSAVE()
                    {
                        receiptNumber = _in.SJH,
                        payableCost = _in.CASH_JE,

                        medicareParam = medicareParam,
                        cardNo = _in.HOSPATID,  //院内就诊卡号
                        clinicCode = _in.HOS_SN,  //门诊挂号流水号
                        existsThirdPay = existsThirdPay,  //是否涉及第三方支付 1是 0否
                        lifeEquityCardNo = "",  //权益卡卡号
                        lifeEquityCardType = "",  //权益卡类型
                        operCode = _in.USER_ID,  //操作员编号
                        pactCode = "01",  //合同编号
                        billType = _in.TKBILL_TYPE,
                        recipeNo = _in.PRE_NO,  //处方号,多个以#分割
                        terminalCode = _in.LTERMINAL_SN,  //设备终端编号号
                        totalFee = decimal.Parse(_in.JEALL),  //总费用
                        ybPay = isYbPay  //是否医保支付 0 否 1是
                    };
                    paysave.thirdPayInfo = thirdPayInfo;
                    paysave.medicareInfo = medicareInfo;

                    string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(paysave);

                    Output<Hos185_His.Models.MZ.OUTFEEPAYSAVEDATA> output
                        = GlobalVar.CallAPI<Hos185_His.Models.MZ.OUTFEEPAYSAVEDATA>("/hischargesinfo/outpatientfee/savefee", jsonstr);
                    dataReturn.Code = output.code;
                    dataReturn.Msg = output.message;
                    P0601 p0601;


                    if (output.code != 0)
                    {
                        if (output.code == 110 && output.statusCode == 200)
                        {
                            if (decimal.Parse(_in.CASH_JE) != 0m)
                            {
                                p0601 = new P0601()
                                {
                                    outTradeNo = "", //商⼾订单号, 商⼾订单号和平台订单号必填⼀个
                                    transactionId = _in.QUERYID, //平台订单号 商⼾订单号和平台订单号必填⼀个
                                    confirmState = "fail", //确认状态 success 确认成功 fail 失败
                                    confirmDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), //确认时间 yyyy-mm-dd HH:mm:ss
                                    receiptNo = "" //HIS发票号,多张发票⽤,分隔
                                };

                                jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(p0601);

                                var outnocare1 = GlobalVar.CallAPI<JObject>("/platformpayment/pay/confirmPay", jsonstr);
                            }
                            if (!string.IsNullOrEmpty(medicareParam))
                            {
                                dataReturn.Code = 222;//医保退款标识
                            }


                        }
                        return JsonConvert.SerializeObject(dataReturn);

                    }

                    if (!string.IsNullOrEmpty(_in.QUERYID))
                    {
                        p0601 = new P0601()
                        {
                            outTradeNo = "", //商⼾订单号, 商⼾订单号和平台订单号必填⼀个
                            transactionId = _in.QUERYID, //平台订单号 商⼾订单号和平台订单号必填⼀个
                            confirmState = "success", //确认状态 success 确认成功 fail 失败
                            confirmDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), //确认时间 yyyy-mm-dd HH:mm:ss
                            receiptNo = output.data.invoiceNo //HIS发票号,多张发票⽤,分隔
                        };

                        jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(p0601);

                        var outnocare2 = GlobalVar.CallAPI<JObject>("/platformpayment/pay/confirmPay", jsonstr);
                    }

                    _out.HOS_PAY_SN = output.data.clinicCode;
                    _out.HOS_REG_SN = output.data.clinicCode;
                    _out.RCPT_NO = output.data.invoiceNo;
                    _out.OPT_SN = output.data.cardNo;
                    _out.DJ_ID = _in.SJH;
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
    }
}