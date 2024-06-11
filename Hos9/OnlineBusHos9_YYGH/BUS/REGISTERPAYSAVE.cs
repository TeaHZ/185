using CommonModel;
using EncryptionKey;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos9_YYGH.HISModels;
using OnlineBusHos9_YYGH.Model;

using System;
using System.Collections.Generic;

namespace OnlineBusHos9_YYGH.BUS
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

                string dangTianPBID = "";
                string yiZhouPBID = "";
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
                if (_in.BUS_TYPE == "0")
                {
                    string[] schinfos = _in.SCH_ID.Split('|');


                    dangTianPBID = schinfos[0];
                    yiZhouPBID = schinfos[1];
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

                #region 挂号结算 ，支付方式给1

                T2021.JiaoYiLS jiaoyils = new T2021.JiaoYiLS()
                {
                    jinE = _in.JEALL, //挂号总支付金额
                    ziFeiJE = _in.CASH_JE, //自费金额
                    yiBaoJE = _in.MEDFEE_SUMAMT, //医保金额
                    yiBaoJSJG = "", //医保结算结果，包含医保详细结算数据
                    yiBaoDJRC = "", //  医保登记入参
                    yiBaoDJJG = "", // 医保登记返回结果
                    yiBaoCFSCJG = "", // 医保处方上传返回结果
                    yiBaoJSRC = "", //   医保结算入参
                    yiBaoDKXX = "", //  医保读卡信息
                    danJuHao = "", //  单据号(医保单据号)
                    appID = _in.APPID,//  支付平台ID
                    shangHuDDH = _in.QUERYID, //  订单号
                    dingDanHao = _in.DEFRAYNO, // 第三方流水号
                    channelTradeNo = _in.CHANNELTRADENO,
                    jiaoYiRQ = DateTime.Parse(_in.DEAL_TIME).ToString("yyyy-MM-dd"), //   交易日期
                };

                T2021.input t2021 = new T2021.input()
                {
                    hospitalId = "320282466455146",// 医院id
                    zhengJianHM = _in.SFZ_NO,//  身份证号
                    dianHua = _in.MOBILE_NO,//  手机号码
                    guaHaoBC = _in.SCH_TIME == "上午" ? "1" : "2",//    挂号班次
                    dangTianPBID = dangTianPBID,//    当天排班ID
                    keShiDM = _in.DEPT_CODE,//  科室代码
                    keShiMC = "",// 科室名称
                    guoBiaoKSDM = "",//  国标科室编码
                    jiuZhenRQ = _in.SCH_DATE,//   就诊日期
                    jiuZhenSJ = "",//   就诊时间
                    yiZhouPBID = yiZhouPBID,//   一周排班ID
                    yiShengDM = _in.DOC_NO,//   医生代码
                    yiShengXM = "",//  医生姓名
                    bingRenXM = _in.PAT_NAME,//   病人姓名
                    guaHaoXH = _in.PERIOD_ID,//   挂号序号
                    guaHaoLB = _in.REGISTER_TYPE,//    挂号类别
                    jiuZhenKa = "",//   就诊卡号
                    bingRenLX = _in.YLCARD_TYPE == "2" ? "2" : "1",//    病人类型
                    yiBaoKa = _in.YLCARD_NO,//  医保卡号(病人类型为2时必传)
                    yiBaoKLX = "",//   医保卡类型
                    shiFouXYF = "",//   是否信用付
                    laiYuan = laiyuan,//  预约来源 1	微信 2   支付宝 3   APP 4   医保
                    zhiFuZT = "1",// 支付状态  0	待支付 1   已付款
                    yeWuLX = "2021",//  业务类型
                    guaHaoID = _in.HOS_SN,//    挂号预结算返回的预结算ID(挂号需要走支付流程时 候必传) 2023 05 30 锁号的话这里传锁号的 yeWuLSH
                    yuGuaHaoID = _in.HOS_SN,//  挂号预结算返回的预挂号ID 2023 05 30 锁号的话这里传锁号的 yeWuLSH
                    duKaFS = duKaFS,//  读卡方式
                    yiBaoData = _in.BUS_CARD_INFO,//
                    yiBaoXX = _in.CARD_INFO,// 医保信息
                    jiaoYiLS = jiaoyils,//    交易流水实体
                    yeWuLY = "2",//   业务来源
                    caoZuoRId = _in.USER_ID,//    操作人ID
                    shouJiHao = _in.MOBILE_NO
                };
                string tradecode = "2021-QHZZJ";
                if (_in.BUS_TYPE == "1")
                {
                    tradecode = "2023-QHZZJ";
                }

                PushServiceResult<T2021.data> result2021 = HerenHelper<T2021.data>.pushService(tradecode, JsonConvert.SerializeObject(t2021));

                if (result2021.code != 1)//code=222
                {
                    dataReturn.Code = 6;
                    dataReturn.Msg = result2021.msg;

                    if (decimal.Parse(_in.CASH_JE)!=0m)
                    {
                        if (!Paycancel("2", "0", _in.QUERYID, _in.CASH_JE))
                        {
                            dataReturn.Msg += "自动退款失败";

                        }
                    }
       
             
                    return JsonConvert.SerializeObject(dataReturn);
                }

                #endregion 挂号结算 ，支付方式给1

                var db = new DbMySQLZZJ().Client;

                _out.HOS_SN = result2021.data.yeWuLSH;
                _out.APPT_PAY = result2021.data.heji;
                _out.APPT_ORDER = result2021.data.guaHaoXH;
                _out.APPT_TIME = result2021.data.jiuZhenRQ +" "+result2021.data.jiuZhenSJ;
                _out.APPT_PLACE = result2021.data.daoZhenXX;
                //_out.OPT_SN = _in.HOSPATID;
                _out.OPT_SN = result2021.data.daoZhenID;//因为有的患者的身份证可能绑了两个ID号 所以取his返回的
                _out.RCPT_NO = "";
                _out.OTH_PAY= result2021.data.qiTaZF;
                _out.GUID_INFO = result2021.data.daoZhenXX;
                _out.RCPT_URL = "https://h5.eheren.com/scan/yixing/invoice?pid=" + _in.HOSPATID;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="op"> 1 撤销，2 退费</param>
        /// <param name="type"></param>
        /// <param name="queryid"></param>
        /// <param name="cash_je"></param>
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