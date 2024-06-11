using CommonModel;
using Hos185_His.Models;
using Hos185_His.Models.MZ;
using Hos185_His.Models.OriginPay;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos185_YYGH.Model;

using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBusHos185_YYGH.BUS
{
    internal class REGISTERPAYSAVE
    {
        public static string B_REGISTERPAYSAVE(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                REGISTERPAYSAVE_M.REGISTERPAYSAVE_IN _in = JsonConvert.DeserializeObject<REGISTERPAYSAVE_M.REGISTERPAYSAVE_IN>(json_in);
                REGISTERPAYSAVE_M.REGISTERPAYSAVE_OUT _out = new REGISTERPAYSAVE_M.REGISTERPAYSAVE_OUT();
                Dictionary<string, string> dic_filter = GlobalVar.Get_Filter(FormatHelper.GetStr(_in.FILTER));

                Hos185_His.Models.MZ.ConsultInfoParam consultInfoParam = new Hos185_His.Models.MZ.ConsultInfoParam()
                {
                    kfDoctorCode = "",
                    kfDoctorName = "",
                    parkCode = "",
                    parkName = ""
                };
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
                string jsonstr = "";

                string clinicCode = "";
                string receiptNumber = "";

                _in.CASH_JE = string.IsNullOrEmpty(_in.CASH_JE) ? "0" : _in.CASH_JE;

                P0601 p0601;

                if (_in.IS_YY == "2")
                {
                    string pactCode = "";// 01  自费,17 医保

                    switch (_in.YLCARD_TYPE)
                    {
                        case "2":
                            pactCode = "17";
                            break;

                        default:
                            pactCode = "01";
                            break;
                    }

                    if (pactCode == "01")
                    {
                        #region 自费预算

                        REGISTERFEE registerfee = new REGISTERFEE()
                        {
                            medicareParam = "",//        医保预留
                            pactCode = "01",//   结算code      FALSE
                            patientID = _in.HOSPATID,// 患者ID        FALSE
                            scheduleId = _in.SCH_ID,//         FALSE
                            vipCardNo = "",//        FALSE
                            vipCardType = "",//            FALSE
                            preid = _in.HOS_SN//预约取号 必传
                        };

                        jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(registerfee);

                        Hos185_His.Models.Output<REGISTERFEEDATA> outputregisterfee
                  = GlobalVar.CallAPI<REGISTERFEEDATA>("/hisbooking/register/calcRegisterFee", jsonstr);

                        dataReturn.Code = outputregisterfee.code;
                        dataReturn.Msg = outputregisterfee.message;

                        if (outputregisterfee.code != 0)
                        {
                            #region P0601

                            p0601 = new P0601()
                            {
                                outTradeNo = "", //商⼾订单号, 商⼾订单号和平台订单号必填⼀个
                                transactionId = _in.QUERYID, //平台订单号 商⼾订单号和平台订单号必填⼀个
                                confirmState = "fail", //确认状态 success 确认成功 fail 失败
                                confirmDate = DateTime.Now.ToString("yyyy-mm-dd HH:mm:ss"), //确认时间 yyyy-mm-dd HH:mm:ss
                                receiptNo = "" //HIS发票号,多张发票⽤,分隔
                            };
                            jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(p0601);

                            var outputp0601 = GlobalVar.CallAPI<JObject>("/platformpayment/pay/confirmPay", jsonstr);

                            #endregion P0601

                            return JsonConvert.SerializeObject(dataReturn);
                        }

                        _in.SJH = outputregisterfee.data.receiptNumber + "|" + outputregisterfee.data.ghxh;//收据号|挂号序号（王丹那边就不用改了）modi by wyq 2023 01 06

                        clinicCode = outputregisterfee.data.ghxh;
                        receiptNumber = outputregisterfee.data.receiptNumber;

                        #endregion 自费预算
                    }
                    else
                    {
                        //医保，已经预算过

                        string[] hisneed = _in.SJH.Split('|');

                        if (hisneed.Length == 0)
                        {
                            dataReturn.Code = 222;
                            dataReturn.Msg = "未能获取到有效的医保支付数据，请确认是否医保支付";
                            return JsonConvert.SerializeObject(dataReturn);
                        }
                        clinicCode = hisneed[1];
                        receiptNumber = hisneed[0];
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(_in.SJH))
                    {
                        dataReturn.Code = 222;
                        dataReturn.Msg = "未能获取到HIS预算收据号";
                        return JsonConvert.SerializeObject(dataReturn);
                    }

                    string[] hisneed = _in.SJH.Split('|');

                    clinicCode = hisneed[1];
                    receiptNumber = hisneed[0];
                }

                var db = new DbMySQLZZJ().Client;
                string transSerialNo = "";
                if (decimal.Parse(_in.CASH_JE) != 0m)
                {
                    #region 获取源启支付流水号

                    var Hoshmpay = db.Queryable<SqlSugarModel.Hoshmpay>().Where(x => x.COMM_MAIN == _in.QUERYID).Where(x => x.TXN_TYPE == "01").ToList();

                    if (Hoshmpay.Count == 0)
                    {
                        dataReturn.Code = 8;
                        dataReturn.Msg = "未能获取到有效的支付数据，请联系工作人员";

                        return JsonConvert.SerializeObject(dataReturn);
                    }
                    transSerialNo = Hoshmpay[0].ThirdTradeNo;

                    #endregion 获取源启支付流水号
                }

                string medicareParam = "";
                if ((_in.YLCARD_TYPE == "2" || _in.YLCARD_TYPE == "6") && decimal.Parse(_in.JEALL) != 0)
                {
                    string TRAN_ID = _in.HOS_ID + "_01_" + clinicCode;//这里用挂号序号，当日 apptsave返回的hos-sn 是 0，预约取号的 存表也用 医保预算中的挂号序号
                    var dbinsur = new DbContext().Client;
                    SqlSugarModel.ChsTran chsTran = dbinsur.Queryable<SqlSugarModel.ChsTran>().Where(t => t.TRAN_ID == TRAN_ID).Single();
                    if (chsTran == null)
                    {
                        dataReturn.Code = 12;
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
                    businessType = "1", //业务类型 1挂号 2收费 3发卡 4预交款
                    businessType2 = "1", //业务类型 1现场挂号 2预约挂号 3⻔诊收费 4住院预交⾦
                    cardNo = _in.HOSPATID, //医院内部就诊卡号,唯⼀
                    cashFee = decimal.Parse(_in.CASH_JE), //第三⽅⽀付⾦额
                    clinicCode = clinicCode, //挂号流⽔号
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
                    thirdOrderNo = string.IsNullOrEmpty(_in.QUERYID) ? "" : _in.QUERYID, //第三⽅订单号   ptlsh平台流水号
                    thirdTradeInParam = "", //第三⽅交易⼊参参数
                    thirdTradeOutParam = "", //第三⽅交易出参参数
                    tradeTime = _in.DEAL_TIME, //交易时间 yyyy-mm-dd HH24:mi:ss
                    tranCardNum = "", //银⾏卡号
                    transSerialNo = transSerialNo //交易流⽔号 （必传）   zflsh源启支付流水号
                };

                Hos185_His.Models.MZ.REGISTERPAYSAVE paysave = new Hos185_His.Models.MZ.REGISTERPAYSAVE()
                {
                    appointMentCode = _in.HOS_SN, //预约流⽔号
                    cardNo = _in.HOSPATID, //医院内部就诊卡号,唯⼀
                    daypartId = "", //分时段Id
                    existsThirdPay = existsThirdPay, //是否涉及第三⽅⽀付 1是 0否
                    invoiceNo = "", //发票号,如果不传，则系统⽣成
                    isUnionClinic = "", //是否涉及康复医院会诊信息
                    lifeEquityCardNo = "", //权益卡卡号
                    lifeEquityCardType = "", //权益卡类型
                    mcardNo = "", //医疗证号
                    mcardNoType = "", //医疗证类型
                    operCode = _in.USER_ID, //操作员编号
                    pactCode = "01", //合同编号
                    payNature = payNature, //收费性质0 免费1 ⾃费2 医保3 医保+⾃费
                    periodEnd = DateTime.Parse(_in.PERIOD_END).TimeOfDay.ToString(), //时间段结束时间 hh24:mi:ss
                    periodStart = DateTime.Parse(_in.PERIOD_START).TimeOfDay.ToString(), //时间段开始时间 hh24:mi:ss
                    schemaId = "", //排班序号
                    schemaPeriodId = "", //号源编号
                    sourceType = "XCYY", //号源类别 XCYY:线下 XCGG:12320 OLYY:线上(互联⽹在线问诊)
                    terminalCode = _in.LTERMINAL_SN, //设备终端编号号
                    timePeriodFlag = "1", //挂号启⽤分时段标志 0不分时段 1分时段
                    totalFee = decimal.Parse(_in.JEALL),//总费⽤
                    receiptNumber = receiptNumber,
                    medicareParam = medicareParam
                };

                paysave.thirdPayInfo = thirdPayInfo;
                paysave.medicareInfo = medicareInfo;
                paysave.consultInfoParam = consultInfoParam;

                jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(paysave);

                Output<Hos185_His.Models.MZ.REGISTERPAYSAVEDATA> output
                    = GlobalVar.CallAPI<Hos185_His.Models.MZ.REGISTERPAYSAVEDATA>("/hisbooking/register/save", jsonstr);

                dataReturn.Code = output.code;
                dataReturn.Msg = output.message;

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

                            var outputp0601 = GlobalVar.CallAPI<JObject>("/platformpayment/pay/confirmPay", jsonstr);
                        }

                        if (!string.IsNullOrEmpty(medicareParam))
                        {
                            dataReturn.Code = 222;//医保退款标识
                        }
                    }

                    try
                    {//尝试取消预约
                        jsonstr = string.Format("apointMentCode={0}", _in.HOS_SN);
                        //application/x-www-form-urlencoded
                        Dictionary<string, string> header = new Dictionary<string, string>();
                        header.Add("operCode", _in.USER_ID);

                        Hos185_His.Models.Output<JObject> outputappt
        = GlobalVar.CallAPIForm<JObject>("/hisbooking/appointment/cancel", jsonstr, "application/x-www-form-urlencoded", header);
                    }
                    catch
                    {
                    }
                    return JsonConvert.SerializeObject(dataReturn);
                }

                #region P0601

                if (decimal.Parse(_in.CASH_JE) != 0m)
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

                    var outputp0601 = GlobalVar.CallAPI<JObject>("/platformpayment/pay/confirmPay", jsonstr);
                }

                #endregion P0601

                //急诊内科 20012002
                //急诊外科 20012003
                //急诊妇科 05012001
                //急诊产科 0502201
                //急诊耳鼻喉科 11012001
                //急诊眼科 10012001
                //急诊口腔科 12012001
                string[] kids = new string[] { "07011001", "07011002", "20012002", "20012003", "05012001", "0502201", "11012001", "10012001", "12012001" };

                string seeNo = output.data.seeNo;

                if (kids.Contains(_in.DEPT_CODE))
                {
                    seeNo = "以现场签到为准";
                }

                _out.HOS_SN = output.data.clinicCode;
                _out.APPT_PAY = output.data.regFee.ToString();
                _out.APPT_ORDER = seeNo;
                _out.APPT_TIME = output.data.seeDate;
                _out.APPT_PLACE = output.data.seeAddress;
                _out.OPT_SN = output.data.cardNo;
                _out.RCPT_NO = output.data.invoiceNo;
                _out.DJ_ID = receiptNumber;

                dataReturn.Param = JsonConvert.SerializeObject(_out);

                #region 平台数据保存

                try
                {
                    SqlSugarModel.RegisterPay modelregister_pay = new SqlSugarModel.RegisterPay();
                    SqlSugarModel.RegisterAppt modelregister_appt = db.Queryable<SqlSugarModel.RegisterAppt>().Where(t => t.HOS_ID == _in.HOS_ID && t.HOS_SN == _in.HOS_SN).First();
                    int pay_id = 0;//
                    if (!PubFunc.GetSysID("REGISTER_PAY", out pay_id))
                    {
                        goto EndPoint;
                    }

                    modelregister_pay.PAY_ID = pay_id;
                    modelregister_pay.REG_ID = modelregister_appt.REG_ID;
                    modelregister_pay.HOS_ID = _in.HOS_ID;
                    modelregister_pay.PAT_ID = modelregister_appt.PAT_ID;
                    modelregister_pay.CHARGE_TYPE = "";
                    modelregister_pay.QUERYID = _in.QUERYID ?? "";
                    modelregister_pay.DEAL_TYPE = _in.DEAL_TYPE;
                    modelregister_pay.SUM_JE = FormatHelper.GetDecimal(_in.JEALL);
                    modelregister_pay.CASH_JE = FormatHelper.GetDecimal(_in.CASH_JE);
                    modelregister_pay.ACCT_JE = 0;
                    modelregister_pay.FUND_JE = modelregister_pay.SUM_JE - modelregister_pay.CASH_JE;
                    modelregister_pay.OTHER_JE = 0;

                    modelregister_pay.HOS_SN = _out.HOS_SN;
                    modelregister_pay.OPT_SN = _out.OPT_SN;
                    modelregister_pay.PRE_NO = "";
                    modelregister_pay.RCPT_NO = _out.RCPT_NO;

                    modelregister_pay.DJ_DATE = DateTime.Parse(DateTime.Now.ToString("yyyy.MM.dd"));
                    modelregister_pay.DJ_TIME = DateTime.Now.ToString("HH:mm:ss");
                    modelregister_pay.USER_ID = _in.USER_ID;
                    modelregister_pay.SOURCE = "ZZJ";
                    modelregister_pay.lTERMINAL_SN = _in.LTERMINAL_SN;

                    modelregister_pay.IS_TH = false;

                    modelregister_appt.APPT_TYPE = "1";
                    var row1 = db.Updateable(modelregister_appt).ExecuteCommand();
                    var row = db.Insertable(modelregister_pay).ExecuteCommand();
                }
                catch (Exception ex)
                {
                    SqlSugarModel.Sqlerror sqlerror = new SqlSugarModel.Sqlerror();
                    sqlerror.TYPE = "REGISTERPAYSAVE";
                    sqlerror.Exception = ex.Message;
                    sqlerror.DateTime = DateTime.Now;
                    LogHelper.SaveSqlerror(sqlerror);
                }

                #endregion 平台数据保存
            }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常" + ex.Message;
            }

        EndPoint:
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}