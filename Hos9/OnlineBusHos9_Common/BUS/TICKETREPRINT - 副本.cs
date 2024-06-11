using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_Common.HISModels;
using System;
using System.Collections.Generic;

namespace OnlineBusHos9_Common.BUS
{
    internal class TICKETREPRINT
    {
        public static string B_TICKETREPRINT(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                Model.TICKETREPRINT.TICKETREPRINT_IN _in = JsonConvert.DeserializeObject<Model.TICKETREPRINT.TICKETREPRINT_IN>(json_in);
                Model.TICKETREPRINT.TICKETREPRINT_OUT _out = new Model.TICKETREPRINT.TICKETREPRINT_OUT();

                var db = new DbMySQLZZJ().Client;

                if (string.IsNullOrEmpty(_in.TYPE))
                {
                    _in.TYPE = "1";
                }

                if (_in.TYPE == "1")
                {
                    int SERIAL_NO = 0;//预约标识
                    if (!PubFunc.GetSysID("TICKETREPRINT", out SERIAL_NO))
                    {
                        dataReturn.Code = 1;
                        dataReturn.Msg = "取[TICKETREPRINT]系统ID失败";
                        dataReturn.Param = JsonConvert.SerializeObject(_out);
                        goto EndPoint;
                    }
                    SqlSugarModel.Ticketreprint model = new SqlSugarModel.Ticketreprint();
                    model.SERIAL_NO = SERIAL_NO;
                    model.HOS_ID = _in.HOS_ID;
                    model.BIZ_TYPE = _in.PT_TYPE;
                    model.HOS_SN = _in.DJ_ID;
                    model.TEXT = _in.TEXT;
                    model.lTERMINAL_SN = _in.LTERMINAL_SN;
                    model.NOW = DateTime.Now;
                    model.SFZ_NO = _in.SFZ_NO;
                    model.print_times = 1;
                    int row = db.Insertable(model).ExecuteCommand();
                }
                else if (_in.TYPE == "2")
                {
                    int TICKETREPRINTDAYS = 7;
                    int TICKETREALLOWPRINTTIMES = 2;//允许打印次数
                    SqlSugarModel.SysConfig model = db.Queryable<SqlSugarModel.SysConfig>().Where(t => t.HOS_ID == _in.HOS_ID && t.config_key == "TICKETREPRINTDAYS").First();
                    if (model != null)
                    {
                        TICKETREPRINTDAYS = FormatHelper.GetInt(model.config_value);
                    }
                    model = db.Queryable<SqlSugarModel.SysConfig>().Where(t => t.HOS_ID == _in.HOS_ID && t.config_key == "TICKETREALLOWPRINTTIMES").First();
                    if (model != null)
                    {
                        TICKETREALLOWPRINTTIMES = FormatHelper.GetInt(model.config_value);
                    }
                    //var list = db.Queryable<SqlSugarModel.Ticketreprint>().Where(t => t.HOS_ID == _in.HOS_ID && t.BIZ_TYPE == _in.PT_TYPE && t.SFZ_NO == _in.SFZ_NO && SqlFunc.Between(t.NOW, DateTime.Now.AddDays(-1 * TICKETREPRINTDAYS), DateTime.Now)).ToList();
                    _out.ITEMLIST = new List<Model.TICKETREPRINT.ITEM>();

                    //foreach (var ptitem in list)
                    //{
                    //    Model.TICKETREPRINT.ITEM item = new Model.TICKETREPRINT.ITEM();
                    //    item.CAN_PRINT = ptitem.print_times >= TICKETREALLOWPRINTTIMES ? "0" : "1";
                    //    item.DJ_ID = ptitem.HOS_SN;
                    //    item.TEXT = ptitem.TEXT;
                    //    item.PRINT_TIMES = ptitem.print_times.ToString();
                    //    _out.ITEMLIST.Add(item);
                    //}

                    if (_in.CARD_TYPE=="1")
                    {
                        _in.HOSPATID = _in.YLCARD_NO;
                    }
         
                    if (_in.PT_TYPE == "p1")
                    {
                        HISModels.T5203.input t5203 = new HISModels.T5203.input()
                        {
                            zhengJianHM = _in.SFZ_NO,// 证件号码        证件号码
                            hospitalId = "320282466455146",
                            yeWuLX = "5203",// 业务类型        业务类型(5203：获取预约记录)
                            bingRenID = _in.HOSPATID,// 病人ID        病人ID
                            bingRenXM = "",// 病人姓名        病人姓名
                        };
                        PushServiceResult<List<T5203.data>> result5203 = HerenHelper<List<T5203.data>>.pushService("5203-QHZZJ", JsonConvert.SerializeObject(t5203));

                        if (result5203.code == 1 && result5203.data.Count > 0)
                        {
                            foreach (var recordinfo in result5203.data)
                            {
                                if (recordinfo.zhuangTai != "2"&& recordinfo.zhuangTai != "3")
                                {
                                    continue;
                                }
                                Model.TICKETREPRINT.TEXTDATA text = new Model.TICKETREPRINT.TEXTDATA()
                                {
                                    PAT_NAME = recordinfo.name,
                                    AGE = recordinfo.age.Replace("岁",""),

                                    SEX = recordinfo.sex,
                                    SFZ_NO = _in.SFZ_NO,
                                    YLCARD_NO = "",
                                    MEDFEE_SUMAMT = "",
                                    PSN_TYPE="",
                                    ACCT_PAY = recordinfo.geRenZHZF,
                                    FUND_PAY_SUMAMT = recordinfo.yiBaoTCZF,
                                    PSN_CASH_PAY = recordinfo.xianJinZF,
                                    OTH_PAY=recordinfo.qiTaZF,
                                    ACCT_MULAID_PAY=recordinfo.gongJiJinZF,
                                    MDTRT_ID = "",
                                    SETL_ID = "",
                                    YLLB = "",
                                    DISE_NAME = "",
                                    BALC = recordinfo.zhangHuYE,
                                    JEALL = recordinfo.heji,
                                    DERATE_REASON="",
                                    APPT_PAY = recordinfo.zhenLiaoFei,
                                    CASH_JE = recordinfo.xianJinZF,
                                    DEPT_NAME = recordinfo.keShiMC,
                                    DOC_NAME = recordinfo.yiShengXM,
                                    APPT_PLACE = recordinfo.daoZhenXX,
                                    APPT_TIME = recordinfo.jiuZhenRQ + " " + recordinfo.jiuZhenSJ,
                                    APPT_ORDER = recordinfo.guaHaoXH,
                                    SCH_TYPE = recordinfo.clinicType,
                                    HOS_SNAPPT = recordinfo.yiBaoLX,
                                    BIZCODE = "",
                                    RCPT_NO = "",
                                    RCPT_URL=recordinfo.dianZiFB,
                                    DEAL_TIME = recordinfo.shouFeiRQ,
                                    OPER_TIME = recordinfo.shouFeiRQ,
                                    DEAL_TYPE = recordinfo.zhiFuFS,
                                    DJ_ID = recordinfo.yeWuLSH,
                                    FEE_TYPE = recordinfo.yiBaoLX,
                                    OPT_SN = _in.HOSPATID,
                                    SJH = recordinfo.yeWuLSH,
                                    HOS_REG_SN = "",
                                    HOS_PAY_SN = "",
                                    PAY_QR_OPT = "",
                                    USER_ID = recordinfo.shouFeiYuan,
                                    LTERMINAL_SN = _in.LTERMINAL_SN,
                                    SLIPMB_TYPE = recordinfo.yiBaoLX.Contains("医保") ? "22-0" : "21-0",
                                    HOSPATID = recordinfo.bingRenID,
                                    BLH = recordinfo.bingRenID,
                                    HOS_NO = recordinfo.guaHaoID,
                                    BUS_TYPE="",
                                    MED_TYPE="",
                                    INSUTYPE="",
                                    PSN_INSU_DATE="",
                                    INHOSP_STAS="",
                                    PSN_NO="",
                                    CVLSERV_FLAG="",
                                    PSN_INSU_STAS="",
                                    OPSP_BALC="",
                                    OPT_POOL_BALC="",
                                    BED_NO = "",
                                    dtmx = ""
                                };
                                Model.TICKETREPRINT.ITEM item = new Model.TICKETREPRINT.ITEM();
                                item.CAN_PRINT = "1";//ptitem.print_times >= TICKETREALLOWPRINTTIMES ? "0" :
                                item.JEALL = recordinfo.zhenLiaoFei;
                                item.DEPT_NAME = recordinfo.keShiMC;
                                item.DJ_TIME = recordinfo.jiuZhenRQ;
                                item.PT_TYPE_NAME= "挂号";
                                item.DJ_ID = recordinfo.daoZhenID;
                                item.TEXT = JsonConvert.SerializeObject(text);
                                item.PRINT_TIMES = "1";
                                _out.ITEMLIST.Add(item);


                            }
                        }
                    }

                    if (_in.PT_TYPE == "p2")
                    {
                        HISModels.T5207.input t5207 = new T5207.input()
                        {
                            zhengJianHM = _in.SFZ_NO,// 证件号码        证件号码

                            kaiShiSJ = DateTime.Now.AddDays(-1 * TICKETREPRINTDAYS).ToString("yyyy-MM-dd"),//  查询开始时间      查询开始时间
                            jieShuSJ = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"),//  查询结束时间      查询结束时间
                            yeWuLX = "5207",// 业务类型        业务类型(5207：查询收费记录)
                            bingRenXM = "",// 病人姓名        病人姓名
                            bingRenID=_in.HOSPATID,
                            chuShengRQ = "",// 出生日期        出生日期
                            hospitalId = "320282466455146",// 医院ID        医院ID
                            xingBieMC = "",//  性别名称        性别名称
                        };
                        PushServiceResult<List<T5207.data>> result5207 = HerenHelper<List<T5207.data>>.pushService("5207-QHZZJ", JsonConvert.SerializeObject(t5207));
                        if (result5207.code == 1 && result5207.data.Count > 0)
                        {
                            foreach (var masterinfodata in result5207.data)
                            {
                                //缴费凭条内容
                                Model.TICKETREPRINT.TEXTDATA text = new Model.TICKETREPRINT.TEXTDATA()
                                {
                                    PAT_NAME = masterinfodata.name,
                                    AGE = masterinfodata.age.Replace("岁", ""),
                                    SEX = masterinfodata.sex,
                                    SFZ_NO = _in.SFZ_NO,
                                    YLCARD_NO = "",
                                    MEDFEE_SUMAMT = "",
                                    PSN_TYPE = "",
                                    ACCT_PAY = masterinfodata.geRenZHZF,
                                    FUND_PAY_SUMAMT = masterinfodata.yiBaoTCZF,
                                    PSN_CASH_PAY = masterinfodata.xianJinZF,
                                    OTH_PAY = masterinfodata.qiTaZF,
                                    ACCT_MULAID_PAY = masterinfodata.gongJiJinZF,
                                    MDTRT_ID = "",
                                    SETL_ID = "",
                                    YLLB = "",
                                    DISE_NAME = "",
                                    BALC = masterinfodata.zhangHuYE,
                                    JEALL = masterinfodata.heji,
                                    DERATE_REASON = "",
                                    APPT_PAY = masterinfodata.zongJinE,
                                    CASH_JE = masterinfodata.xianJinZF,
                                    DEPT_NAME = masterinfodata.keShiMC,
                                    DOC_NAME = masterinfodata.yiShengXM,
                                    APPT_PLACE = masterinfodata.daoZhenXX,
                                    APPT_TIME = "",
                                    APPT_ORDER = "",
                                    SCH_TYPE = "",
                                    HOS_SNAPPT = "",
                                    BIZCODE = "",
                                    RCPT_NO = masterinfodata.faPiaoHM,
                                    RCPT_URL = masterinfodata.dianZiFB,
                                    DEAL_TIME = masterinfodata.shouFeiRQ,
                                    OPER_TIME = masterinfodata.shouFeiRQ,
                                    DEAL_TYPE = masterinfodata.zhiFuFS,
                                    DJ_ID = masterinfodata.yeWuLSH,
                                    FEE_TYPE = masterinfodata.yiBaoLX,
                                    OPT_SN = _in.HOSPATID,
                                    SJH = masterinfodata.yeWuLSH,
                                    HOS_REG_SN = "",
                                    HOS_PAY_SN = "",
                                    PAY_QR_OPT = "",
                                    USER_ID =  masterinfodata.shouFeiYuan,
                                    LTERMINAL_SN = _in.LTERMINAL_SN,
                                    SLIPMB_TYPE = masterinfodata.yiBaoLX.Contains("医保") ? "32-0" : "31-0",
                                    HOSPATID = masterinfodata.bingRenID,
                                    BLH = masterinfodata.bingRenID,
                                    HOS_NO = masterinfodata.shouFeiID,
                                    BUS_TYPE = "",
                                    MED_TYPE = "",
                                    INSUTYPE = "",
                                    PSN_INSU_DATE = "",
                                    INHOSP_STAS = "",
                                    PSN_NO = "",
                                    CVLSERV_FLAG = "",
                                    PSN_INSU_STAS = "",
                                    OPSP_BALC = "",
                                    OPT_POOL_BALC = "",
                                    BED_NO = "",
                                    GUID_INFO=masterinfodata.daoZhenXX,
                                    dtmx = ""
                                };

                                List<Model.TICKETREPRINT.DAMX> damxlist = new List<Model.TICKETREPRINT.DAMX>();
                                foreach (var detail in masterinfodata.shouFeiGLJEList)
                                {
                                    foreach (var mingx in detail.mingXiJEs)
                                    {
                                        Model.TICKETREPRINT.DAMX damx = new Model.TICKETREPRINT.DAMX()
                                        {
                                            DAID = "",
                                            DATIME = "",
                                            ITEM_ID = mingx.xiangMUDM,
                                            ITEM_NAME = mingx.xiangMuMC,
                                            AMOUNT = mingx.jinE,
                                            AUT_NAME = mingx.danWei,
                                            CAMTALL = mingx.shuLiang,
                                            PRICE = "",
                                            RATE = ""
                                        };

                                        damxlist.Add(damx);
                                    }
                                }
                                text.dtmx = JsonConvert.SerializeObject(damxlist);

                                Model.TICKETREPRINT.ITEM item = new Model.TICKETREPRINT.ITEM();
                                item.CAN_PRINT = "1";//ptitem.print_times >= TICKETREALLOWPRINTTIMES ? "0" :

                                item.JEALL = masterinfodata.zongJinE;
                                item.DEPT_NAME = masterinfodata.keShiMC;
                                item.DJ_TIME = masterinfodata.shouFeiRQ;
                                item.PT_TYPE_NAME = "门诊缴费";
                                item.DJ_ID = masterinfodata.shouFeiID;
                                item.TEXT = JsonConvert.SerializeObject(text);
                                item.PRINT_TIMES = "1";
                                _out.ITEMLIST.Add(item);

                                SqlSugarModel.Ticketreprint ticketreprint = new SqlSugarModel.Ticketreprint()
                                {
                                    HOS_ID = _in.HOS_ID,
                                    SFZ_NO = _in.SFZ_NO,
                                    HOS_SN = masterinfodata.shouFeiID,
                                    BIZ_TYPE = "p2",
                                    TEXT = "|",
                                    lTERMINAL_SN = "",
                                    NOW = DateTime.Now,
                                    print_times = 1
                                };

                                db.Saveable(ticketreprint);
                            }
                        }
                    }
                }
                else if (_in.TYPE == "3")
                {
                    var model = db.Queryable<SqlSugarModel.Ticketreprint>().Where(t => t.HOS_ID == _in.HOS_ID && t.SFZ_NO == _in.SFZ_NO && t.HOS_SN == _in.DJ_ID && t.BIZ_TYPE == _in.PT_TYPE).First();
                    model.print_times++;
                    db.Updateable(model);
                }
                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
                dataReturn.Param = JsonConvert.SerializeObject(_out);
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

        public static string GetAge(DateTime dtBirthday, DateTime dtNow)
        {
            string strAge = string.Empty; // 年龄的字符串表示
            int intYear = 0; // 岁
            int intMonth = 0; // 月
            int intDay = 0; // 天

            // 计算天数
            intDay = dtNow.Day - dtBirthday.Day;
            if (intDay < 0)
            {
                dtNow = dtNow.AddMonths(-1);
                intDay += DateTime.DaysInMonth(dtNow.Year, dtNow.Month);
            }

            // 计算月数
            intMonth = dtNow.Month - dtBirthday.Month;
            if (intMonth < 0)
            {
                intMonth += 12;
                dtNow = dtNow.AddYears(-1);
            }

            // 计算年数
            intYear = dtNow.Year - dtBirthday.Year;

            // 格式化年龄输出
            if (intYear >= 1) // 年份输出
            {
                strAge = intYear.ToString();//+ "岁"
            }

            if (intMonth > 0 && intYear <= 5) // 五岁以下可以输出月数
            {
                strAge += intMonth.ToString() + "月";
            }

            if (intDay >= 0 && intYear < 1) // 一岁以下可以输出天数
            {
                if (strAge.Length == 0 || intDay > 0)
                {
                    strAge += intDay.ToString() + "日";
                }
            }

            return strAge;
        }
    }
}