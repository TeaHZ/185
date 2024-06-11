using CommonModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OnlineBusHos9_Common.HISModels;
using System;
using System.Collections.Generic;
using System.IO;
using static OnlineBusHos9_Common.Model.TICKETREPRINT;

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
                    _out.ITEMLIST = new List<Model.TICKETREPRINT.ITEM>();

                    if (_in.CARD_TYPE == "1")
                    {
                        _in.HOSPATID = _in.YLCARD_NO;
                    }

                    //正常打印
                    if (_in.PRINT_TYPE == "1")
                    {
                        if (_in.PT_TYPE == "p4")//出院结算
                        {
                            var rcptnoListJson = _in.RCPTNOLIST.ToString();
                            var rcptnoList = JsonConvert.DeserializeObject<List<string>>(rcptnoListJson);

                            List<SLIP_FILE> ticketlist = new List<SLIP_FILE>();

                            foreach (var rcptno in rcptnoList)
                            {
                                HISModels.T5302.Input input = new HISModels.T5302.Input()
                                {
                                    rcptNo = rcptno
                                };

                                PushServiceResult<T5302.Data> result = HerenHelper<T5302.Data>.pushService("5302-QHZZJ", JsonConvert.SerializeObject(input));

                                if (result.code != 1)
                                {
                                    dataReturn.Code = 6;
                                    dataReturn.Msg = result.msg;
                                    dataReturn.Param = JsonConvert.SerializeObject(_out);
                                    goto EndPoint;
                                }
                                string base64String = result.data.base64;
                                #region
                                //string filePath = "D:\\PDFdownload\\PT\\" + rcptno + ".pdf";
                                ////TODO 
                                //string destinationFolderPath = "\\\\192.168.31.157\\PDFdownload\\PT"; // 共享文件夹的路径 多个paas 直接保存到共享文件夹上
                                //string fileName = rcptno + ".pdf"; // 文件名

                                //try
                                //{
                                //    SaveBase64AsPdf(base64String, destinationFolderPath, fileName);

                                //}
                                //catch (Exception ex)
                                //{
                                //    dataReturn.Code = 6;
                                //    dataReturn.Msg = "程序处理异常";
                                //    dataReturn.Param = ex.Message;
                                //}
                                //TODO
                                //ConvertBase64ToPdf(base64String, filePath);
                                #endregion
                                SLIP_FILE sftledata = new SLIP_FILE()
                                {
                                    SLIP_FILE_TYPE = "1",//1 base64 2 pdf地址 4 共享文件夹地址
                                    SLIP_FILE_URL = result.data.base64
                                    //SLIP_FILE_URL = filePath
                                    //SLIP_FILE_URL = @"\\192.168.31.157\PDFdownload\PT\" + rcptno + ".pdf"
                                    //SLIP_FILE_URL = "http://192.168.31.144:9099/PT/" + rcptno + ".pdf"
                                    //"http://192.168.31.144:9099/PT/230824000025.pdf" 通过nginx转发
                                };

                                ticketlist.Add(sftledata);
                            }

                            _out.TICKETLIST = ticketlist;
                            dataReturn.Code = 0;
                            dataReturn.Msg = "success";
                            dataReturn.Param = JsonConvert.SerializeObject(_out);
                            json_out = JsonConvert.SerializeObject(dataReturn);
                            return json_out;
                        }
                        #region 自助开单
                        //    else if(_in.PT_TYPE == "p21")
                        //    {

                        //        //JObject j6003 = new JObject
                        //        //{
                        //        //    { "rcptNo", _in.DJ_ID }

                        //        //};
                        //        //List<SLIP_FILE> ticketlist = new List<SLIP_FILE>();
                        //        //PushServiceResult<T6003.Data> result = HerenHelper<T6003.Data>.pushService("6003-QHZZJ", j6003.ToString());
                        //        //if (result.code != 1)
                        //        //{
                        //        //    dataReturn.Code = 6;
                        //        //    dataReturn.Msg = result.msg;
                        //        //    dataReturn.Param = JsonConvert.SerializeObject(_out);
                        //        //    goto EndPoint;
                        //        //}
                        //        //string base64String = result.data.base64;
                        //        //SLIP_FILE sftledata = new SLIP_FILE()
                        //        //{
                        //        //    SLIP_FILE_TYPE = "1",//1 base64 2 pdf地址 4 共享文件夹地址
                        //        //    SLIP_FILE_URL = result.data.base64

                        //        //};

                        //        //ticketlist.Add(sftledata);

                        //        //_out.ITEMLIST.AddRange(GetticketData(_in.DJ_ID));

                        //        //dataReturn.Code = 0;
                        //        //dataReturn.Msg = "success";
                        //        //dataReturn.Param = JsonConvert.SerializeObject(_out);
                        //        //json_out = JsonConvert.SerializeObject(dataReturn);
                        //        //return json_out;

                        //}
                        #endregion
                        else
                        {
                            #region PDF
                            //JObject j6003 = new JObject
                            //{
                            //     { "rcptNo", _in.DJ_ID }
                            //};
                            //List<SLIP_FILE> ticketlist = new List<SLIP_FILE>();
                            //PushServiceResult<T6003.Data> result = HerenHelper<T6003.Data>.pushService("6003-QHZZJ", j6003.ToString());
                            //if (result.code != 1)
                            //{
                            //    dataReturn.Code = 6;
                            //    dataReturn.Msg = result.msg;
                            //    dataReturn.Param = JsonConvert.SerializeObject(_out);
                            //    goto EndPoint;
                            //}
                            //string base64String = result.data.base64;
                            //SLIP_FILE sftledata = new SLIP_FILE()
                            //{
                            //    SLIP_FILE_TYPE = "1_1",//1 base64 2 pdf地址 4 共享文件夹地址
                            //    SLIP_FILE_URL = result.data.base64

                            //};

                            //ticketlist.Add(sftledata);
                            //_out.TICKETLIST = ticketlist;
                            #endregion
                            _out.ITEMLIST.AddRange(GetticketData(_in.DJ_ID));
                        }
                    }
                    else
                    {
                        //补打

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
                                    if (recordinfo.zhuangTai != "2" && recordinfo.zhuangTai != "3")
                                    {
                                        continue;
                                    }

                                    if (recordinfo.shouFeiYuan != "线上APP")
                                    {
                                        if (DateTime.Parse(recordinfo.jiuZhenRQ).Date != DateTime.Now.Date)
                                        {
                                            continue;
                                        }
                                    }

                                    Model.TICKETREPRINT.TEXTDATA text = new Model.TICKETREPRINT.TEXTDATA()
                                    {
                                        //PAT_NAME = recordinfo.name,
                                        PAT_NAME = recordinfo.name != null ? recordinfo.name : "",
                                        //AGE = recordinfo.age.Replace("岁", ""),
                                        AGE = recordinfo.age != null ? recordinfo.age.Replace("岁", "") : "",

                                        //SEX = recordinfo.sex,
                                        SEX = recordinfo.sex != null ? recordinfo.sex : "",
                                        SFZ_NO = _in.SFZ_NO,
                                        YLCARD_NO = "",
                                        MEDFEE_SUMAMT = "",
                                        PSN_TYPE = "",
                                        ACCT_PAY = recordinfo.geRenZHZF,
                                        FUND_PAY_SUMAMT = recordinfo.yiBaoTCZF,
                                        PSN_CASH_PAY = recordinfo.xianJinZF,
                                        OTH_PAY = recordinfo.qiTaZF,
                                        ACCT_MULAID_PAY = recordinfo.gongJiJinZF,
                                        MDTRT_ID = "",
                                        SETL_ID = "",
                                        YLLB = "",
                                        DISE_NAME = "",
                                        BALC = recordinfo.zhangHuYE,
                                        JEALL = recordinfo.heji,
                                        DERATE_REASON = recordinfo.feiYongBZ,
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
                                        RCPT_URL = recordinfo.dianZiFB,
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
                                        dtmx = ""
                                    };
                                    Model.TICKETREPRINT.ITEM item = new Model.TICKETREPRINT.ITEM();
                                    item.CAN_PRINT = "1";//ptitem.print_times >= TICKETREALLOWPRINTTIMES ? "0" :
                                    item.JEALL = recordinfo.zhenLiaoFei;
                                    item.DEPT_NAME = recordinfo.keShiMC;
                                    item.DJ_TIME = recordinfo.jiuZhenRQ;
                                    item.PT_TYPE_NAME = "挂号";
                                    item.DJ_ID = recordinfo.daoZhenID;
                                    item.TEXT = JsonConvert.SerializeObject(text);
                                    item.PRINT_TIMES = "1";
                                    _out.ITEMLIST.Add(item);
                                }
                            }
                        }
                        else
                        {

                            //JObject j6003 = new JObject
                            //{
                            //     { "rcptNo", _in.DJ_ID }
                            //};
                            //List<SLIP_FILE> ticketlist = new List<SLIP_FILE>();
                            //PushServiceResult<T6003.Data> result = HerenHelper<T6003.Data>.pushService("6003-QHZZJ", j6003.ToString());
                            //if (result.code != 1)
                            //{
                            //    dataReturn.Code = 6;
                            //    dataReturn.Msg = result.msg;
                            //    dataReturn.Param = JsonConvert.SerializeObject(_out);
                            //    goto EndPoint;
                            //}
                            //string base64String = result.data.base64;
                            //SLIP_FILE sftledata = new SLIP_FILE()
                            //{
                            //    SLIP_FILE_TYPE = "1",//1 base64 2 pdf地址 4 共享文件夹地址
                            //    SLIP_FILE_URL = result.data.base64

                            //};

                            //ticketlist.Add(sftledata);


                            JObject j52091 = new JObject();

                            j52091.Add("patientId", _in.HOSPATID);
                            j52091.Add("startDate", DateTime.Now.AddDays(-1 * TICKETREPRINTDAYS).ToString("yyyy-MM-dd"));
                            j52091.Add("endDate", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"));

                            PushServiceResult<List<TiketList>> result5209 = HerenHelper<List<TiketList>>.pushService("52091-QHZZJ", j52091.ToString());

                            foreach (var item in result5209.data)
                            {
                                _out.ITEMLIST.AddRange(GetticketData(item.rcptNo));
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

        //private static void ConvertBase64ToPdf(string base64String, string filePath)
        //{
        //    byte[] pdfBytes = Convert.FromBase64String(base64String);

        //    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        //    {
        //        fileStream.Write(pdfBytes, 0, pdfBytes.Length);
        //    }
        //}
        //private static void SaveBase64AsPdf(string base64String, string destinationFolderPath, string fileName)
        //{
        //    string destinationFilePath = Path.Combine(destinationFolderPath, fileName);

        //    // 将 Base64 字符串转换为字节数组
        //    byte[] fileBytes = Convert.FromBase64String(base64String);

        //    // 将字节数组保存为 PDF 文件
        //    File.WriteAllBytes(destinationFilePath, fileBytes);
        //}

        private static List<Model.TICKETREPRINT.ITEM> GetticketData(string DJ_ID)
        {
            List<Model.TICKETREPRINT.ITEM> ITEMLIST = new List<Model.TICKETREPRINT.ITEM>();
            JObject J5209 = new JObject();

            J5209.Add("rcptNos", DJ_ID);

            PushServiceResult<List<PrintData>> result5209 = HerenHelper<List<PrintData>>.pushService("5209-QHZZJ", J5209.ToString());
            if (result5209.code == 1 && result5209.data.Count > 0)
            {
                foreach (var masterinfodata in result5209.data)
                {
                    if (masterinfodata.operatorEmpName != "线上APP")
                    {
                        if (DateTime.Parse(masterinfodata.shouFeiRQ).Date != DateTime.Now.Date)
                        {
                            continue;
                        }
                    }
                    //缴费凭条内容
                    Model.TICKETREPRINT.TEXTDATA text = new Model.TICKETREPRINT.TEXTDATA()
                    {
                        PAT_NAME = masterinfodata.name,
                        AGE = masterinfodata.age.Replace("岁", ""),
                        SEX = masterinfodata.sex,
                        SFZ_NO = "",
                        YLCARD_NO = "",
                        MEDFEE_SUMAMT = "",
                        PSN_TYPE = "",
                        ACCT_PAY = masterinfodata.geRenZHZF,
                        FUND_PAY_SUMAMT = masterinfodata.yiBaoTCZF,
                        PSN_CASH_PAY = masterinfodata.xjzf.ToString(),
                        OTH_PAY = masterinfodata.qiTaZF,
                        ACCT_MULAID_PAY = masterinfodata.accountMulaidPay,
                        MDTRT_ID = "",
                        SETL_ID = "",
                        YLLB = "",
                        DISE_NAME = "",
                        BALC = masterinfodata.zhangHuYE,
                        JEALL = masterinfodata.totalCharges.ToString(),
                        DERATE_REASON = "",
                        APPT_PAY = masterinfodata.totalCharges.ToString(),
                        CASH_JE = masterinfodata.xjzf.ToString(),
                        DEPT_NAME = "",
                        DOC_NAME = masterinfodata.doctorName,
                        APPT_PLACE = masterinfodata.daoZhenXX,
                        APPT_TIME = masterinfodata.shouFeiRQ,
                        APPT_ORDER = "",
                        SCH_TYPE = "",
                        HOS_SNAPPT = "",
                        BIZCODE = "",
                        RCPT_NO = DJ_ID,
                        RCPT_URL = masterinfodata.einvoiceUrl,
                        DEAL_TIME = masterinfodata.shouFeiRQ,
                        OPER_TIME = masterinfodata.shouFeiRQ,
                        DEAL_TYPE = masterinfodata.moneyType,
                        DJ_ID = DJ_ID,
                        FEE_TYPE = masterinfodata.chargeType,
                        OPT_SN = "",
                        SJH = DJ_ID,
                        HOS_REG_SN = "",
                        HOS_PAY_SN = "",
                        PAY_QR_OPT = "",
                        USER_ID = masterinfodata.operatorEmpName,
                        LTERMINAL_SN = masterinfodata.operatorEmpName,
                        SLIPMB_TYPE = masterinfodata.chargeType.Contains("医保") ? "32-0" : "31-0",
                        HOSPATID = masterinfodata.patientId,
                        BLH = masterinfodata.patientId,
                        HOS_NO = DJ_ID,
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
                        GUID_INFO = masterinfodata.daoZhenXX,
                        dtmx = ""
                    };
                    List<Model.TICKETREPRINT.DAMX> damxlist = new List<Model.TICKETREPRINT.DAMX>();

                    int row_id = 0;

                    #region 检查

                    if (masterinfodata.examList.Count > 0)
                    {
                        //检查抬头：
                        Model.TICKETREPRINT.DAMX damx3 = new Model.TICKETREPRINT.DAMX()
                        {
                            ROW_ID = row_id,
                            DA_TYPE = "【检查】",
                            DAID = "",
                            DATIME = "",
                            ITEM_ID = "",
                            ITEM_NAME = "",
                            AMOUNT = "",
                            AUT_NAME = "",
                            CAMTALL = "",
                            PRICE = "",
                            RATE = ""
                        };
                        row_id++;
                        damxlist.Add(damx3);
                    }

                    //检查
                    foreach (var detail in masterinfodata.examList)
                    {
                        Model.TICKETREPRINT.DAMX damx = new Model.TICKETREPRINT.DAMX()
                        {
                            ROW_ID = row_id,
                            DA_TYPE = "",
                            DAID = "",
                            DATIME = "",
                            ITEM_ID = detail.itemName,
                            ITEM_NAME = detail.itemName,
                            AMOUNT = detail.charge.ToString(),
                            AUT_NAME = "",
                            CAMTALL = "",
                            PRICE = "",
                            RATE = ""
                        };
                        row_id++;
                        damxlist.Add(damx);
                        if (!string.IsNullOrEmpty(detail.place))
                        {
                            Model.TICKETREPRINT.DAMX damxnotice = new Model.TICKETREPRINT.DAMX()
                            {
                                ROW_ID = row_id,
                                DA_TYPE = "",
                                DAID = "",
                                DATIME = "",
                                ITEM_ID = "",
                                ITEM_NAME = " ╏ ※" + detail.notice.Trim(),
                                AMOUNT = "",
                                AUT_NAME = "",
                                CAMTALL = "",
                                PRICE = "",
                                RATE = ""
                            };
                            row_id++;
                            damxlist.Add(damxnotice);
                        }

                        if (!string.IsNullOrEmpty(detail.scheduledDateTime) || !string.IsNullOrEmpty(detail.place))
                        {
                            Model.TICKETREPRINT.DAMX damxnotice = new Model.TICKETREPRINT.DAMX()
                            {
                                ROW_ID = row_id,
                                DA_TYPE = "",
                                DAID = "",
                                DATIME = "",
                                ITEM_ID = "",
                                ITEM_NAME = " └ 时间：" + detail.scheduledDateTime.Trim() + " 地点：" + detail.place ,
                                AMOUNT = "",
                                AUT_NAME = "",
                                CAMTALL = "",
                                PRICE = "",
                                RATE = ""
                            };
                            row_id++;
                            damxlist.Add(damxnotice);
                        }
                    }

                    #endregion 检查

                    #region 检验

                    if (masterinfodata.labList.Count > 0)
                    {
                        //检验抬头：
                        Model.TICKETREPRINT.DAMX damx2 = new Model.TICKETREPRINT.DAMX()
                        {
                            ROW_ID = row_id,
                            DA_TYPE = "【检验】",
                            DAID = "",
                            DATIME = "",
                            ITEM_ID = "",
                            ITEM_NAME = "",
                            AMOUNT = "",
                            AUT_NAME = "",
                            CAMTALL = "",
                            PRICE = "",
                            RATE = ""
                        };
                        row_id++;
                        damxlist.Add(damx2);
                    }

                    //检验
                    foreach (var detail in masterinfodata.labList)
                    {
                        Model.TICKETREPRINT.DAMX damx = new Model.TICKETREPRINT.DAMX()
                        {
                            ROW_ID = row_id,
                            DA_TYPE = "",

                            DAID = "",
                            DATIME = "",
                            ITEM_ID = detail.itemName,
                            ITEM_NAME = detail.itemName,
                            AMOUNT = detail.charge.ToString(),
                            AUT_NAME = "",
                            CAMTALL = "",
                            PRICE = "",
                            RATE = ""
                        };
                        row_id++;
                        damxlist.Add(damx);
                    }
                    if (masterinfodata.labList.Count > 0)
                    {
                        //检验地点：
                        Model.TICKETREPRINT.DAMX damx2 = new Model.TICKETREPRINT.DAMX()
                        {
                            ROW_ID = row_id,
                            DA_TYPE = "",
                            DAID = "",
                            DATIME = "",
                            ITEM_ID = "",
                            ITEM_NAME = " └地点：" + masterinfodata.labPlace,
                            AMOUNT = "",
                            AUT_NAME = "",
                            CAMTALL = "",
                            PRICE = "",
                            RATE = ""
                        };
                        row_id++;
                        damxlist.Add(damx2);
                    }

                    #endregion 检验

                    #region 病理

                    //病理分组
                    if (masterinfodata.blList.Count > 0)
                    {
                        //病理信息抬头：
                        Model.TICKETREPRINT.DAMX damx6 = new Model.TICKETREPRINT.DAMX()
                        {
                            ROW_ID = row_id,
                            DA_TYPE = "【病理】",
                            DAID = "",
                            DATIME = "",
                            ITEM_ID = "",
                            ITEM_NAME = "",
                            AMOUNT = "",
                            AUT_NAME = "",
                            CAMTALL = "",
                            PRICE = "",
                            RATE = ""
                        };
                        row_id++;
                        damxlist.Add(damx6);
                    }

                    //病理
                    foreach (var detail in masterinfodata.blList)
                    {
                        Model.TICKETREPRINT.DAMX damx = new Model.TICKETREPRINT.DAMX()
                        {
                            ROW_ID = row_id,
                            DA_TYPE = "",

                            DAID = "",
                            DATIME = "",
                            ITEM_ID = detail.itemName,
                            ITEM_NAME = detail.itemName,
                            AMOUNT = detail.charge.ToString(),
                            AUT_NAME = "",
                            CAMTALL = "",
                            PRICE = "",
                            RATE = ""
                        };
                        row_id++;
                        damxlist.Add(damx);
                    }

                    //病理地点
                    if (masterinfodata.blList.Count > 0)
                    {
                        //病理地点：
                        Model.TICKETREPRINT.DAMX damx6 = new Model.TICKETREPRINT.DAMX()
                        {
                            ROW_ID = row_id,
                            DA_TYPE = "",
                            DAID = "",
                            DATIME = "",
                            ITEM_ID = "",
                            ITEM_NAME = " └地点：" + masterinfodata.blPlace,
                            AMOUNT = "",
                            AUT_NAME = "",
                            CAMTALL = "",
                            PRICE = "",
                            RATE = ""
                        };
                        row_id++;
                        damxlist.Add(damx6);
                    }

                    #endregion 病理

                    #region 其他

                    if (masterinfodata.noOrderIdList.Count > 0)
                    {
                        //诊察抬头：
                        Model.TICKETREPRINT.DAMX damx4 = new Model.TICKETREPRINT.DAMX()
                        {
                            ROW_ID = row_id,
                            DA_TYPE = "【其他】",
                            DAID = "",
                            DATIME = "",
                            ITEM_ID = "",
                            ITEM_NAME = "",
                            AMOUNT = "",
                            AUT_NAME = "",
                            CAMTALL = "",
                            PRICE = "",
                            RATE = ""
                        };
                        row_id++;
                        damxlist.Add(damx4);
                    }

                    //诊察
                    foreach (var detail in masterinfodata.noOrderIdList)
                    {
                        Model.TICKETREPRINT.DAMX damx = new Model.TICKETREPRINT.DAMX()
                        {
                            ROW_ID = row_id,
                            DA_TYPE = "",

                            DAID = "",
                            DATIME = "",
                            ITEM_ID = detail.itemName,
                            ITEM_NAME = detail.itemName,
                            AMOUNT = detail.charge.ToString(),
                            AUT_NAME = "",
                            CAMTALL = detail.amount,
                            PRICE = "",
                            RATE = ""
                        };
                        row_id++;
                        damxlist.Add(damx);
                    }
                    if (masterinfodata.treatList.Count > 0)
                    {
                        //处置信息抬头：
                        Model.TICKETREPRINT.DAMX damx5 = new Model.TICKETREPRINT.DAMX()
                        {
                            ROW_ID = row_id,
                            DA_TYPE = "【其他】",
                            DAID = "",
                            DATIME = "",
                            ITEM_ID = "",
                            ITEM_NAME = "",
                            AMOUNT = "",
                            AUT_NAME = "",
                            CAMTALL = "",
                            PRICE = "",
                            RATE = ""
                        };
                        row_id++;
                        damxlist.Add(damx5);
                    }

                    //处置
                    foreach (var detail in masterinfodata.treatList)
                    {
                        Model.TICKETREPRINT.DAMX damx = new Model.TICKETREPRINT.DAMX()
                        {
                            ROW_ID = row_id,
                            DA_TYPE = "",

                            DAID = "",
                            DATIME = "",
                            ITEM_ID = detail.itemName,
                            ITEM_NAME = detail.itemName,
                            AMOUNT = detail.charge.ToString(),
                            AUT_NAME = "",
                            CAMTALL = detail.amount,
                            PRICE = "",
                            RATE = ""
                        };
                        row_id++;
                        damxlist.Add(damx);
                    }

                    #endregion 其他

                    #region 药品

                    if (masterinfodata.drugList.Count > 0)
                    {
                        //药品抬头：
                        Model.TICKETREPRINT.DAMX damx1 = new Model.TICKETREPRINT.DAMX()
                        {
                            ROW_ID = row_id,
                            DA_TYPE = "【药品】",
                            DAID = "",
                            DATIME = "",
                            ITEM_ID = "",
                            ITEM_NAME = "",
                            AMOUNT = "",
                            AUT_NAME = "",
                            CAMTALL = "",
                            PRICE = "",
                            RATE = ""
                        };
                        row_id++;
                        damxlist.Add(damx1);
                    }

                    //药品
                    foreach (var detail in masterinfodata.drugList)
                    {
                        Model.TICKETREPRINT.DAMX damx = new Model.TICKETREPRINT.DAMX()
                        {
                            ROW_ID = row_id,
                            DA_TYPE = "",
                            DAID = "",
                            DATIME = "",
                            ITEM_ID = detail.itemName,
                            ITEM_NAME = detail.itemName,
                            AMOUNT = detail.charge.ToString(),
                            AUT_NAME = "",
                            CAMTALL = detail.amount,
                            PRICE = "",
                            RATE = ""
                        };
                        row_id++;
                        damxlist.Add(damx);
                    }

                    #endregion 药品

                    text.dtmx = JsonConvert.SerializeObject(damxlist);

                    Model.TICKETREPRINT.ITEM item = new Model.TICKETREPRINT.ITEM();
                    item.CAN_PRINT = "1";//ptitem.print_times >= TICKETREALLOWPRINTTIMES ? "0" :

                    item.JEALL = masterinfodata.totalCharges.ToString();
                    item.DEPT_NAME = "";
                    item.DJ_TIME = "";
                    item.PT_TYPE_NAME = "";
                    item.DJ_ID = DJ_ID;
                    item.TEXT = JsonConvert.SerializeObject(text);
                    item.PRINT_TIMES = "1";
                    ITEMLIST.Add(item);
                }

                return ITEMLIST;
            }

            return null;
        }
    }
}