using CommonModel;
using Hos185_His.Models.MZ;
using Newtonsoft.Json;
using OnlineBusHos185_YYGH.Model;

using System;

namespace OnlineBusHos185_YYGH.BUS
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

                string jsonstr = "";

                string preid = "0";
                string preCheckNo = "";
                string deptCode = "";
                string registerType = "";


                if (_in.IS_YY != "3" && DateTime.Parse(_in.SCH_DATE).Date != DateTime.Now.Date)
                {
                    Hos185_His.Models.MZ.REGISTERAPPTSAVE appt = new Hos185_His.Models.MZ.REGISTERAPPTSAVE()
                    {
                        cardNo = _in.HOSPATID, //医院内部就诊卡号，唯⼀
                        daypartId = _in.PERIOD_ID, //分时段id
                        operCode = _in.USER_ID, //操作员编号
                        pactCode = pactCode, //合同类型编码
                        patiSourceCode = "", //客⼾来源ID
                        patiSourceName = "", //客⼾来源Name
                        periodEnd = DateTime.Parse(_in.PERIOD_END).ToString("HH:mm:ss"), //时间段结束时间 hh24:mi:ss
                        periodStart = DateTime.Parse(_in.PERIOD_START).ToString("HH:mm:ss"), //时间段开始时间 hh24:mi:ss
                        phoneNo = _in.MOBILE_NO, //预约时联系电话（不设置时取患者档案中的联系电话）
                        schemaId = _in.SCH_ID, //排班序号
                        schemaPeriodId = "", //号源编号
                        sourceType = "XCYY", //号源类别 XCYY:线下 XCGG:12320 OLYY:线上(互联⽹在线问诊)
                        takeNumberPass = "", //取号密码
                        timePeriodFlag = "1", //预约挂号启⽤分时段标志 0不分时段 1分时段
                        ynfr = ""  //是否初诊 1 是 0 否
                    };

                    jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(appt);

                    Hos185_His.Models.Output<REGISTERAPPTSAVEDATA> output
              = GlobalVar.CallAPI<REGISTERAPPTSAVEDATA>("/hisbooking/appointment/save", jsonstr);

                    if (output.code != 0)
                    {
                        dataReturn.Code = output.code;
                        dataReturn.Msg = output.message;

                        return JsonConvert.SerializeObject(dataReturn);
                    }
                    preid = output.data.apointMentCode;
                    _out.HOS_SN = output.data.apointMentCode;
                    _out.APPT_ORDER = output.data.numNo.ToString();
                    _out.APPT_TIME = output.data.seeDate;
                    _out.APPT_PLACE = output.data.seeAddress;
                }


                //急诊分诊转诊
                // "registerType": "挂号类型：0普通挂号 1急诊挂号 2专家挂号 3点名专家 4特殊挂号 5义诊 6外宾挂号 7免挂号费 8免费挂号9预约挂号，急诊转诊必传

                if (_in.IS_YY=="3")
                {
                    preCheckNo = _in.HOS_SN;
                    deptCode = _in.DEPT_CODE;
                    registerType = "1";

                }

                try
                {
                    REGISTERFEE registerfee = new REGISTERFEE()
                    {
                        medicareParam = "",//        医保预留
                        pactCode = "01",//   结算code      FALSE
                        patientID = _in.HOSPATID,// 患者ID        FALSE
                        scheduleId = _in.SCH_ID,//         FALSE
                        vipCardNo = "",//        FALSE
                        vipCardType = "",//            FALSE
                        preCheckNo= preCheckNo,
                        preid = preid,
                        deptCode=deptCode,
                        registerType= registerType
                    };

                    jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(registerfee);

                    Hos185_His.Models.Output<REGISTERFEEDATA> outputregisterfee
              = GlobalVar.CallAPI<REGISTERFEEDATA>("/hisbooking/register/calcRegisterFee", jsonstr);
                    dataReturn.Code = outputregisterfee.code;
                    dataReturn.Msg = outputregisterfee.message;

                    if (outputregisterfee.code != 0)
                    {
                        return JsonConvert.SerializeObject(dataReturn);
                    }

                    _out.HOS_SN = preid;//如果是医保交易，真实的挂号序号是医保交易中的预算接口返回的
                    _out.APPT_ORDER = "";
                    _out.APPT_TIME = "";
                    _out.APPT_PLACE = "";
                    _out.SJH = outputregisterfee.data.receiptNumber + "|" + outputregisterfee.data.ghxh;//收据号|挂号序号（王丹那边就不用改了）modi by wyq 2023 01 06

                    _out.APPT_PAY = outputregisterfee.data.totalFee.ToString();
                    _out.JEALL = outputregisterfee.data.totalFee.ToString();

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
                        modelregister_appt.PERIOD_START = DateTime.Parse(_in.PERIOD_START).ToString("HH:mm:ss");
                        modelregister_appt.PERIOD_END = DateTime.Parse(_in.PERIOD_END).ToString("HH:mm:ss");
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