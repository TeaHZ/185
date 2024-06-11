using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_YYGH.HISModels;
using OnlineBusHos9_YYGH.Model;

using System;

namespace OnlineBusHos9_YYGH.BUS
{
    internal class REGISTERAPPTSAVE
    {
        public static string B_REGISTERAPPTSAVE(string json_in)
        {
            return DoBusiness(json_in);
        }

        public static string DoBusiness(string json_in)
        {
            DataReturn dataReturn = new DataReturn();

            try
            {
                REGISTERAPPTSAVE_M.REGISTERAPPTSAVE_IN _in = JsonConvert.DeserializeObject<REGISTERAPPTSAVE_M.REGISTERAPPTSAVE_IN>(json_in);
                REGISTERAPPTSAVE_M.REGISTERAPPTSAVE_OUT _out = new REGISTERAPPTSAVE_M.REGISTERAPPTSAVE_OUT();

                try
                {
                    //读卡方式 1-医保卡 2-电子社保卡 3-人脸识别 4-电子健康卡 5- 电子医保凭证
                    string duKaFS = "1";

                    switch (_in.MDTRT_CERT_TYPE)
                    {
                        case "03":
                            duKaFS = "1";
                            break;

                        case "01":
                            duKaFS = "5";
                            break;

                        default:
                            break;
                    }

                    string yuGuaHaoId = "";
                    string daoZhenXX = "";
                    string daoZhenID = "";//病人id

                    string dangTianPBID = "";
                    string yiZhouPBID = "";

                    //非 预约取号
                    if (_in.IS_YY != "2")
                    {
                        string[] schinfos = _in.SCH_ID.Split('|');

                        if (schinfos.Length == 0)
                        {
                            dataReturn.Code = 6;
                            dataReturn.Msg = "排班ID无法获取，请重试";
                            goto EndPoint;
                        }

                        dangTianPBID = schinfos[0];
                        yiZhouPBID = schinfos[1];

                        #region 占号（当日挂号和预约挂号（含不支付仅预约）需要锁号）

                        T2021.JiaoYiLS jiaoyils = new T2021.JiaoYiLS()
                        {
                            jinE = "", //挂号总支付金额
                            ziFeiJE = "", //自费金额
                            yiBaoJE = "", //医保金额
                            yiBaoJSJG = "", //医保结算结果，包含医保详细结算数据
                            yiBaoDJRC = "", //  医保登记入参
                            yiBaoDJJG = "", // 医保登记返回结果
                            yiBaoCFSCJG = "", // 医保处方上传返回结果
                            yiBaoJSRC = "", //   医保结算入参
                            yiBaoDKXX = "", //  医保读卡信息
                            danJuHao = "", //  单据号(医保单据号)
                            shangHuDDH = "", //  订单号
                            dingDanHao = "", // 第三方流水号
                            jiaoYiRQ = "", //   交易日期
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
                            jiuZhenSJ = _in.PERIOD_START + "-" + _in.PERIOD_END,//   就诊时间
                            yiZhouPBID = yiZhouPBID,//   一周排班ID
                            yiShengDM = _in.DOC_NO,//   医生代码
                            yiShengXM = "",//  医生姓名
                            bingRenXM = _in.PAT_NAME,//   病人姓名
                            guaHaoXH = _in.PERIOD_ID,//   挂号序号   //分时段返回的
                            guaHaoLB = _in.REGISTER_TYPE,//    挂号类别
                            jiuZhenKa = _in.YLCARD_NO,//   就诊卡号 就诊卡号(1001 1002 患者建档推送的卡号，自助 机读卡返回的卡号)
                            bingRenLX = _in.YLCARD_TYPE == "2" ? "2" : "1",//    病人类型 1	自费  2   医保
                            yiBaoKa = _in.YLCARD_TYPE == "2" ? _in.YLCARD_NO : "",//  医保卡号(病人类型为2时必传)
                            yiBaoKLX = "",//   医保卡类型
                            shiFouXYF = "",//   是否信用付
                            laiYuan = "5",//  预约来源 1	微信 2   支付宝 3   APP 4   医保
                            zhiFuZT = "0",// 支付状态  0	待支付 1   已付款
                            yeWuLX = "2021",//  业务类型(固定值：  2021)
                            guaHaoID = "",//    挂号预结算返回的预结算ID(挂号需要走支付流程时 候必传)
                            yuGuaHaoID = "",//   挂号预结算返回的预挂号ID  (锁号ID)，挂号需要 走支付流程时候必传
                            duKaFS = duKaFS,//  读卡方式 1-医保卡 2-电子社保卡 3-人脸识别 4-电子健康卡 5- 电子医保凭证
                            yiBaoData = _in.MDTRT_CERT_NO,// duKaFS=1时 社会保障卡卡号|pBusiCardInfo duKaFS=5时 电子凭证令牌
                            yiBaoXX = _in.CARD_INFO,// 医保信息
                            jiaoYiLS = jiaoyils,//    交易流水实体
                            yeWuLY = "2",//  业务来源 1.线上 2.自助机
                            caoZuoRId = _in.USER_ID,//    操作人ID
                            shouJiHao = _in.MOBILE_NO
                        };

                        PushServiceResult<T2021.data> result2021 = HerenHelper<T2021.data>.pushService("2021-QHZZJ", JsonConvert.SerializeObject(t2021));

                        if (result2021.code != 1)
                        {
                            dataReturn.Code = 6;
                            dataReturn.Msg = result2021.msg;

                            return JsonConvert.SerializeObject(dataReturn);
                        }
                        yuGuaHaoId = result2021.data.yeWuLSH;
                        daoZhenXX = result2021.data.daoZhenXX;
                        daoZhenID = result2021.data.daoZhenID;

                        #endregion 占号（当日挂号和预约挂号（含不支付仅预约）需要锁号）
                    }
                    else
                    {
                        #region 预约取号逻辑

                        yuGuaHaoId = _in.HOS_SN;

                        #endregion 预约取号逻辑
                    }

                    #region 预结算

                    T2025.input input = new T2025.input()
                    {
                        hospitalId = "320282466455146",//  医院id
                        zhengJianHM = _in.SFZ_NO,//  身份证号
                        dianHua = _in.MOBILE_NO,// 手机号码
                        yuGuaHaoId = yuGuaHaoId,//  取号时传预约返回的预约ID
                        guaHaoBC = _in.SCH_TIME == "上午" ? "1" : "2",//    挂号班次
                        dangTianPBID = "",//    当天排班ID
                        keShiDM = _in.DEPT_CODE,//  科室代码
                        keShiMC = "",//  科室名称
                        guoBiaoKSDM = "",//  国标科室编码
                        jiuZhenRQ = _in.SCH_DATE,//   就诊日期
                        jiuZhenSJ = "",//   就诊时间
                        yiZhouPBID = "",//   一周排班ID
                        yiShengDM = _in.DOC_NO,//   医生代码
                        yiShengXM = "",//   医生姓名
                        bingRenXM = _in.PAT_NAME,//   病人姓名
                        guaHaoXH = _in.PERIOD_ID,//    挂号序号
                        guaHaoLB = _in.REGISTER_TYPE,//   挂号类别
                        yeWuLY = "2",//  业务来源
                        jiuZhenKa = "",//   就诊卡号 (区域内病人唯一标识)
                        yiBaoXX = _in.CARD_INFO,
                        bingRenLX = _in.YLCARD_TYPE == "2" ? "2" : "1",//   病人类型 (1：自费；  2：医保；)
                        yiBaoKa = _in.YLCARD_NO,//  医保卡号 医保卡号(病人类型为2时必传)
                        duKaFS = duKaFS,//  读卡方式 1-医保卡 2-电子社保卡 3-人脸识别 4-电子健康卡 5- 电子医保凭证
                        yiBaoData = _in.MDTRT_CERT_NO,//duKaFS=1时 社会保障卡卡号|pBusiCardInfo duKaFS=5时 电子凭证令牌
                        yeWuLX = "2025",//  业务类型
                        caoZuoRId = "NSH015",//   操作人ID
                        shouJiHao = _in.MOBILE_NO
                    };

                    PushServiceResult<T2025.data> result = HerenHelper<T2025.data>.pushService("2025-QHZZJ", JsonConvert.SerializeObject(input));

                    if (result.code != 1)
                    {
                        dataReturn.Code = 6;
                        dataReturn.Msg = result.msg;

                        return JsonConvert.SerializeObject(dataReturn);
                    }

                    #endregion 预结算

                    _out.HOS_SN = yuGuaHaoId;
                    _out.APPT_ORDER = "";

                    if (_in.IS_YY == "2")
                    {
                        try
                        {
                            string[] sch_time = _in.SCH_TIME.Split('-');
                            _in.PERIOD_START = sch_time[0].Trim();
                            _in.PERIOD_END = sch_time[1].Trim();
                        }
                        catch
                        {
                            _out.APPT_TIME = _in.SCH_DATE;
                        }
                    }
                    else
                    {
                        _out.APPT_TIME = _in.SCH_DATE + " " + _in.PERIOD_START + "-" + _in.PERIOD_END;

                    }
                    _out.APPT_PLACE = daoZhenXX;
                    _out.SJH = "";

                    _out.APPT_PAY = result.data.ziFeiJE;
                    _out.JEALL = result.data.zongJinE;
                    if (decimal.Parse(result.data.jianMianJE) != 0m && decimal.Parse(result.data.jianMianJE) == decimal.Parse(result.data.zongJinE))
                    {
                    }

                    _out.MEDFEE_SUMAMT = FormatHelper.GetStr(result.data.yiBaoJE);
                    _out.ACCT_PAY = FormatHelper.GetStr(result.data.geRenZHZF);
                    _out.PSN_CASH_PAY = FormatHelper.GetStr(result.data.ziFeiJE);
                    _out.FUND_PAY_SUMAMT = FormatHelper.GetStr(result.data.yiBaoTCZF);
                    _out.OTH_PAY = "0.00";
                    _out.BALC = FormatHelper.GetStr(result.data.zhangHuYE);
                    _out.ACCT_MULAID_PAY = FormatHelper.GetStr(result.data.gongJiJinZF);
                    _out.DERATE_REASON = FormatHelper.GetStr(result.data.feiYongBZ);

                    dataReturn.Code = 0;
                    dataReturn.Msg = "SUCCESS";
                    dataReturn.Param = JsonConvert.SerializeObject(_out);

                    #region 平台数据保存

                    try
                    {
                        var db = new DbMySQLZZJ().Client;

                        SqlSugarModel.RegisterAppt modelregister_appt = new SqlSugarModel.RegisterAppt();

                        int reg_id = 0;//预约标识
                        if (!PubFunc.GetSysID("REGISTER_APPT", out reg_id))
                        {
                            goto EndPoint;
                        }
                        int pat_id = 0;
                        var pat_info = db.Queryable<SqlSugarModel.PatInfo>().Where(t => t.SFZ_NO == _in.SFZ_NO).First();
                        if (pat_info != null)
                        {
                            pat_id = pat_info.PAT_ID;
                        }

                        modelregister_appt.REG_ID = reg_id;
                        modelregister_appt.HOS_ID = _in.HOS_ID;
                        modelregister_appt.PAT_ID = pat_id;

                        modelregister_appt.SFZ_NO = _in.SFZ_NO;
                        modelregister_appt.PAT_NAME = _in.PAT_NAME;
                        modelregister_appt.BIRTHDAY = _in.BIRTHDAY;
                        modelregister_appt.SEX = _in.SEX;
                        modelregister_appt.ADDRESS = _in.ADDRESS;
                        modelregister_appt.MOBILE_NO = _in.MOBILE_NO;

                        modelregister_appt.YLCARD_NO = _in.YLCARD_NO;
                        modelregister_appt.YLCARD_TYPE = FormatHelper.GetInt(_in.YLCARD_TYPE);

                        modelregister_appt.DEPT_CODE = _in.DEPT_CODE;
                        modelregister_appt.DOC_NO = _in.DOC_NO;
                        modelregister_appt.DEPT_NAME = "";
                        modelregister_appt.DOC_NAME = "";

                        modelregister_appt.SCH_DATE = _in.SCH_DATE;
                        modelregister_appt.SCH_TIME = _in.SCH_TIME;
                        modelregister_appt.SCH_TYPE = _in.SCH_TYPE;
                        modelregister_appt.PERIOD_START =string.IsNullOrEmpty(_in.PERIOD_START)?"": DateTime.Parse(_in.PERIOD_START).ToString("HH:mm:ss");
                        modelregister_appt.PERIOD_END = string.IsNullOrEmpty(_in.PERIOD_END) ? "" : DateTime.Parse(_in.PERIOD_END).ToString("HH:mm:ss");
                        modelregister_appt.TIME_POINT = "";
                        modelregister_appt.REGISTER_TYPE = _in.REGISTER_TYPE;
                        modelregister_appt.IS_FZ = false;//初复诊

                        modelregister_appt.ZL_FEE = FormatHelper.GetDecimal(_out.APPT_PAY);
                        modelregister_appt.GH_FEE = 0;
                        modelregister_appt.APPT_PAY = FormatHelper.GetDecimal(_out.APPT_PAY);

                        modelregister_appt.APPT_DATE = DateTime.Now.Date;
                        modelregister_appt.APPT_TIME = DateTime.Now.ToString("HH:mm:ss");

                        modelregister_appt.HOS_SN = _out.HOS_SN;
                        modelregister_appt.APPT_SN = _out.HOS_SN;//待定
                        modelregister_appt.APPT_ORDER = _out.APPT_ORDER;//待定
                        modelregister_appt.ZS_NAME = _out.APPT_PLACE;//预约标记
                        modelregister_appt.APPT_TYPE = "0";//预约标记
                        modelregister_appt.HOSPATID = _in.HOSPATID;//预约标记

                        modelregister_appt.USER_ID = _in.USER_ID;
                        modelregister_appt.lTERMINAL_SN = _in.LTERMINAL_SN;
                        modelregister_appt.SOURCE = "ZZJ";

                        var row = db.Insertable(modelregister_appt).ExecuteCommand();
                    }
                    catch (Exception ex)
                    {
                        SqlSugarModel.Sqlerror sqlerror = new SqlSugarModel.Sqlerror();
                        sqlerror.TYPE = "REGISTERAPPTSAVE";
                        sqlerror.Exception = ex.Message;
                        sqlerror.DateTime = DateTime.Now;
                        LogHelper.SaveSqlerror(sqlerror);
                    }

                    #endregion 平台数据保存
                }
                catch (Exception ex)
                {
                    dataReturn.Code = 5;
                    dataReturn.Msg = ex.Message;
                }
            }
            catch
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常";
            }

        EndPoint:
            string json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}