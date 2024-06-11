using CommonModel;
using Newtonsoft.Json;
using OnlineBusHos9_OutHos.HISModels;

using System;
using System.Collections.Generic;
using System.Data;

namespace OnlineBusHos9_OutHos.BUS
{
    internal class OUTFEEPAYLOCK
    {
        public static string B_OUTFEEPAYLOCK(string json_in)
        {
            return Business(json_in);
        }

        public static string Business(string json_in)
        {
            DataReturn dataReturn = new DataReturn();
            string json_out = "";
            try
            {
                Model.OUTFEEPAYLOCK_M.OUTFEEPAYLOCK_IN _in = JsonConvert.DeserializeObject<Model.OUTFEEPAYLOCK_M.OUTFEEPAYLOCK_IN>(json_in);
                Model.OUTFEEPAYLOCK_M.OUTFEEPAYLOCK_OUT _out = new Model.OUTFEEPAYLOCK_M.OUTFEEPAYLOCK_OUT();
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

                DataTable dtpre = new DataTable();
                dtpre.Columns.Add("HOS_ID", typeof(string));
                dtpre.Columns.Add("lTERMINAL_SN", typeof(string));
                dtpre.Columns.Add("USER_ID", typeof(string));
                dtpre.Columns.Add("OPT_SN", typeof(string));
                dtpre.Columns.Add("PRE_NO", typeof(string));
                dtpre.Columns.Add("HOS_SN", typeof(string));
                dtpre.Columns.Add("SFZ_NO", typeof(string));
                dtpre.Columns.Add("MB_ID", typeof(string));

                DataTable dtMed = new DataTable();
                DataTable dtChkt = new DataTable();
                DataTable dtPre = new DataTable();
                dtPre.Columns.Add("HOS_ID", typeof(string));
                dtPre.Columns.Add("OPT_SN", typeof(string));
                dtPre.Columns.Add("PRE_NO", typeof(string));
                dtPre.Columns.Add("HOS_SN", typeof(string));
                dtPre.Columns.Add("JEALL", typeof(string));
                dtPre.Columns.Add("CASH_JE", typeof(string));
                dtPre.Columns.Add("DJ_DATE", typeof(string));
                dtPre.Columns.Add("DJ_TIME", typeof(string));

                foreach (Model.OUTFEEPAYLOCK_M.PRE pre in _in.PRELIST)
                {
                    DataRow drdtp = dtPre.NewRow();
                    drdtp["HOS_ID"] = _in.HOS_ID;
                    drdtp["OPT_SN"] = pre.OPT_SN;
                    drdtp["PRE_NO"] = pre.PRE_NO;
                    drdtp["HOS_SN"] = pre.HOS_SN;
                    drdtp["JEALL"] = 0;
                    drdtp["CASH_JE"] = 0;
                    drdtp["DJ_DATE"] = DateTime.Now.ToString("yyyy-MM-dd");
                    drdtp["DJ_TIME"] = DateTime.Now.ToString("HH:mm:ss");
                    dtPre.Rows.Add(drdtp);

                    HISModels.T5205.input input
                = new HISModels.T5205.input()
                {
                    yeWuLX = "5205",// 业务类型        业务类型(固定值为5205)
                    yeWuLY = "2",//业务来源        业务来源(1 - 线上 2 - 自助机)
                    keShiMC = "",// 科室名称
                    bingRenID = _in.HOSPATID,//病人ID
                    yiBaoBH = _in.YLCARD_NO,//医保编号
                    duKaFS = duKaFS,//读卡方式

                    jiuZhenKH = _in.SFZ_NO,// 同证件号码
                    yiBaoXX = _in.CARD_INFO,// 医保信息
                    zhengJianHM = _in.SFZ_NO,// 证件号码
                    hospitalId = "320282466455146",// 医院ID
                    yiBaoData = _in.BUS_CARD_INFO,// 医保交易数据
                    jiuZhenJLID = pre.PRE_NO,// 就诊记录ID
                    caoZuoRId = "NSH015",// 操作人ID
                };
                    PushServiceResult<T5205.data> result = HerenHelper<T5205.data>.pushService("5205-QHZZJ", JsonConvert.SerializeObject(input));

                    if (result.code != 1)
                    {
                        dataReturn.Code = 6;
                        dataReturn.Msg = result.msg;

                        return JsonConvert.SerializeObject(dataReturn);
                    }
                   
                        Double lcsyje = Double.Parse(result.data.lcsyAmount);
                        Double zongje = Double.Parse(result.data.zongJinE);
                        Double chae = zongje - lcsyje;//差额

                        if (result.data.lcsyAmount != "0" && chae == 0 && !(string.IsNullOrEmpty(result.data.lcsyAmount)))
                        {
                            //_out.JEALL = result.data.zongJinE;
                            _out.JEALL = result.data.ziFeiJE;//临床试验  0 

                            _out.CASH_JE = result.data.ziFeiJE;

                            _out.MEDFEE_SUMAMT = FormatHelper.GetStr(result.data.yiBaoJE);
                            _out.ACCT_PAY = FormatHelper.GetStr(result.data.geRenZHZF);
                            _out.PSN_CASH_PAY = FormatHelper.GetStr(result.data.ziFeiJE);
                            _out.FUND_PAY_SUMAMT = FormatHelper.GetStr(result.data.yiBaoTCZF);
                            _out.OTH_PAY = FormatHelper.GetStr("0.00");
                            _out.BALC = FormatHelper.GetStr(result.data.zhangHuYE);
                            _out.ACCT_MULAID_PAY = FormatHelper.GetStr(result.data.gongJiJinZF);
                        }
                        else
                        {
                            _out.JEALL = result.data.zongJinE;

                            _out.CASH_JE = result.data.ziFeiJE;

                            _out.MEDFEE_SUMAMT = FormatHelper.GetStr(result.data.yiBaoJE);
                            _out.ACCT_PAY = FormatHelper.GetStr(result.data.geRenZHZF);
                            _out.PSN_CASH_PAY = FormatHelper.GetStr(result.data.ziFeiJE);
                            _out.FUND_PAY_SUMAMT = FormatHelper.GetStr(result.data.yiBaoTCZF);
                            _out.OTH_PAY = FormatHelper.GetStr("0.00");
                            _out.BALC = FormatHelper.GetStr(result.data.zhangHuYE);
                            _out.ACCT_MULAID_PAY = FormatHelper.GetStr(result.data.gongJiJinZF);

                        }
                   
                }
                string PAY_ID = "";

                dataReturn.Code = 0;
                dataReturn.Msg = "success";

                #region 平台数据保存

                try
                {
                    var db = new DbMySQLZZJ().Client;
                    SqlSugarModel.OptPayLock Modelopt_pay_lock = new SqlSugarModel.OptPayLock();

                    int payid = 0;//
                    if (!PubFunc.GetSysID("opt_pay_lock", out payid))
                    {
                        dataReturn.Code = 2;
                        dataReturn.Msg = "获取payid 失败";
                        goto EndPoint;
                    }
                    int pat_id = 0;
                    var pat_info = db.Queryable<SqlSugarModel.PatInfo>().Where(t => t.SFZ_NO == _in.SFZ_NO).First();
                    if (pat_info != null)
                    {
                        pat_id = pat_info.PAT_ID;
                    }

                    string PRE_NO = "";
                    decimal CASH_JE = 0;
                    decimal JE_ALL = 0;
                    string HOS_SN = "";
                    foreach (DataRow dr in dtPre.Rows)
                    {
                        PRE_NO += dr["PRE_NO"].ToString().Trim() + "|";
                        CASH_JE = decimal.Parse(dr["CASH_JE"].ToString().Trim());
                        JE_ALL = decimal.Parse(dr["JEALL"].ToString().Trim());
                        HOS_SN += dr["HOS_SN"].ToString().Trim() + "|";
                    }
                    PAY_ID = payid.ToString().PadLeft(10, '0');
                    Modelopt_pay_lock.PAY_ID = PAY_ID;
                    Modelopt_pay_lock.HOS_ID = _in.HOS_ID;
                    Modelopt_pay_lock.PAT_ID = pat_id;
                    Modelopt_pay_lock.PAT_NAME = _in.PAT_NAME;
                    Modelopt_pay_lock.SFZ_NO = _in.SFZ_NO;
                    Modelopt_pay_lock.YLCARD_TYPE = FormatHelper.GetInt(_in.YLCARD_TYPE);
                    Modelopt_pay_lock.YLCARD_NO = _in.YLCARD_NO;
                    Modelopt_pay_lock.HOSPATID = _in.HOSPATID;

                    Modelopt_pay_lock.DEPT_CODE = "";
                    Modelopt_pay_lock.DEPT_NAME = "";
                    Modelopt_pay_lock.DOC_NO = "";
                    Modelopt_pay_lock.DOC_NAME = "";
                    Modelopt_pay_lock.CHARGE_TYPE = "";
                    Modelopt_pay_lock.QUERYID = "";
                    Modelopt_pay_lock.DEAL_TYPE = "";
                    Modelopt_pay_lock.SUM_JE = JE_ALL;
                    Modelopt_pay_lock.CASH_JE = CASH_JE;
                    Modelopt_pay_lock.ACCT_JE = 0;
                    Modelopt_pay_lock.FUND_JE = 0;
                    Modelopt_pay_lock.OTHER_JE = 0;
                    Modelopt_pay_lock.HOS_REG_SN = "";
                    Modelopt_pay_lock.HOS_SN = HOS_SN.TrimEnd('|');
                    Modelopt_pay_lock.OPT_SN = dtPre.Rows[0]["OPT_SN"].ToString().Trim();
                    Modelopt_pay_lock.PRE_NO = PRE_NO.TrimEnd('|');
                    Modelopt_pay_lock.RCPT_NO = "";
                    Modelopt_pay_lock.HOS_PAY_SN = "";
                    Modelopt_pay_lock.DJ_DATE = DateTime.Now.Date;
                    Modelopt_pay_lock.DJ_TIME = DateTime.Now.ToString("HH:mm:ss");

                    Modelopt_pay_lock.USER_ID = _in.USER_ID;
                    Modelopt_pay_lock.lTERMINAL_SN = _in.LTERMINAL_SN;
                    Modelopt_pay_lock.SOURCE = "ZZJ";

                    List<SqlSugarModel.OptPayMxLock> Modelopt_pay_mx_locks = new List<SqlSugarModel.OptPayMxLock>();

                    int item_no = 0;
                    //foreach (var item in datas)
                    //{
                    //    item_no++;
                    //    SqlSugarModel.OptPayMxLock Modelopt_pay_mx_lock = new SqlSugarModel.OptPayMxLock();

                    //    Modelopt_pay_mx_lock.PAY_ID = Modelopt_pay_lock.PAY_ID;
                    //    Modelopt_pay_mx_lock.ITEM_NO = item_no;
                    //    Modelopt_pay_mx_lock.DAID = item.moOrder;
                    //    Modelopt_pay_mx_lock.ITEM_TYPE = "0";
                    //    Modelopt_pay_mx_lock.ITEM_CODE = item.itemCode;
                    //    Modelopt_pay_mx_lock.ITEM_NAME = item.itemName;
                    //    Modelopt_pay_mx_lock.ITEM_SPEC = item.specs;
                    //    Modelopt_pay_mx_lock.ITEM_UNITS = item.priceUnit;
                    //    Modelopt_pay_mx_lock.ITEM_PRICE = item.unitPrice;
                    //    Modelopt_pay_mx_lock.AMOUNT = item.qty;
                    //    Modelopt_pay_mx_lock.COSTS = item.payCost;
                    //    Modelopt_pay_mx_lock.CHARGES = 0;
                    //    Modelopt_pay_mx_lock.ZFBL = null;

                    //    Modelopt_pay_mx_locks.Add(Modelopt_pay_mx_lock);
                    //}

                    try
                    {
                        db.BeginTran();

                        db.Insertable(Modelopt_pay_lock).ExecuteCommand();
                        db.Insertable(Modelopt_pay_mx_locks).ExecuteCommand();
                        db.CommitTran();
                    }
                    catch (Exception ex)
                    {
                        db.RollbackTran();

                        SqlSugarModel.Sqlerror sqlerror = new SqlSugarModel.Sqlerror();
                        sqlerror.TYPE = "OUTFEEPAYLOCK1";
                        sqlerror.Exception = JsonConvert.SerializeObject(ex);
                        sqlerror.DateTime = DateTime.Now;
                        LogHelper.SaveSqlerror(sqlerror);
                    }
                }
                catch (Exception ex)
                {
                    SqlSugarModel.Sqlerror sqlerror = new SqlSugarModel.Sqlerror();
                    sqlerror.TYPE = "OUTFEEPAYLOCK2";
                    sqlerror.Exception = JsonConvert.SerializeObject(ex);
                    sqlerror.DateTime = DateTime.Now;
                    LogHelper.SaveSqlerror(sqlerror);
                }

                #endregion 平台数据保存

                _out.PAY_ID = PAY_ID;
                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
                dataReturn.Param = JsonConvert.SerializeObject(_out);
                goto EndPoint;
        }
            catch (Exception ex)
            {
                dataReturn.Code = 6;
                dataReturn.Msg = "程序处理异常:" + ex.Message;
                dataReturn.Param = ex.Message;
            }
    EndPoint:
            json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }
    }
}