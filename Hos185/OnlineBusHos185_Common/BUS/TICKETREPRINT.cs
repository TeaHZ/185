using CommonModel;
using Hos185_His;
using Hos185_His.Models;
using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace OnlineBusHos185_Common.BUS
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
                    var list = db.Queryable<SqlSugarModel.Ticketreprint>().Where(t => t.HOS_ID == _in.HOS_ID && t.BIZ_TYPE == _in.PT_TYPE && t.SFZ_NO == _in.SFZ_NO && SqlFunc.Between(t.NOW, DateTime.Now.AddDays(-1 * TICKETREPRINTDAYS), DateTime.Now)).ToList();
                    _out.ITEMLIST = new List<Model.TICKETREPRINT.ITEM>();


                    TKHelper main = new TKHelper();

                    string inputjson = "";

                    if (_in.PT_TYPE == "p1")
                    {
                        Hos185_His.Models.queryRecord record = new queryRecord()
                        {
                            cardNo = _in.HOSPATID,  //医院内部就诊卡号
                            clinicCode = "",  //挂号流水号
                            endTime = DateTime.Now.ToString("yyyy-MM-dd"),  //结束时间
                            idCardNo = "",  //证件号
                            idCardType = "",  //证件类型
                            invoiceNo = "",  //发票号
                            isPendPay = "",  //是否只显示有待缴费的记录，0，不显示,1，显示
                            mcardNo = "",  //绑定的医疗证号
                            mcardNoType = "",  //绑定的医疗证类型
                            payCode = "",  //pay_id查询代码
                            paySwitch = "",  //pay_id查询开关
                            pendPayDays = 0,  //显示有待缴费的记录天数,默认7天
                            specialDeptDays = 0,  //特殊权限部门显示天数,默认30天
                            specialDeptFlag = "",  //特殊权限部门
                            //2024-4-25要求挂号缴费开始时间区分开
                            startTime = DateTime.Now.AddDays(-1 * TICKETREPRINTDAYS).ToString("yyyy-MM-dd"),  //开始时间
                            transType = "",  //交易类型
                            validDays = "",  //有效天数
                            validFlag = "",  //挂号状态
                            ynSee = ""  //看诊类型
                        };

                        inputjson = Newtonsoft.Json.JsonConvert.SerializeObject(record);
                        Hos185_His.Models.Output<List<Hos185_His.Models.queryRecordData>>
 outputrecord = main.CallServiceAPI<List<Hos185_His.Models.queryRecordData>>("/hisbooking/register/queryRecord", inputjson);

                        if (!string.IsNullOrEmpty(_in.PRINT_TYPE) && _in.PRINT_TYPE == "1" && !string.IsNullOrEmpty(_in.DJ_ID))
                        {
                            outputrecord.data = outputrecord.data.FindAll(x => x.queryid == _in.DJ_ID);

                        }
                        //要加下07011006 儿科(呼吸专病门诊)
                        //07011007 儿科（内分泌专病门诊）
                        string[] deptQD = { "07011007", "07011006", "07011001", "20012002", "20012003", "05012001", "0502201", "11012001", "10012001", "12012001", };

                        if (outputrecord.code == 0)
                        {


                            foreach (var recordinfo in outputrecord.data)
                            {
                                Model.TICKETREPRINT.TEXTDATA text = new Model.TICKETREPRINT.TEXTDATA()
                                {
                                    PAT_NAME = recordinfo.name,
                                    AGE = GetAge(DateTime.Parse(recordinfo.birthday), DateTime.Now),
                                    SEX = recordinfo.sexCodeName,
                                    SFZ_NO = recordinfo.cardNo,
                                    YLCARD_NO = recordinfo.markNo,
                                    MEDFEE_SUMAMT = recordinfo.regFee,
                                    ACCT_PAY = recordinfo.medicalInsurancePay,
                                    FUND_PAY_SUMAMT = recordinfo.medicalInsurancePool,
                                    PSN_CASH_PAY = recordinfo.payCost,
                                    MDTRT_ID = "",
                                    SETL_ID = "",
                                    YLLB = "",
                                    DISE_NAME = "",
                                    BALC = recordinfo.medicalInsuranceLeft,
                                    JEALL = recordinfo.regFee,
                                    APPT_PAY = recordinfo.regFee,
                                    CASH_JE = recordinfo.payCost,
                                    DEPT_NAME = recordinfo.deptName,
                                    DOC_NAME = recordinfo.doctName,
                                    APPT_PLACE = recordinfo.seeAddress,
                                    APPT_TIME = recordinfo.seeDate,
                                    APPT_ORDER = deptQD.Contains(recordinfo.deptCode) ? "以现场签到为准" : recordinfo.seeNo,
                                    SCH_TYPE = recordinfo.regLevelName,
                                    HOS_SNAPPT = recordinfo.clinicCode,
                                    BIZCODE = "",
                                    RCPT_NO = recordinfo.invoiceNo,
                                    DEAL_TIME = recordinfo.regDate,
                                    OPER_TIME = recordinfo.regDate,
                                    DEAL_TYPE = recordinfo.payModeName,
                                    DJ_ID = recordinfo.clinicCode,
                                    FEE_TYPE = recordinfo.paykindName,
                                    OPT_SN = _in.HOSPATID,
                                    SJH = recordinfo.queryid,
                                    HOS_REG_SN = recordinfo.clinicCode,
                                    HOS_PAY_SN = recordinfo.clinicCode,
                                    PAY_QR_OPT = "https://apph5.ztejsapp.cn/aliMini/index.html?appId=2021002127624236&originalId=&source=S21&busId=PnNI9r6aPJMB5FN8ogcb1I0n0fkImf%2BBjD%2BJdyTogp4%3D",
                                    USER_ID = "QHZZJ",
                                    LTERMINAL_SN = _in.LTERMINAL_SN,
                                    SLIPMB_TYPE = recordinfo.paykindCode == "1" ? "11-0" : "12-0",
                                    HOSPATID = _in.HOSPATID,
                                    BLH = _in.HOSPATID,
                                    HOS_NO = recordinfo.clinicCode,
                                    BED_NO = "",
                                    dtmx = "",
                                    selfCare = recordinfo.selfCare,
                                    selfPay = recordinfo.selfPay,
                                    commercialInsurancePay = recordinfo.commercialInsurancePay,
                                    medicalInsurancePool = recordinfo.medicalInsurancePool,
                                    discountAmount = recordinfo.discountAmount,
                                    bookAmount = recordinfo.bookAmount,
                                    triageLevel = recordinfo.triageLevel,
                                    message = recordinfo.message,
                                    statusCode = recordinfo.statusCode,
                                    paykindName=recordinfo.paykindName,
                                    otherFundPay = recordinfo.otherFundPay,
                                    identityId = recordinfo.identityId,
                                    ownCost = recordinfo.checkAmount,
                                    beginTime = recordinfo.beginTime,
                                    queueCallNumberFlag = recordinfo.queueCallNumberFlag,
                                    cashAmount = recordinfo.cashAmount,
                                    medicalInsurancePay = recordinfo.medicalInsurancePay,
                                    checkAmount = recordinfo.cashAmount,
                                };
                                Model.TICKETREPRINT.ITEM item = new Model.TICKETREPRINT.ITEM();
                                item.CAN_PRINT = "1";//ptitem.print_times >= TICKETREALLOWPRINTTIMES ? "0" :

                                item.DJ_ID = recordinfo.clinicCode;
                                item.TEXT = JsonConvert.SerializeObject(text);
                                item.PRINT_TIMES = "1";
                                _out.ITEMLIST.Add(item);

                                SqlSugarModel.Ticketreprint ticketreprint = new SqlSugarModel.Ticketreprint()
                                {
                                    HOS_ID = _in.HOS_ID,
                                    SFZ_NO = _in.SFZ_NO,
                                    HOS_SN = recordinfo.clinicCode,
                                    BIZ_TYPE = "p1",
                                    TEXT = JsonConvert.SerializeObject(text),
                                    lTERMINAL_SN = "",
                                    NOW = DateTime.Now,
                                    print_times = 1
                                };

                                db.Saveable(ticketreprint);
                            }
                        }
                    }
                    else if (_in.PT_TYPE == "p2")
                    {



                        Hos185_His.Models.masterInfoList masterinfo = new Hos185_His.Models.masterInfoList()
                        {
                            cardNo = _in.HOSPATID,//  卡号
                            cardType = "01",// 卡类型
                            clinicCode = "",//  挂号流水号
                            endTime = DateTime.Now.ToString("yyyy-MM-dd 23:59:59"),//结束时间
                            exchangeFlag = "",//    自助机、预约补打标志
                            isEinvoiceList = false,// true-只查门诊缴费发票列表，false-查门诊列表
                            isPe = false,//  true-只查体检，false-只查非体检
                            payFlag = "",// 支付标志-0划价 1收费 3预收费团体体检 4 药品预审核

                            //2024-4-25要求挂号缴费开始时间区分开挂号3天取表，缴费90天暂写死
                            //startTime = DateTime.Now.AddDays(-1 * TICKETREPRINTDAYS).ToString("yyyy-MM-dd 00:00:00"),// 开始时间
                            startTime = DateTime.Now.AddDays(-1 * 90).ToString("yyyy-MM-dd 00:00:00"),// 开始时间

                            type = "1",// 发票已开未开类型.1查 未开发票，2查已开发票（查已支付列表不传）
                            validFlag = "1",//   挂号标志-0退费,1有效,2作废
                        };

                        inputjson = Newtonsoft.Json.JsonConvert.SerializeObject(masterinfo);

                        Hos185_His.Models.Output<List<Hos185_His.Models.masterInfoListData>>
              output = main.CallServiceAPI<List<Hos185_His.Models.masterInfoListData>>("/hischargesinfo/outPatient/masterInfoList", inputjson);



                        if (output.code == 0)
                        {
                            if (!string.IsNullOrEmpty(_in.PRINT_TYPE) && _in.PRINT_TYPE == "1")
                            {
                                output.data = output.data.FindAll(x => x.jssjh == _in.DJ_ID);

                            }

                            foreach (var masterinfodata in output.data)
                            {
                                //缴费凭条内容
                                Model.TICKETREPRINT.TEXTDATA text = new Model.TICKETREPRINT.TEXTDATA()
                                {
                                    PAT_NAME = masterinfodata.name,
                                    AGE = masterinfodata.age,
                                    SEX = masterinfodata.sexCode,
                                    SFZ_NO = _in.SFZ_NO,
                                    YLCARD_NO = masterinfodata.markNo,
                                    //医保范围内凭条设计使用该字段
                                    MEDFEE_SUMAMT = masterinfodata.compliesInsuranceCatalog,
                                    ACCT_PAY = masterinfodata.medicalInsurancePay,
                                    FUND_PAY_SUMAMT = masterinfodata.pubcost,
                                    PSN_CASH_PAY = masterinfodata.ownCost,
                                    MDTRT_ID = "",
                                    SETL_ID = "",
                                    YLLB = masterinfodata.pactName,
                                    DISE_NAME = "",
                                    BALC = masterinfodata.medicalInsuranceLeft,
                                    JEALL = masterinfodata.totCost,
                                    APPT_PAY = masterinfodata.totCost,
                                    CASH_JE = masterinfodata.ownCost,
                                    DEPT_NAME = masterinfodata.doctDeptName,
                                    DOC_NAME = masterinfodata.doctName,
                                    APPT_PLACE = "",
                                    APPT_TIME = "",
                                    APPT_ORDER = "",
                                    SCH_TYPE = "",
                                    HOS_SNAPPT = "",
                                    BIZCODE = "",
                                    RCPT_NO = "",
                                    DEAL_TIME = masterinfodata.feeDate,
                                    OPER_TIME = masterinfodata.operDate,
                                    DEAL_TYPE = masterinfodata.payway,
                                    DJ_ID = masterinfodata.clinicCode,
                                    FEE_TYPE = masterinfodata.pactName,
                                    OPT_SN = _in.HOSPATID,
                                    SJH = masterinfodata.jssjh,
                                    HOS_REG_SN = "",
                                    HOS_PAY_SN = "",
                                    PAY_QR_OPT = "https://apph5.ztejsapp.cn/aliMini/index.html?appId=2021002127624236&originalId=&source=S21&busId=PnNI9r6aPJMB5FN8ogcb1I0n0fkImf%2BBjD%2BJdyTogp4%3D",
                                    USER_ID = "QHZZJ",
                                    LTERMINAL_SN = _in.LTERMINAL_SN,
                                    SLIPMB_TYPE = masterinfodata.payKindCode == "01" ? "31-0" : "32-0",
                                    HOSPATID = _in.HOSPATID,
                                    BLH = _in.HOSPATID,
                                    HOS_NO = masterinfodata.clinicCode,
                                    BED_NO = "",

                                    insuranceOutside = masterinfodata.insuranceOutside,
                                    compliesInsuranceCatalog = masterinfodata.compliesInsuranceCatalog,//医保范围内
                                    selfCare = masterinfodata.selfCare,
                                    medicalInsurancePool = masterinfodata.medicalInsurancePool,
                                    selfPay = masterinfodata.selfPay,
                                    selfAmount = masterinfodata.selfAmount,
                                    civilServants = masterinfodata.civilServants,
                                    employeeIllness = masterinfodata.employeeIllness,
                                    residentIllness = masterinfodata.residentIllness,
                                    civilAssistance = masterinfodata.civilAssistance,
                                    commercialInsurancePay = masterinfodata.commercialInsurancePay,
                                    enterpriseSupple = masterinfodata.enterpriseSupple,//企业补充
                                    otherPay = masterinfodata.otherPay,
                                    emergencyTriageName = masterinfodata.emergencyTriageName,
                                    settlementType = masterinfodata.settlementType,//
                              
                                    discountAmount = masterinfodata.discountAmount,//优惠金额    string
                                    bookAmount = masterinfodata.bookAmount,// 记账金额    string
                                    triageLevel = masterinfodata.triageLevel,//分诊等级    string
                                    identityId = masterinfodata.identityId,// 身份标识    string
                                    otherFundPay = masterinfodata.otherFundPay,//   其他基金支付  string
                                    cashAmount= masterinfodata.cashAmount,
                                    feeMemo = masterinfodata.feeMemo
                                };

                                Hos185_His.Models.masterDetailList detailList = new masterDetailList()
                                {
                                    cancelFlag = "",//  0退费，1正常，2重打，3注销
                                    cardNo = _in.HOSPATID,// 卡号
                                    costType = "",// 费用状态(三个 状态 ， 1 处方，2 检查，3处方+检查)
                                    exchangeFlag = "",//   自助机、预约补打标志

                                    jssjh = masterinfodata.jssjh,//  卫宁上线后需新增的字段
                                    payFlag = "",//支付标志-0划价 1收费 3预收费团体体检 4 药品预审核
                                    validFlag = "",//  挂号标志-0退费,1有效,2作废
                                    ybTypePactCode = "",// 查询医保类型时用到的pactcode，如果没有设置就使用实际的pactcode
                                };

                                inputjson = Newtonsoft.Json.JsonConvert.SerializeObject(detailList);

                                Hos185_His.Models.Output<List<Hos185_His.Models.masterDetailListData>>
                      outputdeital = main.CallServiceAPI<List<Hos185_His.Models.masterDetailListData>>("/hischargesinfo/outPatient/masterDetailList", inputjson);

                                List<Model.TICKETREPRINT.DAMX> damxlist = new List<Model.TICKETREPRINT.DAMX>();
                                foreach (var detail in outputdeital.data)
                                {
                                    Model.TICKETREPRINT.DAMX damx = new Model.TICKETREPRINT.DAMX()
                                    {
                                        DAID = "",
                                        DATIME = detail.feeDate,
                                        ITEM_ID = detail.itemCode,
                                        ITEM_NAME = detail.itemName,
                                        AMOUNT = detail.totalCost,
                                        AUT_NAME = detail.priceUnit,
                                        CAMTALL = detail.qty,
                                        PRICE = detail.unitPrice,
                                        RATE = detail.newItemRate
                                    };

                                    damxlist.Add(damx);
                                }
                                text.dtmx = JsonConvert.SerializeObject(damxlist);

                                Model.TICKETREPRINT.ITEM item = new Model.TICKETREPRINT.ITEM();
                                item.CAN_PRINT = "1";//ptitem.print_times >= TICKETREALLOWPRINTTIMES ? "0" :

                                item.DJ_ID = masterinfodata.clinicCode;
                                item.TEXT = JsonConvert.SerializeObject(text);
                                item.PRINT_TIMES = "1";
                                _out.ITEMLIST.Add(item);


                            }
                        }




                    }

                    dataReturn.Code = 0;
                    dataReturn.Msg = "SUCCESS";
                    dataReturn.Param = JsonConvert.SerializeObject(_out);

                    return JsonConvert.SerializeObject(dataReturn);


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
            else {

                strAge= intMonth.ToString() + "月"+ intDay.ToString() + "天";
            }


            //if (intMonth > 0 && intYear <= 5) // 五岁以下可以输出月数
            //{
            //    strAge += intMonth.ToString() + "月";
            //}

            //if (intDay >= 0 && intYear < 1) // 一岁以下可以输出天数
            //{
            //    if (strAge.Length == 0 || intDay > 0)
            //    {
            //        strAge += intDay.ToString() + "日";
            //    }
            //}

            return strAge;
        }
    }
}