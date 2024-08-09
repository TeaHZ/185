using Hos185_His.Models;
using Hos185_His.Models.MZ;
using Hos185_His.Models.OriginPay;
using Hos185_His.Models.Report;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos185.Models;
using OnlineBusHos185.Models.OriginPay;
using PasS.Base.Lib;
using QHSiInterface;
using Soft.Common;
using Soft.DBUtility;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;

namespace OnlineBusHos185
{
    public class ExternalHos4MidSevice
    {
        private static string HOS_ID = "185";

        /// <summary>
        /// 中台交易
        /// </summary>
        private bool midservice = true;

        /// <summary>
        /// 统一调用函数
        /// </summary>
        /// <param name="xmlstr"></param>
        /// <returns></returns>
        private static string CallService(string xmlstr, string source)
        {
            DateTime InTime = DateTime.Now;

            SaveLog(InTime, xmlstr, DateTime.Now, "");//保存his接口日志

            return "正在开发中...";
        }

        public DataTable SENDCARDINFO(string YLCARD_TYPE, string YLCARD_NO, string PAT_NAME, string SEX, string BIRTHDAY, string GUARDIAN_NAME, string SFZ_NO, string MOBILE_NO, string ADDRESS, string OPERATOR, Dictionary<string, string> dic)
        {
            //XmlDocument doc = new XmlDocument();
            //doc = QHXmlMode.GetBaseXml("SENDCARDINFO", "0");
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_NO", YLCARD_NO.Trim() == "" ? SFZ_NO.Trim() : YLCARD_NO.Trim());
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_NAME", PAT_NAME.Trim());
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SEX", SEX.Trim());
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "BIRTHDAY", BIRTHDAY.Trim());
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "GUARDIAN_NAME", GUARDIAN_NAME.Trim());
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", SFZ_NO.Trim());
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MOBILE_NO", MOBILE_NO.Trim());
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "ADDRESS", ADDRESS.Trim());
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "OPERATOR", OPERATOR.Trim());
            //XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "CARDTYPE", YLCARD_TYPE);
            //try
            //{
            //    string rtnxml = CallService(doc.OuterXml);
            //    XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
            //    DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
            //    return dtrev;
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
            DataTable dt = new DataTable();
            dt.Columns.Add("CLBZ", typeof(string));
            dt.Columns.Add("CLJG", typeof(string));
            dt.Columns.Add("FLAG", typeof(string));
            dt.Columns.Add("YLCARD_NO", typeof(string));

            if (YLCARD_TYPE == "0")//无卡
            {
                if (YLCARD_NO != "")
                {
                    if (CommonFunction.isValidSFZ(YLCARD_NO))//只支持身份证
                    {
                        DataRow dr = dt.NewRow();
                        dr["CLBZ"] = "0";
                        dr["CLJG"] = "";
                        dr["FLAG"] = "2";
                        dr["YLCARD_NO"] = YLCARD_NO;
                        dt.Rows.Add(dr);
                        return dt;
                    }
                    else//其他医院无卡不算
                    {
                        DataRow dr = dt.NewRow();
                        dr["CLBZ"] = "1";
                        dr["CLJG"] = "非本院卡，请重新添加卡号";
                        dr["FLAG"] = "2";
                        dr["YLCARD_NO"] = YLCARD_NO;
                        dt.Rows.Add(dr);
                        return dt;
                    }
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    dr["CLBZ"] = "0";
                    dr["CLJG"] = "";
                    dr["FLAG"] = "2";
                    dr["YLCARD_NO"] = SFZ_NO;
                    dt.Rows.Add(dr);
                    return dt;
                }
            }
            else
            {
                DataRow dr = dt.NewRow();
                dr["CLBZ"] = "0";
                dr["CLJG"] = "";
                dr["FLAG"] = "2";
                dr["YLCARD_NO"] = YLCARD_NO;
                dt.Rows.Add(dr);
                return dt;
            }
        }

        /// <summary>
        /// 保存调用HIS的日志
        /// </summary>
        /// <param name="intime"></param>
        /// <param name="inxml"></param>
        /// <param name="outTimem"></param>
        /// <param name="outxml"></param>
        private static void SaveLog(DateTime intime, string inxml, DateTime outTime, string outxml)
        {
            Log.Helper.Model.ModLogHos logHos = new Log.Helper.Model.ModLogHos();
            logHos.inTime = intime;
            logHos.inXml = inxml;
            logHos.outTime = outTime;
            logHos.outXml = outxml;
            Log.Helper.LogHelper.Addlog(logHos);
        }

        public DataTable ReturnFail(string CLBZ, string CLJG)
        {
            DataTable dtresult = new DataTable();
            dtresult.Columns.Add("CLBZ", typeof(string));
            dtresult.Columns.Add("CLJG", typeof(string));
            DataRow newrow = dtresult.NewRow();
            newrow["CLBZ"] = CLBZ;
            newrow["CLJG"] = CLJG;
            dtresult.Rows.Add(newrow);
            return dtresult;
        }

        /// <summary>
        /// 获取医保明细数据（医院端）
        /// </summary>
        /// <param name="STARTDATE"></param>
        /// <param name="ENDDATE"></param>
        /// <param name="HXFLAG"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public DataSet GETHISYBDETAILINFOBYDATE(string STARTDATE, string ENDDATE, string HXFLAG, Dictionary<string, string> dic)
        {
            return null;
        }

        /// <summary>
        /// 获取医保明细数据（医保端）
        /// </summary>
        /// <param name="STARTDATE"></param>
        /// <param name="ENDDATE"></param>
        /// <param name="HXFLAG"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public DataSet GETNJYBDETAILINFOBYDATE(string STARTDATE, string ENDDATE, string HXFLAG, Dictionary<string, string> dic)
        {
            return null;
        }

        /// <summary>
        /// 对帐明细支付端数据（不含APP） 20161122
        /// </summary>
        /// <param name="INCHECKDATE"></param>
        /// <returns></returns>
        public DataSet GETZFOTHERMX(string INCHECKDATE, string ZFTYPE, Dictionary<string, string> dic)
        {
            return null;
        }

        /// <summary>
        /// 获取医院服务器上平台明细数据 20170922
        /// </summary>
        /// <param name="INCHECKDATE"></param>
        /// <param name="ZFTYPE"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public DataSet GETPLATPOSMX(string INCHECKDATE, string ZFTYPE, Dictionary<string, string> dic)
        {
            return null;
        }

        public bool CHECKHOSCARD(string HOS_ID, string YLCARTD_TYPE, string YLCARD_NO, Dictionary<string, string> dic)
        {
            return true;
        }

        private bool regcalc(string BARCODE, string schemaId, string HOS_SN, string appid, string appsec, ref string sjh)
        {
            #region calcRegisterFee

            REGISTERFEE registerfee = new REGISTERFEE()
            {
                medicareParam = "",//        医保预留
                pactCode = "01",//   结算code      FALSE
                patientID = BARCODE,// 患者ID        FALSE
                scheduleId = schemaId,//         FALSE
                vipCardNo = "",//        FALSE
                vipCardType = "",//            FALSE
                preid = HOS_SN
            };

            var jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(registerfee);

            Hos185_His.Models.Output<REGISTERFEEDATA> outputregisterfee
      = new MIDServiceHelper().CallServiceAPI<REGISTERFEEDATA>("/hisbooking/register/calcRegisterFee", jsonstr, appid, appsec);

            if (outputregisterfee.code != 0)
            {
                return false;
            }

            sjh = outputregisterfee.data.receiptNumber + "|" + outputregisterfee.data.ghxh;

            return true;

            #endregion calcRegisterFee
        }

        /// <summary>
        /// 预约挂号保存
        /// </summary>
        /// <param name="Doc"></param>
        /// <returns></returns>
        public DataTable REGISTERAPPTSAVE(string SFZ_NO, string MOBILE_NO, string PAT_NAME, string SEX, string BIRTHDAY, string ADDRESS, string GUARDIAN_NAME, string GUARDIAN_SFZ_NO, int YLCARTD_TYPE, string YLCARD_NO, string HOS_ID, string DEPT_CODE, string DOC_NO, string SCH_DATE, string SCH_TIME, int SCH_TYPE, string PERIOD_START, string PERIOD_END, string WAIT_ID, string lTERMINAL_SN, string PASSWORD, Dictionary<string, string> dic)
        {
            string operCode = "MYNJ";

            if (dic.Keys.Contains("SOURCE"))
            {
                if (dic["SOURCE"].Trim() == "A000S185")
                {
                    operCode = "QHAPP";
                }
                if (dic["SOURCE"].Trim() == "H001S185")
                {
                    operCode = "MYNJ";
                }
                if (dic["SOURCE"].Trim().ToUpper() == "JSYBY")
                {
                    operCode = "YBYAPP";
                }
            }

            DataTable dtresult = new DataTable();
            dtresult.Columns.Add("CLBZ", typeof(string));
            dtresult.Columns.Add("CLJG", typeof(string));

            string YNCARDNO = "";
            string PAT_ID = dic.ContainsKey("PAT_ID") ? FormatHelper.GetStr(dic["PAT_ID"]) : "0";

            string BARCODE = GETPATHOSPITALID(YNCARDNO, SFZ_NO, PAT_NAME, SEX, BIRTHDAY, GUARDIAN_NAME, MOBILE_NO, ADDRESS, PAT_ID, YLCARTD_TYPE.ToString(), YLCARD_NO);
            if (BARCODE == "")
            {
                DataRow newrow = dtresult.NewRow();
                newrow["CLBZ"] = "1";
                newrow["CLJG"] = "很抱歉，未能获取到您的院内卡信息，请去人工窗口预约！";
                dtresult.Rows.Add(newrow);
                return dtresult;
            }

            string[] REGISTER_TYPE = dic["REGISTER_TYPE"].Split('|');

            string regLevelCode = REGISTER_TYPE[0];
            string noonCode = REGISTER_TYPE[1];
            string schemaId = REGISTER_TYPE[2];

            string pactCode;
            switch (YLCARTD_TYPE)
            {
                //case 2:
                //    pactCode = "17";
                //    break;

                default:
                    pactCode = "01";
                    break;
            }

            Hos185_His.Models.MZ.REGISTERAPPTSAVE appt = new REGISTERAPPTSAVE()
            {
                cardNo = BARCODE, //医院内部就诊卡号，唯⼀
                daypartId = "", //分时段id
                operCode = operCode, //操作员编号
                pactCode = pactCode, //合同类型编码
                patiSourceCode = "", //客⼾来源ID
                patiSourceName = "", //客⼾来源Name
                periodEnd = DateTime.Parse(PERIOD_END).ToString("HH:mm:ss"), //时间段结束时间 hh24:mi:ss
                periodStart = DateTime.Parse(PERIOD_START).ToString("HH:mm:ss"), //时间段开始时间 hh24:mi:ss
                phoneNo = MOBILE_NO, //预约时联系电话（不设置时取患者档案中的联系电话）
                schemaId = schemaId, //排班序号
                schemaPeriodId = "", //号源编号
                sourceType = "XCYY", //号源类别 XCYY:线下 XCGG:12320 OLYY:线上(互联⽹在线问诊)
                takeNumberPass = "", //取号密码
                timePeriodFlag = "1", //预约挂号启⽤分时段标志 0不分时段 1分时段
                ynfr = ""  //是否初诊 1 是 0 否
            };

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(appt);

            Hos185_His.Models.Output<REGISTERAPPTSAVEDATA> output
      = new MIDServiceHelper().CallServiceAPI<REGISTERAPPTSAVEDATA>("/hisbooking/appointment/save", jsonstr, operCode, operCode);

            DataRow drbody = dtresult.NewRow();
            drbody["CLBZ"] = output.code;
            drbody["CLJG"] = output.message;
            dtresult.Rows.Add(drbody);
            try
            {
                if (output.code != 0)
                {
                    return dtresult;
                }

                string SJH = "";
                string HOS_SN = output.data.apointMentCode;

                if (!regcalc(BARCODE, schemaId, HOS_SN, operCode, operCode, ref SJH))
                {
                    dtresult.Rows[0]["CLBZ"] = "7";
                    dtresult.Rows[0]["CLJG"] = "挂号预结算失败，请重试";
                    return dtresult;
                }

                dtresult.Columns.Add("HOS_SN");//: "837246",
                dtresult.Columns.Add("JE_ALL");//: "12.0",
                dtresult.Columns.Add("APPT_PAY");//: "12.0",
                dtresult.Columns.Add("APPT_ORDER");//: "2",
                dtresult.Columns.Add("APPT_TIME");//: "2023-02-10 08:05:00",
                dtresult.Columns.Add("APPT_PLACE");//: "门诊二楼五诊区",
                dtresult.Columns.Add("YQBZ");//: "H185"

                drbody["HOS_SN"] = output.data.apointMentCode;//: "837246",
                drbody["JE_ALL"] = output.data.payAll;//: "12.0",
                drbody["APPT_PAY"] = output.data.payAll;//: "12.0",
                drbody["APPT_ORDER"] = output.data.numNo;//: "2",

                string APPT_TIME = output.data.seeDate.Trim() + " " + output.data.periodSeeTime.Split('-')[0].ToString().Trim();
                drbody["APPT_TIME"] = APPT_TIME; //: "2023-02-10 08:05:00",
                drbody["APPT_PLACE"] = output.data.seeAddress;//: "门诊二楼五诊区",
                drbody["YQBZ"] = "H185";//: "H185"

                #region 排班及预算数据缓存

                RedisHelperSentinels redis = new RedisHelperSentinels();

                JObject jappt = new JObject
                {
                    { "YNCARDNO",BARCODE },
                    { "HOS_SN",output.data.apointMentCode },
                    { "REGISTER_TYPE", dic["REGISTER_TYPE"] },
                    { "APPT_PAY",output.data.payAll },
                    { "SJH_Zifei",SJH }
                };

                redis.Set("GH" + output.data.apointMentCode + HOS_ID, jappt, DateTime.Now.AddMinutes(120), 7);

                #endregion 排班及预算数据缓存

                return dtresult;
            }
            catch (Exception ex)
            {
                DataRow newrow = dtresult.NewRow();
                newrow["CLBZ"] = "1";
                newrow["CLJG"] = ex.Message; ;
                dtresult.Rows.Add(newrow);
                return dtresult;
            }
        }

        /// <summary>
        /// 预约(实时)挂号支付
        /// </summary>
        /// <param name="Doc"></param>
        /// <returns></returns>
        public DataTable REGISTERPAYSAVE(string SFZ_NO, string MOBILE_NO, string PAT_NAME, string SEX, string BIRTHDAY, string ADDRESS, string GUARDIAN_NAME, string GUARDIAN_SFZ_NO, int YLCARTD_TYPE, string YLCARD_NO, string QUERYID, string HOS_ID, string DEPT_CODE, string DOC_NO, string SCH_DATE, string SCH_TIME, string SCH_TYPE, string PERIOD_START, string WAIT_ID, string HOS_SN, decimal CASH_JE, string DEAL_STATES, string DEAL_TIME, string DEAL_TYPE, string lTERMINAL_SN, string PassWord, Dictionary<string, string> dic)
        {
            string PAT_ID = dic["PAT_ID"];
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";

            string BARCODE = GETPATHOSPITALID("", SFZ_NO, PAT_NAME, SEX, BIRTHDAY, GUARDIAN_NAME, MOBILE_NO, ADDRESS, PAT_ID, YLCARTD_TYPE.ToString(), YLCARD_NO);

            DataTable dtresult = new DataTable();
            dtresult.Columns.Add("CLBZ", typeof(string));
            dtresult.Columns.Add("CLJG", typeof(string));
            bool issyb = dic.ContainsKey("YBPAT_TYPE") && dic["YBPAT_TYPE"] == "JSSYB";//是否是省医保病人
            bool isnjyb = dic.ContainsKey("YBPAT_TYPE") && dic["YBPAT_TYPE"] == "NJSYB";//是否是南京市医保病人
            bool isgjyb = dic.ContainsKey("YBPAT_TYPE") && dic["YBPAT_TYPE"] == "CHSYB";//是否是国家医保病人
            bool isyby = dic.ContainsKey("YBPAT_TYPE") && dic["YBPAT_TYPE"] == "JSYBY";//南京医保云（江苏医保云）
            string transSerialNo = QUERYID;
            if (CASH_JE == 0)
            {
                QUERYID = "";
            }
            else
            {
                string sqlhmpay = "select *from hoshmpay where comm_main=@comm_main and HOS_ID=@HOS_ID and txn_type='01' ";
                MySqlParameter[] parameter2 = {
                    new MySqlParameter("@comm_main",QUERYID),
                    new MySqlParameter("@HOS_ID",HOS_ID) };

                DataTable dthmpay = DBQuery("", sqlhmpay.ToString(), parameter2).Tables[0];

                if (dthmpay != null && dthmpay.Rows.Count > 0)
                {
                    transSerialNo = CommonFunction.GetStr(dthmpay.Rows[0]["thirdtradeno"]);
                }
            }

            string deal_type = DEAL_TYPE;

            string jsonstr = "";
            string SJH = "";

            string register_type = dic["REGISTER_TYPE"].Trim();

            //兼容南京医保云 ，从缓存取 REGISTER_TYPE
            if (register_type == "" && (isyby || issyb))
            {
                Soft.DBUtility.RedisHelperSentinels redis = new RedisHelperSentinels();
                var appt = redis.Get("GH" + HOS_SN + HOS_ID, 7);
                JObject jappt = JObject.Parse(appt);

                register_type = jappt["REGISTER_TYPE"].ToString();
            }

            if (string.IsNullOrEmpty(register_type))
            {
                dtresult.Rows[0]["CLBZ"] = "222";
                dtresult.Rows[0]["CLJG"] = "分时段号源信息校验失败";
                return dtresult;
            }

            string[] REGISTER_TYPE = register_type.Split('|');

            string regLevelCode = REGISTER_TYPE[0];
            string noonCode = REGISTER_TYPE[1];
            string schemaId = REGISTER_TYPE[2];

            int existsThirdPay = 1;

            string sqlappt = "select SOURCE from register_appt where REG_ID=@REG_ID ";
            MySqlParameter[] parameter6 = {
                    new MySqlParameter("@REG_ID",dic["REG_ID"]) };

            DataTable dtREG = DBQuery("", sqlappt.ToString(), parameter6).Tables[0];

            if (dtREG != null && dtREG.Rows.Count == 1)
            {
                SOURCE = dtREG.Rows[0][0].ToString();
            }
            int payNature = 1;
            string hisPayMode = "51";
            string operCode = "MYNJ";

            if (SOURCE.Contains("H001S"))
            {
                hisPayMode = "55";
            }
            if (SOURCE.Contains("A000S"))
            {
                hisPayMode = "54";
                operCode = "QHAPP";
            }
            if (SOURCE.Contains("JSYBY"))
            {
                hisPayMode = "60";
                operCode = "YBYAPP";
            }
            if (CASH_JE == 0m)
            {
                hisPayMode = "0";
                existsThirdPay = 0;
                payNature = 2;
            }

            ///省医保app挂号
            if (issyb)
            {
                hisPayMode = "56";
                payNature = 3;
                operCode = "SYBAPP";
            }

            string medicareParam = "";
            P0601 p0601;
            DataRow dr = dtresult.NewRow();
            dtresult.Rows.Add(dr);

            if (isgjyb)
            {
                Dictionary<string, string> expContent = JSONSerializer.Deserialize<Dictionary<string, string>>(dic["EXPCONTENT"]);
                SJH = expContent["SJH"];
                string in2203 = expContent["chsInput2203"];

                string in2206 = expContent["chsInput2206"];
                string in2207 = dic["CHSINPUT2207"];

                string out2204 = expContent["chsOutput2204"];

                string out2207 = dic["CHSOUTPUT2207"];

                JObject jin2207 = JObject.Parse(in2207);

                JObject jzzj = new JObject();

                JObject jybrc = new JObject();
                jybrc.Add("in2201", "");
                jybrc.Add("in2203", JObject.Parse(in2203)["input"]);
                jybrc.Add("in2204", "");
                jybrc.Add("in2206", JObject.Parse(in2206)["input"]);
                jybrc.Add("in2207", jin2207);
                jybrc.Add("out2203", "");
                jybrc.Add("out2204", JObject.Parse(out2204)["output"]);
                jybrc.Add("out2206", "");
                jybrc.Add("out2207", JObject.Parse(out2207)["output"]);

                JObject jexpContentinfo = new JObject();
                jexpContentinfo.Add("in2207msgid", "");//我的南京无法获取 input 的msgid
                jybrc.Add("expContentinfo", jexpContentinfo);

                jzzj.Add("zzj", jybrc);

                medicareParam = Base64Encode(jzzj.ToString());
            }
            else if (issyb)
            {
                Soft.DBUtility.RedisHelperSentinels redis = new Soft.DBUtility.RedisHelperSentinels();
                var apptinfo = JObject.Parse(redis.Get("GH" + HOS_SN + HOS_ID, 7));

                SJH = apptinfo["sjh"].ToString();

                string in2203 = apptinfo["chsInput2203"].ToString();
                string out2204 = dic["CHSOUTPUT2204"].ToString();

                string in2206 = dic["CHSINPUT2207"].ToString();
                string in2207 = dic["CHSINPUT2207"];
                string out2207 = dic["CHSOUTPUT2207"];

                JObject jin2207 = JObject.Parse(in2207);

                JObject jzzj = new JObject();

                JObject jybrc = new JObject
                {
                    { "in2201", "" },
                    { "in2203", JObject.Parse(in2203) },
                    { "in2204", "" },
                    { "in2206", JObject.Parse(in2206)},
                    { "in2207", jin2207 },
                    { "out2203", "" },
                    { "out2204", JObject.Parse(out2204) },
                    { "out2206", "" },
                    { "out2207", JObject.Parse(out2207)["output"] }
                };

                JObject jexpContentinfo = new JObject();
                jexpContentinfo.Add("in2207msgid", "");//我的南京无法获取 input 的msgid
                jybrc.Add("expContentinfo", jexpContentinfo);

                jzzj.Add("zzj", jybrc);

                medicareParam = Base64Encode(jzzj.ToString());
                /*
                //DataTable dtappt = new Plat.BLL.BaseFunction().GetList("register_appt", "hos_sn='" + HOS_SN + "' and hos_id=" + HOS_ID + " ", "pre_no");

                string sqlappt2 = "select *from register_appt where HOS_SN=@HOS_SN and HOS_ID=@HOS_ID";
                MySqlParameter[] parameter5 = {
                    new MySqlParameter("@HOS_SN",HOS_SN),
                    new MySqlParameter("@HOS_ID",HOS_ID) };

                DataTable dtappt = DBQuery("", sqlappt2.ToString(), parameter5).Tables[0];

                if (dtappt != null && dtappt.Rows.Count == 1)
                {
                    SJH = dtappt.Rows[0]["pre_no"].ToString();
                }

                if (string.IsNullOrEmpty(SJH))
                {
                    return RtnResultDatatable("222", "获取收据号和挂号序号失败");
                }
                string[] js_out = dic["JS_OUT"].Split("|");

                JObject jzzj = new JObject();
                JObject jybrc = new JObject();
                JObject syb = new JObject();
                syb.Add("MdtrtId", js_out[0]);
                syb.Add("TotalAmount", js_out[1]);
                syb.Add("PubPay", js_out[2]);
                syb.Add("AccPay", js_out[3]);
                syb.Add("CashPay", js_out[4]);

                jzzj.Add("outSYBGH", syb);
                jybrc.Add("zzj", jzzj);
                medicareParam = Base64Encode(jybrc.ToString());
                */
            }
            else if (isyby)
            {
                Soft.DBUtility.RedisHelperSentinels redis = new Soft.DBUtility.RedisHelperSentinels();
                var apptinfo = JObject.Parse(redis.Get("GH" + HOS_SN + HOS_ID, 7));

                SJH = apptinfo["sjh"].ToString();
                string in2203 = apptinfo["ChsInput2203"].ToString();
                string out2204 = apptinfo["chsOutput2204"].ToString();

                string in2206 = apptinfo["chsInput2206"].ToString();
                string in2207 = dic["CHSINPUT2207"];
                string out2207 = dic["CHSOUTPUT2207"];

                JObject jin2207 = JObject.Parse(in2207);

                JObject jzzj = new JObject();

                JObject jybrc = new JObject
                {
                    { "in2201", "" },
                    { "in2203", JObject.Parse(in2203)["input"] },
                    { "in2204", "" },
                    { "in2206", JObject.Parse(in2206)["input"] },
                    { "in2207", jin2207 },
                    { "out2203", "" },
                    { "out2204", JObject.Parse(out2204)["output"] },
                    { "out2206", "" },
                    { "out2207", JObject.Parse(out2207)["output"] }
                };

                JObject jexpContentinfo = new JObject();
                jexpContentinfo.Add("in2207msgid", "");//我的南京无法获取 input 的msgid
                jybrc.Add("expContentinfo", jexpContentinfo);

                jzzj.Add("zzj", jybrc);

                medicareParam = Base64Encode(jzzj.ToString());
            }
            else
            {
                //自费的预算收据号放缓存中，在apptsave时保存
                Soft.DBUtility.RedisHelperSentinels redis = new Soft.DBUtility.RedisHelperSentinels();

                var rediscontent = redis.Get("GH" + HOS_SN + HOS_ID, 7);

                if (rediscontent != null)
                {
                    var apptinfo = JObject.Parse(rediscontent);

                    SJH = apptinfo["SJH_Zifei"].ToString();
                }

                if (string.IsNullOrEmpty(SJH))
                {
                    if (!regcalc(BARCODE, schemaId, HOS_SN, operCode, operCode, ref SJH))
                    {
                        dtresult.Rows[0]["CLBZ"] = "7";
                        dtresult.Rows[0]["CLJG"] = "挂号预结算失败，请重试";
                        return dtresult;
                    }
                }
            }

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

            string clinicCode = "";
            string receiptNumber = "";

            string je_all = dic["JE_ALL"];

            string[] hisneed = SJH.Split('|');
            clinicCode = hisneed[1];
            receiptNumber = hisneed[0];

            Hos185_His.Models.MZ.ThirdPayInfo thirdPayInfo = new Hos185_His.Models.MZ.ThirdPayInfo()
            {
                appPayMode = "", //对应APP⽀付⽅式
                bankNo = "", //银⾏名称
                businessType = "1", //业务类型 1挂号 2收费 3发卡 4预交款
                businessType2 = "1", //业务类型 1现场挂号 2预约挂号 3⻔诊收费 4住院预交⾦
                cardNo = BARCODE, //医院内部就诊卡号,唯⼀
                cashFee = CASH_JE, //第三⽅⽀付⾦额
                clinicCode = clinicCode, //挂号流⽔号
                hisOrderNo = "", //HIS订单号
                hisPayMode = hisPayMode, //对应HIS⽀付⽅式
                intenetAddress = "", //互联⽹医院收件地址
                intenetRecivePerson = "", //互联⽹医院收件⼈
                intenetTelNo = "", //互联⽹医院收件电话
                invoiceNo = "", //发票号
                machineNo = lTERMINAL_SN, //机器编号
                merchantOrderNo = string.IsNullOrEmpty(QUERYID) ? "" : QUERYID, //商⼾订单号
                operCode = operCode, //操作员编号
                payChannelNo = "", //⽀付渠道
                powerTranID = "", //平台交易ID
                powerTranType = "", //平台交易类型
                powerTranUserCode = "", //平台交易⽤⼾
                rrn = "",//对账流⽔号
                terminalNo = "", //交易终端号
                thirdOrderNo = string.IsNullOrEmpty(QUERYID) ? "" : QUERYID, //第三⽅订单号   ptlsh平台流水号
                thirdTradeInParam = "", //第三⽅交易⼊参参数
                thirdTradeOutParam = "", //第三⽅交易出参参数
                tradeTime = DEAL_TIME, //交易时间 yyyy-mm-dd HH24:mi:ss
                tranCardNum = "", //银⾏卡号
                transSerialNo = transSerialNo //交易流⽔号 （必传）   zflsh源启支付流水号
            };

            Hos185_His.Models.MZ.REGISTERPAYSAVE paysave = new Hos185_His.Models.MZ.REGISTERPAYSAVE()
            {
                appointMentCode = HOS_SN, //预约流⽔号
                cardNo = BARCODE, //医院内部就诊卡号,唯⼀
                daypartId = "", //分时段Id
                existsThirdPay = existsThirdPay, //是否涉及第三⽅⽀付 1是 0否
                invoiceNo = "", //发票号,如果不传，则系统⽣成
                isUnionClinic = "", //是否涉及康复医院会诊信息
                lifeEquityCardNo = "", //权益卡卡号
                lifeEquityCardType = "", //权益卡类型
                mcardNo = "", //医疗证号
                mcardNoType = "", //医疗证类型
                operCode = operCode, //操作员编号
                pactCode = "01", //合同编号
                payNature = payNature, //收费性质0 免费1 ⾃费2 医保3 医保+⾃费
                periodEnd = "", //时间段结束时间 hh24:mi:ss
                periodStart = DateTime.Parse(PERIOD_START).TimeOfDay.ToString(), //时间段开始时间 hh24:mi:ss
                schemaId = "", //排班序号
                schemaPeriodId = "", //号源编号
                sourceType = "XCYY", //号源类别 XCYY:线下 XCGG:12320 OLYY:线上(互联⽹在线问诊)
                terminalCode = lTERMINAL_SN, //设备终端编号号
                timePeriodFlag = "1", //挂号启⽤分时段标志 0不分时段 1分时段
                totalFee = decimal.Parse(je_all),//总费⽤
                receiptNumber = receiptNumber,
                medicareParam = medicareParam
            };

            paysave.thirdPayInfo = thirdPayInfo;
            paysave.medicareInfo = medicareInfo;
            paysave.consultInfoParam = consultInfoParam;
            jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(paysave);

            Output<REGISTERPAYSAVEDATA> output
                = new MIDServiceHelper().CallServiceAPI<REGISTERPAYSAVEDATA>("/hisbooking/register/save", jsonstr, operCode, operCode);

            try
            {
                if (output.code != 0)
                {
                    if (output.code == 110 && output.statusCode == 200)
                    {
                        dtresult.Rows[0]["CLBZ"] = "222";
                        dtresult.Rows[0]["CLJG"] = output.message;
                        return dtresult;
                    }
                }
                else
                {
                    #region P0601

                    if (CASH_JE != 0m && deal_type.ToUpper() == "HM")
                    {
                        p0601 = new P0601()
                        {
                            outTradeNo = "", //商⼾订单号, 商⼾订单号和平台订单号必填⼀个
                            transactionId = QUERYID, //平台订单号 商⼾订单号和平台订单号必填⼀个
                            confirmState = "success", //确认状态 success 确认成功 fail 失败
                            confirmDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), //确认时间 yyyy-mm-dd HH:mm:ss
                            receiptNo = output.data.invoiceNo //HIS发票号,多张发票⽤,分隔
                        };
                        jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(p0601);

                        var outputp0601 = new MIDServiceHelper().CallServiceAPI<JObject>("/platformpayment/pay/confirmPay", jsonstr, operCode, operCode);
                    }

                    #endregion P0601

                    dtresult.Columns.Add("HOS_ID");
                    dtresult.Columns.Add("HOS_SN");
                    dtresult.Columns.Add("APPT_PAY");
                    dtresult.Columns.Add("OPT_SN");
                    //dtresult.Columns.Add("APPT_SN");
                    dtresult.Columns.Add("ZS_NAME");
                    dtresult.Columns.Add("BH");
                    dtresult.Columns.Add("REALTIMES");
                    dtresult.Columns.Add("RCPT_NO");
                    dtresult.Columns.Add("PRE_NO");
                    dtresult.Rows[0]["CLBZ"] = "0";

                    dtresult.Rows[0]["CLJG"] = "SUCCESS";
                    dtresult.Rows[0]["HOS_ID"] = "185";
                    dtresult.Rows[0]["HOS_SN"] = output.data.clinicCode;
                    dtresult.Rows[0]["APPT_PAY"] = output.data.regFee;
                    dtresult.Rows[0]["OPT_SN"] = output.data.cardNo;
                    //dtresult.Rows[0]["APPT_SN"] = SJH.Split('|')[0];
                    dtresult.Rows[0]["ZS_NAME"] = output.data.deptName;

                    string bh = output.data.seeNo;

                    if (DEPT_CODE == "07011001")
                    {
                        bh = "以现场签到为准";
                    }

                    dtresult.Rows[0]["BH"] = bh;
                    dtresult.Rows[0]["REALTIMES"] = "";
                    dtresult.Rows[0]["RCPT_NO"] = output.data.invoiceNo;
                    dtresult.Rows[0]["PRE_NO"] = "";

                    if (SCH_TYPE == "4")
                    {
                        HLWPaysaveMessageSend(lTERMINAL_SN, DOC_NO, PAT_NAME, SCH_DATE, SCH_TIME, PERIOD_START, SCH_TYPE, DEPT_CODE);
                    }
                }

                return dtresult;
            }
            catch (Exception ex)
            {
                SaveLog(DateTime.Now, JsonConvert.SerializeObject(ex), DateTime.Now, "Registerpaysave执行异常");//保存his接口日志

                return null;
            }
        }

        /// <summary>
        /// 互联网医生短信通知
        /// </summary>
        /// <param name="lTERMINAL_SN"></param>
        /// <param name="DOC_NO"></param>
        /// <param name="PAT_NAME"></param>
        /// <param name="SCH_DATE"></param>
        /// <param name="SCH_TIME"></param>
        /// <param name="PERIOD_START"></param>
        /// <param name="SCH_TYPE"></param>
        private void HLWPaysaveMessageSend(string lTERMINAL_SN, string DOC_NO, string PAT_NAME, string SCH_DATE, string SCH_TIME, string PERIOD_START, string SCH_TYPE, string DEPT_CODE)
        {
            try
            {
                if (lTERMINAL_SN != "yunhosCooperate" && SCH_TYPE == "4")
                {
                    //DataTable dtDoc = new Plat.BLL.BaseFunction().GetList("yunhou.yunhos_doc", "HOS_ID='" + HOS_ID + "' and DOC_NO='" + DOC_NO + "'", "MOBILE_NO,DOC_NAME");

                    string sqlregpay = "select * from yunhos_doc where HOS_ID=@HOS_ID and DOC_NO=@DOC_NO";

                    MySqlParameter[] parameter3 = {
                    new MySqlParameter("@HOS_ID",HOS_ID),
                    new MySqlParameter("@DOC_NO",DOC_NO)};

                    DataTable dtDoc = DBQuery("yunhou", sqlregpay.ToString(), parameter3).Tables[0];

                    if (dtDoc.Rows.Count > 0)
                    {
                        if (FormatHelper.GetStr(dtDoc.Rows[0]["MOBILE_NO"]) != "")
                        {
                            try
                            {
                                //DataTable dtdeptinfo = new Plat.BLL.BaseFunction().GetList("DEPT_INFO", "HOS_ID='" + HOS_ID + "' and DEPT_CODE='" + DEPT_CODE + "'", "DEPT_NAME");

                                string sqlappt = "select *from DEPT_INFO where DEPT_CODE=@DEPT_CODE and HOS_ID=@HOS_ID";
                                MySqlParameter[] parameter2 = {
                    new MySqlParameter("@DEPT_CODE",DEPT_CODE),
                    new MySqlParameter("@HOS_ID",HOS_ID) };

                                DataTable dtdeptinfo = DBQuery("", sqlappt.ToString(), parameter2).Tables[0];

                                /*{南京明基医院}尊敬的xxxxxxxxxxxxx医生：互联网xxxxxxxxxxxxxxxxx 有患者xxxxxxxxxxxxx 问诊xxxxxxxxxxxxxxx（日期）的号，请您及时接诊。*/
                                string message = string.Format(@"尊敬的{0}医生：互联网{1}有患者{2} 问诊{3}（日期）的号，请您及时接诊。", FormatHelper.GetStr(dtDoc.Rows[0]["DOC_NAME"]), dtdeptinfo.Rows.Count > 0 ? FormatHelper.GetStr(dtdeptinfo.Rows[0]["DEPT_NAME"]) : "", PAT_NAME, SCH_DATE);
                                SENDMSG(HOS_ID, message, FormatHelper.GetStr(dtDoc.Rows[0]["MOBILE_NO"]), "", "");
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 检查预约是否可以取消
        /// 返回dt中有2个字段，是否可以取消，具体说明
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="HOS_SNAPPT">院内预约唯一流水号</param>
        /// <param name="HOS_SN">院内挂号唯一流水号</param>
        /// <returns></returns>
        public DataTable REGISTERCANCELCHECK(string HOS_ID, string HOS_SNAPPT, string HOS_SN, Dictionary<string, string> dic)
        {
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("REGISTERCANCELCHECK", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SNAPPT", HOS_SNAPPT.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", HOS_SN.Trim());
            //支付了不能取消，未支付的都可以取消,没有医院调用
            try
            {
                //DataTable dt = new Plat.BLL.BaseFunction().GetList("register_appt", "HOS_ID='" + HOS_ID + "' and HOS_SN='" + HOS_SN + "' ", "APPT_TYPE");

                string sqlappt = "select *from register_appt where HOS_SN=@HOS_SN and HOS_ID=@HOS_ID";
                MySqlParameter[] parameter2 = {
                    new MySqlParameter("@HOS_SN",HOS_SN),
                    new MySqlParameter("@HOS_ID",HOS_ID) };

                DataTable dt = DBQuery("", sqlappt.ToString(), parameter2).Tables[0];

                if (dt != null && dt.Rows.Count > 0)
                {
                    string cancancel = "0";//不可以取消
                    string cnote = "已支付不能取消";
                    if (dt.Rows[0]["APPT_TYPE"].ToString().Trim() == "0")//允许撤销
                    {
                        cancancel = "1";
                        cnote = "可以取消";
                    }
                    DataTable dtrev = new DataTable();
                    dtrev.Columns.Add("CLBZ", typeof(string));
                    dtrev.Columns.Add("CLJG", typeof(string));
                    dtrev.Columns.Add("CANCANCEL", typeof(string));
                    dtrev.Columns.Add("NOTE", typeof(string));
                    DataRow dr = dtrev.NewRow();
                    dr["CLBZ"] = "0";
                    dr["CLJG"] = "SUCCESS";
                    dr["CANCANCEL"] = cancancel;
                    dr["NOTE"] = "";
                    dtrev.Rows.Add(dr);
                    return dtrev;
                }
                else
                {
                    DataTable dtrev = new DataTable();
                    dtrev.Columns.Add("CLBZ", typeof(string));
                    dtrev.Columns.Add("CLJG", typeof(string));
                    dtrev.Columns.Add("CANCANCEL", typeof(string));
                    dtrev.Columns.Add("NOTE", typeof(string));
                    DataRow dr = dtrev.NewRow();
                    dr["CLBZ"] = "1";
                    dr["CLJG"] = "FAIL";
                    dr["CANCANCEL"] = "0";
                    dr["NOTE"] = "支付取消失败";
                    dtrev.Rows.Add(dr);
                    return dtrev;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 预约取消(含支付)
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="HOS_SNAPPT">院内预约唯一流水号</param>
        /// <param name="HOS_SN">院内挂号唯一流水号</param>
        /// <param name="CASH_JE">退费金额（以下为已支付退费信息）</param>
        /// <param name="DEAL_STATES">交易状态</param>
        /// <param name="DEAL_TIME">交易时间</param>
        /// <param name="DEAL_TYPE">交易方式</param>
        /// <param name="lTERMINAL_SN">终端标识</param>
        /// <returns></returns>
        public bool REGISTERPAYCANCEL(string HOS_ID, string HOS_SNAPPT, string HOS_SN, decimal CASH_JE, string DEAL_STATES, DateTime DEAL_TIME, string DEAL_TYPE, string lTERMINAL_SN, string PASSWORD, out string CLJG, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";

            string BARCODE;
            string clinicCode;
            string pat_id;
            string pat_name;
            string sfz_no;
            if (SOURCE == "NJCLOUD" || SOURCE == "JSSYB")
            {
                pat_id = dic["PAT_ID"];

                string sqlregpay = "select * from register_pay where reg_id=@reg_id";
                MySqlParameter[] parameter3 = {
                    new MySqlParameter("@reg_id",dic["REG_ID"]) };

                DataTable dtpat_card = DBQuery("", sqlregpay.ToString(), parameter3).Tables[0];

                clinicCode = dtpat_card.Rows[0]["HOS_SN"].ToString();
            }
            else
            {
                string sqlregpay = "select * from register_pay where pay_id=@pay_id";
                MySqlParameter[] parameter3 = {
                    new MySqlParameter("@pay_id",dic["PAY_ID"]) };

                DataTable dtpat_card = DBQuery("", sqlregpay.ToString(), parameter3).Tables[0];

                pat_id = dtpat_card.Rows[0]["PAT_ID"].ToString();
                clinicCode = dic["HOS_SN"];
                string reg_id = dtpat_card.Rows[0]["REG_ID"].ToString();

                string sqlappt = "select * from register_appt where reg_id=@reg_id";
                MySqlParameter[] parameterappt = {
                    new MySqlParameter("@reg_id",reg_id) };

                DataTable dtappt = DBQuery("", sqlappt.ToString(), parameterappt).Tables[0];

                SOURCE = dtappt.Rows[0]["SOURCE"].ToString();
            }
            string sqlpatinfo = "select * from pat_info where PAT_ID=@PAT_ID";

            MySqlParameter[] parameter5 = {
                    new MySqlParameter("@PAT_ID", pat_id) };

            DataTable dtpat_info = DBQuery("", sqlpatinfo.ToString(), parameter5).Tables[0];
            pat_name = dtpat_info.Rows[0]["PAT_NAME"].ToString();

            sfz_no = PlatDataSecret.DataSecret.DeSfzNoSecretByAes(CommonFunction.GetStr(dtpat_info.Rows[0]["SFZ_SECRET"]));

            BARCODE = GETPATHOSPITALID("", sfz_no, pat_name, "", "", "", "", "", pat_id, "", "");

            try
            {
                string transSerialNo = "";// dic["ORDERID"];

                if (DEAL_TYPE.ToUpper() == "HM")//南京医保云没有这个值 省医保支付方式是D
                {
                    transSerialNo = dic["ORDERID"];
                }

                string operCode = "MYNJ";
                string hisPayMode = "51";
                if (SOURCE.Contains("H0001S"))
                {
                    operCode = "MYNJ";

                    hisPayMode = "55";
                }
                if (SOURCE.Contains("A000S"))
                {
                    hisPayMode = "54";
                    operCode = "QHAPP";
                }
                if (SOURCE.Contains("H005P") || SOURCE == "JSSYB")
                {
                    hisPayMode = "56";

                    operCode = "SYBAPP";
                }
                if (SOURCE.Contains("NJCLOUD"))
                {
                    operCode = "YBYAPP";

                    hisPayMode = "60";
                }
                if (CASH_JE == 0m)
                {
                    hisPayMode = "0";
                }

                REGISTERPAYCANCEL paycancel = new REGISTERPAYCANCEL()
                {
                    apointMentCode = HOS_SN,  //预约流水号
                    cardNo = BARCODE,  //医院内部就诊卡号,唯一
                    clinicCode = clinicCode,  //挂号流水号(结算）
                    merchantOrderNo = "", //商户订单号
                    outTradeNo = "",
                    transactionId = transSerialNo,//同queryid
                    operCode = operCode,  //操作员编号
                    returnFee = CASH_JE,  //退费金额
                    rrn = "",  //对账流水号
                    sourceType = "XCYY",  //号源类别 XCYY:线下 XCGG:12320 OLYY:线上(互联网在线问诊)
                    thirdOrderNo = "",  //第三方订单号
                    transSerialNo = "", //交易流水号
                    payMode = hisPayMode//支付方式，对接卫宁必传
                };

                string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(paycancel);

                var output = new MIDServiceHelper().CallServiceAPI<REGISTERPAYCANCELDATA>("/hisbooking/register/cancel", jsonstr, operCode, operCode);

                if (output.code != 0)
                {
                    CLJG = output.message;
                    return false;
                }


                #region
                var p0601 = new P0601()
                {
                    outTradeNo = "", //商⼾订单号, 商⼾订单号和平台订单号必填⼀个
                    transactionId = transSerialNo, //平台订单号 商⼾订单号和平台订单号必填⼀个
                    confirmState = "fail", //确认状态 success 确认成功 fail 失败
                    confirmDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), //确认时间 yyyy-mm-dd HH:mm:ss
                    receiptNo = "" //HIS发票号,多张发票⽤,分隔
                };
                var jsonstr0601 = Newtonsoft.Json.JsonConvert.SerializeObject(p0601);

                var outputp0601 = new MIDServiceHelper().CallServiceAPI<JObject>("/platformpayment/pay/confirmPay", jsonstr0601, operCode, operCode);

                if (outputp0601.code == 0)
                {
                    DS_RtnDealInfo("0", "提交退款申请成功");
                }
                else
                {
                     DS_RtnDealInfo("0", "退款申请提交失败");
                }






                #endregion

                if (DEAL_TYPE == "U")//南京医保云App，退号-退费，自建小程序 公司平台会调用chargerefund退费
                {
                    //取表 jsyby_orderTran

                    string payOrdId = "";
                    string feeSumamt = "";
                    string ownpayAmt = "";
                    string psnAcctPay = "";
                    string fundPay = "";
                    string payauthno = "";

                    DataTable dtjsyby_orderTran = new Plat.BLL.BaseFunction().GetList("jsyby_orderTran", "medorgord='" + HOS_SN + "' and hos_id=" + HOS_ID + " "
                        , "payOrdId", "feeSumamt"
                         , "ownpayAmt", "psnAcctPay"
                          , "fundPay");

                    if (dtjsyby_orderTran.Rows.Count > 0)
                    {
                        DataTable dtappt = new Plat.BLL.BaseFunction().GetList("register_appt", "hos_sn='" + HOS_SN + "' and hos_id=" + HOS_ID + " ", "payauthno");

                        payOrdId = dtjsyby_orderTran.Rows[0]["payOrdId"].ToString();
                        feeSumamt = dtjsyby_orderTran.Rows[0]["feeSumamt"].ToString();
                        ownpayAmt = dtjsyby_orderTran.Rows[0]["ownpayAmt"].ToString();
                        psnAcctPay = dtjsyby_orderTran.Rows[0]["psnAcctPay"].ToString();
                        fundPay = dtjsyby_orderTran.Rows[0]["fundPay"].ToString();
                        payauthno = dtappt.Rows[0]["payauthno"].ToString();

                        Dictionary<string, string> dicrefund = new Dictionary<string, string>() {
                                    {"INSUDIVID",payOrdId },
                                    {"TOTLREFDAMT",feeSumamt },
                                    {"CASHREFDAMT",ownpayAmt },
                                    {"PSNACCTREFDAMT",psnAcctPay },
                                    {"FUNDREFDAMT",fundPay },
                                    {"PAYAUTHNO",payauthno },
                                };
                        DataSet dsrefund = CHARGEREFUND(dicrefund);

                        if (dsrefund.Tables[0].Rows[0]["CLBZ"].ToString() == "0")
                        {
                            string i2008 = dsrefund.Tables[0].Rows[0]["CHSINPUT2208"].ToString();
                            string o2008 = dsrefund.Tables[0].Rows[0]["CHSOUTPUT2208"].ToString();
                            string refund_id = dsrefund.Tables[0].Rows[0]["REFUND_ID"].ToString();

                            XmlDocument doctfinfo = new XmlDocument();
                            doctfinfo = QHXmlMode.GetBaseXml("CHSREGREFUNDSAVE", "0");
                            XMLHelper.X_XmlInsertNode(doctfinfo, "ROOT/BODY", "HOS_ID", "185");
                            XMLHelper.X_XmlInsertNode(doctfinfo, "ROOT/BODY", "HOS_SN", HOS_SN);
                            XMLHelper.X_XmlInsertNode(doctfinfo, "ROOT/BODY", "USER_ID", "");
                            XMLHelper.X_XmlInsertNode(doctfinfo, "ROOT/BODY", "CHSINPUT2208", i2008);
                            XMLHelper.X_XmlInsertNode(doctfinfo, "ROOT/BODY", "CHSOUTPUT2208", o2008);
                            XMLHelper.X_XmlInsertNode(doctfinfo, "ROOT/BODY", "SOURCE", "JSYBY");

                            //CallService(doctfinfo.InnerXml);
                        }
                    }
                    else
                    {
                    }
                }

                JObject jobj = new JObject
                {
                    { "refundReceiptNumber", output.data.refundReceiptNumber },
                    { "refundId", output.data.refundId }
                };

                RedisHelperSentinels redis = new RedisHelperSentinels();
                redis.Set("TH" + HOS_SN + HOS_ID, jobj.ToString(), DateTime.Now.AddMinutes(120), 7);

                CLJG = output.message;
                return true;
            }
            catch (Exception ex)
            {
                //ServiceBUS.Log.LogHelper.SaveLogHOS(DateTime.Now, "REGISTERPAYCANCEL", DateTime.Now, ex.ToString().Replace("'", ""));
                Log.Helper.Model.ModLogHos modLogHos = new Log.Helper.Model.ModLogHos();
                modLogHos.inTime = DateTime.Now;
                modLogHos.outTime = DateTime.Now;
                modLogHos.inXml = "REGISTERPAYCANCEL";
                modLogHos.outXml = ex.ToString().Replace("'", "");
                Log.Helper.LogHelper.Addlog(modLogHos);
                CLJG = ex.ToString();
                return false;
            }
        }

        private DataSet CHARGEREFUND(Dictionary<string, string> external)
        {
            string payOrdId = external["INSUDIVID"].ToString();//minijsyby.INSUDIVID
            string totlRefdAmt = external["TOTLREFDAMT"].ToString().Trim();//minijsyby.feesumamt
            string cashRefdAmt = external["CASHREFDAMT"].ToString().Trim();//minijsyby.ownpayamt
            string psnAcctRefdAmt = external["PSNACCTREFDAMT"].ToString().Trim();//psnacctpay
            string fundRefdAmt = external["FUNDREFDAMT"].ToString().Trim();//feesumamt
            string payAuthNo = external["PAYAUTHNO"].ToString().Trim();//PAYAUTHNO

            JObject j6203 = new JObject
            {
                { "HOS_ID", "185" },

                { "payOrdId", payOrdId },
                { "appRefdSn", Guid.NewGuid().ToString()  },
                { "appRefdTime", DateTime.Now.ToString("yyyyMMddHHmmss") },

                { "psnAcctRefdAmt", null },
                { "fundRefdAmt", null },
                { "ecToken", null },

                { "totlRefdAmt", decimal.Parse(totlRefdAmt) },
                { "cashRefdAmt", decimal.Parse(cashRefdAmt) },
                { "refdType", "ALL" },//ALL:全部，CASH:只退现金 HI:只退医保
                { "payAuthNo", payAuthNo },
                { "exp_content", "" }
            };

            try
            {
                string rtnxml = CallMidService(j6203.ToString(), "CHARGEREFUND");

                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataSet dsbody = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY");
                return dsbody;
            }
            catch (Exception ex)
            {
                DataSet ds = new DataSet();
                DataTable dtrev = RtnResultDatatable("99", ex.Message);
                ds.Tables.Add(dtrev.Copy());
                return ds;
            }
        }

        /// <summary>
        /// 未缴费诊间费用查询(汇总、按医嘱处方)
        /// 返回未缴费列表dt，多条数据
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="HOS_SN">院内挂号唯一流水号</param>
        /// <param name="SFZ_NO">身份证号</param>
        /// <returns></returns>
        public DataSet GETOUTFEENOPAY(string HOS_ID, string HOS_SN, string SFZ_NO, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";

            //if (SOURCE == "A000S185")
            //{
            //    return null;
            //}

            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("GETOUTFEENOPAY", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", "-1");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", SFZ_NO);
            string SFZ_SECRET = PlatDataSecret.DataSecret.GetSfzNoSecret(SFZ_NO);
            long PAT_ID = dic.ContainsKey("PAT_ID") ? long.Parse(dic["PAT_ID"]) : 0;

            DataTable dtresult = new DataTable();
            dtresult.Columns.Add("CLBZ", typeof(string));
            dtresult.Columns.Add("CLJG", typeof(string));

            string YLCARD_TYPE = "";
            string YLCARD_NO = "";
            //DataTable dtpat_card = new Plat.BLL.BaseFunction().Query(string.Format(@"select a.ylcartd_type,a.ylcard_no,b.BIRTHDAY,b.sex,b.GUARDIAN_NAME,b.address,a.ylcard_no,b.sfz_secret,b.mobile_secret,b.pat_name from pat_card a,pat_info b where  a.pat_id=b.pat_id and a.pat_id='{0}' and A.mark_del='0' order by a.ylcartd_type", PAT_ID));

            string sqlpatinfo = "select a.ylcartd_type,a.ylcard_no,b.BIRTHDAY,b.sex,b.GUARDIAN_NAME,b.address,a.ylcard_no,b.sfz_secret,b.mobile_secret,b.pat_name from pat_card a,pat_info b where  a.pat_id=b.pat_id and a.pat_id=@pat_id and A.mark_del='0' order by a.ylcartd_type";

            MySqlParameter[] parameter5 = {
                    new MySqlParameter("@PAT_ID",  PAT_ID) };

            DataTable dtpat_card = DBQuery("", sqlpatinfo.ToString(), parameter5).Tables[0];

            string PAT_NAME = "";
            string YNCARDNO = "";

            #region 获取院内号 ，如果不存在则建档

            if (dtpat_card != null && dtpat_card.Rows.Count > 0)
            {
                DataRow[] drssyb = dtpat_card.Select("YLCARTD_TYPE='6'");
                DataRow[] drsyb = dtpat_card.Select("YLCARTD_TYPE='2'");
                if (drssyb.Length > 0)
                {
                    YLCARD_TYPE = "6";
                    YLCARD_NO = FormatHelper.GetStr(drssyb[0]["YLCARD_NO"]);
                }
                else if (drsyb.Length > 0)
                {
                    YLCARD_TYPE = "2";
                    YLCARD_NO = FormatHelper.GetStr(drsyb[0]["YLCARD_NO"]);
                }
                else
                {
                    YLCARD_TYPE = FormatHelper.GetStr(dtpat_card.Rows[0]["ylcartd_type"]) == "0" ? "4" : FormatHelper.GetStr(dtpat_card.Rows[0]["ylcartd_type"]);
                    YLCARD_NO = FormatHelper.GetStr(dtpat_card.Rows[0]["YLCARD_NO"]);
                }
            }
            //DataTable dtpatcardbind = new Plat.BLL.BaseFunction().GetList("pat_card_bind", "HOS_ID='" + HOS_ID + "' and PAT_ID='" + PAT_ID + "' and YLCARTD_TYPE=1 order by BAND_TIME DESC", "YLCARD_NO");

            string sqlcardbind = "select * from pat_card_bind where HOS_ID=@HOS_ID and PAT_ID=@PAT_ID and YLCARTD_TYPE=1  order by BAND_TIME DESC";

            MySqlParameter[] parameter4 = {
                    new MySqlParameter("@HOS_ID",HOS_ID),
                    new MySqlParameter("@PAT_ID", PAT_ID) };

            DataTable dtpatcardbind = DBQuery("", sqlcardbind.ToString(), parameter4).Tables[0];

            if (dtpatcardbind.Rows.Count > 0)
            {
                YNCARDNO = dtpatcardbind.Rows[0]["YLCARD_NO"].ToString().Trim();
            }
            else
            {
                if (dtpat_card.Rows.Count > 0)
                {
                    PAT_NAME = dtpat_card.Rows[0]["pat_name"].ToString().Trim();
                    string SEX = dtpat_card.Rows[0]["SEX"].ToString().Trim();
                    string BIRTHDAY = dtpat_card.Rows[0]["BIRTHDAY"].ToString().Trim();
                    string GUARDIAN_NAME = dtpat_card.Rows[0]["GUARDIAN_NAME"].ToString().Trim();
                    string MOBILE_NO = PlatDataSecret.DataSecret.DeMobileSecretByAes(FormatHelper.GetStr(dtpat_card.Rows[0]["mobile_secret"]));
                    string ADDRESS = dtpat_card.Rows[0]["ADDRESS"].ToString().Trim();
                    YNCARDNO = GETPATHOSPITALID(YNCARDNO, SFZ_NO, PAT_NAME, SEX, BIRTHDAY, GUARDIAN_NAME, MOBILE_NO, ADDRESS, PAT_ID.ToString(), YLCARD_TYPE, YLCARD_NO);
                }
            }

            #endregion 获取院内号 ，如果不存在则建档

            Hos185_His.Models.MZ.GETOUTFEENOPAY nopay = new Hos185_His.Models.MZ.GETOUTFEENOPAY()
            {
                cardNo = YNCARDNO,  //医院内部就诊卡号,唯一
                clinicCode = "",  //挂号流水号
                idCardNo = SFZ_NO,  //证件号
                idCardType = "01",  //证件类型 01:身份证 03:护照 06:港澳居民来往内地通行证 07:台湾居民来往内地通行证
                lifeEquityCardNo = "",  //权益卡卡号
                lifeEquityCardType = "",  //权益卡类型 2 医慧卡
                mcardNo = "",  //绑定的医疗证号
                mcardNoType = "",  //绑定的医疗证类型 1:就诊卡 4:身份证 5:医保/市民卡/护照
                pactCode = "01"  //合同编号
            };

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(nopay);

            Output<List<Hos185_His.Models.MZ.GETOUTFEENOPAYDATA>> output
   = new MIDServiceHelper().CallServiceAPI<List<Hos185_His.Models.MZ.GETOUTFEENOPAYDATA>>("/hischargesinfo/outpatientfee/recipeinfo", jsonstr);

            DataTable dtbody = new DataTable();

            DataTable dtrev = new DataTable();

            dtbody.Columns.Add("CLBZ", typeof(string));
            dtbody.Columns.Add("CLJG", typeof(string));
            DataRow drbody = dtbody.NewRow();
            drbody["CLBZ"] = output.code;
            drbody["CLJG"] = output.message;
            dtbody.Rows.Add(drbody);

            if (output.code == 0 && output.data.Count > 0)
            {
                #region 合并处方

                string YBPAY = "0";

                if (output.data.FindAll(x => x.ybPay == "1").Count > 0)
                {
                    YBPAY = "1";
                }
                var query = from a in output.data.AsEnumerable()
                            group a by new
                            {
                                OPT_SN = a.cardNo,
                                HOS_SN = a.clinicCode,
                                DEPT_CODE = a.deptCode,
                                DEPT_NAME = a.deptName,
                                DOC_NO = a.doctCode,
                                DOC_NAME = a.doctName,

                                YB_NOPAY_REASON = "",
                                YLLB = "",
                                DIS_CODE = a.mainDiagCode,
                                DIS_TYPE = ""
                            }
                      into g
                            select
                new
                {
                    g.Key.OPT_SN,
                    g.Key.HOS_SN,
                    PRE_NO = string.Join("#", g.Select(a => FormatHelper.GetStr(a.recipeNo)).ToArray()),
                    g.Key.DEPT_CODE,
                    g.Key.DEPT_NAME,
                    g.Key.DOC_NO,
                    g.Key.DOC_NAME,

                    g.Key.YB_NOPAY_REASON,
                    g.Key.YLLB,
                    g.Key.DIS_CODE,
                    g.Key.DIS_TYPE,
                    JEALL = g.Sum(a => FormatHelper.GetDecimal(a.totalFee)),
                    CASH_JE = g.Sum(a => FormatHelper.GetDecimal(a.totalFee))
                };

                #endregion 合并处方

                dtrev.Columns.Add("HOS_ID");
                dtrev.Columns.Add("OPT_SN");
                dtrev.Columns.Add("PRE_NO");
                dtrev.Columns.Add("HOS_SN");
                dtrev.Columns.Add("DEPT_CODE");
                dtrev.Columns.Add("DEPT_NAME");
                dtrev.Columns.Add("DOC_NO");
                dtrev.Columns.Add("DOC_NAME");
                dtrev.Columns.Add("JEALL");
                dtrev.Columns.Add("CASH_JE");
                dtrev.Columns.Add("YB_PAY");
                dtrev.Columns.Add("ONLINE_PRE");
                dtrev.Columns.Add("YQBZ");

                foreach (var item in query)
                {
                    DataRow dr = dtrev.NewRow();

                    dr["HOS_ID"] = "185";
                    dr["OPT_SN"] = item.OPT_SN;
                    dr["PRE_NO"] = item.PRE_NO;
                    dr["HOS_SN"] = item.HOS_SN + "_" + item.PRE_NO.Split("#")[0] + "_" + item.PRE_NO.Split("#").Length;
                    dr["DEPT_CODE"] = item.DEPT_CODE;
                    dr["DEPT_NAME"] = item.DEPT_NAME;
                    dr["DOC_NO"] = item.DOC_NO;
                    dr["DOC_NAME"] = item.DOC_NAME;
                    dr["JEALL"] = item.JEALL;
                    dr["CASH_JE"] = item.CASH_JE;
                    dr["YB_PAY"] = YBPAY;
                    dr["ONLINE_PRE"] = "0";
                    dr["YQBZ"] = "H185";

                    dtrev.Rows.Add(dr);
                }
            }
            else
            {
                //没有待缴费的处方

                return DS_RtnDealInfo("2", "没有待缴费的处方");
            }

            dtrev.TableName = "dt2";
            dtbody.Columns.Add("no_repeat", typeof(int));//add by hlw 2018.01.23 只是普通勿进行重复查询
            dtbody.Rows[0]["no_repeat"] = 1;
            DataSet dsrev = new DataSet();
            dsrev.Tables.Add(dtbody.Copy());
            dsrev.Tables.Add(dtrev.Copy());
            return dsrev;
        }

        private decimal GetMXJE(string HOS_ID, string OPT_SN, string PRE_NO, string HOS_SN, string lTERMINAL_SN, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            decimal je = 0;
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("GETOUTFEENOPAYMX", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "OPT_SN", OPT_SN.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PRE_NO", PRE_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", HOS_SN.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "LTERMINAL_SN", lTERMINAL_SN.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOSPATID", "");

            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtbody = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                if (dtbody.Rows.Count == 0)
                {
                    return 0;
                }
                return Convert.ToDecimal(dtbody.Rows[0]["JEALL"]);
            }
            catch
            {
                je = 0;
            }
            return je;
        }

        public static Dictionary<string, string> getpatinfobypat_id(long PAT_ID, string HOS_ID)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string PAT_NAME = "";
            string SFZ_NO = "";
            string MOBILE_NO = "";
            string YNCARDNO = "";
            string YLCARTD_TYPE = "";
            string YLCARD_NO = "";
            string SEX = "";
            string BIRTHDAY = "";
            string sqlcmd = string.Format(@"select a.pat_name, a.sfz_secret,a.mobile_secret,b.ylcartd_type,b.ylcard_no,c.ylcard_no YNCARDNO,a.BIRTHDAY,a.sex from pat_info a left OUTER JOIN pat_card b on a.pat_id=b.pat_id  and b.mark_del=0
left outer join pat_card_bind c on a.pat_id=c.pat_id and c.hos_id=@HOS_ID and c.ylcartd_type=1 AND C.MARK_BIND='1'
where  a.pat_id=@PAT_ID
ORDER BY C.BAND_TIME DESC");
            MySqlParameter[] sqlParameters = new MySqlParameter[] { new MySqlParameter("@HOS_ID", HOS_ID), new MySqlParameter("@PAT_ID", PAT_ID) };
            DataTable dtpatinfo = DbHelperMySQL.Query(sqlcmd, sqlParameters).Tables[0];
            if (dtpatinfo != null && dtpatinfo.Rows.Count > 0)
            {
                PAT_NAME = FormatHelper.GetStr(dtpatinfo.Rows[0]["pat_name"]);
                SFZ_NO = PlatDataSecret.DataSecret.DeSfzNoSecretByAes(FormatHelper.GetStr(dtpatinfo.Rows[0]["sfz_secret"]));
                MOBILE_NO = PlatDataSecret.DataSecret.DeMobileSecretByAes(FormatHelper.GetStr(dtpatinfo.Rows[0]["mobile_secret"]));
                YNCARDNO = FormatHelper.GetStr(dtpatinfo.Rows[0]["YNCARDNO"]);
                SEX = FormatHelper.GetStr(dtpatinfo.Rows[0]["SEX"]);
                BIRTHDAY = FormatHelper.GetStr(dtpatinfo.Rows[0]["BIRTHDAY"]);
                DataRow[] drspat_cards = dtpatinfo.Select("ylcartd_type='2'");
                if (drspat_cards.Length > 0 && FormatHelper.GetStr(drspat_cards[0]["YLCARD_NO"]) != "")
                {
                    YLCARTD_TYPE = "2";
                    YLCARD_NO = FormatHelper.GetStr(drspat_cards[0]["YLCARD_NO"]);
                }
                else
                {
                    YLCARTD_TYPE = FormatHelper.GetStr(dtpatinfo.Rows[0]["ylcartd_type"]) == "" ? "4" : FormatHelper.GetStr(dtpatinfo.Rows[0]["ylcartd_type"]);
                    YLCARD_NO = (YLCARTD_TYPE == "4" ? SFZ_NO : FormatHelper.GetStr(dtpatinfo.Rows[0]["ylcard_no"]));
                }
            }
            dic.Add("PAT_NAME", PAT_NAME);
            dic.Add("SFZ_NO", SFZ_NO);
            dic.Add("MOBILE_NO", MOBILE_NO);
            dic.Add("YNCARDNO", YNCARDNO);
            dic.Add("YLCARTD_TYPE", YLCARTD_TYPE);
            dic.Add("YLCARD_NO", YLCARD_NO);
            dic.Add("SEX", SEX);
            dic.Add("BIRTHDAY", BIRTHDAY);
            return dic;
        }

        /// <summary>
        /// 诊间处方费用明细
        /// 该ds含有3个dt，其中dt0为总金额
        /// dt1为药品明细（处方号、执行时间、药品编码、名称、规格、途径、单位、数量、单价、频次等）
        /// dt2为项目明细（执行时间、项目编码、名称、单位、数量、单价等）
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="OPT_SN">病人门诊号</param>
        /// <param name="PRE_NO">处方号</param>
        /// <param name="HOS_SN">院内本次处方唯一流水号</param>
        /// <param name="lTERMINAL_SN">终端标识</param>
        /// <returns></returns>
        public DataSet GETOUTFEENOPAYMX(string HOS_ID, string OPT_SN, string PRE_NO, string HOS_SN, string lTERMINAL_SN, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            long PAT_ID = dic.ContainsKey("PAT_ID") ? long.Parse(dic["PAT_ID"]) : 0;
            string MB_ID = dic.ContainsKey("MB_ID") ? FormatHelper.GetStr(dic["MB_ID"]) : "";
            XmlDocument doc = new XmlDocument();
            string pre_no = "";
            string yb_pay = "";
            string YLCARD_TYPE = "";
            string YLCARD_NO = "";

            string YNCARDNO = "";
            GETPATBARCODE(HOS_ID, PAT_ID.ToString(), ref YNCARDNO);

            #region 卡号卡类型？

            //DataTable dtpat_card = new Plat.BLL.BaseFunction().Query(string.Format(@"select a.ylcartd_type,a.ylcard_no,b.BIRTHDAY,b.mobile_no,b.GUARDIAN_NAME,b.address,a.ylcard_no,b.sfz_secret from pat_card a,pat_info b where  a.pat_id=b.pat_id and a.pat_id='{0}' and a.mark_del='0' order by a.ylcartd_type", PAT_ID));

            string sqlpatinfo = "select a.ylcartd_type,a.ylcard_no,b.BIRTHDAY,b.mobile_no,b.GUARDIAN_NAME,b.address,a.ylcard_no,b.sfz_secret from pat_card a,pat_info b where  a.pat_id=b.pat_id and a.pat_id=@PAT_ID and a.mark_del='0' order by a.ylcartd_type";

            MySqlParameter[] parameter5 = {
                    new MySqlParameter("@PAT_ID", PAT_ID) };

            DataTable dtpat_card = DBQuery("", sqlpatinfo.ToString(), parameter5).Tables[0];

            if (dtpat_card != null && dtpat_card.Rows.Count > 0)
            {
                DataRow[] drssyb = dtpat_card.Select("YLCARTD_TYPE='6'");
                DataRow[] drsyb = dtpat_card.Select("YLCARTD_TYPE='2'");
                if (drssyb.Length > 0)
                {
                    YLCARD_TYPE = "6";
                    YLCARD_NO = FormatHelper.GetStr(drssyb[0]["YLCARD_NO"]);
                }
                else if (drsyb.Length > 0)
                {
                    YLCARD_TYPE = "2";
                    YLCARD_NO = FormatHelper.GetStr(drsyb[0]["YLCARD_NO"]);
                }
                else
                {
                    YLCARD_TYPE = FormatHelper.GetStr(dtpat_card.Rows[0]["ylcartd_type"]) == "0" ? "4" : FormatHelper.GetStr(dtpat_card.Rows[0]["ylcartd_type"]);
                    YLCARD_NO = FormatHelper.GetStr(dtpat_card.Rows[0]["YLCARD_NO"]);
                }
            }

            #endregion 卡号卡类型？

            #region 核算缴费控制

            if (MB_ID != "")
            {
                bool canPay = false;
                string _strWorkingDayAM = "07:30";//工作时间上午08:30
                string _strWorkingDayPM = "17:00";
                TimeSpan dspWorkingDayAM = DateTime.Parse(_strWorkingDayAM).TimeOfDay;
                TimeSpan dspWorkingDayPM = DateTime.Parse(_strWorkingDayPM).TimeOfDay;

                TimeSpan dspNow = DateTime.Now.TimeOfDay;
                if (dspNow > dspWorkingDayAM && dspNow < dspWorkingDayPM)
                {
                    canPay = true;
                }

                if (!canPay)
                {
                    DataSet ds = DS_RtnDealInfo("12", "核酸缴费的时间段为07:30至17:00 ");

                    return ds;
                }
            }

            #endregion 核算缴费控制

            DataTable dtbody = new DataTable();

            DataTable dtmed = new DataTable();
            DataTable dtchk = new DataTable();

            #region 中台调用

            Hos185_His.Models.MZ.GETOUTFEENOPAYMX nopaymx = new Hos185_His.Models.MZ.GETOUTFEENOPAYMX()
            {
                clinicCode = HOS_SN.Split("_")[0],  //挂号流水号
                invoiceNo = "",  //发票号
                invoiceSeq = "",  //发票流水号
                lifeEquityCardNo = "",  //权益卡卡号
                lifeEquityCardType = "",  //权益卡类型
                pactCode = "01",  //合同编号
                recipeNo = PRE_NO,  //处方号,多个以#分割
                sequenceNo = 0,  //处方流水号
                ybPay = "1"  //是否能够医保支付 0 不可以 1可以
            };

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(nopaymx);

            Output<List<Hos185_His.Models.MZ.GETOUTFEENOPAYMXDATA>> output
   = new MIDServiceHelper().CallServiceAPI<List<Hos185_His.Models.MZ.GETOUTFEENOPAYMXDATA>>("/hischargesinfo/outpatientfee/recipedetailinfo", jsonstr);

            if (output.code != 0)
            {
            }

            List<Hos185_His.Models.MZ.GETOUTFEENOPAYMXDATA> datas = output.data;

            List<Hos185_His.Models.MZ.GETOUTFEENOPAYMXDATA> meds = datas.FindAll(x => x.drugFlag == "1").ToList();

            List<Hos185_His.Models.MZ.GETOUTFEENOPAYMXDATA> ckts = datas.FindAll(x => x.drugFlag == "0").ToList();

            List<MED> medsdt = new List<MED>();

            if (meds.Count > 0)
            {
                foreach (var dr in meds)
                {
                    Models.MED med = new Models.MED();
                    med.PRENO = dr.recipeNo;
                    med.DATIME = dr.confirmDate;
                    med.DAID = dr.moOrder;
                    med.MED_ID = dr.itemCode;
                    med.MED_NAME = dr.itemName;
                    med.MED_GG = dr.specs;
                    med.GROUPID = dr.groupCode;
                    med.USAGE = dr.usageCode;
                    med.AUT_NAME = dr.priceUnit;
                    med.CAMT = dr.qty.ToString();
                    med.AUT_NAMEALL = dr.priceUnit;
                    med.CAMTALL = dr.qty.ToString();
                    med.TIMES = dr.frequencyCode;
                    med.PRICE = dr.unitPrice.ToString();
                    med.AMOUNT = (dr.unitPrice * dr.qty).ToString();
                    med.YB_CODE = dr.centerCode;
                    med.YB_CODE_GJM = dr.centerCode;
                    med.IS_QX = "0";
                    med.MINAUT_FLAG = "";
                    medsdt.Add(med);
                }
                dtmed = JsonHelper.toDataTable(medsdt);
            }

            List<CHKT> chktdt = new List<CHKT>();
            if (ckts.Count > 0)
            {
                foreach (var dr in ckts)
                {
                    CHKT chkt = new CHKT();
                    chkt.DATIME = dr.confirmDate;
                    chkt.DAID = dr.moOrder;
                    chkt.CHKIT_ID = dr.itemCode;
                    chkt.CHKIT_NAME = dr.itemName;
                    chkt.AUT_NAME = dr.priceUnit;
                    chkt.CAMTALL = dr.qty.ToString();
                    chkt.PRICE = dr.unitPrice.ToString();
                    chkt.AMOUNT = (dr.unitPrice * dr.qty).ToString();
                    chkt.YB_CODE = dr.centerCode;
                    chkt.YB_CODE_GJM = dr.centerCode;
                    chkt.IS_QX = "1";
                    chkt.MINAUT_FLAG = "";
                    chkt.FEE_TYPE = "";
                    chktdt.Add(chkt);
                }
                dtchk = JsonHelper.toDataTable(chktdt);
            }

            #endregion 中台调用

            dtbody.Columns.Add("CLBZ", typeof(string));
            dtbody.Columns.Add("CLJG", typeof(string));
            DataRow drbody = dtbody.NewRow();
            drbody["CLBZ"] = output.code;
            drbody["CLJG"] = output.message;
            dtbody.Rows.Add(drbody);

            dtbody.Columns.Add("JEALL", typeof(string));
            dtbody.Rows[0]["JEALL"] = "0";

            dtbody.TableName = "dt1";

            dtmed.TableName = "dt2";
            dtchk.TableName = "dt3";

            DataSet dsrev = new DataSet();
            dsrev.Tables.Add(dtbody.Copy());
            dsrev.Tables.Add(dtmed.Copy());
            dsrev.Tables.Add(dtchk.Copy());
            return dsrev;
        }

        /// <summary>
        /// 诊间支付锁定
        /// ds含有3个dt，其中dt0为信息表（状态、医院代码，门诊处方、金额等）
        /// dt1为药品明细（处方号、执行时间、药品编码、名称、规格、途径、单位、数量、单价、频次等）
        /// dt2为项目明细（执行时间、项目编码、名称、单位、数量、单价等）
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="OPT_SN">病人门诊号</param>
        /// <param name="PRE_NO">处方号</param>
        /// <param name="HOS_SN">院内本次处方唯一流水号</param>
        /// <param name="lTERMINAL_SN">终端标识</param>
        /// <returns></returns>
        public DataSet OUTFEEPAYLOCK(string HOS_ID, string OPT_SN, string PRE_NO, string HOS_SN, string lTERMINAL_SN, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            long PAT_ID = dic.ContainsKey("PAT_ID") ? long.Parse(dic["PAT_ID"]) : 0;
            string MB_ID = dic.ContainsKey("MB_ID") ? FormatHelper.GetStr(dic["MB_ID"]) : "";
            XmlDocument doc = new XmlDocument();

            string YLCARD_TYPE = "";
            string YLCARD_NO = "";

            string YNCARDNO = "";
            GETPATBARCODE(HOS_ID, PAT_ID.ToString(), ref YNCARDNO);
            //DataTable dtpat_card = new Plat.BLL.BaseFunction().Query(string.Format(@"select a.ylcartd_type,a.ylcard_no,b.BIRTHDAY,b.mobile_no,b.GUARDIAN_NAME,b.address,a.ylcard_no,b.sfz_secret from pat_card a,pat_info b where  a.pat_id=b.pat_id and a.pat_id='{0}' and a.mark_del='0' order by a.ylcartd_type", PAT_ID));

            string sqlpatinfo = "select a.ylcartd_type,a.ylcard_no,b.BIRTHDAY,b.mobile_no,b.GUARDIAN_NAME,b.address,a.ylcard_no,b.sfz_secret from pat_card a,pat_info b where  a.pat_id=b.pat_id and a.pat_id=@PAT_ID and a.mark_del='0' order by a.ylcartd_type";

            MySqlParameter[] parameter5 = {
                    new MySqlParameter("@PAT_ID", PAT_ID) };

            DataTable dtpat_card = DBQuery("", sqlpatinfo.ToString(), parameter5).Tables[0];

            if (dtpat_card != null && dtpat_card.Rows.Count > 0)
            {
                DataRow[] drssyb = dtpat_card.Select("YLCARTD_TYPE='6'");
                DataRow[] drsyb = dtpat_card.Select("YLCARTD_TYPE='2'");
                if (drssyb.Length > 0)
                {
                    YLCARD_TYPE = "6";
                    YLCARD_NO = FormatHelper.GetStr(drssyb[0]["YLCARD_NO"]);
                }
                else if (drsyb.Length > 0)
                {
                    YLCARD_TYPE = "2";
                    YLCARD_NO = FormatHelper.GetStr(drsyb[0]["YLCARD_NO"]);
                }
                else
                {
                    YLCARD_TYPE = FormatHelper.GetStr(dtpat_card.Rows[0]["ylcartd_type"]) == "0" ? "4" : FormatHelper.GetStr(dtpat_card.Rows[0]["ylcartd_type"]);
                    YLCARD_NO = FormatHelper.GetStr(dtpat_card.Rows[0]["YLCARD_NO"]);
                }
            }

            DataTable dtbody = new DataTable();

            DataTable dtmed = new DataTable();
            DataTable dtchk = new DataTable();

            #region 中台调用

            string hisPayMode = "51";
            string operCode = "MYNJ";

            if (SOURCE.Contains("H001S"))
            {
                hisPayMode = "55";
            }
            if (SOURCE.Contains("A000S"))
            {
                hisPayMode = "54";
                operCode = "QHAPP";
            }

            Hos185_His.Models.MZ.GETOUTFEENOPAYMX nopaymx = new Hos185_His.Models.MZ.GETOUTFEENOPAYMX()
            {
                clinicCode = HOS_SN.Split("_")[0],  //挂号流水号
                invoiceNo = "",  //发票号
                invoiceSeq = "",  //发票流水号
                lifeEquityCardNo = "",  //权益卡卡号
                lifeEquityCardType = "",  //权益卡类型
                pactCode = "01",  //合同编号
                recipeNo = PRE_NO,  //处方号,多个以#分割
                sequenceNo = 0,  //处方流水号
                ybPay = "1"  //是否能够医保支付 0 不可以 1可以
            };

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(nopaymx);

            Output<List<Hos185_His.Models.MZ.GETOUTFEENOPAYMXDATA>> output
   = new MIDServiceHelper().CallServiceAPI<List<Hos185_His.Models.MZ.GETOUTFEENOPAYMXDATA>>("/hischargesinfo/outpatientfee/recipedetailinfo", jsonstr, operCode, operCode);

            if (output.code != 0)
            {
                return DS_RtnDealInfo(output.code.ToString(), output.message);
            }

            Hos185_His.Models.MZ.OUTFEEPAYPRESAVE presave = new Hos185_His.Models.MZ.OUTFEEPAYPRESAVE()
            {
                hospitalcode = "",//医院代码
                lifeEquityCardNo = "",//权益卡卡号
                lifeEquityCardType = "",//权益卡类型
                medicareParam = "",//医保参数
                pactCode = "01",//合同单位
                recipeNos = PRE_NO.Replace('#', ','),
                regid = HOS_SN.Split("_")[0]//挂号单号
            };

            jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(presave);

            Output<Hos185_His.Models.MZ.OUTFEEPAYPRESAVEDATA> outputpre
   = new MIDServiceHelper().CallServiceAPI<Hos185_His.Models.MZ.OUTFEEPAYPRESAVEDATA>("/hischargesinfo/outpatientfee/preSaveFeeXTBY", jsonstr, operCode, operCode);

            if (outputpre.code != 0)
            {
                return DS_RtnDealInfo(outputpre.code.ToString(), outputpre.message);
            }

            //保存自费预结算的收据号 （获取待缴费列表时已存） HOS_SN 是 为保证 唯一的 拼接字段
            RedisHelperSentinels redis = new RedisHelperSentinels();
            var payinfo = new JObject
            {
                { "HOS_SN", HOS_SN },
                { "PRE_NO", PRE_NO },
                { "SJH_Zifei", outputpre.data.receiptNumber }
            };
            redis.Set("MZ" + HOS_SN + HOS_ID, payinfo, DateTime.Now.AddDays(1), 7);

            var jeall = outputpre.data.totCost;
            dtbody.Columns.Add("CLBZ", typeof(string));
            dtbody.Columns.Add("CLJG", typeof(string));
            DataRow drbody = dtbody.NewRow();
            drbody["CLBZ"] = output.code;
            drbody["CLJG"] = output.message;
            dtbody.Rows.Add(drbody);

            List<Hos185_His.Models.MZ.GETOUTFEENOPAYMXDATA> datas = output.data;

            List<Hos185_His.Models.MZ.GETOUTFEENOPAYMXDATA> meds = datas.FindAll(x => x.drugFlag == "1").ToList();

            List<Hos185_His.Models.MZ.GETOUTFEENOPAYMXDATA> ckts = datas.FindAll(x => x.drugFlag == "0").ToList();

            List<MED> medsdt = new List<MED>();

            if (meds.Count > 0)
            {
                foreach (var dr in meds)
                {
                    Models.MED med = new Models.MED();
                    med.PRENO = dr.recipeNo;
                    med.DATIME = dr.confirmDate;
                    med.DAID = dr.moOrder;
                    med.MED_ID = dr.itemCode;
                    med.MED_NAME = dr.itemName;
                    med.MED_GG = dr.specs;
                    med.GROUPID = dr.groupCode;
                    med.USAGE = dr.usageCode;
                    med.AUT_NAME = dr.priceUnit;
                    med.CAMT = dr.qty.ToString();
                    med.AUT_NAMEALL = dr.priceUnit;
                    med.CAMTALL = dr.qty.ToString();
                    med.TIMES = dr.frequencyCode;
                    med.PRICE = dr.unitPrice.ToString();
                    med.AMOUNT = dr.totalFee.ToString();
                    med.YB_CODE = dr.centerCode;
                    med.YB_CODE_GJM = dr.centerCode;
                    med.IS_QX = "0";
                    med.MINAUT_FLAG = "";
                    medsdt.Add(med);
                }
                dtmed = JsonHelper.toDataTable(medsdt);
            }

            List<CHKT> chktdt = new List<CHKT>();
            if (ckts.Count > 0)
            {
                foreach (var dr in ckts)
                {
                    CHKT chkt = new CHKT();
                    chkt.DATIME = dr.confirmDate;
                    chkt.DAID = dr.moOrder;
                    chkt.CHKIT_ID = dr.itemCode;
                    chkt.CHKIT_NAME = dr.itemName;
                    chkt.AUT_NAME = dr.priceUnit;
                    chkt.CAMTALL = dr.qty.ToString();
                    chkt.PRICE = dr.unitPrice.ToString();
                    chkt.AMOUNT = dr.totalFee.ToString();
                    chkt.YB_CODE = dr.centerCode;
                    chkt.YB_CODE_GJM = dr.centerCode;
                    chkt.IS_QX = "1";
                    chkt.MINAUT_FLAG = "";
                    chkt.FEE_TYPE = "";
                    chktdt.Add(chkt);
                }
                dtchk = JsonHelper.toDataTable(chktdt);
            }

            #endregion 中台调用

            try
            {
                if (dtbody != null && dtbody.Rows.Count > 0 && dtbody.Columns.Contains("HOS_SN") && MB_ID != "")
                {
                    HOS_SN = FormatHelper.GetStr(dtbody.Rows[0]["HOS_SN"]);
                }
                if (dtbody != null && dtbody.Rows.Count > 0 && dtbody.Columns.Contains("OPT_SN") && MB_ID != "")
                {
                    OPT_SN = FormatHelper.GetStr(dtbody.Rows[0]["OPT_SN"]);
                }
                if (dtbody != null && dtbody.Rows.Count > 0 && dtbody.Columns.Contains("PRE_NO") && MB_ID != "")
                {
                    PRE_NO = FormatHelper.GetStr(dtbody.Rows[0]["PRE_NO"]) + "|" + "1";
                }

                if (dtbody.Rows[0]["CLBZ"].ToString().Trim() == "0")//取明细成功
                {
                    DateTime date = DateTime.Now;
                    XmlDocument docrev = new XmlDocument();
                    docrev = QHXmlMode.GetBaseXml("OUTFEEPAYLOCK", "0");
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "CLBZ", "0");
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "CLJG", "SUCCESS");
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "STATES", "1");
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "HOS_ID", HOS_ID.Trim());
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "OPT_SN", OPT_SN.Trim());
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "PRE_NO", PRE_NO.Trim());
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "HOS_SN", HOS_SN.Trim());
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "JEALL", jeall);
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "CASH_JE", jeall);
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "DJ_DATE", date.ToString("yyyy-MM-dd"));
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "DJ_TIME", date.ToString("HH:mm:ss"));
                    XMLHelper.X_XmlInsertNode(docrev, "ROOT/BODY", "YQBZ", "H" + HOS_ID);
                    DataTable dta = XMLHelper.X_GetXmlData(docrev, "ROOT/BODY").Tables[0];
                    dta.TableName = "dt1";
                    dtmed.TableName = "dt2";
                    dtchk.TableName = "dt3";

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dta.Copy());
                    ds.Tables.Add(dtmed.Copy());
                    ds.Tables.Add(dtchk.Copy());
                    return ds;
                }
                else//直接返回失败
                {
                    DataTable dtrev = new DataTable();
                    dtrev.Columns.Add("CLBZ", typeof(string));
                    dtrev.Columns.Add("CLJG", typeof(string));
                    DataRow dr = dtrev.NewRow();
                    dr["CLBZ"] = "1";
                    dr["CLJG"] = "支付锁定失败";
                    dtrev.Rows.Add(dr);
                    dtrev.TableName = "dt1";

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtrev.Copy());
                    return ds;
                }
            }
            catch (Exception ex)
            {
                return DS_RtnDealInfo("2", "获取明细异常" + ex.Message);
            }
        }

        /// <summary>
        /// 诊间支付保存
        /// 返回的dt含有发票号，院内收费唯一流水号等
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="HOS_SN">院内本次处方唯一流水号</param>
        /// <param name="CASH_JE">本次缴费现金支付金额</param>
        /// <param name="PAY_TYPE">现金支付方式</param>
        /// <param name="JEALL">缴费成功总金额</param>
        /// <param name="JZ_CODE">人员费用结算类别编码 必填</param>
        /// <param name="ybDJH">医保单据号</param>
        /// <param name="DEAL_STATES">交易状态</param>
        /// <param name="DEAL_TIME">交易时间</param>
        /// <param name="DEAL_TYPE">交易方式</param>
        /// <param name="lTERMINAL_SN">终端标识</param>
        /// <returns></returns>
        public DataTable OUTFEEPAYSAVE(string HOS_ID, string HOS_SN, decimal CASH_JE, string PAY_TYPE, decimal JEALL, string JZ_CODE, string ybDJH, string QUERYID, string DEAL_STATES, DateTime DEAL_TIME, string DEAL_TYPE, string lTERMINAL_SN, Dictionary<string, string> dic)
        {
            try
            {
                string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
                string YLCARD_TYPE = dic.ContainsKey("YLCARD_TYPE") ? dic["YLCARD_TYPE"].Trim() : "4";
                string YBPAT_TYPE = dic.ContainsKey("YBPAT_TYPE") ? FormatHelper.GetStr(dic["YBPAT_TYPE"]) : "";//JSSYB//NJSYB//GJYB YBPAT_TYPE
                string PRE_NO = dic.ContainsKey("PRE_NO") ? FormatHelper.GetStr(dic["PRE_NO"]) : "";

                string YNCARDNO = "";
                GETPATBARCODE(HOS_ID, dic["PAT_ID"].ToString(), ref YNCARDNO);

                string transSerialNo = "";

                if (CASH_JE == 0)
                {
                    QUERYID = "";
                }
                else
                {   //第三方支付流水号查询
                    string sqlpatinfo = "select * from hoshmpay where HOS_ID=@HOS_ID and txn_type='01' and comm_main=@comm_main ";
                    MySqlParameter[] parameter5 = {
                    new MySqlParameter("@HOS_ID",  HOS_ID) ,  new MySqlParameter("@comm_main", QUERYID)};
                    DataTable dthmpay = DBQuery("", sqlpatinfo.ToString(), parameter5).Tables[0];
                    transSerialNo = QUERYID;
                    if (dthmpay != null && dthmpay.Rows.Count > 0)
                    {
                        transSerialNo = CommonFunction.GetStr(dthmpay.Rows[0]["thirdtradeno"]);
                    }
                }

                DataTable dtrev = new DataTable();

                string SJH = "";

                bool isgjyb = dic.ContainsKey("YBPAT_TYPE") && dic["YBPAT_TYPE"] == "CHSYB";//新国家医保
                bool isyby = dic.ContainsKey("YBPAT_TYPE") && dic["YBPAT_TYPE"] == "JSYBY";//南京医保云

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

                string medicareParam = "";

                #region N: 医保参数拼接

                if (isgjyb)
                {
                    #region 5360拼接

                    QHSiInterface.RT1101.Root rt1101 = JSONSerializer.Deserialize<QHSiInterface.RT1101.Root>(dic["CHSOUTPUT1101"]);
                    string CHSOUTPUT5360 = "";
                    if (rt1101.output.data.Count > 0)
                    {
                        QHSiInterface.RT5360.Root rt5360 = new QHSiInterface.RT5360.Root();
                        rt5360.output = new QHSiInterface.RT5360.Output();
                        rt5360.output.data = new List<QHSiInterface.RT5360.insuinfo>();
                        rt5360.infcode = "0";
                        rt5360.err_msg = "";
                        rt5360.inf_refmsgid = "";
                        rt5360.respond_time = "";
                        rt5360.refmsg_time = "";
                        for (int i = 0; i < rt1101.output.data.Count; i++)
                        {
                            QHSiInterface.RT5360.insuinfo insuinfo = new QHSiInterface.RT5360.insuinfo()
                            {
                                psn_no = rt1101.output.data[i].psn_no,
                                med_type = rt1101.output.data[i].med_type,
                                insutype = rt1101.output.data[i].insutype,
                                begndate = rt1101.output.data[i].begndate,
                                dise_codg = rt1101.output.data[i].dise_codg,
                                dise_name = rt1101.output.data[i].dise_name,
                                enddate = "",
                                exp_content = "",
                                hilist_code = rt1101.output.data[i].hilist_code,
                                hilist_name = rt1101.output.data[i].hilist_name
                            };
                            rt5360.output.data.Add(insuinfo);
                        }
                        CHSOUTPUT5360 = JSONSerializer.Serialize(rt5360);
                    }

                    #endregion 5360拼接

                    Dictionary<string, string> dicCHS = JSONSerializer.Deserialize<Dictionary<string, string>>(dic["EXPCONTENT"]);

                    SJH = dicCHS["SJH"];
                    string chsOutput1101 = dic["CHSOUTPUT1101"];
                    string chsOutput5360 = CHSOUTPUT5360;
                    string chsInput2201 = dicCHS["chsInput2201"];
                    string chsOutput2201 = dic["CHSOUTPUT2201"];
                    string chsInput2203 = dicCHS["chsInput2203"];
                    string chsInput2204 = dicCHS["chsInput2204"];
                    string chsOutput2204 = dicCHS["chsOutput2204"];
                    string chsOutput2206 = dicCHS["chsOutput2206"];
                    string chsInput2206 = dicCHS["chsInput2206"];
                    string chsInput2207 = dicCHS["chsInput2207"];
                    string chsOutput2207 = dic["CHSOUTPUT2207"];
                    string EXPCONTENT = dic["EXPCONTENT"];

                    JObject jin2207 = JObject.Parse(chsInput2207);

                    JObject jzzj = new JObject();

                    JObject jybrc = new JObject
                        {
                            { "in2201", "" },
                            { "in2203", JObject.Parse(chsInput2203)["input"] },
                            { "in2204", "" },
                            { "in2206", JObject.Parse(chsInput2206)["input"]},
                            { "in2207", jin2207 },
                            { "out2203", "" },
                            { "out2204", JObject.Parse(chsOutput2204)["output"] },
                            { "out2206", "" },
                            { "out2207", JObject.Parse(chsOutput2207)["output"] }
                        };

                    JObject jexpContentinfo = new JObject
                        {
                            { "in2207msgid", "" }
                        };
                    jybrc.Add("expContentinfo", jexpContentinfo);
                    jzzj.Add("zzj", jybrc);

                    medicareParam = Base64Encode(jzzj.ToString());
                }
                if (isyby)
                {
                    string CHSOUTPUT5360 = "";

                    Soft.DBUtility.RedisHelperSentinels redis = new Soft.DBUtility.RedisHelperSentinels();
                    var apptinfo = JObject.Parse(redis.Get("MZ" + HOS_SN + HOS_ID, 7));

                    SJH = apptinfo["sjh"].ToString();
                    string in2203 = apptinfo["ChsInput2203"].ToString();
                    string out2204 = apptinfo["chsOutput2204"].ToString();

                    string in2206 = apptinfo["chsInput2206"].ToString();
                    string in2207 = dic["CHSINPUT2207"];
                    string out2207 = dic["CHSOUTPUT2207"];

                    JObject jin2207 = JObject.Parse(in2207);

                    JObject jzzj = new JObject();

                    JObject jybrc = new JObject
                    {
                        { "in2201", "" },
                        { "in2203", JObject.Parse(in2203)["input"] },
                        { "in2204", "" },
                        { "in2206", JObject.Parse(in2206)["input"] },
                        { "in2207", jin2207 },
                        { "out2203", "" },
                        { "out2204", JObject.Parse(out2204)["output"] },
                        { "out2206", "" },
                        { "out2207", JObject.Parse(out2207)["output"] }
                    };

                    JObject jexpContentinfo = new JObject();
                    jexpContentinfo.Add("in2207msgid", "");//我的南京无法获取 input 的msgid
                    jybrc.Add("expContentinfo", jexpContentinfo);

                    jzzj.Add("zzj", jybrc);

                    medicareParam = Base64Encode(jzzj.ToString());
                }

                #endregion N: 医保参数拼接

                //是否涉及第三⽅⽀付 1是 0否
                int existsThirdPay = 1;

                int payNature = 1;
                string hisPayMode = "51";
                string operCode = "MYNJ";

                #region N: 操作员号 支付方式对照

                if (SOURCE.Contains("H001S"))
                {
                    hisPayMode = "55";
                }
                if (SOURCE.Contains("A000S"))
                {
                    hisPayMode = "54";
                    operCode = "QHAPP";
                }
                if (SOURCE.Contains("JSYBY"))
                {
                    hisPayMode = "60";
                    operCode = "YBYAPP";
                }
                if (CASH_JE == 0m)
                {
                    hisPayMode = "0";
                    existsThirdPay = 0;
                    payNature = 2;
                }

                #endregion N: 操作员号 支付方式对照

                Hos185_His.Models.MZ.ThirdPayInfo thirdPayInfo = new Hos185_His.Models.MZ.ThirdPayInfo()
                {
                    appPayMode = "", //对应APP⽀付⽅式
                    bankNo = "", //银⾏名称
                    businessType = "2", //业务类型 1挂号 2收费 3发卡 4预交款
                    businessType2 = "3", //业务类型 1现场挂号 2预约挂号 3⻔诊收费 4住院预交⾦
                    cardNo = YNCARDNO, //医院内部就诊卡号,唯⼀
                    cashFee = CASH_JE, //第三⽅⽀付⾦额
                    clinicCode = HOS_SN.Split("_")[0], //挂号流⽔号
                    hisOrderNo = "", //HIS订单号
                    hisPayMode = hisPayMode, //对应HIS⽀付⽅式
                    intenetAddress = "", //互联⽹医院收件地址
                    intenetRecivePerson = "", //互联⽹医院收件⼈
                    intenetTelNo = "", //互联⽹医院收件电话
                    invoiceNo = "", //发票号
                    machineNo = "", //机器编号
                    merchantOrderNo = string.IsNullOrEmpty(QUERYID) ? "" : QUERYID, //商⼾订单号
                    operCode = operCode, //操作员编号
                    payChannelNo = "", //⽀付渠道
                    powerTranID = "", //平台交易ID
                    powerTranType = "", //平台交易类型
                    powerTranUserCode = "", //平台交易⽤⼾
                    rrn = "",//对账流⽔号
                    terminalNo = "", //交易终端号
                    thirdOrderNo = string.IsNullOrEmpty(QUERYID) ? "" : QUERYID, //第三⽅订单号
                    thirdTradeInParam = "", //第三⽅交易⼊参参数
                    thirdTradeOutParam = "", //第三⽅交易出参参数
                    tradeTime = DEAL_TIME.ToString("yyyy-MM-dd HH:mm:ss"), //交易时间 yyyy-mm-dd HH24:mi:ss
                    tranCardNum = "", //银⾏卡号
                    transSerialNo = transSerialNo //交易流⽔号 （必传）
                };

                string isYbPay = "0";

                string jsonstr = "";

                P0601 p0601;
                dtrev.Columns.Add("CLBZ", typeof(string));
                dtrev.Columns.Add("CLJG", typeof(string));
                DataRow drbody = dtrev.NewRow();
                drbody["CLBZ"] = "0";
                drbody["CLJG"] = "SUCCESS";
                dtrev.Rows.Add(drbody);
                if (!isgjyb && !isyby)//自费收据号获取（outfeepaylock时存储在缓存中） 说明：省医保只有挂号
                {
                    //自费的预算收据号放缓存中
                    Soft.DBUtility.RedisHelperSentinels redis = new Soft.DBUtility.RedisHelperSentinels();

                    var rediscontent = redis.Get("MZ" + HOS_SN + HOS_ID, 7);
                    SaveLog(DateTime.Now, "查询redis自费预算缓存：" + "MZ" + HOS_SN + HOS_ID, DateTime.Now, rediscontent);

                    if (rediscontent != null)
                    {
                        var apptinfo = JObject.Parse(rediscontent);

                        SJH = apptinfo.ContainsKey("SJH_Zifei") ? apptinfo["SJH_Zifei"].ToString() : "";
                    }

                    if (string.IsNullOrEmpty(SJH))
                    {
                        return DS_RtnDealInfo("222", "获取收据号异常").Tables[0];
                    }
                }

                Hos185_His.Models.MZ.OUTFEEPAYSAVE paysave = new Hos185_His.Models.MZ.OUTFEEPAYSAVE()
                {
                    receiptNumber = SJH,
                    payableCost = CASH_JE.ToString(),

                    medicareParam = medicareParam,
                    cardNo = YNCARDNO,  //院内就诊卡号
                    clinicCode = HOS_SN.Split("_")[0],  //门诊挂号流水号
                    existsThirdPay = existsThirdPay,  //是否涉及第三方支付 1是 0否
                    lifeEquityCardNo = "",  //权益卡卡号
                    lifeEquityCardType = "",  //权益卡类型
                    operCode = operCode,  //操作员编号
                    pactCode = "01",  //合同编号
                    recipeNo = PRE_NO,  //处方号,多个以#分割
                    terminalCode = "",  //设备终端编号号
                    totalFee = JEALL,  //总费用
                    ybPay = isYbPay  //是否医保支付 0 否 1是
                };
                paysave.thirdPayInfo = thirdPayInfo;
                paysave.medicareInfo = medicareInfo;

                jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(paysave);

                Output<Hos185_His.Models.MZ.OUTFEEPAYSAVEDATA> output
                    = new MIDServiceHelper().CallServiceAPI<Hos185_His.Models.MZ.OUTFEEPAYSAVEDATA>("/hischargesinfo/outpatientfee/savefee", jsonstr, operCode, operCode);

                if (output.code == 110 && output.statusCode == 200)
                {
                    ////交货： 失败
                    //if (CASH_JE != 0m)
                    //{
                    //    p0601 = new P0601()
                    //    {
                    //        outTradeNo = "", //商⼾订单号, 商⼾订单号和平台订单号必填⼀个
                    //        transactionId = QUERYID, //平台订单号 商⼾订单号和平台订单号必填⼀个
                    //        confirmState = "fail", //确认状态 success 确认成功 fail 失败
                    //        confirmDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), //确认时间 yyyy-mm-dd HH:mm:ss
                    //        receiptNo = "" //HIS发票号,多张发票⽤,分隔
                    //    };

                    //    jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(p0601);

                    //    var outnocare1 = new MIDServiceHelper().CallServiceAPI<JObject>("/platformpayment/pay/confirmPay", jsonstr, operCode, operCode);
                    //}
                    dtrev.Rows[0]["CLBZ"] = "222";//通知退款
                    return dtrev;
                }

                ///交货： 成功
                if (CASH_JE != 0m)
                {
                    p0601 = new P0601()
                    {
                        outTradeNo = "", //商⼾订单号, 商⼾订单号和平台订单号必填⼀个
                        transactionId = QUERYID, //平台订单号 商⼾订单号和平台订单号必填⼀个
                        confirmState = "success", //确认状态 success 确认成功 fail 失败
                        confirmDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), //确认时间 yyyy-mm-dd HH:mm:ss
                        receiptNo = output.data.invoiceNo //HIS发票号,多张发票⽤,分隔
                    };

                    jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(p0601);

                    var outnocare2 = new MIDServiceHelper().CallServiceAPI<JObject>("/platformpayment/pay/confirmPay", jsonstr, operCode, operCode);
                }

                dtrev.Columns.Add("OPT_SN");
                dtrev.Columns.Add("HOS_REG_SN");
                dtrev.Columns.Add("RCPT_NO");
                dtrev.Columns.Add("HOS_PAY_SN");
                dtrev.Columns.Add("ADD_INFO");

                dtrev.Rows[0]["OPT_SN"] = output.data.cardNo;
                dtrev.Rows[0]["HOS_REG_SN"] = output.data.clinicCode;
                dtrev.Rows[0]["RCPT_NO"] = SJH;
                dtrev.Rows[0]["HOS_PAY_SN"] = output.data.clinicCode;
                dtrev.Rows[0]["ADD_INFO"] = "";

                return dtrev;
            }
            catch (Exception ex)
            {
                return RtnResultDatatable("2", ex.Message);
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        /// <summary>
        /// 返回的基础xml
        /// </summary>
        /// <param name="code">编码 9exception 8调用err</param>
        /// <param name="cljg"></param>
        /// <returns></returns>
        private static DataSet DS_RtnDealInfo(string code, string cljg)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("CLBZ", typeof(string));
            dt.Columns.Add("CLJG", typeof(string));
            DataRow dr = dt.NewRow();
            dr["CLBZ"] = code;
            dr["CLJG"] = cljg;
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);
            return ds;
        }

        /// <summary>
        /// 获取病人检验报告单列表
        /// 返回dt可多行（含报告单流水号、名称、类型等）
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="HOS_SN">挂号院内唯一流水号</param>
        /// <param name="SFZ_NO">病人身份证</param>
        /// <param name="PAGEINDEX">分页索引</param>
        /// <param name="PAGESIZE">分页页大小</param>
        /// <returns></returns>
        public DataSet GETLISREPORT(string HOS_ID, string HOS_SN, string YLCARD_NO, string SFZ_NO, int PAGEINDEX, int PAGESIZE, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";

            //SFZ_NO = "342427198903065517";

            inspectionCheckList checkList
                       = new inspectionCheckList()
                       {
                           bdate = "",
                           checkType = "test",
                           edate = "",
                           idCard = SFZ_NO,
                           idCardType = "01",////证件类型 01:身份证 06:护照 08:港澳台居民来往内地通行证
                           medCardSource = "",//就诊来源 src_mz-门诊 src_zy-住院,查询门诊时传src_mz，查询住院时传src_zy，查询住院时传src_tj，不传则查询所有
                           name = ""
                       }
                   ;
            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(checkList);

            Output<List<inspectionCheckListData>> output
              = new MIDServiceHelper().CallServiceAPI<List<inspectionCheckListData>>("/hislispacs/inspectionCheckListXTBYXL", jsonstr);

            DataTable dtlis = new DataTable();

            dtlis.Columns.Add("HOS_SN");
            dtlis.Columns.Add("REPORT_SN");
            dtlis.Columns.Add("REPORT_TYPE");
            dtlis.Columns.Add("PRINT_FLAG");
            dtlis.Columns.Add("SAMPLE_NO");
            dtlis.Columns.Add("SAMPLE_TYPE");
            dtlis.Columns.Add("TEST_DATE");
            dtlis.Columns.Add("TEST_DOC_NO");
            dtlis.Columns.Add("TEST_DOC_NAME");
            dtlis.Columns.Add("TEST_DEPT_CODE");
            dtlis.Columns.Add("TEST_DEPT_NAME");
            dtlis.Columns.Add("REPORT_DATE");
            dtlis.Columns.Add("AUDIT_DOC_NO");
            dtlis.Columns.Add("AUDIT_DOC_NAME");
            dtlis.Columns.Add("AUDIT_DATE");
            dtlis.Columns.Add("APPLY_DEPT_CODE");
            dtlis.Columns.Add("APPLY_DEPT_NAME");
            dtlis.Columns.Add("APPLY_DOC_NO");
            dtlis.Columns.Add("APPLY_DOC_NAME");
            dtlis.Columns.Add("APPLY_DATE");
            dtlis.Columns.Add("PAT_BLH");
            dtlis.Columns.Add("REPORT_ALL_NUM");
            dtlis.Columns.Add("REPORT_PRINT_NUM");

            DataTable dtbody = new DataTable();
            dtbody.Columns.Add("CLJG");//add by hlw 2018.03.01 只是普通勿进行重复查询
            dtbody.Columns.Add("CLBZ");//add by hlw 2018.03.01 只是普通勿进行重复查询

            dtbody.Columns.Add("no_repeat", typeof(int));//add by hlw 2018.03.01 只是普通勿进行重复查询

            DataRow dr = dtbody.NewRow();
            dr["no_repeat"] = 1;
            dr["CLJG"] = output.message;
            dr["CLBZ"] = output.code;
            dtbody.Rows.Add(dr);

            try
            {
                if (output.code != 0)
                {
                    return null;
                }

                foreach (var lis in output.data)
                {
                    foreach (var item in lis.list)
                    {
                        DataRow drlis = dtlis.NewRow();
                        drlis["HOS_SN"] = "";
                        drlis["REPORT_SN"] = item.inspectNo;
                        drlis["REPORT_TYPE"] = item.itemName;
                        drlis["PRINT_FLAG"] = "";
                        drlis["SAMPLE_NO"] = item.barcode;
                        drlis["SAMPLE_TYPE"] = "";
                        drlis["TEST_DATE"] = lis.inspectDate;
                        drlis["TEST_DOC_NO"] = "";
                        drlis["TEST_DOC_NAME"] = "";
                        drlis["TEST_DEPT_CODE"] = "";
                        drlis["TEST_DEPT_NAME"] = "";
                        drlis["REPORT_DATE"] = lis.inspectDate;
                        drlis["AUDIT_DOC_NO"] = "";
                        drlis["AUDIT_DOC_NAME"] = "";
                        drlis["AUDIT_DATE"] = lis.inspectDate;
                        drlis["APPLY_DEPT_CODE"] = "";
                        drlis["APPLY_DEPT_NAME"] = "";
                        drlis["APPLY_DOC_NO"] = "";
                        drlis["APPLY_DOC_NAME"] = "";
                        drlis["APPLY_DATE"] = "";
                        drlis["PAT_BLH"] = "";
                        drlis["REPORT_ALL_NUM"] = "";
                        drlis["REPORT_PRINT_NUM"] = "";

                        dtlis.Rows.Add(drlis);
                    }
                }

                DataSet dsrev = new DataSet();
                dsrev.Tables.Add(dtbody.Copy());
                dsrev.Tables.Add(dtlis.Copy());
                return dsrev;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取检验报告结果明细
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="HOS_SN">医院唯一代码</param>
        /// <param name="SFZ_NO">病人身份证</param>
        /// <param name="REPORT_SN">报告单流水号</param>
        /// <returns></returns>
        public DataSet GETLISRESULT(string HOS_ID, string HOS_SN, string YLCARD_NO, string SFZ_NO, string REPORT_SN, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";

            if (SOURCE.ToUpper() == "JSYBY")
            {
                return DS_RtnDealInfo("0", "详见PDF");
            }
            Hos185_His.Models.Report.downloadFile download = new Hos185_His.Models.Report.downloadFile()
            {
                filePath = "",
                inspectType = "testType",//检验检查列表返回的inspectType   检查类型枚举   InspectTypeEnum
                reportId = REPORT_SN,//检验检查列表返回的报告id  inspectNo
                visitType = ""
            };

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(download);

            Output<downloadFileData> output
      = new MIDServiceHelper().CallServiceAPI<downloadFileData>("/medicaloss/file/downloadFile", jsonstr);

            DataTable dtbody = new DataTable();
            dtbody.Columns.Add("CLJG");//add by hlw 2018.03.01 只是普通勿进行重复查询
            dtbody.Columns.Add("CLBZ");//add by hlw 2018.03.01 只是普通勿进行重复查询

            dtbody.Columns.Add("no_repeat", typeof(int));//add by hlw 2018.03.01 只是普通勿进行重复查询

            DataRow dr = dtbody.NewRow();
            dr["no_repeat"] = 1;
            dr["CLJG"] = output.message;
            dr["CLBZ"] = output.code;
            dtbody.Rows.Add(dr);
            if (output.code != 0)
            {
                return null;
            }

            DataTable dtLIS = new DataTable();

            dtLIS.Columns.Add("EXAMURL");//
            dtLIS.Columns.Add("DATA_TYPE");
            dtLIS.Columns.Add("LISMX_DATA");//LISMX_DATA

            string base64head = output.data.fileBase64String.Substring(0, 3);
            try
            {
                DataRow drresult = dtLIS.NewRow();

                drresult["DATA_TYPE"] = "6";
                drresult["EXAMURL"] = "";
                drresult["LISMX_DATA"] = output.data.fileBase64String;
                dtLIS.Rows.Add(drresult);

                dtLIS.TableName = "dt2";

                DataTable dtList = new DataTable();

                dtList.TableName = "dt3";

                DataSet dsrev = new DataSet();
                dsrev.Tables.Add(dtbody.Copy());
                dsrev.Tables.Add(dtLIS.Copy());
                dsrev.Tables.Add(dtList.Copy());
                return dsrev;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取病人检查报告
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="HOS_SN">挂号院内唯一流水号</param>
        /// <param name="SFZ_NO">病人身份证</param>
        /// <param name="PAGEINDEX">分页索引</param>
        /// <param name="PAGESIZE">分页页大小</param>
        /// <returns></returns>
        public DataSet GETRISREPORT(string HOS_ID, string HOS_SN, string YLCARD_NO, string SFZ_NO, int PAGEINDEX, int PAGESIZE, Dictionary<string, string> dic)
        {
            return DS_RtnDealInfo("8", "没有开通该业务");
        }

        /// <summary>
        /// 获取指定医院及科室专家排班列表(当日即实时)
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="DEPT_CODE">科室代码</param>
        /// <param name="SCH_TYPE">排班类型</param>
        /// <returns></returns>
        public DataSet GETSCHDOC(string HOS_ID, string DEPT_CODE, string SCH_TYPE, string DOC_NO, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            DataTable dtbody = new DataTable();

            dtbody.Columns.Add("CLBZ");
            dtbody.Columns.Add("CLJG");
            try
            {
                string seeStartDate = DateTime.Now.AddDays(SCH_TYPE == "01" ? 1 : 0).ToString("yyyy-MM-dd");
                string seeEndDate = DateTime.Now.AddDays(SCH_TYPE == "01" ? 14 : 0).ToString("yyyy-MM-dd");

                Hos185_His.Models.MZ.GETSCHINFO getschinfodoc = new Hos185_His.Models.MZ.GETSCHINFO()
                {
                    deptCode = DEPT_CODE.Trim(), //科室编号
                    doctCode = DOC_NO.Trim(), //医⽣编号
                    isTh = "1", //是否停号 1未停 2已停
                    isTy = "1", //是否停约 0停约 1未停约
                    noonCodeStr = "", //午别编码,多个以#分割
                    pactCode = "", //合同编号
                    reglevlCodeStr = "", //号别编码,多个以#分割
                    schemaId = "", //排班序号
                    schemaType = "1", //排班类型 1专家 0普通
                    seeEndDate = seeEndDate, //看诊结束⽇期 yyyy-MM-dd
                    seeStartDate = seeStartDate, //看诊开始⽇期 yyyy-MM-dd
                    sourceType = "XCYY", //号源类别 XCYY=""线下 XCGG=""12320 OLYY=""线上(互联⽹在线问诊)
                    validFlag = "1"  //是否停诊=""0 停诊 1或空 正常 2全部
                };
                string jsonstrdept = Newtonsoft.Json.JsonConvert.SerializeObject(getschinfodoc);

                Hos185_His.Models.Output<List<GETSCHINFODATA>> outputdept
          = new MIDServiceHelper().CallServiceAPI<List<GETSCHINFODATA>>("/hisbooking/schema/schemaInfo", jsonstrdept);

                DataTable dataTable = new DataTable();
                if (outputdept.code == 0)
                {
                    List<GETSCHINFODATA> schlist = outputdept.data;

                    dataTable.Columns.Add("DEPT_CODE");
                    dataTable.Columns.Add("DEPT_NAME");
                    dataTable.Columns.Add("DOC_NO");
                    dataTable.Columns.Add("SCH_DATE");
                    dataTable.Columns.Add("SCH_TIME");
                    dataTable.Columns.Add("SCH_TYPE");
                    dataTable.Columns.Add("COUNT_REM");
                    dataTable.Columns.Add("GH_FEE");
                    dataTable.Columns.Add("ZL_FEE");
                    dataTable.Columns.Add("START_TIME");
                    dataTable.Columns.Add("END_TIME");
                    dataTable.Columns.Add("CAN_WAIT");
                    dataTable.Columns.Add("REGISTER_TYPE");

                    foreach (GETSCHINFODATA sch in schlist)
                    {
                        //普通号
                        DataRow dr = dataTable.NewRow();

                        dr["DEPT_CODE"] = sch.deptCode;// "2006";
                        dr["DEPT_NAME"] = sch.deptName;// "心血管内科门诊";
                        dr["DOC_NO"] = sch.doctCode;// "心血管内科门诊";
                        dr["SCH_DATE"] = sch.seeDate;// "2023-02-08";
                        dr["SCH_TIME"] = sch.noonName;// "上午";
                        dr["SCH_TYPE"] = "1";
                        dr["COUNT_REM"] = sch.numremain;
                        dr["GH_FEE"] = sch.regFee;
                        dr["ZL_FEE"] = sch.treatfee;
                        dr["START_TIME"] = DateTime.Parse(sch.seeStartTime).ToString("HH:mm");

                        dr["END_TIME"] = DateTime.Parse(sch.seeEndTime).ToString("HH:mm");
                        dr["CAN_WAIT"] = "0";
                        dr["REGISTER_TYPE"] = sch.reglevlCode + "|" + sch.noonCode + "|" + sch.schemaId;

                        dataTable.Rows.Add(dr);
                    }
                }
                DataRow drbody = dtbody.NewRow();
                drbody["CLBZ"] = outputdept.code;
                drbody["CLJG"] = outputdept.message;
                dtbody.Rows.Add(drbody);

                dtbody.TableName = "ds1";
                dataTable.TableName = "ds2";

                DataSet ds = new DataSet();
                ds.Tables.Add(dtbody.Copy());
                ds.Tables.Add(dataTable.Copy());
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取指定医院科室排班列表(当日即实时)
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="DEPT_CODE">科室代码</param>
        /// <param name="SCH_TYPE">排班类型</param>
        /// <returns></returns>
        public DataSet GETSCHDEPT(string HOS_ID, string DEPT_CODE, string SCH_TYPE, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";

            DataTable dtbody = new DataTable();

            dtbody.Columns.Add("CLBZ");
            dtbody.Columns.Add("CLJG");
            try
            {
                string seeStartDate = DateTime.Now.AddDays(SCH_TYPE == "01" ? 1 : 0).ToString("yyyy-MM-dd");
                string seeEndDate = DateTime.Now.AddDays(SCH_TYPE == "01" ? 14 : 0).ToString("yyyy-MM-dd");
                Hos185_His.Models.MZ.GETSCHINFO getschinfodept = new Hos185_His.Models.MZ.GETSCHINFO()
                {
                    deptCode = DEPT_CODE, //科室编号
                    doctCode = "", //医⽣编号
                    isTh = "1", //是否停号 1未停 2已停
                    isTy = "1", //是否停约 0停约 1未停约
                    noonCodeStr = "", //午别编码,多个以#分割
                    pactCode = "", //合同编号
                    reglevlCodeStr = "", //号别编码,多个以#分割
                    schemaId = "", //排班序号
                    schemaType = "0", //排班类型 1专家 0普通
                    seeEndDate = seeEndDate, //看诊结束⽇期 yyyy-MM-dd
                    seeStartDate = seeStartDate, //看诊开始⽇期 yyyy-MM-dd
                    sourceType = "XCYY", //号源类别 XCYY=""线下 XCGG=""12320 OLYY=""线上(互联⽹在线问诊)
                    validFlag = "1"  //是否停诊=""0 停诊 1或空 正常 2全部
                };

                string jsonstrdept = Newtonsoft.Json.JsonConvert.SerializeObject(getschinfodept);

                Hos185_His.Models.Output<List<GETSCHINFODATA>> outputdept
          = new MIDServiceHelper().CallServiceAPI<List<GETSCHINFODATA>>("/hisbooking/schema/schemaInfo", jsonstrdept);
                DataTable dataTable = new DataTable();
                if (outputdept.code == 0)
                {
                    List<GETSCHINFODATA> schlist = outputdept.data;

                    dataTable.Columns.Add("DEPT_CODE");
                    dataTable.Columns.Add("DEPT_NAME");
                    dataTable.Columns.Add("SCH_DATE");
                    dataTable.Columns.Add("SCH_TIME");
                    dataTable.Columns.Add("SCH_TYPE");
                    dataTable.Columns.Add("COUNT_REM");
                    dataTable.Columns.Add("GH_FEE");
                    dataTable.Columns.Add("ZL_FEE");
                    dataTable.Columns.Add("START_TIME");
                    dataTable.Columns.Add("END_TIME");
                    dataTable.Columns.Add("CAN_WAIT");
                    dataTable.Columns.Add("REGISTER_TYPE");

                    foreach (GETSCHINFODATA sch in schlist)
                    {
                        //普通号
                        DataRow dr = dataTable.NewRow();

                        dr["DEPT_CODE"] = sch.deptCode;// "2006";
                        dr["DEPT_NAME"] = sch.deptName;// "心血管内科门诊";
                        dr["SCH_DATE"] = sch.seeDate;// "2023-02-08";
                        dr["SCH_TIME"] = sch.noonName;// "上午";
                        dr["SCH_TYPE"] = "1";
                        dr["COUNT_REM"] = sch.numremain;
                        dr["GH_FEE"] = sch.regFee;
                        dr["ZL_FEE"] = sch.treatfee;
                        dr["START_TIME"] = DateTime.Parse(sch.seeStartTime).ToString("HH:mm");

                        dr["END_TIME"] = DateTime.Parse(sch.seeEndTime).ToString("HH:mm");
                        dr["CAN_WAIT"] = "0";
                        dr["REGISTER_TYPE"] = sch.reglevlCode + "|" + sch.noonCode + "|" + sch.schemaId;

                        dataTable.Rows.Add(dr);
                    }
                }

                DataRow drbody = dtbody.NewRow();
                drbody["CLBZ"] = outputdept.code;
                drbody["CLJG"] = outputdept.message;
                dtbody.Rows.Add(drbody);

                dtbody.TableName = "ds1";
                dataTable.TableName = "ds2";

                DataSet ds = new DataSet();
                ds.Tables.Add(dtbody.Copy());
                ds.Tables.Add(dataTable.Copy());

                SaveLog(DateTime.Now, "CHS_YBY(preSaveFeeXTBY)", DateTime.Now, "cheshi12345");//保存his接口日志

                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取指定医院科室(专家)日期排班时间段
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="DEPT_CODE">科室代码</param>
        /// <param name="DOC_NO">医生工号</param>
        /// <param name="SCH_DATE">筛选日期</param>
        /// <param name="SCH_TIME">排班时间</param>
        /// <returns></returns>
        public DataSet GETSCHPERIOD(string HOS_ID, string DEPT_CODE, string DOC_NO, string SCH_DATE, string SCH_TIME, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";

            string beginTime = "";
            string endTime = "";

            string[] REGISTER_TYPE = dic["REGISTER_TYPE"].Split('|');

            string regLevelCode = REGISTER_TYPE[0];
            string noonCode = REGISTER_TYPE[1];
            string schemaId = REGISTER_TYPE[2];

            switch (SCH_TIME)
            {
                case "全天":
                    beginTime = SCH_DATE.Trim() + " " + "00:00:00";
                    endTime = SCH_DATE.Trim() + " " + "23:59:00";
                    break;

                default:
                    break;
            }

            try
            {
                Hos185_His.Models.MZ.GETSCHPERIOD getschperiod = new Hos185_His.Models.MZ.GETSCHPERIOD()
                {
                    beginTime = beginTime, //看诊开始⽇期yyyy-MM-dd HH:mm:ss
                    deptCode = DEPT_CODE, //科室Code
                    doctCode = DOC_NO, //医⽣code
                    doctName = "", //医⽣姓名（⽀持模糊查询）
                    endTime = endTime, //看诊结束⽇期yyyy-MM-dd HH:mm:ss
                    noonCode = noonCode, //午别code
                    regLevelCode = regLevelCode, //挂号级别code
                    schemaIdList = new List<int>() { int.Parse(schemaId) }, //排班序号
                    sourceType = "XCYY" //号源类别 XCYY:线下 XCGG:12320 OLYY:线上(互联⽹在线问诊)
                };

                string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(getschperiod);

                Hos185_His.Models.Output<List<GETSCHPERIODDATA>> output
          = new MIDServiceHelper().CallServiceAPI<List<GETSCHPERIODDATA>>("/hisbooking/schema/schemaDaypartInfo", jsonstr);

                DataSet ds = new DataSet();

                DataTable dtbody = new DataTable();

                dtbody.Columns.Add("CLBZ");
                dtbody.Columns.Add("CLJG");
                DataRow drbody = dtbody.NewRow();
                drbody["CLBZ"] = output.code;
                drbody["CLJG"] = output.message;
                dtbody.Rows.Add(drbody);

                DataTable dtsch = new DataTable();
                dtsch.Columns.Add("PERIOD_START");//: "09:30:00"
                dtsch.Columns.Add("PERIOD_END");// "10:00:00",
                dtsch.Columns.Add("COUNT_REM");// "4"
                if (output.code != 0)
                {
                    ds.Tables.Add(dtbody.Copy());

                    return ds;
                }

                foreach (var item in output.data)
                {
                    DataRow dr = dtsch.NewRow();
                    dr["PERIOD_START"] = DateTime.Parse(item.beginTime).ToString("HH:mm");
                    dr["PERIOD_END"] = DateTime.Parse(item.endTime).ToString("HH:mm");
                    dr["COUNT_REM"] = item.numremain;
                    dtsch.Rows.Add(dr);
                }

                dtsch.TableName = "ds2";

                ds.Tables.Add(dtbody.Copy());
                ds.Tables.Add(dtsch.Copy());
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 预约取消 不含支付
        /// </summary>
        /// <param name="HOS_ID"></param>
        /// <param name="HOS_SNAPPT"></param>
        /// <param name="HOS_SN"></param>
        /// <param name="lTERMINAL_SN"></param>
        /// <returns></returns>
        public bool REGISTERPAYCANCEL(string HOS_ID, string HOS_SNAPPT, string HOS_SN, string lTERMINAL_SN, string PASSWORD, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            XmlDocument doc = new XmlDocument();
            //DataTable dtREG = new Plat.BLL.BaseFunction().GetList("register_appt", "HOS_SN='" + HOS_SN + "'", "SOURCE");

            string sqlappt = "select SOURCE from register_appt where HOS_SN=@HOS_SN and HOS_ID=@HOS_ID";
            MySqlParameter[] parameter2 = {
                    new MySqlParameter("@HOS_SN",HOS_SN),
                    new MySqlParameter("@HOS_ID",HOS_ID) };

            DataTable dtREG = DBQuery("", sqlappt.ToString(), parameter2).Tables[0];

            if (dtREG != null && dtREG.Rows.Count == 1)
            {
                SOURCE = dtREG.Rows[0][0].ToString();
            }
            string jsonstr = string.Format("apointMentCode={0}", HOS_SN);

            string operCode = "MYNJ";
            if (SOURCE.Contains("H001S"))
            {
                operCode = "MYNJ";
            }
            if (SOURCE.Contains("A000S"))
            {
                operCode = "QHAPP";
            }

            //application/x-www-form-urlencoded
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("operCode", operCode);

            Hos185_His.Models.Output<string> outputappt
= new MIDServiceHelper().CallServiceAPIForm<string>("/hisbooking/appointment/cancel", jsonstr, "application/x-www-form-urlencoded", header, operCode, operCode);

            return outputappt.code == 0;
        }

        /// <summary>
        /// 发送短信(附带IP)
        /// </summary>
        /// <param name="MESSAGE">消息内容</param>
        /// <param name="MOBILE_NO">电话号码</param>
        /// <param name="PAT_NAME">病人姓名</param>
        /// <returns></returns>
        public DataTable SENDMSG(string HOS_ID, string MESSAGE, string MOBILE_NO, string PAT_NAME, string IP)
        {
            //DataTable dtHOS = new Plat.BLL.BaseFunction().GetList("hos_configuration", "HOS_ID='" + HOS_ID + "'", "MESSAGE_COUNT", "IPMESSAGE_COUNT");//获取后台配置

            string sqlappt = "select *from hos_configuration where HOS_SN=@HOS_SN and HOS_ID=@HOS_ID";
            MySqlParameter[] parameter2 = {
                    new MySqlParameter("@HOS_ID",HOS_ID) };

            DataTable dtHOS = DBQuery("", sqlappt.ToString(), parameter2).Tables[0];

            int MESSAGE_COUNT = dtHOS.Rows[0]["MESSAGE_COUNT"] == null ? 0 : Convert.ToInt32(dtHOS.Rows[0]["MESSAGE_COUNT"]);//手机号限制短信条数 目前为10
            int IPMESSAGE_COUNT = dtHOS.Rows[0]["IPMESSAGE_COUNT"] == null ? 0 : Convert.ToInt32(dtHOS.Rows[0]["IPMESSAGE_COUNT"]);//IP地址限制短信条数 目前为10

            if (MESSAGE_COUNT > 0)//零表示不限制短信条数
            {
                string condition = string.Format(@"HOS_ID='" + HOS_ID + "' and phone_no='{0}' and create_time BETWEEN '{1} 00:00:00' and '{1} 23:59:59'", MOBILE_NO, DateTime.Now.ToString("yyyy-MM-dd"));//获取当天同一个手机号短信条数
                //DataTable dtCA = new Plat.BLL.BaseFunction().GetList("phone_captcha", condition, "phone_no");

                string phone_captcha = "select *from  phone_captcha where HOS_ID=@HOS_ID and phone_no=@phone_no   and create_time BETWEEN @starttime and @endtime";
                MySqlParameter[] parameter5 = {
                    new MySqlParameter("@HOS_ID",HOS_ID),
                   new MySqlParameter("@phone_no",MOBILE_NO),
                   new MySqlParameter("@starttime",DateTime.Now.ToString("yyyy-MM-dd 00:00:00")),
                   new MySqlParameter("@endtime",DateTime.Now.ToString("yyyy-MM-dd 23:59:59"))};

                DataTable dtCA = DBQuery("", phone_captcha.ToString(), parameter5).Tables[0];
                if (dtCA.Rows.Count >= MESSAGE_COUNT)//同一个手机号超过指定条数
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("CLBZ", typeof(string));
                    dt.Columns.Add("CLJG", typeof(string));

                    DataRow dr = dt.NewRow();
                    dr["CLBZ"] = "1";
                    dr["CLJG"] = "提示：今日短信发送次数已达上限";
                    dt.Rows.Add(dr);
                    return dt;
                }
            }
            if (IPMESSAGE_COUNT > 0)
            {
                //string condition = string.Format(@"HOS_ID='" + HOS_ID + "' and IP='{0}' and create_time BETWEEN '{1} 00:00:00' and '{1} 23:59:59'", IP, DateTime.Now.ToString("yyyy-MM-dd"));//获取当天同一个IP短信条数
                //DataTable dtCA = new Plat.BLL.BaseFunction().GetList("phone_captcha", condition, "IP");

                string phone_captcha = "select *from  phone_captcha where HOS_ID=@HOS_ID and IP=@IP   and create_time BETWEEN @starttime and @endtime";
                MySqlParameter[] parameter3 = {
                    new MySqlParameter("@HOS_ID",HOS_ID),
                   new MySqlParameter("@IP",IP),
                   new MySqlParameter("@starttime",DateTime.Now.ToString("yyyy-MM-dd 00:00:00")),
                   new MySqlParameter("@endtime",DateTime.Now.ToString("yyyy-MM-dd 23:59:59"))};

                DataTable dtCA = DBQuery("", phone_captcha.ToString(), parameter3).Tables[0];

                if (dtCA.Rows.Count >= IPMESSAGE_COUNT)//同一个IP超过指定条数
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("CLBZ", typeof(string));
                    dt.Columns.Add("CLJG", typeof(string));

                    DataRow dr = dt.NewRow();
                    dr["CLBZ"] = "1";
                    dr["CLJG"] = "提示：今日短信发送次数已达上限";
                    dt.Rows.Add(dr);
                    return dt;
                }
            }
            try
            {
                DataTable dtrev = SendMessage(MOBILE_NO, MESSAGE);
                return dtrev;
            }
            catch (Exception ex)
            {
                DataTable dtrev = new DataTable();
                dtrev.Columns.Add("CLBZ", typeof(string));
                dtrev.Columns.Add("CLJG", typeof(string));
                DataRow newrow = dtrev.NewRow();
                newrow["CLBZ"] = "9";
                newrow["CLJG"] = "发送失败";
                dtrev.Rows.Add(newrow);
                return dtrev;
            }
        }

        /// <summary>
        /// 预约挂号数据同步
        /// </summary>
        /// <param name="SFZ_NO">身份证</param>
        /// <param name="PAT_NAME">病人姓名</param>
        /// <param name="YLCARD_NO">医疗卡号</param>
        /// <param name="PAT_ID">病人ID</param>
        /// <returns></returns>
        public DataSet YYGHUPLOADSAVE(string SFZ_NO, string PAT_NAME, string YLCARD_NO, string PAT_ID, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            //DataTable dtREG = new Plat.BLL.BaseFunction().GetList("register_appt", "PAT_ID='" + PAT_ID + "' and HOS_ID='" + HOS_ID + "' and sch_date>=curdate()", "APPT_TYPE", "SCH_DATE", "APPT_TATE", "HOS_SN", "YLCARD_TYPE", "YLCARD_NO");

            string sqlappt = "select *from register_appt where PAT_ID=@PAT_ID and HOS_ID=@HOS_ID  and sch_date>=curdate()";
            MySqlParameter[] parameter2 = {
                    new MySqlParameter("@PAT_ID",PAT_ID),
                    new MySqlParameter("@HOS_ID",HOS_ID) };

            DataTable dtREG = DBQuery("", sqlappt.ToString(), parameter2).Tables[0];

            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("YYGHUPLOADSAVE", "0");

            string HOS_SN = "";
            if (dtREG.Rows.Count == 0)
            {
                return null;
            }

            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", SFZ_NO);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_NAME", PAT_NAME);
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("HOS_SN", typeof(string));
            dtNew.Columns.Add("APPT_TYPE", typeof(string));
            dtNew.Columns.Add("SCH_TYPE", typeof(string));
            dtNew.Columns.Add("YLCARD_TYPE", typeof(string));
            dtNew.Columns.Add("YLCARD_NO", typeof(string));
            foreach (DataRow dr in dtREG.Rows)
            {
                DataRow dr_new = dtNew.NewRow();
                dr_new["HOS_SN"] = dr["HOS_SN"].ToString().Trim();
                dr_new["APPT_TYPE"] = dr["APPT_TYPE"].ToString().Trim();
                dr_new["SCH_TYPE"] = Convert.ToDateTime(dr["SCH_DATE"]).ToShortDateString() == Convert.ToDateTime(dr["APPT_TATE"]).ToShortDateString() ? "02" : "01";
                dr_new["YLCARD_TYPE"] = FormatHelper.GetStr(dr["YLCARD_TYPE"]);
                dr_new["YLCARD_NO"] = FormatHelper.GetStr(dr["YLCARD_NO"]);
                dtNew.Rows.Add(dr_new);
            }
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SNLIST");
            XMLHelper.X_XmlInsertTable(doc, "ROOT/BODY/SNLIST", dtNew, "SN");

            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtbody = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                dtbody.TableName = "body";
                DataSet ds = new DataSet();
                ds.Tables.Add(dtbody.Copy());

                DataTable dtSTATELIST = new DataTable();
                dtSTATELIST.TableName = "STATELIST";
                try
                {
                    dtSTATELIST = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/STATELIST").Tables[0];
                    if (dtSTATELIST != null && !dtSTATELIST.Columns.Contains("DJ_TIME"))
                    {
                        dtSTATELIST.Columns.Add("DJ_TIME", typeof(string));
                        foreach (DataRow dr in dtSTATELIST.Rows)
                        {
                            dr["DJ_TIME"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                    }
                }
                catch
                { }
                ds.Tables.Add(dtSTATELIST.Copy());

                DataTable dtAPPT_NOPAYLIST = new DataTable();
                dtAPPT_NOPAYLIST.TableName = "APPT_NOPAYLIST";
                try
                {
                    dtAPPT_NOPAYLIST = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/APPT_NOPAYLIST").Tables[0];
                }
                catch
                { }
                ds.Tables.Add(dtAPPT_NOPAYLIST.Copy());

                DataTable dtAPPT_PAYLIST = new DataTable();
                dtAPPT_PAYLIST.TableName = "APPT_PAYLIST";
                try
                {
                    dtAPPT_PAYLIST = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/APPT_PAYLIST").Tables[0];
                }
                catch
                { }
                ds.Tables.Add(dtAPPT_PAYLIST.Copy());
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 检查是否可以退费
        /// </summary>
        /// <param name="HOS_ID"></param>
        /// <param name="HOS_SN"></param>
        /// <param name="TYPE"></param>
        /// <returns></returns>
        public DataTable CHECKCANCELSTATE(string HOS_ID, string HOS_SN, string TYPE, Dictionary<string, string> dic)
        {
            DataTable dtrev = new DataTable();
            dtrev.Columns.Add("CLBZ", typeof(string));
            dtrev.Columns.Add("CLJG", typeof(string));
            //string REG_ID = dic["REG_ID"];
            string REG_ID = dic.ContainsKey("REG_ID") ? dic["REG_ID"] : "";
            if (TYPE == "0" && !string.IsNullOrEmpty(REG_ID))//挂号
            {
                //DataTable dtAPPT = new Plat.BLL.BaseFunction().GetList("register_APPT", "REG_ID='" + REG_ID + "'", "SCH_DATE", "APPT_TYPE");
                 
                string sqlappt = "select *from register_APPT where REG_ID=@REG_ID";
                MySqlParameter[] parameter2 = {
                    new MySqlParameter("@REG_ID",REG_ID) };

                DataTable dtAPPT = DBQuery("", sqlappt.ToString(), parameter2).Tables[0];

                if (dtAPPT.Rows[0]["APPT_TYPE"].ToString().Trim() == "1" && FormatHelper.GetStr(dtAPPT.Rows[0]["SCH_DATE"]).CompareTo(DateTime.Now.ToString("yyyy-MM-dd")) > 0)
                {
                    DataRow dr = dtrev.NewRow();
                    dr["CLBZ"] = "0";
                    dr["CLJG"] = "SUCCESS";
                    dtrev.Rows.Add(dr);
                }
                else
                {
                    DataRow dr = dtrev.NewRow();
                    dr["CLBZ"] = "1";
                    dr["CLJG"] = "不符合手机退费条件，请去人工窗口退费";
                    dtrev.Rows.Add(dr);
                }
            }
            else
            {
                DataRow dr = dtrev.NewRow();
                dr["CLBZ"] = "1";
                dr["CLJG"] = "请去人工窗口退费";
                dtrev.Rows.Add(dr);
            }
            return dtrev;
        }

        /// <summary>
        /// 检查是否可以退费
        /// </summary>
        /// <param name="HOS_ID"></param>
        /// <param name="HOS_SN"></param>
        /// <param name="TYPE"></param>
        /// <returns></returns>
        public DataTable CHECKCANCELSTATE(string HOS_ID, string HOS_SN, string TF_SOURCE, string TYPE, Dictionary<string, string> dic)
        {
            return CHECKCANCELSTATE(HOS_ID, HOS_SN, TYPE, dic);
        }

        /// <summary>
        /// 门诊退费同步
        /// </summary>
        /// <param name="PAT_ID"></param>
        /// <param name="SFZ_NO"></param>
        /// <param name="PAT_NAME"></param>
        /// <param name="YLCARD_NO"></param>
        /// <param name="HOS_ID"></param>
        /// <returns></returns>
        public DataTable MZSTFUPLOAD(string PAT_ID, string SFZ_NO, string PAT_NAME, string YLCARD_NO, string HOS_ID, Dictionary<string, string> dic)
        {
            string SOURCE = dic.ContainsKey("SOURCE") ? FormatHelper.GetStr(dic["SOURCE"]) : "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("YYMZUPLOADSAVE", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFLIST");

            string BARCODE = "";
            //DataTable dtpatcardbind = new Plat.BLL.BaseFunction().GetList("opt_pay", "HOS_ID='" + HOS_ID + "' and PAT_ID='" + PAT_ID + "' and dj_date>='" + DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd") + "' and HOS_PAY_SN<>'ERROR'", "HOS_PAY_SN", "HOS_ID", "LTERMINAL_SN");
            string sqlappt = "select *from opt_pay where PAT_ID=@PAT_ID and HOS_ID=@HOS_ID and dj_date>=CURDATE()-90";
            MySqlParameter[] parameter2 = {
                    new MySqlParameter("@PAT_ID",PAT_ID),
                    new MySqlParameter("@HOS_ID",HOS_ID) };

            DataTable dtpatcardbind = DBQuery("", sqlappt.ToString(), parameter2).Tables[0];

            XMLHelper.X_XmlInsertTable(doc, "ROOT/BODY/SFLIST", dtpatcardbind, "SF");

            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                if (dtrev.Rows[0]["CLBZ"].ToString().Trim() != "0")
                {
                    return null;
                }
                dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY/STATELIST").Tables[0];
                return dtrev;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 医保在线支付预结算
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="SFZ_NO">病人身份证号码</param>
        /// <param name="PAT_NAME">病人姓名</param>
        /// <param name="YLCARD_TYPE">卡类型</param>
        /// <param name="YLCARD_NO">卡号</param>
        /// <param name="HOS_SN">HIS预约唯一流水号</param>
        /// <param name="lTERMINAL_SN">终端标示</param>
        ///<param name="external">各医院自己定制的字段</param>
        /// <returns></returns>
        public DataTable YYGHMOBPAYYJS(string HOS_ID, string SFZ_NO, string PAT_NAME, string YLCARD_TYPE, string YLCARD_NO, string HOS_SN, string lTERMINAL_SN, Dictionary<string, string> external)
        {
            string SOURCE = external.ContainsKey("SOURCE") ? FormatHelper.GetStr(external["SOURCE"]) : "";
            DataTable dtrev = new DataTable();

            string YNCARDNO = "";

            string sjh = "";

            GETPATBARCODE(HOS_ID, external["PAT_ID"].ToString(), ref YNCARDNO);
            JObject jzzj = new JObject();

            JObject jybrc = new JObject();

            JObject jinSYBGH = new JObject
            {
                { "pactcode", "SYB" }
            };

            jybrc.Add("inSYBGH", jinSYBGH);

            jzzj.Add("zzj", jybrc);

            string medicareParam = Newtonsoft.Json.JsonConvert.SerializeObject(jzzj);
            medicareParam = Base64Encode(medicareParam);

            string[] REGISTER_TYPE = external["REGISTER_TYPE"].Split('|');

            string regLevelCode = REGISTER_TYPE[0];
            string noonCode = REGISTER_TYPE[1];
            string schemaId = REGISTER_TYPE[2];

            REGISTERFEE registerfee = new REGISTERFEE()
            {
                medicareParam = medicareParam,//        医保预留
                pactCode = "",//   结算code      FALSE
                patientID = YNCARDNO,// 患者ID        FALSE
                scheduleId = schemaId,//         FALSE
                vipCardNo = "",//        FALSE
                vipCardType = "",//            FALSE
                preid = HOS_SN
            };

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(registerfee);

            Output<REGISTERFEEDATA> outputregisterfee
      = new MIDServiceHelper().CallServiceAPI<REGISTERFEEDATA>("/hisbooking/register/calcRegisterFee", jsonstr);

            if (outputregisterfee.code != 0)
            {
                dtrev = RtnResultDatatable("99", outputregisterfee.message);
                return dtrev;
            }
            sjh = outputregisterfee.data.receiptNumber + "|" + outputregisterfee.data.ghxh;//收据号|挂号序号（王丹那边就不用改了）modi by wyq 2023 01 06

            new Plat.BLL.BaseFunction().UpdateList("register_appt", "HOS_ID='" + HOS_ID + "' and hos_sn='" + HOS_SN + "' ", "pre_no='" + sjh + "'");

            JObject jybcc = JObject.Parse(Base64Decode(outputregisterfee.data.medicareParam));

            /*
        DepartCode: "科室代码",
        DoctorCode: "医生代码",
        DiagFeeCode: "诊疗费代码",
        DiagFeeName: "诊疗费名称",
        RegFeeCode: "挂号费代码",
        RegFeeName: "挂号费名称",
        TotalAmount: "总金额",
        DiagFee: "诊疗费",
        RegFee: "挂号费",
        DepartName: "科室名称",
        DoctorName: "医生名称",
        RegName: "挂号类别编码"
             */
            //              江苏省医保（YJS_IN）：
            //科室代码 | 医生代码 | 诊疗费代码 | 诊疗费名称 | 挂号费代码 | 挂号费名称 | 总金额 | 诊疗费 | 挂号费 | 科室名称 | 医生名称 | 挂号类别编码
            //              江苏省医保病人JS_OUT出参：
            //医保单据号 | 总费用 | 统筹费用 | 个人账户费用 | 现金费用

            dtrev.Columns.Add("CLBZ");
            dtrev.Columns.Add("CLJG");

            dtrev.Columns.Add("YJS_IN");
            dtrev.Columns.Add("APPT_PAY");
            dtrev.Columns.Add("MZNO");
            dtrev.Columns.Add("JBR");

            string yjs_in = jybcc["zzj"]["inSYBGH"]["DepartCode"].ToString() + "|" +
                jybcc["zzj"]["inSYBGH"]["DoctorCode"].ToString() + "|" +
                jybcc["zzj"]["inSYBGH"]["DiagFeeCode"].ToString() + "|" +
                jybcc["zzj"]["inSYBGH"]["DiagFeeName"].ToString() + "|" +
                jybcc["zzj"]["inSYBGH"]["RegFeeCode"].ToString() + "|" +
                jybcc["zzj"]["inSYBGH"]["RegFeeName"].ToString() + "|" +
                jybcc["zzj"]["inSYBGH"]["TotalAmount"].ToString() + "|" +
                jybcc["zzj"]["inSYBGH"]["DiagFee"].ToString() + "|" +
                jybcc["zzj"]["inSYBGH"]["RegFee"].ToString() + "|" +

                jybcc["zzj"]["inSYBGH"]["DepartName"].ToString() + "|" +
                jybcc["zzj"]["inSYBGH"]["DoctorName"].ToString() + "|" +
                jybcc["zzj"]["inSYBGH"]["RegName"].ToString();

            DataRow dr = dtrev.NewRow();
            dr["CLBZ"] = "0";

            dr["CLJG"] = "success";

            dr["YJS_IN"] = yjs_in;
            dr["APPT_PAY"] = outputregisterfee.data.totalFee;
            dr["MZNO"] = "";
            dr["JBR"] = "";
            dtrev.Rows.Add(dr);

            return dtrev;
        }

        public static string GETPATHOSPITALID(string YNCARDNO, string SFZ_NO, string PAT_NAME, string SEX, string BIRTHDAY, string GUARDIAN_NAME, string MOBILE_NO, string ADDRESS, string PAT_ID, string YLCARD_TYPE, string YLCARD_NO)
        {
            string SOURCE = "";

            Hos185_His.Models.GETPATINFO getpatinfo = new Hos185_His.Models.GETPATINFO()
            {
                businessType = "",
                cardNo = "", //医院内部就诊卡号
                idCardNo = SFZ_NO, //证件号 和 cardNo不能同时为空
                idCardType = "01", //证件类型 01:⾝份证 06:护照 08:港澳台居⺠来往内地通⾏证
                mcardNo = "", //绑定的医疗证号
                mcardNoType = "", //绑定的医疗证类型 4:⾝份证/港澳台通⾏证 5:护照
                name = PAT_NAME, //姓名（精确查找）
                needWHBindHealthCardFlag = "", //是否获取武汉电⼦健康卡绑定信息
                phoneNo = ""  //⼿机号
            };

            string inputjson = Newtonsoft.Json.JsonConvert.SerializeObject(getpatinfo);

            Hos185_His.Models.Output<List<Hos185_His.Models.GETPATINFODATA>>
                output = new MIDServiceHelper().CallServiceAPI<List<Hos185_His.Models.GETPATINFODATA>>("/hispatientinfo/compatient/getComPatientInfo", inputjson);

            if (output.code == 0 && output.data.Count > 0)
            {
                return output.data[0].cardNo;
            }

            string sex = "", sexname = "", birthday = "";
            GetAgeBySFZ(SFZ_NO, ref birthday, ref sex, ref sexname);

            Hos185_His.Models.SENDCARDINFO sendcardinfo = new Hos185_His.Models.SENDCARDINFO()
            {
                area = "", //现住地：区code
                birthDay = birthday, //出⽣⽇期 yyyy-MM-dd
                city = "", //现住地：市code
                detailAddress = ADDRESS, //现住地：详细信
                guardianIdCardNo = SFZ_NO, //监护⼈证件号
                home = "", //⼾⼝或家庭地址
                homeTel = MOBILE_NO, //联系电话
                idCardNo = SFZ_NO, //证件号
                idCardType = "01", //证件类型 01:⾝份证 06:护照 08:港澳台居⺠来往内地通⾏证
                linkManAddress = ADDRESS, //联系⼈地址
                linkManName = PAT_NAME, //联系⼈姓名
                linkManTel = MOBILE_NO, //联系⼈电话
                mcardNo = SFZ_NO, //绑定的医疗证号
                mcardNoType = "4", //绑定的医疗证类型 4:⾝份证/港澳台通⾏证 5:护照
                name = PAT_NAME, //姓名
                nationCode = "", //⺠族code
                operCode = "", //操作⼈
                pactCode = "", //合同编号
                province = "", //现住地：省code
                relaCode = "", //联系⼈和患者关系编码
                road = ADDRESS, //现住地：街道code
                sexCode = sex, //性别编码 M:男, F:⼥
                sourceCode = "", //客⼾来源
                sourceFlag = "MZ", //建档来源 MZ ⻔诊 JZ 急诊 TJ 体检
                ternimalNo = "" //ternimalNo
            };

            inputjson = Newtonsoft.Json.JsonConvert.SerializeObject(sendcardinfo);

            Hos185_His.Models.Output<List<Hos185_His.Models.SENDCARDINFODATA>>
                outputsend = new MIDServiceHelper().CallServiceAPI<List<Hos185_His.Models.SENDCARDINFODATA>>("/hispatientinfo/compatient/saveComPatientInfo", inputjson);

            if (outputsend.code == 0)
            {
                YNCARDNO = outputsend.data[0].cardNo;
                Plat.BLL.pat_card_bind BLLpat_card_bind = new Plat.BLL.pat_card_bind();
                bool exists = BLLpat_card_bind.Exists(HOS_ID, long.Parse(PAT_ID), 1, YNCARDNO);
                if (!exists)
                {
                    Plat.Model.pat_card_bind pat_card_bind = new Plat.Model.pat_card_bind();
                    pat_card_bind.HOS_ID = HOS_ID;
                    pat_card_bind.PAT_ID = long.Parse(PAT_ID);
                    pat_card_bind.YLCARTD_TYPE = 1;
                    pat_card_bind.YLCARD_NO = YNCARDNO;
                    pat_card_bind.MARK_BIND = 1;
                    pat_card_bind.BAND_TIME = DateTime.Now;
                    BLLpat_card_bind.Add(pat_card_bind);
                }
            }

            return YNCARDNO;
        }

        public static int GetAgeBySFZ(string identityCard, ref string birthday, ref string sexid, ref string sexname)
        {
            string sex = "";

            DateTime dpbirthday = DateTime.Now;
            //处理18位的身份证号码从号码中得到生日和性别代码
            if (identityCard.Length == 18)
            {
                birthday = identityCard.Substring(6, 4) + "-" + identityCard.Substring(10, 2) + "-" + identityCard.Substring(12, 2);

                sex = identityCard.Substring(14, 3);
            }
            //处理15位的身份证号码从号码中得到生日和性别代码
            if (identityCard.Length == 15)
            {
                birthday = "19" + identityCard.Substring(6, 2) + "-" + identityCard.Substring(8, 2) + "-" + identityCard.Substring(10, 2);

                sex = identityCard.Substring(12, 3);
            }

            dpbirthday = Convert.ToDateTime(birthday);
            int age = DateTime.Now.Year - dpbirthday.Year;
            if (DateTime.Now.Month < dpbirthday.Month || (DateTime.Now.Month == dpbirthday.Month && DateTime.Now.Day < dpbirthday.Day)) age--;

            if (int.Parse(sex) % 2 == 0)
            {
                sexid = "2";
                sexname = "女性";
            }
            else
            {
                sexid = "1";
                sexname = "男性";
            }
            return age;
        }

        /// <summary>
        /// 获取病人体检记录 （TYPE：GETPERECORD）
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="PAT_NAME">病人姓名</param>
        /// <param name="SFZ_NO">身份证号码</param>
        /// <param name="BEGINDATE">开始查询日期</param>
        /// <param name="ENDDATE">截止查询日期</param>
        /// <param name="lTERMINAL_SN">终端号</param>
        /// <param name="external">附加字段</param>
        /// <returns></returns>
        public DataSet GETPERECORD(string HOS_ID, string PAT_NAME, string SFZ_NO, string BEGINDATE, string ENDDATE, string lTERMINAL_SN, Dictionary<string, string> external)
        {
            return null;
        }

        /// <summary>
        /// 7.2获取体检报告包含明细指标（分科室） （TYPE：GETALLPEREPORT）
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="TASK_ID">任务ID</param>
        /// <param name="HIS_PAT_ID">HIS病人唯一ID</param>
        /// <param name="TYPE">查询类型</param>
        /// <param name="lTERMINAL_SN">终端号</param>
        /// <param name="external">附加字段</param>
        /// <returns></returns>
        public DataSet GETALLPEREPORT(string HOS_ID, string TASK_ID, string HIS_PAT_ID, string TYPE, string lTERMINAL_SN, Dictionary<string, string> external)
        {
            return null;
        }

        /// <summary>
        /// 7.3获取体检报告科室列表 （TYPE：GETDEPTPEREPORT）
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="TASK_ID">任务ID</param>
        /// <param name="HIS_PAT_ID">HIS病人唯一ID</param>
        /// <param name="TYPE">查询类型</param>
        /// <param name="lTERMINAL_SN">终端号</param>
        /// <param name="external">附加字段</param>
        /// <returns></returns>
        public DataSet GETDEPTPEREPORT(string HOS_ID, string TASK_ID, string HIS_PAT_ID, string TYPE, string lTERMINAL_SN, Dictionary<string, string> external)
        {
            return null;
        }

        /// <summary>
        ///7.4获取体检报告明细（分科室） （TYPE：GETDEPTPEREPORTMX）
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="TASK_ID">任务ID</param>
        /// <param name="HIS_PAT_ID">HIS病人唯一ID</param>
        /// <param name="TYPE">查询类型</param>
        /// <param name="DEPT_CODE">科室代码</param>
        /// <param name="lTERMINAL_SN">终端号</param>
        /// <param name="external">附加字段</param>
        /// <returns></returns>
        public DataSet GETDEPTPEREPORT(string HOS_ID, string TASK_ID, string HIS_PAT_ID, string TYPE, string DEPT_CODE, string lTERMINAL_SN, Dictionary<string, string> external)
        {
            return null;
        }

        /// <summary>
        /// 7.5获取体检报告结论（TYPE：GETPEREPORTRESULT）
        /// </summary>
        /// <param name="HOS_ID">医院代码</param>
        /// <param name="TASK_ID">任务ID</param>
        /// <param name="HIS_PAT_ID">病人唯一ID</param>
        /// <param name="lTERMINAL_SN">终端号</param>
        /// <param name="external">附加字段</param>
        /// <returns></returns>
        public DataSet GETPEREPORTRESULT(string HOS_ID, string TASK_ID, string HIS_PAT_ID, string lTERMINAL_SN, Dictionary<string, string> external)
        {
            return null;
        }

        /// <summary>
        /// 通用HIS接口
        /// </summary>
        /// <param name="external"></param>
        /// <returns></returns>
        public DataSet COMMONINTERFACE(Dictionary<string, string> external)
        {
            string TYPE = external["MODULE_TYPE"];

            string SOURCE = external.ContainsKey("SOURCE") ? FormatHelper.GetStr(string.IsNullOrEmpty(external["SOURCE"]) ? "" : external["SOURCE"]) : "";
            switch (TYPE)
            {
                case "GETSTATUSBYORDERID"://订单状态查询
                    return GETSTATUSBYORDERID(external["COMM_MAIN"], external["ZF_TYPE"], external["BIZ_TYPE"], SOURCE);

                case "CHECKMBCANPAY":
                    return CHECKMBCANPAY(external["SFZ_NO"], external["MB_ID"]);

                case "YUNHOSPRESEXAMSAVE"://互联网医师审方结果保存到HIS系统
                    return YUNHOSPRESEXAMSAVE(external["HOS_ID"], external["HOS_SN"], external["PAT_NAME"], external["SFZ_NO"], external["YLCARTD_TYPE"], external["YLCARD_NO"], external["DEPT_CODE"], external["DOC_NO"], external["SF_DOC"], external["RECIPE_NO"], external["SF_STATUS"], external["SF_DESC"], external.ContainsKey("CASign") ? external["CASign"] : "");

                case "GETPATBARCODE"://根据病人ID获取院内卡
                    string YNCARDNO = "";
                    return GETPATBARCODE(external["HOS_ID"], external["PAT_ID"], ref YNCARDNO);

                case "CHECKSCHVALID":
                    return null;

                case "GETHOSZFDATA":
                    return GETHOSZFDATA(external);

                case "GETHOSZFRESULT":
                    return GETHOSZFRESULT(external);

                case "HOSZFREFUND":
                    return HOSZFREFUND(external);

                case "PREORDER":
                    return PreOrder(external);

                case "GETRPTPDF":
                    return GETRPTPDF(external);

                case "NANJINGMENZHEN":

                    return NanJingMenZhen(external);

                case "QUERYINVOICE":
                    return QUERYINVOICE(external);

                case "JSYBYTFNOTICE":
                    return SYB_TFNOTICE(external);

                default:
                    return COMMONINTERFACE(TYPE, external);
            }
            return null;
        }

        public DataSet SYB_TFNOTICE(Dictionary<string, string> dic)
        {
            string payhossn = "";

            if (dic.ContainsKey("HOS_ID") && dic.ContainsKey("HOS_SN"))
            {
                var regappt = new Plat.BLL.register_appt(PBusHos185.AUID.Value).GetModelByHOS_SNAndHOSID(dic["HOS_SN"], dic["HOS_ID"]);

                //DataTable regPay = new Plat.BLL.BaseFunction(PBusHos45.AUID.Value).GetList("register_pay", "reg_id='" + regappt.REG_ID + "'", "HOS_SN", "OPT_SN");

                string sqlregpay = "select * from register_pay where reg_id=@reg_id";

                MySqlParameter[] parameter3 = {
                    new MySqlParameter("@reg_id",regappt.REG_ID) };

                DataTable regPay = DBQuery("", sqlregpay.ToString(), parameter3).Tables[0];

                payhossn = CommonFunction.GetStr(regPay.Rows[0]["HOS_SN"]);
            }

            #region 退款

            string out2208 = dic["CHSOUTPUT2208"];

            JObject o2208 = JObject.Parse(out2208) ;
            string setlid = o2208["setlinfo"]["setl_id"].ToString();
            string medfee_sumamt = o2208["setlinfo"]["medfee_sumamt"].ToString();
            Soft.DBUtility.RedisHelperSentinels redis = new RedisHelperSentinels();

            string psn_name = o2208["setlinfo"]["psn_name"].ToString();

            //退费通知
            string redistfinfokey = "TH" + dic["HOS_SN"] + dic["HOS_ID"];
            JObject jobj = JObject.Parse(redis.Get(redistfinfokey, 7));
            //通知

            string refund_order_id = jobj.ContainsKey("refund_order_id") ? jobj["refund_order_id"].ToString() : "";

            string refundReceiptNumber = jobj["refundReceiptNumber"].ToString();

            refundMsg refundMsg = new refundMsg()
            {
                oldReceiptNumber = "",//原收据号    N  原HIS内收据号
                patientName = psn_name,//患者姓名    Y  患者姓名，用于验证防止误操作
                receiptNumber = refundReceiptNumber,//退费收据号   Y  HIS标记唯一一次退费的单据号。根据对应退费接口反馈的tsjh
                balanceNo = setlid,
                refundAmount = Math.Abs(decimal.Parse(medfee_sumamt)).ToString(),//退费金额    Y  退费金额，用于验证
                refundNo = refund_order_id,//退费流水号   Y  支付金融机构交易流水号，如源启支付，支付宝、微信、银行等机构的原始退费流水号，用于对账
                refundPayType = "56"//退支付方式   Y
            };

            var jsonstr = JsonConvert.SerializeObject(refundMsg);
            var outputmsg = new MIDServiceHelper().CallServiceAPI<object>("/hisbooking/register/refundMsg", jsonstr, "SYBAPP", "SYBAPP");

            #endregion 退款

            return DS_RtnDealInfo("0", "succ");
        }

        private DataSet GETRPTPDF(Dictionary<string, string> external)
        {
            if (external["examTest"] == "2")//1:检查报告 2:检验报告
            {
            }
            Hos185_His.Models.Report.downloadFile download = new Hos185_His.Models.Report.downloadFile()
            {
                filePath = "",
                inspectType = "testType",//检验检查列表返回的inspectType   检查类型枚举   InspectTypeEnum
                reportId = external["rpotcNo"].Replace(" ", "+"),//检验检查列表返回的报告id  inspectNo
                visitType = ""
            };

            string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(download);

            Output<downloadFileData> output
      = new MIDServiceHelper().CallServiceAPI<downloadFileData>("/medicaloss/file/downloadFile", jsonstr);

            DataTable dtbody = new DataTable();
            dtbody.Columns.Add("CLJG");//add by hlw 2018.03.01 只是普通勿进行重复查询
            dtbody.Columns.Add("CLBZ");//add by hlw 2018.03.01 只是普通勿进行重复查询

            dtbody.Columns.Add("no_repeat", typeof(int));//add by hlw 2018.03.01 只是普通勿进行重复查询

            DataRow dr = dtbody.NewRow();
            dr["no_repeat"] = 1;
            dr["CLJG"] = output.message;
            dr["CLBZ"] = output.code;
            dtbody.Rows.Add(dr);
            if (output.code != 0)
            {
                return DS_RtnDealInfo("2", output.message);
            }

            XmlDocument docrtn = QHXmlMode.GetBaseXml("GETRPTPDF", "");

            XMLHelper.X_XmlInsertNode(docrtn, "ROOT/BODY", "pdfBase64", "");
            XMLHelper.X_XmlInsertNode(docrtn, "ROOT/BODY", "CLJG", "success");
            XMLHelper.X_XmlInsertNode(docrtn, "ROOT/BODY", "CLBZ", "0");

            DataSet dsbody = XMLHelper.X_GetXmlData(docrtn, "ROOT/BODY");
            return dsbody;
        }

        public DataSet COMMONINTERFACE(string TYPE, Dictionary<string, string> dic)
        {
            if (dic.ContainsKey("SOURCE") && dic["SOURCE"] == "JSYBY")
            {//医保云的请求
                return CHS_YBY(TYPE, dic);
            }

            if (dic.ContainsKey("SOURCE") && dic["SOURCE"] == "JSSYB")
            {//医保云的请求
                return CHS_SYB(TYPE, dic);
            }
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml(TYPE, "0");
            DataSet ds = new DataSet();

            string HOS_ID = dic["HOS_ID"].ToString();
            string HOS_SN = "";
            string chsOutput1101 = "";
            string MDTRT_CERT_TYPE = dic["MDTRT_CERT_TYPE"].ToString();
            string MDTRT_CERT_NO = dic["MDTRT_CERT_NO"].ToString();
            string YNCARDNO = "";
            string YBDJH = "";

            string sjh = "";//卫宁

            string chsInput2201 = "";
            string chsInput2203 = "";
            string chsInput2204 = "";
            string chsInput2206 = "";
            string chsInput2207 = "";//新增2207入参

            decimal jeall = 0;

            if (TYPE != "CHSREGREFUNDSAVE")
            {
                jeall = dic.Keys.Contains("APPT_PAY")
                    ? decimal.Parse(dic["APPT_PAY"])
                    : dic.Keys.Contains("JEALL")
                        ? decimal.Parse(dic["JEALL"])
                        : 0;
            }
            decimal totalfee = 0;

            JObject jzzj = new JObject();
            JObject jybrc = new JObject();
            switch (TYPE)
            {
                case "GETCHSREGTRY":

                    if (!dic.ContainsKey("YNCARDNO"))
                    {
                        GETPATBARCODE(HOS_ID, dic["PAT_ID"].ToString(), ref YNCARDNO);
                    }
                    else
                    {
                        YNCARDNO = dic["YNCARDNO"];
                    }
                    HOS_SN = dic["HOS_SN"].ToString();

                    chsOutput1101 = dic.ContainsKey("CHSOUTPUT1101") ? dic["CHSOUTPUT1101"].ToString() :
                        dic.ContainsKey("chsOutput1101") ? dic["chsOutput1101"].ToString() : "";

                    if (string.IsNullOrEmpty(chsOutput1101))
                    {
                        return DS_RtnDealInfo("2", "chsOutput1101为空");
                    }

                    QHSiInterface.T1101.Data t1101 = new QHSiInterface.T1101.Data()
                    {
                        mdtrt_cert_type = MDTRT_CERT_TYPE,
                        mdtrt_cert_no = MDTRT_CERT_NO,
                        psn_name = dic["PAT_NAME"],
                        card_sn = "",
                        psn_cert_type = "",
                        begntime = "",
                        certno = "",
                    };

                    T1101.Root in1101 = new T1101.Root();
                    in1101.data = t1101;

                    #region 预算

                    string[] REGISTER_TYPE = dic["REGISTER_TYPE"].Split('|');

                    string regLevelCode = REGISTER_TYPE[0];
                    string noonCode = REGISTER_TYPE[1];
                    string schemaId = REGISTER_TYPE[2];

                    JObject out1101 = JObject.Parse(chsOutput1101);

                    jybrc.Add("in1101", JObject.Parse(JsonConvert.SerializeObject(in1101)));

                    jybrc.Add("out1101", out1101["output"]);

                    jzzj.Add("zzj", jybrc);

                    string medicareParam = Newtonsoft.Json.JsonConvert.SerializeObject(jzzj);
                    medicareParam = Base64Encode(medicareParam);

                    REGISTERFEE registerfee = new REGISTERFEE()
                    {
                        medicareParam = medicareParam,//        医保预留
                        pactCode = "17",//   结算code      FALSE
                        patientID = YNCARDNO,// 患者ID        FALSE
                        scheduleId = schemaId,//         FALSE
                        vipCardNo = "",//        FALSE
                        vipCardType = "",//            FALSE
                        preid = dic["HOS_SN"]
                    };

                    string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(registerfee);

                    Output<REGISTERFEEDATA> outputregisterfee
              = new MIDServiceHelper().CallServiceAPI<REGISTERFEEDATA>("/hisbooking/register/calcRegisterFee", jsonstr);

                    if (outputregisterfee.code != 0)
                    {
                        DataTable dtrev = RtnResultDatatable("99", outputregisterfee.message);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    sjh = outputregisterfee.data.receiptNumber + "|" + outputregisterfee.data.ghxh;//收据号|挂号序号（王丹那边就不用改了）modi by wyq 2023 01 06
                    JObject jybcc = JObject.Parse(Base64Decode(outputregisterfee.data.medicareParam));

                    totalfee = decimal.Parse(jybcc["zzj"]["in2206"]["data"]["medfee_sumamt"].ToString());
                    chsInput2201 = jybcc["zzj"]["in2201"].ToString();
                    chsInput2203 = jybcc["zzj"]["in2203"].ToString();
                    chsInput2204 = jybcc["zzj"]["in2204"].ToString();
                    chsInput2206 = jybcc["zzj"]["in2206"].ToString();
                    chsInput2207 = jybcc["zzj"]["in2207"].ToString();

                    #endregion 预算

                    break;

                case "CHSREGTRY":
                    string msg = "";
                    QHSiInterface.RTNJ1101.Root rt1101 = JSONSerializer.Deserialize<QHSiInterface.RTNJ1101.Root>(dic["CHSOUTPUT1101"]);
                    QHSiInterface.RTNJ2201.Root rt2201 = JSONSerializer.Deserialize<QHSiInterface.RTNJ2201.Root>(dic["CHSOUTPUT2201"]);
                    Dictionary<string, string> expContent = JSONSerializer.Deserialize<Dictionary<string, string>>(dic["EXPCONTENT"]);
                    chsInput2203 = expContent["chsInput2203"];
                    chsInput2204 = expContent["chsInput2204"];
                    chsInput2206 = expContent["chsInput2206"];
                    chsInput2207 = expContent["chsInput2207"];
                    YBDJH = expContent["YBDJH"];
                    sjh = expContent["SJH"];

                    #region 2203A

                    QHSiInterface.T2203.Input t2203 = JSONSerializer.Deserialize<QHSiInterface.T2203.Input>(chsInput2203);
                    t2203.mdtrtinfo.mdtrt_id = rt2201.output.data.mdtrt_id;
                    string infno = "2203A";
                    CHSHelper.InputRoot inputRoot = new CHSHelper.InputRoot();
                    inputRoot.HOS_ID = HOS_ID;
                    inputRoot.infno = infno;
                    inputRoot.insuplc_admdvs = rt1101.output.insuinfo[0].insuplc_admdvs;
                    inputRoot.InData = JSONSerializer.Serialize(t2203); //jin2203.ToString();
                    CHSHelper.OutputRoot outputRoot = new CHSHelper.OutputRoot();
                    bool flag = CHSHelper.Post(inputRoot, ref outputRoot, ref msg);
                    if (!flag)
                    {
                        DataTable dtrev = RtnResultDatatable("99", msg);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    if (outputRoot.Code != "0")
                    {
                        DataTable dtrev = RtnResultDatatable("99", outputRoot.Msg);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    chsInput2203 = outputRoot.chsInput;
                    string chsOutput2203 = outputRoot.chsOutput;

                    #endregion 2203A

                    #region 2204

                    QHSiInterface.T2204.Input t2204 = JSONSerializer.Deserialize<QHSiInterface.T2204.Input>(chsInput2204);
                    for (int i = 0; i < t2204.feedetail.Count; i++)
                    {
                        t2204.feedetail[i].mdtrt_id = rt2201.output.data.mdtrt_id;
                    }
                    infno = "2204";
                    inputRoot = new CHSHelper.InputRoot();
                    inputRoot.HOS_ID = HOS_ID;
                    inputRoot.infno = infno;
                    inputRoot.insuplc_admdvs = rt1101.output.insuinfo[0].insuplc_admdvs;
                    inputRoot.InData = JSONSerializer.Serialize(t2204); //jin2204.ToString();
                    outputRoot = new CHSHelper.OutputRoot();
                    flag = CHSHelper.Post(inputRoot, ref outputRoot, ref msg);
                    if (!flag)
                    {
                        DataTable dtrev = RtnResultDatatable("99", msg);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    if (outputRoot.Code != "0")
                    {
                        DataTable dtrev = RtnResultDatatable("99", outputRoot.Msg);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    chsInput2204 = outputRoot.chsInput;
                    string chsOutput2204 = outputRoot.chsOutput;

                    #endregion 2204

                    #region 2206

                    QHSiInterface.T2206.Input t2206 = JSONSerializer.Deserialize<QHSiInterface.T2206.Input>(chsInput2206);
                    t2206.data.mdtrt_id = rt2201.output.data.mdtrt_id;
                    infno = "2206";
                    inputRoot = new CHSHelper.InputRoot();
                    inputRoot.HOS_ID = HOS_ID;
                    inputRoot.infno = infno;
                    inputRoot.insuplc_admdvs = rt1101.output.insuinfo[0].insuplc_admdvs;
                    inputRoot.InData = JSONSerializer.Serialize(t2206); //jin2206.ToString();
                    outputRoot = new CHSHelper.OutputRoot();
                    flag = CHSHelper.Post(inputRoot, ref outputRoot, ref msg);
                    if (!flag)
                    {
                        DataTable dtrev = RtnResultDatatable("99", msg);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    if (outputRoot.Code != "0")
                    {
                        DataTable dtrev = RtnResultDatatable("99", outputRoot.Msg);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    chsInput2206 = outputRoot.chsInput;

                    string chsOutput2206 = outputRoot.chsOutput;

                    #endregion 2206

                    #region 2207

                    try
                    {
                        QHSiInterface.RT2206.Root rt2206 = JSONSerializer.Deserialize<QHSiInterface.RT2206.Root>(chsOutput2206);

                        JObject in2207 = JObject.Parse(chsInput2207);

                        in2207["data"]["mdtrt_id"] = rt2201.output.data.mdtrt_id;

                        expContent = new Dictionary<string, string>();
                        expContent.Add("SJH", sjh);
                        expContent.Add("YBDJH", YBDJH);
                        expContent.Add("MZNO", HOS_SN);
                        expContent.Add("insuplc_admdvs", rt1101.output.insuinfo[0].insuplc_admdvs);
                        expContent.Add("chsInput2201", dic["CHSINPUT2201"]);
                        expContent.Add("chsOutput1101", dic["CHSOUTPUT1101"]);
                        expContent.Add("chsInput2203", chsInput2203);
                        expContent.Add("chsInput2204", chsInput2204);
                        expContent.Add("chsOutput2204", chsOutput2204);
                        expContent.Add("chsInput2206", chsInput2206);
                        expContent.Add("chsOutput2206", chsOutput2206);
                        expContent.Add("chsInput2207", JsonConvert.SerializeObject(in2207));

                        DataTable dtrev1 = new DataTable();
                        dtrev1.Columns.Add("CHSOUTPUT2206", typeof(string));
                        dtrev1.Columns.Add("CHSINPUT2207", typeof(string));
                        dtrev1.Columns.Add("EXPCONTENT", typeof(string));
                        dtrev1.Columns.Add("CLBZ", typeof(string));
                        dtrev1.Columns.Add("CLJG", typeof(string));
                        DataRow dr = dtrev1.NewRow();
                        dr["CHSOUTPUT2206"] = JSONSerializer.Serialize(rt2206.output);
                        dr["CHSINPUT2207"] = JsonConvert.SerializeObject(in2207);
                        dr["EXPCONTENT"] = JSONSerializer.Serialize<Dictionary<string, string>>(expContent);
                        dr["CLBZ"] = "0";
                        dr["CLJG"] = "";
                        dtrev1.Rows.Add(dr);
                        dtrev1.TableName = "BODY";
                        ds.Tables.Add(dtrev1.Copy());
                    }
                    catch (Exception ex)
                    {
                        DataTable dtrev = RtnResultDatatable("99", ex.ToString());
                        dtrev.TableName = "BODY";
                        ds.Tables.Add(dtrev.Copy());
                    }
                    return ds;

                    #endregion 2207

                    break;

                case "GETCHSOUTPTRY":
                    chsOutput1101 = dic.ContainsKey("chsOutput1101") ? dic["chsOutput1101"] : "";

                    chsOutput1101 = dic.ContainsKey("CHSOUTPUT1101") ? dic["CHSOUTPUT1101"] : "";

                    QHSiInterface.RT1101.Root rt1101SF = JSONSerializer.Deserialize<QHSiInterface.RT1101.Root>(chsOutput1101);
                    string CHSOUTPUT5360 = "";
                    if (rt1101SF.output.data.Count > 0)
                    {
                        QHSiInterface.RT5360.Root rt5360 = new QHSiInterface.RT5360.Root();
                        rt5360.output = new QHSiInterface.RT5360.Output();
                        rt5360.output.data = new List<QHSiInterface.RT5360.insuinfo>();
                        rt5360.infcode = "0";
                        rt5360.err_msg = "";
                        rt5360.inf_refmsgid = "";
                        rt5360.respond_time = "";
                        rt5360.refmsg_time = "";
                        for (int i = 0; i < rt1101SF.output.data.Count; i++)
                        {
                            QHSiInterface.RT5360.insuinfo insuinfo = new QHSiInterface.RT5360.insuinfo()
                            {
                                psn_no = rt1101SF.output.data[i].psn_no,
                                med_type = rt1101SF.output.data[i].med_type,
                                insutype = rt1101SF.output.data[i].insutype,
                                begndate = rt1101SF.output.data[i].begndate,
                                dise_codg = rt1101SF.output.data[i].dise_codg,
                                dise_name = rt1101SF.output.data[i].dise_name,
                                enddate = "",
                                exp_content = "",
                                hilist_code = rt1101SF.output.data[i].hilist_code,
                                hilist_name = rt1101SF.output.data[i].hilist_name
                            };
                            rt5360.output.data.Add(insuinfo);
                        }
                        CHSOUTPUT5360 = JSONSerializer.Serialize(rt5360);
                    }
                    //MDTRT_CERT_TYPE = "04";
                    //MDTRT_CERT_NO = dic["MDTRT_CERT_NO"].ToString();

                    #region his预算

                    QHSiInterface.T1101.Data t1101fee = new QHSiInterface.T1101.Data()
                    {
                        mdtrt_cert_type = MDTRT_CERT_TYPE,
                        mdtrt_cert_no = MDTRT_CERT_NO,
                        psn_name = dic["PAT_NAME"],
                        card_sn = "",
                        psn_cert_type = "",
                        begntime = "",
                        certno = "",
                    };

                    T1101.Root in1101fee = new T1101.Root();
                    in1101fee.data = t1101fee;

                    chsOutput1101 = dic["CHSOUTPUT1101"];
                    string insuplc_admdvs = rt1101SF.output.insuinfo[0].insuplc_admdvs;//参保地行政区划
                    JObject chsInput1101 = JObject.Parse(JsonConvert.SerializeObject(in1101fee));
                    out1101 = JObject.Parse(chsOutput1101);
                    jybrc.Add("in1101", chsInput1101);
                    jybrc.Add("out1101", out1101["output"]);

                    jzzj.Add("zzj", jybrc);

                    medicareParam = Newtonsoft.Json.JsonConvert.SerializeObject(jzzj);
                    medicareParam = Base64Encode(medicareParam);
                    Hos185_His.Models.MZ.OUTFEEPAYPRESAVE presave = new Hos185_His.Models.MZ.OUTFEEPAYPRESAVE()
                    {
                        hospitalcode = "",//医院代码
                        lifeEquityCardNo = "",//权益卡卡号
                        lifeEquityCardType = "",//权益卡类型
                        medicareParam = medicareParam,//医保参数
                        pactCode = "01",//合同单位
                        recipeNos = dic["PRE_NO"].Replace('#', ','),
                        regid = HOS_SN.Split('_')[0]//挂号单号
                    };

                    jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(presave);

                    Output<Hos185_His.Models.MZ.OUTFEEPAYPRESAVEDATA> outputpre
           = new MIDServiceHelper().CallServiceAPI<Hos185_His.Models.MZ.OUTFEEPAYPRESAVEDATA>("/hischargesinfo/outpatientfee/preSaveFeeXTBY", jsonstr);

                    if (outputpre.code != 0)
                    {
                        DataTable dtrev = RtnResultDatatable("99", outputpre.message);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    sjh = outputpre.data.receiptNumber;

                    jeall = decimal.Parse(outputpre.data.totCost);//订单总金额
                    jybcc = JObject.Parse(Base64Decode(outputpre.data.insuranceparameters));

                    chsInput2201 = jybcc["zzj"]["in2201"].ToString();
                    chsInput2203 = jybcc["zzj"]["in2203"].ToString();
                    chsInput2204 = jybcc["zzj"]["in2204"].ToString();
                    chsInput2206 = jybcc["zzj"]["in2206"].ToString();
                    chsInput2207 = jybcc["zzj"]["in2207"].ToString();
                    totalfee = decimal.Parse(jybcc["zzj"]["in2206"]["data"]["medfee_sumamt"].ToString());//订单 医保范围内总金额

                    #endregion his预算

                    break;

                case "CHSOUTPTRY":

                    #region CHSOUTPTRY

                    string msgTRY = "";
                    QHSiInterface.RTNJ1101.Root rt1101TRY = JSONSerializer.Deserialize<QHSiInterface.RTNJ1101.Root>(dic["CHSOUTPUT1101"]);
                    QHSiInterface.RTNJ2201.Root rt2201TRY = JSONSerializer.Deserialize<QHSiInterface.RTNJ2201.Root>(dic["CHSOUTPUT2201"]);
                    Dictionary<string, string> expContentTRY = JSONSerializer.Deserialize<Dictionary<string, string>>(dic["EXPCONTENT"]);
                    string chsInput2203TRY = expContentTRY["chsInput2203"];
                    string chsInput2204TRY = expContentTRY["chsInput2204"];
                    string chsInput2206TRY = expContentTRY["chsInput2206"];
                    string chsInput2207TRY = expContentTRY["chsInput2207"];
                    string YBDJHTRY = expContentTRY["YBDJH"];
                    sjh = expContentTRY["SJH"];

                    #region 2203A

                    QHSiInterface.T2203.Input t2203TRY = JSONSerializer.Deserialize<QHSiInterface.T2203.Input>(chsInput2203TRY);
                    t2203TRY.mdtrtinfo.mdtrt_id = rt2201TRY.output.data.mdtrt_id;
                    string infnoTRY = "2203A";
                    CHSHelper.InputRoot inputRootTRY = new CHSHelper.InputRoot();
                    inputRootTRY.HOS_ID = HOS_ID;
                    inputRootTRY.infno = infnoTRY;
                    inputRootTRY.insuplc_admdvs = rt1101TRY.output.insuinfo[0].insuplc_admdvs;
                    inputRootTRY.InData = JSONSerializer.Serialize(t2203TRY); //jin2203.ToString();
                    CHSHelper.OutputRoot outputRootTRY = new CHSHelper.OutputRoot();
                    bool flagTRY = CHSHelper.Post(inputRootTRY, ref outputRootTRY, ref msgTRY);
                    if (!flagTRY)
                    {
                        DataTable dtrev = RtnResultDatatable("99", msgTRY);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    if (outputRootTRY.Code != "0")
                    {
                        DataTable dtrev = RtnResultDatatable("99", outputRootTRY.Msg);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    chsInput2203 = outputRootTRY.chsInput;
                    string chsOutput2203TRY = outputRootTRY.chsOutput;

                    #endregion 2203A

                    T2205.Root input2205 = new T2205.Root();
                    T2205.Data data2205 = new T2205.Data()
                    {
                        psn_no = rt2201TRY.output.data.psn_no,
                        mdtrt_id = rt2201TRY.output.data.mdtrt_id,
                        chrg_bchno = "0000"
                    };
                    input2205.data = data2205;

                    infnoTRY = "2205";
                    inputRootTRY = new CHSHelper.InputRoot();
                    inputRootTRY.HOS_ID = HOS_ID;
                    inputRootTRY.infno = infnoTRY;
                    inputRootTRY.insuplc_admdvs = rt1101TRY.output.insuinfo[0].insuplc_admdvs;
                    inputRootTRY.InData = JSONSerializer.Serialize(input2205);
                    outputRootTRY = new CHSHelper.OutputRoot();
                    flag = CHSHelper.Post(inputRootTRY, ref outputRootTRY, ref msgTRY);

                    #region 2204

                    QHSiInterface.T2204.Input t2204TRY = JSONSerializer.Deserialize<QHSiInterface.T2204.Input>(chsInput2204TRY);
                    for (int i = 0; i < t2204TRY.feedetail.Count; i++)
                    {
                        t2204TRY.feedetail[i].mdtrt_id = rt2201TRY.output.data.mdtrt_id;
                    }
                    infnoTRY = "2204";
                    inputRootTRY = new CHSHelper.InputRoot();
                    inputRootTRY.HOS_ID = HOS_ID;
                    inputRootTRY.infno = infnoTRY;
                    inputRootTRY.insuplc_admdvs = rt1101TRY.output.insuinfo[0].insuplc_admdvs;
                    inputRootTRY.InData = JSONSerializer.Serialize(t2204TRY); //jin2204.ToString();
                    outputRootTRY = new CHSHelper.OutputRoot();
                    flag = CHSHelper.Post(inputRootTRY, ref outputRootTRY, ref msgTRY);
                    if (!flag)
                    {
                        DataTable dtrev = RtnResultDatatable("99", msgTRY);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    if (outputRootTRY.Code != "0")
                    {
                        DataTable dtrev = RtnResultDatatable("99", outputRootTRY.Msg);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    chsInput2204TRY = outputRootTRY.chsInput;
                    string chsOutput2204TRY = outputRootTRY.chsOutput;

                    #endregion 2204

                    #region 2206

                    QHSiInterface.T2206.Input t2206TRY = JSONSerializer.Deserialize<QHSiInterface.T2206.Input>(chsInput2206TRY);
                    t2206TRY.data.mdtrt_id = rt2201TRY.output.data.mdtrt_id;
                    infnoTRY = "2206";
                    inputRootTRY = new CHSHelper.InputRoot();
                    inputRootTRY.HOS_ID = HOS_ID;
                    inputRootTRY.infno = infnoTRY;
                    inputRootTRY.insuplc_admdvs = rt1101TRY.output.insuinfo[0].insuplc_admdvs;
                    inputRootTRY.InData = JSONSerializer.Serialize(t2206TRY); //jin2206.ToString();
                    outputRootTRY = new CHSHelper.OutputRoot();
                    flag = CHSHelper.Post(inputRootTRY, ref outputRootTRY, ref msgTRY);
                    if (!flag)
                    {
                        DataTable dtrev = RtnResultDatatable("99", msgTRY);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    if (outputRootTRY.Code != "0")
                    {
                        DataTable dtrev = RtnResultDatatable("99", outputRootTRY.Msg);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    chsInput2206TRY = outputRootTRY.chsInput;
                    string chsOutput2206TRY = outputRootTRY.chsOutput;

                    #endregion 2206

                    #region 2207

                    try
                    {
                        QHSiInterface.RT2206.Root rt2206 = JSONSerializer.Deserialize<QHSiInterface.RT2206.Root>(chsOutput2206TRY);

                        JObject in2207 = JObject.Parse(chsInput2207TRY);

                        in2207["data"]["mdtrt_id"] = rt2201TRY.output.data.mdtrt_id;

                        expContent = new Dictionary<string, string>
                        {
                            { "SJH", sjh },

                            { "YBDJH", YBDJH },
                            { "MZNO", "" },
                            { "insuplc_admdvs", rt1101TRY.output.insuinfo[0].insuplc_admdvs },
                            { "chsInput2201", dic["CHSINPUT2201"] },
                            { "chsOutput1101", dic["CHSOUTPUT1101"] },
                            { "chsInput2203", chsInput2203 },
                            { "chsInput2204", chsInput2204TRY },
                            { "chsOutput2204", chsOutput2204TRY },
                            { "chsInput2206", chsInput2206TRY },
                            { "chsOutput2206", chsOutput2206TRY },
                            { "chsInput2207", JsonConvert.SerializeObject(in2207) }
                        };
                        DataTable dtrev1 = new DataTable();
                        dtrev1.Columns.Add("CHSOUTPUT2206", typeof(string));
                        dtrev1.Columns.Add("CHSINPUT2207", typeof(string));
                        dtrev1.Columns.Add("EXPCONTENT", typeof(string));
                        dtrev1.Columns.Add("CLBZ", typeof(string));
                        dtrev1.Columns.Add("CLJG", typeof(string));
                        DataRow dr = dtrev1.NewRow();
                        dr["CHSOUTPUT2206"] = JSONSerializer.Serialize(rt2206.output);
                        dr["CHSINPUT2207"] = JsonConvert.SerializeObject(in2207);
                        dr["EXPCONTENT"] = JSONSerializer.Serialize<Dictionary<string, string>>(expContent);
                        dr["CLBZ"] = "0";
                        dr["CLJG"] = "";
                        dtrev1.Rows.Add(dr);
                        dtrev1.TableName = "BODY";
                        ds.Tables.Add(dtrev1.Copy());
                    }
                    catch (Exception ex)
                    {
                        DataTable dtrev = RtnResultDatatable("99", ex.ToString());
                        dtrev.TableName = "BODY";
                        ds.Tables.Add(dtrev.Copy());
                    }
                    return ds;

                    #endregion 2207

                    #endregion CHSOUTPTRY

                    break;

                case "CHSREGREFUNDSAVE":

                    #region 退款

                    string out2208 = dic["CHSOUTPUT2208"];

                    JObject o2208 = JObject.Parse(out2208);
                    string setlid = o2208["output"]["setlinfo"]["setl_id"].ToString();
                    string medfee_sumamt = o2208["output"]["setlinfo"]["medfee_sumamt"].ToString();
                    Soft.DBUtility.RedisHelperSentinels redis = new RedisHelperSentinels();

                    string psn_name = o2208["output"]["setlinfo"]["psn_name"].ToString();
                    //退费通知
                    string redistfinfokey = "TH" + dic["HOS_SN"] + dic["HOS_ID"];
                    JObject jobj = JObject.Parse(redis.Get(redistfinfokey, 7));
                    //通知

                    string refund_order_id = jobj.ContainsKey("refund_order_id") ? jobj["refund_order_id"].ToString() : "";

                    string refundReceiptNumber = jobj["refundReceiptNumber"].ToString();

                    refundMsg refundMsg = new refundMsg()
                    {
                        oldReceiptNumber = "",//原收据号    N  原HIS内收据号
                        patientName = psn_name,//患者姓名    Y  患者姓名，用于验证防止误操作
                        receiptNumber = refundReceiptNumber,//退费收据号   Y  HIS标记唯一一次退费的单据号。根据对应退费接口反馈的tsjh
                        balanceNo = setlid,
                        refundAmount = Math.Abs(decimal.Parse(medfee_sumamt)).ToString(),//退费金额    Y  退费金额，用于验证
                        refundNo = refund_order_id,//退费流水号   Y  支付金融机构交易流水号，如源启支付，支付宝、微信、银行等机构的原始退费流水号，用于对账
                        refundPayType = "51"//退支付方式   Y
                    };

                    jsonstr = JsonConvert.SerializeObject(refundMsg);
                    var outputmsg = new MIDServiceHelper().CallServiceAPI<object>("/hisbooking/register/refundMsg", jsonstr);

                    if (outputmsg.code != 0)
                    {
                        RtnFailXml("DOQUIT", "0", "8", outputmsg.message);
                    }

                    #endregion 退款

                    break;

                default:
                    foreach (var item in dic)
                    {
                        XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", item.Key.ToString(), FormatHelper.GetStr(string.IsNullOrEmpty(item.Value) ? "" : item.Value));
                    }
                    break;
            }

            try
            {
                DataSet dsbody = new DataSet();
                DataTable dtrev = RtnResultDatatable("0", "");
                dtrev.Columns.Add("EXPCONTENT");
                dtrev.Columns.Add("RE_DJ");

                dtrev.Columns.Add("CHSINPUT2201");
                dtrev.Columns.Add("CHSINPUT2203");
                dtrev.Columns.Add("CHSINPUT2204");
                dtrev.Columns.Add("CHSINPUT2206");
                dtrev.Columns.Add("AMT_OUTHP");

                dsbody.Tables.Add(dtrev.Copy());

                switch (TYPE)
                {
                    case "GETCHSREGTRY":
                    case "GETCHSOUTPTRY":
                        Dictionary<string, string> dicEXP = new Dictionary<string, string>();
                        dicEXP.Add("chsInput2201", chsInput2201);
                        dicEXP.Add("chsInput2203", chsInput2203);
                        dicEXP.Add("chsInput2204", chsInput2204);
                        dicEXP.Add("chsInput2206", chsInput2206);
                        dicEXP.Add("chsInput2207", chsInput2207);
                        dicEXP.Add("YBDJH", "");
                        dicEXP.Add("SJH", sjh);
                        dsbody.Tables[0].Rows[0]["CHSINPUT2201"] = chsInput2201;
                        dsbody.Tables[0].Rows[0]["CHSINPUT2203"] = chsInput2203;
                        dsbody.Tables[0].Rows[0]["CHSINPUT2204"] = chsInput2204;
                        dsbody.Tables[0].Rows[0]["CHSINPUT2206"] = chsInput2206;
                        dsbody.Tables[0].Rows[0]["AMT_OUTHP"] = ((jeall - totalfee) * 100).ToString("0");

                        dsbody.Tables[0].Rows[0]["EXPCONTENT"] = JSONSerializer.Serialize<Dictionary<string, string>>(dicEXP);
                        dsbody.Tables[0].Rows[0]["RE_DJ"] = "1";
                        break;
                }
                return dsbody;
            }
            catch (Exception ex)
            {
                //DataSet ds = new DataSet();
                DataTable dtrev = RtnResultDatatable("99", ex.Message);
                ds.Tables.Add(dtrev.Copy());
                return ds;
            }
        }

        public DataSet CHS_YBY(string TYPE, Dictionary<string, string> dic)
        {
            XmlDocument doc = QHXmlMode.GetBaseXml(TYPE, "0");
            DataSet ds = new DataSet();

            string HOS_ID = dic["HOS_ID"].ToString();
            string HOS_SN = "";
            string YNCARDNO = "";
            string chsInput2201 = "";
            string chsInput2203 = "";
            string chsInput2204 = "";
            string chsInput2206 = "";
            decimal jeall = dic.Keys.Contains("APPT_PAY")
    ? decimal.Parse(dic["APPT_PAY"])
    : dic.Keys.Contains("JEALL")
        ? decimal.Parse(dic["JEALL"])
        : 0;

            decimal totalfee = 0;

            JObject jzzj = new JObject();
            JObject jybrc = new JObject();
            string MDTRT_CERT_TYPE;
            string MDTRT_CERT_NO;
            string chsOutput1101;
            string chsInput2207;

            string sjh;
            switch (TYPE)
            {
                case "GETCHSREGTRY":

                    if (!dic.ContainsKey("YNCARDNO"))
                    {
                        GETPATBARCODE(HOS_ID, dic["PAT_ID"].ToString(), ref YNCARDNO);
                    }
                    else
                    {
                        YNCARDNO = dic["YNCARDNO"];
                    }
                    HOS_SN = dic["HOS_SN"].ToString();
                    chsOutput1101 = dic["CHSOUTPUT1101"].ToString();
                    MDTRT_CERT_TYPE = "04";
                    MDTRT_CERT_NO = dic["MDTRT_CERT_NO"].ToString();

                    QHSiInterface.T1101.Data t1101 = new QHSiInterface.T1101.Data()
                    {
                        mdtrt_cert_type = MDTRT_CERT_TYPE,
                        mdtrt_cert_no = MDTRT_CERT_NO,
                        psn_name = dic["PAT_NAME"],
                        card_sn = "",
                        psn_cert_type = "",
                        begntime = "",
                        certno = "",
                    };

                    T1101.Root in1101 = new T1101.Root();
                    in1101.data = t1101;

                    #region 预算

                    string[] REGISTER_TYPE = dic["REGISTER_TYPE"].Split('|');

                    string regLevelCode = REGISTER_TYPE[0];
                    string noonCode = REGISTER_TYPE[1];
                    string schemaId = REGISTER_TYPE[2];

                    JObject out1101 = JObject.Parse(chsOutput1101);

                    jybrc.Add("in1101", JObject.Parse(JsonConvert.SerializeObject(in1101)));

                    jybrc.Add("out1101", out1101["output"]);

                    jzzj.Add("zzj", jybrc);

                    string medicareParam = Newtonsoft.Json.JsonConvert.SerializeObject(jzzj);
                    medicareParam = Base64Encode(medicareParam);

                    REGISTERFEE registerfee = new REGISTERFEE()
                    {
                        medicareParam = medicareParam,//        医保预留
                        pactCode = "17",//   结算code      FALSE
                        patientID = YNCARDNO,// 患者ID        FALSE
                        scheduleId = schemaId,//         FALSE
                        vipCardNo = "",//        FALSE
                        vipCardType = "",//            FALSE
                        preid = dic["HOS_SN"]
                    };

                    string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(registerfee);

                    Output<REGISTERFEEDATA> outputregisterfee
              = new MIDServiceHelper().CallServiceAPI<REGISTERFEEDATA>("/hisbooking/register/calcRegisterFee", jsonstr);

                    if (outputregisterfee.code != 0)
                    {
                        DataTable dtrev = RtnResultDatatable("99", outputregisterfee.message);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    sjh = outputregisterfee.data.receiptNumber + "|" + outputregisterfee.data.ghxh;//收据号|挂号序号（王丹那边就不用改了）modi by wyq 2023 01 06
                    JObject jybcc = JObject.Parse(Base64Decode(outputregisterfee.data.medicareParam));

                    totalfee = decimal.Parse(jybcc["zzj"]["in2206"]["data"]["medfee_sumamt"].ToString());
                    chsInput2201 = jybcc["zzj"]["in2201"].ToString();
                    chsInput2203 = jybcc["zzj"]["in2203"].ToString();
                    chsInput2204 = jybcc["zzj"]["in2204"].ToString();
                    chsInput2206 = jybcc["zzj"]["in2206"].ToString();
                    chsInput2207 = jybcc["zzj"]["in2207"].ToString();

                    JObject in2206 = JObject.Parse(chsInput2206);

                    #endregion 预算

                    break;

                case "GETCHSOUTPTRY":

                    chsOutput1101 = dic.ContainsKey("chsOutput1101") ? dic["chsOutput1101"] : "";

                    MDTRT_CERT_TYPE = "04";
                    MDTRT_CERT_NO = dic["MDTRT_CERT_NO"].ToString();

                    #region his预算

                    QHSiInterface.T1101.Data t1101fee = new QHSiInterface.T1101.Data()
                    {
                        mdtrt_cert_type = MDTRT_CERT_TYPE,
                        mdtrt_cert_no = MDTRT_CERT_NO,
                        psn_name = "",
                        card_sn = "",
                        psn_cert_type = "",
                        begntime = "",
                        certno = "",
                    };

                    T1101.Root in1101fee = new T1101.Root();
                    in1101fee.data = t1101fee;

                    JObject chsInput1101 = JObject.Parse(JsonConvert.SerializeObject(in1101fee));
                    out1101 = JObject.Parse(chsOutput1101);
                    jybrc.Add("in1101", chsInput1101);
                    jybrc.Add("out1101", JObject.Parse(chsOutput1101));

                    jzzj.Add("zzj", jybrc);

                    medicareParam = Newtonsoft.Json.JsonConvert.SerializeObject(jzzj);
                    medicareParam = Base64Encode(medicareParam);

                    Hos185_His.Models.MZ.OUTFEEPAYPRESAVE presave = new Hos185_His.Models.MZ.OUTFEEPAYPRESAVE()
                    {
                        hospitalcode = "",//医院代码
                        lifeEquityCardNo = "",//权益卡卡号
                        lifeEquityCardType = "",//权益卡类型
                        medicareParam = medicareParam,//医保参数
                        pactCode = "01",//合同单位
                        recipeNos = dic["PRE_NO"].Replace('#', ','),
                        regid = HOS_SN//挂号单号
                    };

                    jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(presave);

                    Output<Hos185_His.Models.MZ.OUTFEEPAYPRESAVEDATA> outputpre
           = new MIDServiceHelper().CallServiceAPI<Hos185_His.Models.MZ.OUTFEEPAYPRESAVEDATA>("/hischargesinfo/outpatientfee/preSaveFeeXTBY", jsonstr);

                    SaveLog(DateTime.Now, "CHS_YBY(preSaveFeeXTBY)", DateTime.Now, jsonstr);//保存his接口日志

                    if (outputpre.code != 0)
                    {
                        DataTable dtrev = RtnResultDatatable("99", outputpre.message);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    sjh = outputpre.data.receiptNumber;

                    //jeall = decimal.Parse(outputpre.data.totCost);//订单总金额
                    jybcc = JObject.Parse(Base64Decode(outputpre.data.insuranceparameters));

                    chsInput2201 = jybcc["zzj"]["in2201"].ToString();
                    chsInput2203 = jybcc["zzj"]["in2203"].ToString();
                    chsInput2204 = jybcc["zzj"]["in2204"].ToString();
                    chsInput2206 = jybcc["zzj"]["in2206"].ToString();
                    chsInput2207 = jybcc["zzj"]["in2207"].ToString();
                    //totalfee = decimal.Parse(jybcc["zzj"]["in2206"]["data"]["medfee_sumamt"].ToString());//订单 医保范围内总金额

                    #endregion his预算

                    break;

                default:
                    chsInput2207 = "";
                    sjh = "";
                    break;
            }

            try
            {
                SaveLog(DateTime.Now, "CHS_YBY", DateTime.Now, "开始处理出参");//保存his接口日志

                DataSet dsbody = new DataSet();
                DataTable dtrev = RtnResultDatatable("0", "");
                dtrev.Columns.Add("EXPCONTENT");
                dtrev.Columns.Add("RE_DJ");

                dtrev.Columns.Add("CHSINPUT2201");
                dtrev.Columns.Add("CHSINPUT2203");
                dtrev.Columns.Add("CHSINPUT2204");
                dtrev.Columns.Add("CHSINPUT2206");
                dtrev.Columns.Add("AMT_OUTHP");

                dsbody.Tables.Add(dtrev.Copy());

                switch (TYPE)
                {
                    case "GETCHSREGTRY":
                    case "GETCHSOUTPTRY":
                        Dictionary<string, string> dicEXP = new Dictionary<string, string>
                        {
                            { "chsInput2201", chsInput2201 },
                            { "chsInput2203", chsInput2203 },
                            { "chsInput2204", chsInput2204 },
                            { "chsInput2206", chsInput2206 },
                            { "chsInput2207", chsInput2207 },
                            { "YBDJH", "" },
                            { "SJH", sjh }
                        };
                        dsbody.Tables[0].Rows[0]["CHSINPUT2201"] = chsInput2201;
                        dsbody.Tables[0].Rows[0]["CHSINPUT2203"] = chsInput2203;
                        dsbody.Tables[0].Rows[0]["CHSINPUT2204"] = chsInput2204;
                        dsbody.Tables[0].Rows[0]["CHSINPUT2206"] = chsInput2206;
                        //dsbody.Tables[0].Rows[0]["AMT_OUTHP"] = ((jeall - totalfee) * 100).ToString("0");

                        dsbody.Tables[0].Rows[0]["EXPCONTENT"] = JSONSerializer.Serialize<Dictionary<string, string>>(dicEXP);
                        dsbody.Tables[0].Rows[0]["RE_DJ"] = "1";

                        SaveLog(DateTime.Now, "CHS_YBY", DateTime.Now, "出参处理完成");//保存his接口日志

                        break;
                }
                return dsbody;
            }
            catch (Exception ex)
            {
                SaveLog(DateTime.Now, "CHS_YBY", DateTime.Now, "出参处理完成异常" + ex.Message);//保存his接口日志

                //DataSet ds = new DataSet();
                DataTable dtrev = RtnResultDatatable("99", ex.Message);
                ds.Tables.Add(dtrev.Copy());
                return ds;
            }
        }

        public DataSet CHS_SYB(string TYPE, Dictionary<string, string> dic)
        {
            XmlDocument doc = QHXmlMode.GetBaseXml(TYPE, "0");
            DataSet ds = new DataSet();

            string HOS_ID = dic["HOS_ID"].ToString();
            string HOS_SN = "";
            string YNCARDNO = "";
            string chsInput2201 = "";
            string chsInput2203 = "";
            string chsInput2204 = "";
            string chsInput2206 = "";
            decimal jeall = dic.Keys.Contains("APPT_PAY")
    ? decimal.Parse(dic["APPT_PAY"])
    : dic.Keys.Contains("JEALL")
        ? decimal.Parse(dic["JEALL"])
        : 0;

            decimal totalfee = 0;

            JObject jzzj = new JObject();
            JObject jybrc = new JObject();
            string MDTRT_CERT_TYPE;
            string MDTRT_CERT_NO;
            string chsOutput1101;
            string chsInput2207;
            string redisbuskey;
            Soft.DBUtility.RedisHelperSentinels redis = new Soft.DBUtility.RedisHelperSentinels();

            string sjh;
            switch (TYPE)
            {
                case "GETCHSREGTRY":

                    if (!dic.ContainsKey("YNCARDNO"))
                    {
                        GETPATBARCODE(HOS_ID, dic["PAT_ID"].ToString(), ref YNCARDNO);
                    }
                    else
                    {
                        YNCARDNO = dic["YNCARDNO"];
                    }
                    HOS_SN = dic["HOS_SN"].ToString();
                    chsOutput1101 = dic.ContainsKey("CHSOUTPUT1101") ? dic["CHSOUTPUT1101"].ToString() :
             dic.ContainsKey("chsOutput1101") ? dic["chsOutput1101"].ToString() : "";
                    redisbuskey = "GH" + HOS_SN + HOS_ID;
                    string apptinfo = redis.Get(redisbuskey, 7);

                    JObject jappt = JObject.Parse(apptinfo);
                    if (string.IsNullOrEmpty(chsOutput1101))
                    {
                        return DS_RtnDealInfo("2", "chsOutput1101为空");
                    }
                    MDTRT_CERT_TYPE = "04";
                    MDTRT_CERT_NO = dic["MDTRT_CERT_NO"].ToString();

                    QHSiInterface.T1101.Data t1101 = new QHSiInterface.T1101.Data()
                    {
                        mdtrt_cert_type = MDTRT_CERT_TYPE,
                        mdtrt_cert_no = MDTRT_CERT_NO,
                        psn_name = dic["PAT_NAME"],
                        card_sn = "",
                        psn_cert_type = "",
                        begntime = "",
                        certno = "",
                    };

                    T1101.Root in1101 = new T1101.Root();
                    in1101.data = t1101;

                    #region 预算

                    string[] REGISTER_TYPE = jappt["REGISTER_TYPE"].ToString().Split('|');

                    string regLevelCode = REGISTER_TYPE[0];
                    string noonCode = REGISTER_TYPE[1];
                    string schemaId = REGISTER_TYPE[2];

                    JObject out1101 = JObject.Parse(chsOutput1101);

                    jybrc.Add("in1101", JObject.Parse(JsonConvert.SerializeObject(in1101)));

                    jybrc.Add("out1101", out1101["output"]);

                    jzzj.Add("zzj", jybrc);

                    string medicareParam = Newtonsoft.Json.JsonConvert.SerializeObject(jzzj);
                    medicareParam = Base64Encode(medicareParam);

                    REGISTERFEE registerfee = new REGISTERFEE()
                    {
                        medicareParam = medicareParam,//        医保预留
                        pactCode = "17",//   结算code      FALSE
                        patientID = YNCARDNO,// 患者ID        FALSE
                        scheduleId = schemaId,//         FALSE
                        vipCardNo = "",//        FALSE
                        vipCardType = "",//            FALSE
                        preid = dic["HOS_SN"]
                    };

                    string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(registerfee);

                    Output<REGISTERFEEDATA> outputregisterfee
              = new MIDServiceHelper().CallServiceAPI<REGISTERFEEDATA>("/hisbooking/register/calcRegisterFee", jsonstr, "SYBAPP", "SYBAPP");

                    if (outputregisterfee.code != 0)
                    {
                        DataTable dtrev = RtnResultDatatable("99", outputregisterfee.message);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    totalfee = outputregisterfee.data.totalFee; //实际总金额

                    sjh = outputregisterfee.data.receiptNumber + "|" + outputregisterfee.data.ghxh;//收据号|挂号序号（王丹那边就不用改了）modi by wyq 2023 01 06
                    JObject jybcc = JObject.Parse(Base64Decode(outputregisterfee.data.medicareParam));

                    //jybcc["zzj"]["in2206"]["data"]["medfee_sumamt"] = totalfee;
                    //jybcc["zzj"]["in2204"]["feedetail"][0]["pric"] = totalfee;
                    //jybcc["zzj"]["in2204"]["feedetail"][0]["det_item_fee_sumamt"] = totalfee;

                    chsInput2201 = jybcc["zzj"]["in2201"].ToString();
                    chsInput2203 = jybcc["zzj"]["in2203"].ToString();
                    chsInput2204 = jybcc["zzj"]["in2204"].ToString();
                    chsInput2206 = jybcc["zzj"]["in2206"].ToString();
                    chsInput2207 = jybcc["zzj"]["in2207"].ToString();

                    jappt.Add("chsInput2203", chsInput2203);
                    jappt.Add("sjh", sjh);

                    redis.Set(redisbuskey, jappt, DateTime.Now.AddHours(2), 7);

                    #endregion 预算

                    break;

                case "GETCHSOUTPTRY":

                    chsOutput1101 = dic.ContainsKey("chsOutput1101") ? dic["chsOutput1101"] : "";

                    MDTRT_CERT_TYPE = "04";
                    MDTRT_CERT_NO = dic["MDTRT_CERT_NO"].ToString();

                    #region his预算

                    QHSiInterface.T1101.Data t1101fee = new QHSiInterface.T1101.Data()
                    {
                        mdtrt_cert_type = MDTRT_CERT_TYPE,
                        mdtrt_cert_no = MDTRT_CERT_NO,
                        psn_name = "",
                        card_sn = "",
                        psn_cert_type = "",
                        begntime = "",
                        certno = "",
                    };

                    T1101.Root in1101fee = new T1101.Root();
                    in1101fee.data = t1101fee;

                    JObject chsInput1101 = JObject.Parse(JsonConvert.SerializeObject(in1101fee));
                    out1101 = JObject.Parse(chsOutput1101);
                    jybrc.Add("in1101", chsInput1101);
                    jybrc.Add("out1101", JObject.Parse(chsOutput1101));

                    jzzj.Add("zzj", jybrc);

                    medicareParam = Newtonsoft.Json.JsonConvert.SerializeObject(jzzj);
                    medicareParam = Base64Encode(medicareParam);

                    Hos185_His.Models.MZ.OUTFEEPAYPRESAVE presave = new Hos185_His.Models.MZ.OUTFEEPAYPRESAVE()
                    {
                        hospitalcode = "",//医院代码
                        lifeEquityCardNo = "",//权益卡卡号
                        lifeEquityCardType = "",//权益卡类型
                        medicareParam = medicareParam,//医保参数
                        pactCode = "01",//合同单位
                        recipeNos = dic["PRE_NO"].Replace('#', ','),
                        regid = HOS_SN//挂号单号
                    };

                    jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(presave);

                    Output<Hos185_His.Models.MZ.OUTFEEPAYPRESAVEDATA> outputpre
           = new MIDServiceHelper().CallServiceAPI<Hos185_His.Models.MZ.OUTFEEPAYPRESAVEDATA>("/hischargesinfo/outpatientfee/preSaveFeeXTBY", jsonstr);

                    SaveLog(DateTime.Now, "CHS_YBY(preSaveFeeXTBY)", DateTime.Now, jsonstr);//保存his接口日志

                    if (outputpre.code != 0)
                    {
                        DataTable dtrev = RtnResultDatatable("99", outputpre.message);
                        ds.Tables.Add(dtrev.Copy());
                        return ds;
                    }
                    sjh = outputpre.data.receiptNumber;

                    //jeall = decimal.Parse(outputpre.data.totCost);//订单总金额
                    jybcc = JObject.Parse(Base64Decode(outputpre.data.insuranceparameters));

                    chsInput2201 = jybcc["zzj"]["in2201"].ToString();
                    chsInput2203 = jybcc["zzj"]["in2203"].ToString();
                    chsInput2204 = jybcc["zzj"]["in2204"].ToString();
                    chsInput2206 = jybcc["zzj"]["in2206"].ToString();
                    chsInput2207 = jybcc["zzj"]["in2207"].ToString();
                    //totalfee = decimal.Parse(jybcc["zzj"]["in2206"]["data"]["medfee_sumamt"].ToString());//订单 医保范围内总金额

                    #endregion his预算

                    break;

                default:
                    chsInput2207 = "";
                    sjh = "";
                    break;
            }

            try
            {
                SaveLog(DateTime.Now, "CHS_YBY", DateTime.Now, "开始处理出参");//保存his接口日志

                DataSet dsbody = new DataSet();
                DataTable dtrev = RtnResultDatatable("0", "");
                dtrev.Columns.Add("EXPCONTENT");
                dtrev.Columns.Add("RE_DJ");

                dtrev.Columns.Add("CHSINPUT2201");
                dtrev.Columns.Add("CHSINPUT2203");
                dtrev.Columns.Add("CHSINPUT2204");
                dtrev.Columns.Add("CHSINPUT2206");
                dtrev.Columns.Add("AMT_OUTHP");

                dsbody.Tables.Add(dtrev.Copy());

                switch (TYPE)
                {
                    case "GETCHSREGTRY":
                    case "GETCHSOUTPTRY":
                        Dictionary<string, string> dicEXP = new Dictionary<string, string>
                        {
                            { "chsInput2201", chsInput2201 },
                            { "chsInput2203", chsInput2203 },
                            { "chsInput2204", chsInput2204 },
                            { "chsInput2206", chsInput2206 },
                            { "chsInput2207", chsInput2207 },
                            { "YBDJH", "" },
                            { "SJH", sjh }
                        };
                        dsbody.Tables[0].Rows[0]["CHSINPUT2201"] = chsInput2201;
                        dsbody.Tables[0].Rows[0]["CHSINPUT2203"] = chsInput2203;
                        dsbody.Tables[0].Rows[0]["CHSINPUT2204"] = chsInput2204;
                        dsbody.Tables[0].Rows[0]["CHSINPUT2206"] = chsInput2206;
                        //dsbody.Tables[0].Rows[0]["AMT_OUTHP"] = ((jeall - totalfee) * 100).ToString("0");

                        dsbody.Tables[0].Rows[0]["EXPCONTENT"] = JSONSerializer.Serialize<Dictionary<string, string>>(dicEXP);
                        dsbody.Tables[0].Rows[0]["RE_DJ"] = "1";

                        SaveLog(DateTime.Now, "CHS_YBY", DateTime.Now, "出参处理完成");//保存his接口日志

                        break;
                }
                return dsbody;
            }
            catch (Exception ex)
            {
                SaveLog(DateTime.Now, "CHS_YBY", DateTime.Now, "出参处理完成异常" + ex.Message);//保存his接口日志

                //DataSet ds = new DataSet();
                DataTable dtrev = RtnResultDatatable("99", ex.Message);
                ds.Tables.Add(dtrev.Copy());
                return ds;
            }
        }

        public static DataSet GETPATBARCODE(string HOS_ID, string PAT_ID, ref string YNCARDNO)
        {
            DataTable dtresult = new DataTable();
            dtresult.Columns.Add("CLBZ", typeof(string));
            dtresult.Columns.Add("CLJG", typeof(string));
            dtresult.Columns.Add("YNCARDNO", typeof(string));
            try
            {
                Dictionary<string, string> Dictionary = getpatinfobypat_id(long.Parse(PAT_ID), HOS_ID);
                string PAT_NAME = Dictionary.ContainsKey("PAT_NAME") ? FormatHelper.GetStr(Dictionary["PAT_NAME"]) : "";
                string SFZ_NO = Dictionary.ContainsKey("SFZ_NO") ? FormatHelper.GetStr(Dictionary["SFZ_NO"]) : "";
                string YLCARD_TYPE = Dictionary.ContainsKey("YLCARTD_TYPE") ? FormatHelper.GetStr(Dictionary["YLCARTD_TYPE"]) : "";
                string YLCARD_NO = Dictionary.ContainsKey("YLCARD_NO") ? FormatHelper.GetStr(Dictionary["YLCARD_NO"]) : "";
                string SEX = Dictionary.ContainsKey("SEX") ? FormatHelper.GetStr(Dictionary["SEX"]) : "";
                string BIRTHDAY = Dictionary.ContainsKey("BIRTHDAY") ? FormatHelper.GetStr(Dictionary["BIRTHDAY"]) : "";
                string GUARDIAN_NAME = Dictionary.ContainsKey("GUARDIAN_NAME") ? FormatHelper.GetStr(Dictionary["GUARDIAN_NAME"]) : "";
                string MOBILE_NO = Dictionary.ContainsKey("MOBILE_NO") ? FormatHelper.GetStr(Dictionary["MOBILE_NO"]) : "";
                string ADDRESS = Dictionary.ContainsKey("ADDRESS") ? FormatHelper.GetStr(Dictionary["ADDRESS"]) : "";
                YNCARDNO = GETPATHOSPITALID(YNCARDNO, SFZ_NO, PAT_NAME, SEX, BIRTHDAY, GUARDIAN_NAME, MOBILE_NO, ADDRESS, PAT_ID.ToString(), YLCARD_TYPE, YLCARD_NO);
                if (YNCARDNO == "")
                {
                    DataRow newrow = dtresult.NewRow();
                    newrow["CLBZ"] = "1";
                    newrow["CLJG"] = "获取院内号失败";
                    dtresult.Rows.Add(newrow);
                }
                else
                {
                    DataRow newrow = dtresult.NewRow();
                    newrow["CLBZ"] = "0";
                    newrow["CLJG"] = "";
                    newrow["YNCARDNO"] = YNCARDNO;
                    dtresult.Rows.Add(newrow);
                }
            }
            catch (Exception ex)
            {
                DataRow newrow = dtresult.NewRow();
                newrow["CLBZ"] = "2";
                newrow["CLJG"] = "执行失败，失败原因:" + ex.Message + "";
                dtresult.Rows.Add(newrow);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dtresult);
            return ds;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="COMM_MAIN">支付流水号</param>
        /// <param name="ZF_TYPE">支付方式</param>
        /// <param name="BIZ_TYPE">业务类型  1挂号  2门诊缴费  3住院</param>
        /// <returns></returns>
        public DataSet GETSTATUSBYORDERID(string COMM_MAIN, string ZF_TYPE, string BIZ_TYPE, string SOURCE)
        {
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("GETSTATUSBYORDERID", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "COMM_MAIN", COMM_MAIN);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "ZF_TYPE", ZF_TYPE);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "BIZ_TYPE", BIZ_TYPE);
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                DataSet ds = new DataSet();
                ds.Tables.Add(dtrev.Copy());
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet CHECKMBCANPAY(string SFZ_NO, string MB_ID)
        {
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("CHECKMBCANPAY", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", SFZ_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "MB_ID", MB_ID);
            try
            {
                DataSet ds = new DataSet();
                DataTable dtrev = RtnResultDatatable("0", "");
                ds.Tables.Add(dtrev.Copy());
                return ds;
            }
            catch
            {
                return null;
            }
        }

        public DataTable RtnResultDatatable(string CLBZ, string CLJG)
        {
            DataTable dtrev = new DataTable();
            dtrev.Columns.Add("CLBZ", typeof(string));
            dtrev.Columns.Add("CLJG", typeof(string));
            DataRow newrow = dtrev.NewRow();
            newrow["CLBZ"] = CLBZ;
            newrow["CLJG"] = CLJG;
            dtrev.Rows.Add(newrow);
            return dtrev;
        }

        public DataSet YUNHOSPRESEXAMSAVE(string HSO_ID, string HOS_SN, string PAT_NAME, string SFZ_NO, string YLCARD_TYPE, string YLCARD_NO, string DEPT_CODE, string DOC_NO, string SF_DOC, string RECIPE_NO, string SF_STATUS, string SF_DESC, string CASign)
        {
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("YUNHOSPRESEXAMSAVE", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", HOS_SN);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "PAT_NAME", PAT_NAME);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SFZ_NO", SFZ_NO);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_TYPE", YLCARD_TYPE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "YLCARD_NO", YLCARD_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DEPT_CODE", DEPT_CODE.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "DOC_NO", DOC_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SF_DOC", SF_DOC.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "RECIPE_NO", RECIPE_NO.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SF_STATUS", SF_STATUS.Trim());
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "SF_DESC", SF_DESC.Trim());

            try
            {
                string rtnxml = CallService(doc.OuterXml, "");
                XmlDocument xmldoc = XMLHelper.X_GetXmlDocument(rtnxml);
                DataTable dtrev = XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY").Tables[0];
                DataSet ds = new DataSet();
                ds.Tables.Add(dtrev.Copy());
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取互联网医院病人处方信息
        /// </summary>
        /// <param name="HOS_SN"></param>
        /// <returns></returns>
        private DataSet GetPREPDF(string HOS_SN)
        {
            string SOURCE = "";
            XmlDocument doc = new XmlDocument();
            doc = QHXmlMode.GetBaseXml("SENDPRE", "0");
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_ID", HOS_ID);
            XMLHelper.X_XmlInsertNode(doc, "ROOT/BODY", "HOS_SN", HOS_SN);
            try
            {
                string rtnxml = CallService(doc.OuterXml, SOURCE);

                try
                {
                    DataSet ds = new DataSet();

                    DataTable dtpatinfo = new DataTable();//病人信息表
                    DataTable dtmed = new DataTable();
                    DataTable dtsqd = new DataTable();
                    try
                    {
                        dtpatinfo = XMLHelper.X_GetXmlData(rtnxml, "ROOT/BODY").Tables[0];
                    }
                    catch
                    { }
                    try
                    {
                        DataTable dtPRDPDF = new DataTable();
                        DataTable dtDAMEDLIST = new DataTable();
                        DataTable dtDAMED = new DataTable();
                        DataTable dtSQDMXLIST = new DataTable();
                        DataTable dtSQDMX = new DataTable();
                        DataSet dsPREPDFLIST = XMLHelper.X_GetXmlData(rtnxml, "ROOT/BODY/PREPDFLIST");
                        if (dsPREPDFLIST.Tables.Contains("PREPDF"))
                        {
                            dtPRDPDF = dsPREPDFLIST.Tables["PREPDF"];
                        }
                        if (dsPREPDFLIST.Tables.Contains("DAMEDLIST"))
                        {
                            dtDAMEDLIST = dsPREPDFLIST.Tables["DAMEDLIST"];
                        }
                        if (dsPREPDFLIST.Tables.Contains("DAMED"))
                        {
                            dtDAMED = dsPREPDFLIST.Tables["DAMED"];
                        }
                        if (dsPREPDFLIST.Tables.Contains("SQDMXLIST"))
                        {
                            dtSQDMXLIST = dsPREPDFLIST.Tables["SQDMXLIST"];
                        }
                        if (dsPREPDFLIST.Tables.Contains("SQDMX"))
                        {
                            dtSQDMX = dsPREPDFLIST.Tables["SQDMX"];
                        }
                        if (dtPRDPDF.Rows.Count > 0 && dtDAMEDLIST.Rows.Count > 0 && dtDAMED.Rows.Count > 0)
                        {
                            var query = (from a in dtPRDPDF.AsEnumerable()
                                         join b in dtDAMEDLIST.AsEnumerable() on FormatHelper.GetInt(a["PREPDF_ID"]) equals FormatHelper.GetInt(b["PREPDF_ID"])
                                         join c in dtDAMED.AsEnumerable() on FormatHelper.GetInt(b["DAMEDLIST_ID"]) equals FormatHelper.GetInt(c["DAMEDLIST_ID"])
                                         select new
                                         {
                                             OPERA_TYPE = FormatHelper.GetInt(a["OPERA_TYPE"]),
                                             PRE_NO = FormatHelper.GetStr(a["PRE_NO"]),
                                             MED_ID = FormatHelper.GetStr(c["med_id"]),
                                             US_ID = FormatHelper.GetStr(c["US_ID"]),
                                             CAMT = FormatHelper.GetStr(c["CAMT"]),
                                             CAMTALL = FormatHelper.GetStr(c["CAMTALL"]),
                                             AUT_ID = FormatHelper.GetStr(c["AUT_ID"]),
                                             AUT_IDALL = FormatHelper.GetStr(c["AUT_IDALL"]),
                                             PC_ID = FormatHelper.GetStr(c["PC_ID"]),
                                             GROUP_ID = FormatHelper.GetStr(c["GROUP_ID"]),
                                             DAYS = FormatHelper.GetStr(c["DAYS"]),
                                             MED_COST = FormatHelper.GetStr(c["MED_COST"]),
                                             MED_JE_ALL = FormatHelper.GetStr(c["MED_JE_ALL"]),
                                         });
                            dtmed = Projecttable.CopyToDataTable(query);
                        }
                        if (dtpatinfo.Rows.Count > 0 && dtSQDMXLIST.Rows.Count > 0 && dtSQDMX.Rows.Count > 0)
                        {
                            var query = (from a in dtPRDPDF.AsEnumerable()
                                         join b in dtSQDMXLIST.AsEnumerable() on FormatHelper.GetInt(a["PREPDF_ID"]) equals FormatHelper.GetInt(b["PREPDF_ID"])
                                         join c in dtSQDMX.AsEnumerable() on FormatHelper.GetInt(b["SQDMXLIST_ID"]) equals FormatHelper.GetInt(c["SQDMXLIST_ID"])
                                         select new
                                         {
                                             OPERA_TYPE = FormatHelper.GetInt(a["OPERA_TYPE"]),
                                             PRE_NO = FormatHelper.GetStr(a["PRE_NO"]),
                                             SQD_ID = FormatHelper.GetStr(c["SQD_ID"]),
                                             SQD_NAME = FormatHelper.GetStr(c["SQD_NAME"]),
                                             CAMT = FormatHelper.GetStr(c["CAMT"]),
                                             ZS = FormatHelper.GetStr(c["ZS"]),
                                             XBS = "",
                                             JWS = FormatHelper.GetStr(c["JWS"]),
                                             JCBW = FormatHelper.GetStr(c["JCBW"]),
                                             JCYQ = FormatHelper.GetStr(c["JCYQ"]),
                                             JCDD = FormatHelper.GetStr(c["JCDD"]),
                                             JCZD = ""
                                         });
                            dtsqd = Projecttable.CopyToDataTable(query);
                        }
                    }
                    catch (Exception ex)
                    { }
                    if (dtpatinfo != null && dtpatinfo.Rows.Count > 0)
                    {
                        dtpatinfo.TableName = "patinfo";
                        ds.Tables.Add(dtpatinfo.Copy());
                    }
                    if (dtmed != null && dtmed.Rows.Count > 0)
                    {
                        dtmed.TableName = "med";
                        ds.Tables.Add(dtmed);
                    }
                    if (dtsqd != null && dtsqd.Rows.Count > 0)
                    {
                        dtsqd.TableName = "sqd";
                        ds.Tables.Add(dtsqd);
                    }
                    return ds;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public bool SAVEHOSPREINFO()
        {
            return true;
        }

        public DataTable GETAPPTSIGNININFO(string HOS_ID, string HOS_SN, string SCH_DATE, string SCH_TIME, string YLCARD_TYPE, string YLCARD_NO, string lTERMINAL_SN, Dictionary<string, string> dic)
        {
            DataTable dt_rev = new DataTable();
            dt_rev.Columns.Add("CLBZ", typeof(string));
            dt_rev.Columns.Add("CLJG", typeof(string));

            DataRow dr = dt_rev.NewRow();
            dr["CLBZ"] = "0";
            dr["CLJG"] = "";

            dt_rev.Rows.Add(dr);

            return dt_rev;
        }

        private DataTable SendMessage(string mobile, string message)
        {
            DataTable dtrev = new DataTable();
            dtrev.Columns.Add("CLBZ", typeof(string));
            dtrev.Columns.Add("CLJG", typeof(string));
            try
            {
                string poststr = string.Format("mobiles={0}&message={1}&sysid=MA5URL", mobile, message);
                HttpClient httpclient = new HttpClient("http://hbi.tkxlglyy.com:6180/sms/home/sms/sendMessage");
                string result = "";
                int status = httpclient.Send(poststr, Encoding.UTF8, out result);
                if (status == 200)
                {
                    MessageResult messageResult = JsonHelper.DeserializeJsonToObject<MessageResult>(result);
                    if (messageResult != null)
                    {
                        if (messageResult.success == "true")
                        {
                            DataRow newrow = dtrev.NewRow();
                            newrow["CLBZ"] = "0";
                            newrow["CLJG"] = "发送成功";
                            dtrev.Rows.Add(newrow);
                            return dtrev;
                        }
                        else
                        {
                            DataRow newrow = dtrev.NewRow();
                            newrow["CLBZ"] = "9";
                            newrow["CLJG"] = FormatHelper.GetStr(messageResult.desc);
                            dtrev.Rows.Add(newrow);
                            return dtrev;
                        }
                    }
                }
                DataRow newrow1 = dtrev.NewRow();
                newrow1["CLBZ"] = "9";
                newrow1["CLJG"] = "发送失败";
                dtrev.Rows.Add(newrow1);
                return dtrev;
            }
            catch (Exception ex)
            {
                DataRow newrow = dtrev.NewRow();
                newrow["CLBZ"] = "9";
                newrow["CLJG"] = ex.Message;
                dtrev.Rows.Add(newrow);
                return dtrev;
            }
        }

        #region N:源启支付

        public DataSet GETHOSZFDATA(Dictionary<string, string> dic)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                string BARCODE = "";
                //DataTable dtpatcardbind = new Plat.BLL.BaseFunction().GetList("pat_card_bind", "HOS_ID='" + dic["HOS_ID"] + "' and PAT_ID='" + dic["PAT_ID"] + "' and YLCARTD_TYPE=1  order by BAND_TIME DESC", "YLCARD_NO");

                string sqlcardbind = "select * from pat_card_bind where HOS_ID=@HOS_ID and PAT_ID=@PAT_ID and YLCARTD_TYPE=1  order by BAND_TIME DESC";

                MySqlParameter[] parameter4 = {
                    new MySqlParameter("@HOS_ID", dic["HOS_ID"]),
                    new MySqlParameter("@PAT_ID", dic["PAT_ID"] ) };

                DataTable dtpatcardbind = DBQuery("", sqlcardbind.ToString(), parameter4).Tables[0];

                if (dtpatcardbind.Rows.Count > 0)
                {
                    BARCODE = CommonFunction.GetStr(dtpatcardbind.Rows[0]["YLCARD_NO"]);
                }


                //string _sqlappt = "select * from register_appt where reg_id=@reg_id";
                //MySqlParameter[] parameterappt = {
                //    new MySqlParameter("@reg_id", dic["PLAT_LSH"]) };

                //DataTable dtappt = DBQuery("", _sqlappt.ToString(), parameterappt).Tables[0];

                //string SOURCE = dtappt.Rows[0]["SOURCE"].ToString();



                string operCode = "MYNJ";

                if (dic.Keys.Contains("SOURCE"))
                {
                    if (dic["SOURCE"].Trim() == "A000S185")
                    {
                        operCode = "QHAPP";
                    }
                    if (dic["SOURCE"].Trim() == "H001S185")
                    {
                        operCode = "MYNJ";
                    }
                }


                #region
                try {

                    if (dic["CHANNEL"].Trim() != "alipay")
                    {
                        operCode = "MYNJ";


                    }
                    else { operCode = "QHAPP"; }


                }
                catch (Exception ex) { }


                #endregion


                string HOS_SN_HIS = "";
                if (dic["BIZ_TYPE"] == "1")
                {
                    //DataTable dt = new Plat.BLL.BaseFunction().GetList("register_appt", "HOS_ID='" + dic["HOS_ID"] + "' and REG_ID='" + dic["PLAT_LSH"] + "' ", "HOS_SN");

                    string sqlappt = "select *from register_appt where REG_ID=@REG_ID and HOS_ID=@HOS_ID";
                    MySqlParameter[] parameter2 = {
                    new MySqlParameter("@REG_ID",dic["PLAT_LSH"] ),
                    new MySqlParameter("@HOS_ID",dic["HOS_ID"]) };

                    DataTable dt = DBQuery("", sqlappt.ToString(), parameter2).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        HOS_SN_HIS = CommonFunction.GetStr(dt.Rows[0]["HOS_SN"]);
                    }
                }
                else if (dic["BIZ_TYPE"] == "2")
                {
                    //DataTable dt = new Plat.BLL.BaseFunction().GetList("opt_pay_lock", "HOS_ID='" + dic["HOS_ID"] + "' and PAY_ID='" + dic["PLAT_LSH"] + "' ", "HOS_SN");

                    string sqlappt = "select *from opt_pay_lock where PAY_ID=@PAY_ID and HOS_ID=@HOS_ID";
                    MySqlParameter[] parameter2 = {
                    new MySqlParameter("@PAY_ID",dic["PLAT_LSH"] ),
                    new MySqlParameter("@HOS_ID",dic["HOS_ID"]) };

                    DataTable dt = DBQuery("", sqlappt.ToString(), parameter2).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        HOS_SN_HIS = CommonFunction.GetStr(dt.Rows[0]["HOS_SN"]);
                    }
                }

                string RETURN_URL = dic["RETURN_URL"];

                string bizstr = dic["BIZ_TYPE"] == "1" ? "当天挂号" : (dic["BIZ_TYPE"] == "2" ? "门诊缴费" : (dic["BIZ_TYPE"] == "3" ? "住院预交金" : "预约挂号"));
                string type = dic["BIZ_TYPE"] == "1" ? "reg" : (dic["BIZ_TYPE"] == "2" ? "fee" : (dic["BIZ_TYPE"] == "3" ? "pre" : "reg"));

                string transactionId = "";
                string jumpUrl = "";
                string outTradeNo = "";

                //h5pay  wechatpay  alipay
                string chnl = dic["CHANNEL"].ToUpper();

                //h5交易
                if (chnl == "H5PAY")
                {
                    operCode = "MYNJ";
                    P0105 p0105 = new P0105()
                    {
                        tradeChannel = "yqtyzf", //交易渠道 参考附录【交易渠道】
                        outTradeNo = DateTime.Now.ToString("yyyyMMddHHmmss") + dic["PLAT_LSH"], //商⼾订单号
                        description = bizstr, //订单描述
                        totalFee = decimal.Parse(dic["CASH_JE"]), //⾦额（单位：元）
                        name = dic["PAT_NAME"],
                        macNumber = "00-16-EA-AE-3C-40",
                        identityId = dic["SFZ_NO"],
                        mobile = "",
                        type = type, //交易类型 参考附录【交易类型】
                        redirectUrl = RETURN_URL, //⽀付跳转地址
                        quitUrl = RETURN_URL, //⽤⼾付款中途退出返回的地址
                                              //以下为可选项
                        operCode = operCode, //操作者 如果终端对应多个操作者(⽐如HIS窗⼝），必填
                        notifyUrl = "http://218.94.72.190:1443/SLB/Notify/TKHMPAY", //⽀付成功回调地址 具体⻅【5.⽀付成功回调】
                        payActiveTime = 2, //⽀付有效期（单位：分）默认2分钟
                        cardNo = BARCODE, //患者ID
                        optIptNo = HOS_SN_HIS, //⻔诊流⽔号/住院流⽔号
                        recipeNoList = HOS_SN_HIS //⻔诊缴费清单号，多个清单⽤,分割
                    };
                    string jsonstr = JsonConvert.SerializeObject(p0105);
                    Output<P0105DATA> outputpre
    = new MIDServiceHelper().CallServiceAPI<P0105DATA>("/platformpayment/pay/h5Order", jsonstr);

                    if (outputpre.code != 0)
                    {
                    }

                    transactionId = outputpre.data.transactionId;
                    jumpUrl = outputpre.data.jumpUrl;
                    outTradeNo = outputpre.data.outTradeNo;
                }
                else if (chnl == "ALIPAY")//支付宝原生交易
                {
                    operCode = "QHAPP";
                    ocPayOrder ocPay = new ocPayOrder()
                    {
                        tradeChannel = "yqtyzf", //交易渠道 参考附录【交易渠道】
                        outTradeNo = DateTime.Now.ToString("yyyyMMddHHmmss") + dic["PLAT_LSH"], //商⼾订单号
                        description = bizstr, //订单描述
                        totalFee = decimal.Parse(dic["CASH_JE"]), //⾦额（单位：元）
                        name = dic["PAT_NAME"],
                        macNumber = "00-16-EA-AE-3C-40",
                        identityId = dic["SFZ_NO"],
                        mobile = "",
                        type = type, //交易类型 参考附录【交易类型】
                        userId = dic["ZFB_USER_ID"], //userId买家id渠道⾃⾏获取
                                                     //以下为可选项
                        operCode = operCode, //操作者 如果终端对应多个操作者(⽐如HIS窗⼝），必填
                        notifyUrl = "http://218.94.72.190:1443/SLB/Notify/TKHMPAY", //⽀付成功回调地址 具体⻅【5.⽀付成功回调】
                        payActiveTime = 2, //⽀付有效期（单位：分）默认2分钟
                        cardNo = BARCODE, //患者ID
                        optIptNo = HOS_SN_HIS, //⻔诊流⽔号/住院流⽔号
                        recipeNoList = HOS_SN_HIS //⻔诊缴费清单号，多个清单⽤,分割
                    };

                    string jsonstr = JsonConvert.SerializeObject(ocPay);
                    Output<ocPayOrderData> outputpre
    = new MIDServiceHelper().CallServiceAPI<ocPayOrderData>("/platformpayment/pay/aliOrder", jsonstr);

                    if (outputpre.code != 0)
                    {
                        return null;
                    }

                    transactionId = outputpre.data.transactionId;
                    jumpUrl = "";
                    outTradeNo = outputpre.data.tradeNO;
                }

                XmlDocument xmlresult = RtnSucXml("GETQRCODE", "0");

                XMLHelper.X_XmlInsertNode(xmlresult, "ROOT/BODY", "QUERYID", transactionId);//平台交易号
                XMLHelper.X_XmlInsertNode(xmlresult, "ROOT/BODY", "PAY_URL", jumpUrl);//二维码信息串
                XMLHelper.X_XmlInsertNode(xmlresult, "ROOT/BODY", "PAY_PARM", HOS_SN_HIS);//第三方交易流水号 原样返回
                XMLHelper.X_XmlInsertNode(xmlresult, "ROOT/BODY", "TRADE_NO", outTradeNo);//jiaoyi dingdan hao

                DataSet dsbody = XMLHelper.X_GetXmlData(xmlresult, "ROOT/BODY");

                return dsbody;
            }
            catch
            {
                return null;
            }
        }

        public DataSet GETHOSZFRESULT(Dictionary<string, string> dic)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                string BARCODE = "";
                //DataTable dtpatcardbind = new Plat.BLL.BaseFunction().GetList("pat_card_bind", "HOS_ID='" + dic["HOS_ID"] + "' and PAT_ID='" + dic["PAT_ID"] + "' and YLCARTD_TYPE=1  order by BAND_TIME DESC", "YLCARD_NO");

                string sqlcardbind = "select * from pat_card_bind where HOS_ID=@HOS_ID and PAT_ID=@PAT_ID and YLCARTD_TYPE=1  order by BAND_TIME DESC";

                MySqlParameter[] parameter4 = {
                    new MySqlParameter("@HOS_ID", dic["HOS_ID"]),
                    new MySqlParameter("@PAT_ID", dic["PAT_ID"] ) };

                DataTable dtpatcardbind = DBQuery("", sqlcardbind.ToString(), parameter4).Tables[0];

                if (dtpatcardbind.Rows.Count > 0)
                {
                    BARCODE = CommonFunction.GetStr(dtpatcardbind.Rows[0]["YLCARD_NO"]);
                }

                string HOS_SN_HIS = "";
                if (dic["BIZ_TYPE"] == "1")
                {
                    //DataTable dt = new Plat.BLL.BaseFunction().GetList("register_appt", "HOS_ID='" + dic["HOS_ID"] + "' and REG_ID='" + dic["BIZ_SN"] + "' ", "HOS_SN");

                    string sqlappt = "select *from register_appt where REG_ID=@REG_ID and HOS_ID=@HOS_ID";
                    MySqlParameter[] parameter2 = {
                    new MySqlParameter("@REG_ID",dic["BIZ_SN"] ),
                    new MySqlParameter("@HOS_ID",dic["HOS_ID"]) };

                    DataTable dt = DBQuery("", sqlappt.ToString(), parameter2).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        HOS_SN_HIS = CommonFunction.GetStr(dt.Rows[0]["HOS_SN"]);
                    }
                }
                else if (dic["BIZ_TYPE"] == "2")
                {
                    //DataTable dt = new Plat.BLL.BaseFunction().GetList("opt_pay_lock", "HOS_ID='" + dic["HOS_ID"] + "' and PAY_ID='" + dic["BIZ_SN"] + "' ", "HOS_SN");

                    string sqlappt = "select *from opt_pay_lock where PAY_ID=@PAY_ID and HOS_ID=@HOS_ID";
                    MySqlParameter[] parameter2 = {
                    new MySqlParameter("@PAY_ID",dic["BIZ_SN"] ),
                    new MySqlParameter("@HOS_ID",dic["HOS_ID"]) };

                    DataTable dt = DBQuery("", sqlappt.ToString(), parameter2).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        HOS_SN_HIS = CommonFunction.GetStr(dt.Rows[0]["HOS_SN"]);
                    }
                }

                P0201 p0201 = new P0201()
                {
                    outTradeNo = "",
                    transactionId = dic["QUERYID"]
                };

                string jsonstr = JsonConvert.SerializeObject(p0201);

                Output<P0201DATA> output = new MIDServiceHelper().CallServiceAPI<P0201DATA>("/platformpayment/pay/queryOrder", jsonstr);

                if (output.code != 0)
                {
                    return null;
                }
                int STATUS = 0;

                if (output.data.tradeState == "success")
                {
                    STATUS = 1;
                }

                string dealtype = "";
                switch (output.data.tradeChannel)
                {
                    case "yqtyzf":
                        dealtype = "hm";
                        break;

                    default:
                        break;
                }

                XmlDocument xmlresult = RtnSucXml("GETORDERSTATUS", "0");
                XMLHelper.X_XmlInsertNode(xmlresult, "ROOT/BODY", "RESULT", STATUS.ToString());
                XMLHelper.X_XmlInsertNode(xmlresult, "ROOT/BODY", "TRANSID", output.data.extraData.thirdTransactionId);
                XMLHelper.X_XmlInsertNode(xmlresult, "ROOT/BODY", "TRANS_JE", output.data.totalFee.ToString());
                XMLHelper.X_XmlInsertNode(xmlresult, "ROOT/BODY", "TRANS_TIME", output.data.tradeTime);
                XMLHelper.X_XmlInsertNode(xmlresult, "ROOT/BODY", "DEAL_TYPE", dealtype);
                DataSet dsbody = XMLHelper.X_GetXmlData(xmlresult, "ROOT/BODY");

                return dsbody;
            }
            catch
            {
                return null;
            }
        }

        public DataSet  HOSZFREFUND(Dictionary<string, string> dic)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                string BARCODE = "";
                //DataTable dtpatcardbind = new Plat.BLL.BaseFunction().GetList("pat_card_bind", "HOS_ID='" + dic["HOS_ID"] + "' and PAT_ID='" + dic["PAT_ID"] + "' and YLCARTD_TYPE=1  order by BAND_TIME DESC", "YLCARD_NO");

                string sqlcardbind = "select * from pat_card_bind where HOS_ID=@HOS_ID and PAT_ID=@PAT_ID and YLCARTD_TYPE=1  order by BAND_TIME DESC";

                MySqlParameter[] parameter4 = {
                    new MySqlParameter("@HOS_ID", dic["HOS_ID"]),
                    new MySqlParameter("@PAT_ID", dic["PAT_ID"] ) };

                DataTable dtpatcardbind = DBQuery("", sqlcardbind.ToString(), parameter4).Tables[0];
                SaveLog(DateTime.Now, "DBQuery(\"\", sqlcardbind.ToString(), parameter4).Tables[0]失败", DateTime.Now, "cheshi12345");//保存his接口日志

                if (dtpatcardbind.Rows.Count > 0)
                {
                    BARCODE = CommonFunction.GetStr(dtpatcardbind.Rows[0]["YLCARD_NO"]);
                }
                string operCode = "MYNJ";
                                   SaveLog(DateTime.Now, "查询source失败", DateTime.Now, "cheshi12345");//保存his接口日志

                #region 退费操作员更新
                try
                {
                    string SOURCE = "";
                    string register_appt = "select * from register_appt  where HOS_ID=@HOS_ID and reg_id=@biz_sn ";
                    MySqlParameter[] parameter5 = {
                    new MySqlParameter("@HOS_ID", dic["HOS_ID"]),
                    new MySqlParameter("@biz_sn", dic["BIZ_SN"] ) };
                    DataTable registerdt = DBQuery("", register_appt.ToString(), parameter5).Tables[0];
                    if (registerdt.Rows.Count > 0)
                    {
                        SOURCE = CommonFunction.GetStr(registerdt.Rows[0]["SOURCE"]);
                        if (SOURCE.Contains("H")) {
                            operCode = "MYNJ";



                        }
                        else if (SOURCE.Contains("A")) {

                            operCode = "QHAPP";
                        }
                    }



                }
                catch(Exception ex){
                    SaveLog(DateTime.Now, "查询source失败", DateTime.Now, "cheshi12345");//保存his接口日志

                }

                #endregion

                if (dic.Keys.Contains("SOURCE"))
                {
                    if (dic["SOURCE"].Trim().Contains("A"))
                    {
                        operCode = "QHAPP";
                    }
                    if (dic["SOURCE"].Trim().Contains("H"))
                    {
                        operCode = "MYNJ";
                    }
                }

         

                string HOS_SN_HIS = "";
                string refundId = "";
                string refundReceiptNumber = "";

                string QUERYID = dic["QUERYID"];

                RedisHelperSentinels redis = new RedisHelperSentinels();

                //BIZ_TYPE=1 挂号，BIZ_TYPE=2 缴费
                //TF_TYPE= 1 退款，TF_TYPE=2 冲正
                if (dic["BIZ_TYPE"] == "1")
                {
                    //DataTable dt = new Plat.BLL.BaseFunction().GetList("register_appt", "HOS_ID='" + dic["HOS_ID"] + "' and REG_ID='" + dic["BIZ_SN"] + "' ", "HOS_SN");

                    string sqlappt = "select *from register_appt where REG_ID=@REG_ID and HOS_ID=@HOS_ID";
                    MySqlParameter[] parameter2 = {
                    new MySqlParameter("@REG_ID",dic["BIZ_SN"] ),
                    new MySqlParameter("@HOS_ID",dic["HOS_ID"]) };

                    DataTable dt = DBQuery("", sqlappt.ToString(), parameter2).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        HOS_SN_HIS = CommonFunction.GetStr(dt.Rows[0]["HOS_SN"]);
                    }

                    if (dic["TF_TYPE"] == "2")
                    {
                        var p0601 = new P0601()
                        {
                            outTradeNo = "", //商⼾订单号, 商⼾订单号和平台订单号必填⼀个
                            transactionId = QUERYID, //平台订单号 商⼾订单号和平台订单号必填⼀个
                            confirmState = "fail", //确认状态 success 确认成功 fail 失败
                            confirmDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), //确认时间 yyyy-mm-dd HH:mm:ss
                            receiptNo = "" //HIS发票号,多张发票⽤,分隔
                        };
                        var jsonstr0601 = Newtonsoft.Json.JsonConvert.SerializeObject(p0601);

                        var outputp0601 = new MIDServiceHelper().CallServiceAPI<JObject>("/platformpayment/pay/confirmPay", jsonstr0601, operCode, operCode);

                        if (outputp0601.code == 0)
                        {
                            return DS_RtnDealInfo("0", "提交退款申请成功");
                        }
                        else
                        {
                            return DS_RtnDealInfo("0", "退款申请提交失败");
                        }
                    }

                    if (dic["TF_TYPE"] == "1")
                    {
                        //主动退号，获取paycancel缓存的 refundReceiptNumber 和 refundId
                        JObject jobj = JObject.Parse(redis.Get("TH" + HOS_SN_HIS + HOS_ID, 7));
                        refundReceiptNumber = jobj["refundReceiptNumber"].ToString();
                        refundId = jobj["refundId"].ToString();
                    }
                }
                else if (dic["BIZ_TYPE"] == "2")
                {
                    var p0601 = new P0601()
                    {
                        outTradeNo = "", //商⼾订单号, 商⼾订单号和平台订单号必填⼀个
                        transactionId = QUERYID, //平台订单号 商⼾订单号和平台订单号必填⼀个
                        confirmState = "fail", //确认状态 success 确认成功 fail 失败
                        confirmDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), //确认时间 yyyy-mm-dd HH:mm:ss
                        receiptNo = "" //HIS发票号,多张发票⽤,分隔
                    };
                    var jsonstr0601 = Newtonsoft.Json.JsonConvert.SerializeObject(p0601);

                    var outputp0601 = new MIDServiceHelper().CallServiceAPI<JObject>("/platformpayment/pay/confirmPay", jsonstr0601, operCode, operCode);

                    if (outputp0601.code == 0)
                    {
                        return DS_RtnDealInfo("0", "提交退款申请成功");
                    }
                    else
                    {
                        return DS_RtnDealInfo("0", "退款申请提交失败");
                    }

                    string sqlappt = "select *from opt_pay_lock where PAY_ID=@PAY_ID and HOS_ID=@HOS_ID";
                    MySqlParameter[] parameter2 = {
                    new MySqlParameter("@PAY_ID",dic["BIZ_SN"] ),
                    new MySqlParameter("@HOS_ID",dic["HOS_ID"]) };

                    DataTable dt = DBQuery("", sqlappt.ToString(), parameter2).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        HOS_SN_HIS = CommonFunction.GetStr(dt.Rows[0]["HOS_SN"]);
                    }
                }

                //只有正常退号 ，才走退款流程
                decimal jeall = decimal.Parse(dic["JE_ALL"]);
                decimal cashje = decimal.Parse(dic["CASH_JE"]);

                P0301 p0301 = new P0301()
                {
                    outRefundNo = DateTime.Now.ToString("yyyyMMddHHmmss") + HOS_SN_HIS, //商⼾退款订单号

                    //outTradeNo和transactionId⼆选⼀，任填⼀个即可
                    outTradeNo = "", //商⼾订单号 （原交易）
                    transactionId = dic["QUERYID"], //⽀付中台订单号（原交易）
                    refundFee = decimal.Parse(dic["CASH_JE"]), //退款⾦额（元）
                    refundReason = "线上退号",
                    macNumber = operCode,
                    refundId = refundId,

                    //以下选填
                    operCode = operCode, //操作者 如果终端对应多个操作者(⽐如HIS窗⼝），必填
                    name = dic["PAT_NAME"], //不填的就使⽤下单时的数据
                    identityId = dic["SFZ_NO"], //不填的就使⽤下单时的数据
                    mobile = dic["MOBILE_NO"] //不填的就使⽤下单时的数据
                };
                string jsonstr = JsonConvert.SerializeObject(p0301);

                Output<P0301DATA> output = new MIDServiceHelper().CallServiceAPI<P0301DATA>("/platformpayment/pay/refundOrder", jsonstr, operCode, operCode);
                DataSet dsbody = new DataSet();
                XmlDocument xmlresult = new XmlDocument();
                if (output.code == 0)
                {
                    xmlresult = RtnSucXml("DOQUIT", "0");

                    if (output.data.tradeState == "refund_success")
                    {
                        //通知
                        string refund_order_id = output.data.extraData.thirdTransactionId;

                        if (jeall == cashje)
                        {
                            refundMsg msg = new refundMsg()
                            {
                                oldReceiptNumber = dic["QUERYID"],//原收据号    N  原HIS内收据号
                                patientName = dic["PAT_NAME"],//患者姓名    Y  患者姓名，用于验证防止误操作
                                receiptNumber = refundReceiptNumber,//退费收据号   Y  HIS标记唯一一次退费的单据号。根据对应退费接口反馈的tsjh
                                refundAmount = dic["CASH_JE"],//退费金额    Y  退费金额，用于验证
                                refundNo = refund_order_id,//退费流水号   Y  支付金融机构交易流水号，如源启支付，支付宝、微信、银行等机构的原始退费流水号，用于对账
                                refundPayType = "51"//退支付方式   Y
                            };

                            jsonstr = JsonConvert.SerializeObject(msg);
                            var outputmsg = new MIDServiceHelper().CallServiceAPI<object>("/hisbooking/register/refundMsg", jsonstr);
                            if (outputmsg.code != 0)
                            {
                                xmlresult = RtnFailXml("DOQUIT", "0", "8", outputmsg.message);
                            }
                        }
                        else
                        {
                            if (dic["BIZ_TYPE"] == "1")
                            {
                                string key = "TH" + HOS_SN_HIS + HOS_ID;
                                JObject jobj = JObject.Parse(redis.Get(key, 7));
                                jobj.Add("refund_order_id", refund_order_id);

                                redis.Set(key, jobj.ToString(), DateTime.Now.AddMinutes(120), 7);
                            }
                        }
                    }
                    else
                    {
                        xmlresult = RtnFailXml("DOQUIT", "0", "2", output.data.tradeState);
                    }
                }
                else
                {
                    xmlresult = RtnFailXml("DOQUIT", "0", "8", output.message);
                }
                dsbody = XMLHelper.X_GetXmlData(xmlresult, "ROOT/BODY");
                return dsbody;
            }
            catch (Exception ex)
            {
                DataSet dsbody = new DataSet();
                XmlDocument xmlresult = RtnFailXml("DOQUIT", "0", "8", "退费异常"+ex.Message);
                dsbody = XMLHelper.X_GetXmlData(xmlresult, "ROOT/BODY");
                return dsbody;
            }
        }

        private static XmlDocument RtnSucXml(string type, string czlx)
        {
            XmlDocument rtndoc = null;
            rtndoc = XMLHelper.X_CreateXmlDocument("ROOT");
            XMLHelper.X_XmlInsertNode(rtndoc, "ROOT", "HEADER");
            XMLHelper.X_XmlInsertNode(rtndoc, "ROOT/HEADER", "TYPE", type);
            XMLHelper.X_XmlInsertNode(rtndoc, "ROOT/HEADER", "CZLX", czlx);
            XMLHelper.X_XmlInsertNode(rtndoc, "ROOT", "BODY");
            XMLHelper.X_XmlInsertNode(rtndoc, "ROOT/BODY", "CLBZ", "0");//只有0表示成功
            XMLHelper.X_XmlInsertNode(rtndoc, "ROOT/BODY", "CLJG", "Success");
            return rtndoc;
        }

        private static XmlDocument RtnFailXml(string type, string czlx, string clbz, string cljg)
        {
            XmlDocument rtndoc = null;
            rtndoc = XMLHelper.X_CreateXmlDocument("ROOT");
            XMLHelper.X_XmlInsertNode(rtndoc, "ROOT", "HEADER");
            XMLHelper.X_XmlInsertNode(rtndoc, "ROOT/HEADER", "TYPE", type);
            XMLHelper.X_XmlInsertNode(rtndoc, "ROOT/HEADER", "CZLX", czlx);
            XMLHelper.X_XmlInsertNode(rtndoc, "ROOT", "BODY");
            XMLHelper.X_XmlInsertNode(rtndoc, "ROOT/BODY", "CLBZ", clbz);
            XMLHelper.X_XmlInsertNode(rtndoc, "ROOT/BODY", "CLJG", cljg);
            return rtndoc;
        }

        #endregion N:源启支付

        private DataSet DBQuery(string db, string SQLString, params MySqlParameter[] cmdParms)
        {
            try
            {
                if (db == "yunhou")
                {
                    return DbHelperMySQLYunHou.Query(SQLString, PBusHos185.AUID.Value, cmdParms);
                }
                return DbHelperMySQL.Query(SQLString, PBusHos185.AUID.Value, cmdParms);
            }
            catch (Exception ex)
            {
                SaveLog(DateTime.Now, JsonConvert.SerializeObject(ex), DateTime.Now, "DBQuery执行异常");//保存his接口日志

                return null;
            }
        }

        #region 南京医保云

        private string CallMidService(string jsonstr, string funcname)
        {
            DateTime InTime = DateTime.Now;
            string outdata = "";
            string url = "";
            try
            {
                switch (funcname)
                {
                    case "PREBILLCHARGE":

                        url = "http://localhost:8112/MedinsOrder/prebillcharge";

                        break;

                    case "QUERYCHARGESTATUS":

                        url = "http://localhost:8112/MedinsOrder/QueryChargeStatus";

                        break;

                    case "CHARGEREFUND":

                        url = "http://localhost:8112/MedinsOrder/ChargeRefund";

                        break;

                    case "QUERYPSNINFO":

                        url = "http://localhost:8112/MedinsOrder/QueryPsnInfo";
                        //return "{\"baseinfo\":{\"psn_no\":\"32010000000000100002049446\",\"psn_cert_type\":\"01\",\"certno\":\"320114196804080612\",\"psn_name\":\"赵志牛\",\"gend\":\"1\",\"naty\":\"01\",\"brdy\":\"1968-04-08\",\"age\":\"55.5\",\"exp_content\":\"{\\\"oth_dise_balc_M01902\\\":0,\\\"pery_new_exam_balc\\\":0,\\\"opt_spdise_organ_transplant_asst_medn_balc\\\":0,\\\"oth_dise_balc_M01102\\\":0,\\\"oth_dise_balc_M01001\\\":0,\\\"flx_emp_flag\\\":\\\"0\\\",\\\"oth_dise_balc_M07800\\\":0,\\\"oth_dise_balc_M01601\\\":0,\\\"trt_chk_rslt\\\":\\\"\\\",\\\"nhb_flag\\\":\\\"0\\\",\\\"inhosp_stas\\\":\\\"0\\\",\\\"opt_big_dise_organ_transplant_medn_balc\\\":0,\\\"opt_spdise_tmor_asst_medn_balc\\\":0,\\\"opt_big_dise_tmor_medn_balc\\\":0,\\\"opt_big_dise_blo_abd_diay_asst_medn_balc\\\":0,\\\"opt_big_dise_blo_abd_diay_medn_balc\\\":0,\\\"pery_old_exam_balc\\\":0,\\\"oth_dise_balc_M00904\\\":0,\\\"fm_acct_balc\\\":0,\\\"opt_big_dise_tmor_chmo_balc\\\":0,\\\"otp_dise_balc\\\":0,\\\"opt_pool_balc\\\":12772.04,\\\"opt_spdise_blo_abd_diay_asst_medn_balc\\\":0,\\\"oth_dise_balc_M02207\\\":0,\\\"oth_dise_balc_M02700\\\":0,\\\"oth_dise_balc_M00105\\\":0,\\\"opt_big_dise_organ_transplant_asst_medn_balc\\\":0,\\\"opt_spdise_blo_abd_diay_medn_balc\\\":0,\\\"opsp_balc\\\":0,\\\"opt_big_dise_tomr_asst_medn_balc\\\":0,\\\"oth_dise_balc_M07101\\\":0,\\\"opt_spdise_tmor_chmo_balc\\\":0,\\\"opt_spdise_tmor_medn_balc\\\":0,\\\"rsdt_pery_old_exam_balc\\\":0,\\\"opt_spdise_organ_transplant_medn_balc\\\":0}\"},\"insuinfo\":[{\"balc\":1845.67,\"insutype\":\"310\",\"psn_type\":\"11\",\"psn_insu_stas\":\"1\",\"psn_insu_date\":\"2005-02-01\",\"paus_insu_date\":null,\"cvlserv_flag\":\"0\",\"insuplc_admdvs\":\"320113\",\"emp_name\":\"南京栖霞建设物业服务股份有限公司\"},{\"balc\":0.0,\"insutype\":\"510\",\"psn_type\":\"11\",\"psn_insu_stas\":\"1\",\"psn_insu_date\":\"2005-02-01\",\"paus_insu_date\":null,\"cvlserv_flag\":\"0\",\"insuplc_admdvs\":\"320113\",\"emp_name\":\"南京栖霞建设物业服务股份有限公司\"}],\"idetinfo\":[]}";

                        break;

                    case "GETRECONCILIATION":

                        url = "http://localhost:8112/MedinsOrder/GetReconciliation";

                        break;

                    default:
                        break;
                }

                HttpClient http = new HttpClient(url);

                int status = http.SendJson(jsonstr, Encoding.UTF8, out outdata);
            }
            catch (Exception ex)
            {
                outdata = JsonConvert.SerializeObject(ex);
            }
            finally
            {
                SaveLog(InTime, jsonstr, DateTime.Now, outdata);
            }

            return outdata;
        }

        public DataSet PreOrder(Dictionary<string, string> dic)
        {
            string hos_id = "185";

            string medOrgOrd = dic["medOrgOrd"];//医疗机构订单号
            string iptOtpNo = dic["iptOtpNo"];//门诊流水号
                                              //string scheduleId = dic["scheduleId"];//号源id
            string payAuthNo = dic["payAuthNo"];//授权编码	字符型	40		Y	授权医院进行费用明细上传和下单(2小时内有效)。

            string orderType = dic["orderType"];//订单类型	字符型	3		Y	1、挂号2、诊间、3、住院押金4、住院缴费5、担保支付
            string idType = dic["idType"];
            string idNo = dic["idNo"];
            string patnId = dic["patnId"];
            string ocToken = dic["ocToken"];//电子凭证授权ocToken
            Soft.DBUtility.RedisHelperSentinels redis = new Soft.DBUtility.RedisHelperSentinels();

            DataSet ds = new DataSet();

            JObject jl6101 = new JObject
            {
                { "ocToken", ocToken },
                { "hos_id",hos_id }
            };

            JObject rt1101 = JObject.Parse(CallMidService(jl6101.ToString(), "QUERYPSNINFO"));

            //缓存 1101 出参，因为回调的时候拼接的1101出参不全
            redis.Set(idNo, rt1101, DateTime.Now.AddDays(1));

            string ChsOutput1101 = "";
            string ChsInput2201 = "";
            string ChsInput2203 = "";
            string ChsInput2204 = "";
            string ChsInput2206 = "";
            string EXPCONTENT = "";
            string redisbuskey = (orderType == "1" ? "GH" : "MZ") + medOrgOrd + hos_id;
            try
            {
                if (orderType == "1")
                {
                    #region GETCHSREGTRY

                    //占号时将 参数 缓存下来，预结算时使用

                    string apptinfo = redis.Get(redisbuskey, 7);

                    JObject jappt = JObject.Parse(apptinfo);

                    Dictionary<string, string> dicgetchsregtry = new Dictionary<string, string>
                {
                    { "MODULE_TYPE", "GETCHSREGTRY" },
                    { "HOS_ID", hos_id },
                    { "HOS_SN", iptOtpNo },
                    { "SOURCE", "JSYBY" },
                    { "YNCARDNO", jappt["YNCARDNO"].ToString() },
                    { "REGISTER_TYPE", jappt["REGISTER_TYPE"].ToString() },
                    { "APPT_PAY", jappt["APPT_PAY"].ToString() },
                    { "PAT_NAME", "" },
                    { "MDTRT_CERT_TYPE", "02" },
                    { "MDTRT_CERT_NO", idNo },
                    { "CHSOUTPUT1101", new JObject(){ {"output", rt1101 } }.ToString() }
                };

                    DataSet dshis = COMMONINTERFACE("GETCHSREGTRY", dicgetchsregtry);

                    if (dshis.Tables[0].Rows[0]["CLBZ"].ToString() == "0")
                    {
                        ChsOutput1101 = rt1101.ToString();
                        ChsInput2201 = dshis.Tables[0].Rows[0]["ChsInput2201"].ToString();
                        ChsInput2203 = dshis.Tables[0].Rows[0]["ChsInput2203"].ToString();
                        ChsInput2204 = dshis.Tables[0].Rows[0]["ChsInput2204"].ToString();
                        ChsInput2206 = dshis.Tables[0].Rows[0]["ChsInput2206"].ToString();

                        //his 这里适配医保云 ipt_otp_no
                        JObject j2201 = JObject.Parse(ChsInput2201);
                        j2201["data"]["ipt_otp_no"] = medOrgOrd;
                        ChsInput2201 = j2201.ToString();

                        EXPCONTENT = dshis.Tables[0].Rows[0]["EXPCONTENT"].ToString();
                    }

                    #endregion GETCHSREGTRY
                }
                else if (orderType == "2")
                {
                    #region GETCHSOUTPTRY

                    string pre_no = "";

                    var outfee = redis.Get(redisbuskey, 7);

                    if (outfee != null)
                    {
                        pre_no = JObject.Parse(outfee)["PRE_NO"].ToString();
                    }

                    Dictionary<string, string> dicgetchsregtry = new Dictionary<string, string>
                    {
                        { "MODULE_TYPE", "GETCHSOUTPTRY" },
                        { "HOS_ID",hos_id },
                        { "HOS_SN", medOrgOrd.Split("#")[0] },
                        { "SOURCE", "JSYBY" },
                        { "PRE_NO",pre_no },
                        { "MDTRT_CERT_TYPE", "02" },
                        { "MDTRT_CERT_NO", idNo },
                        { "CHSOUTPUT1101",rt1101==null?"": rt1101.ToString() },
                        { "chsOutput1101",rt1101==null?"": rt1101.ToString() }
                    };

                    DataSet dshis = COMMONINTERFACE("GETCHSOUTPTRY", dicgetchsregtry);

                    if (dshis.Tables[0].Rows[0]["CLBZ"].ToString() == "0")
                    {
                        ChsOutput1101 = rt1101.ToString();
                        ChsInput2201 = dshis.Tables[0].Rows[0]["ChsInput2201"].ToString();
                        ChsInput2203 = dshis.Tables[0].Rows[0]["ChsInput2203"].ToString();
                        ChsInput2204 = dshis.Tables[0].Rows[0]["ChsInput2204"].ToString();
                        ChsInput2206 = dshis.Tables[0].Rows[0]["ChsInput2206"].ToString();

                        //his 这里适配医保云 ipt_otp_no
                        JObject j2201 = JObject.Parse(ChsInput2201);
                        j2201["data"]["ipt_otp_no"] = medOrgOrd;
                        ChsInput2201 = j2201.ToString();

                        EXPCONTENT = dshis.Tables[0].Rows[0]["EXPCONTENT"].ToString();
                    }

                    #endregion GETCHSOUTPTRY
                }
            }
            catch (Exception ex)
            {
                DataTable dtbody = RtnResultDatatable("1", ex.Message);
                ds.Tables.Add(dtbody.Copy());

                return ds;
            }
            Models.PreBillCharge preBill = new Models.PreBillCharge()
            {
                hos_id = hos_id,
                Octoken = ocToken,
                Payauthno = payAuthNo,
                ChsOutput1101 = ChsOutput1101,
                ChsInput2201 = ChsInput2201,
                ChsInput2203 = ChsInput2203,
                ChsInput2204 = ChsInput2204,
                ChsInput2206 = ChsInput2206
            };

            string jsonpreBill = JsonConvert.SerializeObject(preBill);

            var strrtn = CallMidService(jsonpreBill, "PREBILLCHARGE");
            Models.PreBillChargeResponse preorder = JsonConvert.DeserializeObject<Models.PreBillChargeResponse>(strrtn);

            if (preorder.resultCode == "0")
            {
                if (orderType == "1")
                {
                    new Plat.BLL.BaseFunction().UpdateList("register_appt", "HOS_ID='" + hos_id + "' and hos_sn='" + medOrgOrd + "' ", "payauthno=" + payAuthNo + "");
                }
                Dictionary<string, string> expcontent = JsonConvert.DeserializeObject<Dictionary<string, string>>(EXPCONTENT);

                var appt = redis.Get(redisbuskey, 7);
                JObject jappt = JObject.Parse(appt);
                jappt.Add("chsOutput2204", preorder.chsOutput2204);
                jappt.Add("ChsInput2203", ChsInput2203);
                jappt.Add("sjh", expcontent["SJH"]);
                jappt.Add("chsInput2206", ChsInput2206);

                redis.Set(redisbuskey, jappt, DateTime.Now.AddMinutes(120), 7);

                DataTable dtbody = RtnResultDatatable("0", "success");

                DataTable dtpreorder = new DataTable();
                dtpreorder.Columns.Add("resultCode");
                dtpreorder.Columns.Add("resultMessage");
                dtpreorder.Columns.Add("uldFeeInfoStr");
                dtpreorder.Columns.Add("payOrderStr");

                DataRow dr = dtpreorder.NewRow();
                dr["resultCode"] = preorder.resultCode;
                dr["resultMessage"] = preorder.resultMessage;
                dr["uldFeeInfoStr"] = preorder.uldFeeInfoStr.ToString();
                dr["payOrderStr"] = preorder.payOrderStr.ToString();

                dtpreorder.Rows.Add(dr);

                ds.Tables.Add(dtbody.Copy());
                ds.Tables.Add(dtpreorder.Copy());
                return ds;
            }
            else
            {
                DataTable dtbody = RtnResultDatatable("1", preorder.resultMessage);
                ds.Tables.Add(dtbody.Copy());

                return ds;
            }
        }

        public DataSet NanJingMenZhen(Dictionary<string, string> dic)
        {
            DataSet ds = new DataSet();

            DataTable dtbus = new DataTable();
            dtbus.Columns.Add("resultCode");
            dtbus.Columns.Add("resultMessage");

            DataRow dr = dtbus.NewRow();
            dr["resultCode"] = "1";
            dr["resultMessage"] = "请至人工窗口退费";
            dtbus.Rows.Add(dr);

            DataTable dtbody = RtnResultDatatable("1", "请至人工窗口退费");
            ds.Tables.Add(dtbody.Copy());
            ds.Tables.Add(dtbus.Copy());

            return ds;
        }

        private DataSet QUERYINVOICE(Dictionary<string, string> external)
        {
            string payOrdId = external["payOrdId"];
            string medOrgOrd = external["medOrgOrd"];

            string strsql = "select reg_id,payid,medorgord from jsyby_ordertran where payordid=@payordid";

            MySqlParameter[] spa = new MySqlParameter[] { new MySqlParameter("@payordid", payOrdId) };

            DataTable dttran = DbHelperMySQL.Query(strsql, spa).Tables[0];

            if (dttran == null || dttran.Rows.Count == 0)
            {
                XmlDocument xmlerror = QHXmlMode.GetBaseXml("QUERYINVOICE", "");
                QHXmlMode.GetReturnXml(xmlerror, "1", "没有找到支付信息");

                return XMLHelper.X_GetXmlData(xmlerror, "ROOT/BODY");
            }
            string payId = dttran.Rows[0]["payid"].ToString();
            string reg_id = dttran.Rows[0]["reg_id"].ToString();

            //发票类型:1、挂号2、门诊收费3、住院结算
            string InvoiceType = "";

            string InvoiceNo = "";
            string ClinicCode = "";
            string invoiceurl = "";

            if (string.IsNullOrEmpty(payId))
            {
                string strsqlreg = "select * from register_pay where REG_ID=@REG_ID";

                MySqlParameter[] spareg = new MySqlParameter[] { new MySqlParameter("@REG_ID", reg_id) };

                DataTable dtreg = DbHelperMySQL.Query(strsqlreg, spareg).Tables[0];

                InvoiceType = "1";
                InvoiceNo = dtreg.Rows[0]["APPT_SN"].ToString();
                ClinicCode = dtreg.Rows[0]["HOS_SN"].ToString();

                invoiceurl = "https://inv.jss.com.cn/fp2/PjbFsy1ojnLp7SEo9Zqsj6B1rqlQ8Usmea9WxloHBazjKfMCuIbRb6OatL_sp97H4g6ASwsn4w6LC3NU_anJKQ.pdf";
            }
            else
            {
                string strsqlpay = "select * from opt_pay where pay_id=@pay_id";

                MySqlParameter[] spapay = new MySqlParameter[] { new MySqlParameter("@pay_id", payId) };

                DataTable dtreg = DbHelperMySQL.Query(strsqlpay, spapay).Tables[0];

                InvoiceType = "2";
                InvoiceNo = dtreg.Rows[0]["RCPT_NO"].ToString();
                ClinicCode = dtreg.Rows[0]["HOS_REG_SN"].ToString();
                invoiceurl = "https://inv.jss.com.cn/fp2/KAr_KhWYRkf8juVUKk1p23VeXKjFuLjx8I5gUCoyi5i8-W2SxRIBMNcNBMcuMs5-MrOe0h47W5ANM_7wKxBoCw.pdf";
            }

            XmlDocument xmldoc = RtnSucXml("QUERYINVOICE", "");

            XMLHelper.X_XmlInsertNode(xmldoc, "ROOT/BODY", "INVOICENUM", InvoiceNo);
            XMLHelper.X_XmlInsertNode(xmldoc, "ROOT/BODY", "INVOICECODE", ClinicCode);
            XMLHelper.X_XmlInsertNode(xmldoc, "ROOT/BODY", "INVOICEURL", invoiceurl);
            XMLHelper.X_XmlInsertNode(xmldoc, "ROOT/BODY", "STATUS", "");

            return XMLHelper.X_GetXmlData(xmldoc, "ROOT/BODY");
        }

        #endregion 南京医保云
    }
}