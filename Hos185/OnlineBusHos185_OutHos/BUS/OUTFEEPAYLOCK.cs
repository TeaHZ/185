using CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Data;
using Hos185_His.Models;
using static OnlineBusHos185_OutHos.Model.GETOUTFEENOPAYMX_M;
using Newtonsoft.Json;

namespace OnlineBusHos185_OutHos.BUS
{
    class OUTFEEPAYLOCK
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


                List<Hos185_His.Models.MZ.GETOUTFEENOPAYMXDATA> datas = new List<Hos185_His.Models.MZ.GETOUTFEENOPAYMXDATA>();

                foreach (Model.OUTFEEPAYLOCK_M.PRE pre in _in.PRELIST)
                {


                    Hos185_His.Models.MZ.GETOUTFEENOPAYMX nopaymx = new Hos185_His.Models.MZ.GETOUTFEENOPAYMX()
                    {
                        clinicCode = pre.HOS_SN,  //挂号流水号
                        invoiceNo = "",  //发票号
                        invoiceSeq = "",  //发票流水号
                        lifeEquityCardNo = "",  //权益卡卡号
                        lifeEquityCardType = "",  //权益卡类型
                        pactCode = "01",  //合同编号
                        recipeNo = pre.PRE_NO,  //处方号,多个以#分割
                        sequenceNo = "0",  //处方流水号
                        ybPay = "1"  //是否能够医保支付 0 不可以 1可以
                    };

                    string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(nopaymx);

                    Output<List<Hos185_His.Models.MZ.GETOUTFEENOPAYMXDATA>> output
           = GlobalVar.CallAPI<List<Hos185_His.Models.MZ.GETOUTFEENOPAYMXDATA>>("/hischargesinfo/outpatientfee/recipedetailinfo", jsonstr);


                    dataReturn.Code = output.code;
                    dataReturn.Msg = output.message;


                    if (output.code != 0)
                    {
                        return JsonConvert.SerializeObject(dataReturn);

                    }

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

                    //if (pactCode=="01")//医保用户也走这个预算，获取sjh（医保自费缴费，不走医保预算了）
                    {
                        Hos185_His.Models.MZ.OUTFEEPAYPRESAVE presave = new Hos185_His.Models.MZ.OUTFEEPAYPRESAVE()
                        {
                            hospitalcode = "",//医院代码
                            lifeEquityCardNo = "",//权益卡卡号
                            lifeEquityCardType = "",//权益卡类型
                            medicareParam = "",//医保参数
                            pactCode = "01",//合同单位
                            recipeNos = pre.PRE_NO.Replace('#', ','),
                            billType=pre.TKBILL_TYPE,
                            regid = pre.HOS_SN//挂号单号
                        };


                        jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(presave);

                        Output<Hos185_His.Models.MZ.OUTFEEPAYPRESAVEDATA> outputpre
               = GlobalVar.CallAPI<Hos185_His.Models.MZ.OUTFEEPAYPRESAVEDATA>("/hischargesinfo/outpatientfee/preSaveFeeXTBY", jsonstr);
                        dataReturn.Code = outputpre.code;
                        dataReturn.Msg = outputpre.message;

                        if (outputpre.code != 0)
                        {
                            return JsonConvert.SerializeObject(dataReturn);

                        }

                        _out.SJH = outputpre.data.receiptNumber;
                        _out.CASH_JE = outputpre.data.totCost;
                        _out.JEALL = outputpre.data.totCost;
                    }
    


     


                    DataRow drdtp = dtPre.NewRow();
                    drdtp["HOS_ID"] = _in.HOS_ID;
                    drdtp["OPT_SN"] = pre.OPT_SN;
                    drdtp["PRE_NO"] = pre.PRE_NO;
                    drdtp["HOS_SN"] = pre.HOS_SN;
                    drdtp["JEALL"] =0;
                    drdtp["CASH_JE"] =0;
                    drdtp["DJ_DATE"] = DateTime.Now.ToString("yyyy-MM-dd");
                    drdtp["DJ_TIME"] = DateTime.Now.ToString("HH:mm:ss");
                    dtPre.Rows.Add(drdtp);
                    datas.AddRange(output.data);


                }
                string PAY_ID = "";
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
                    if (datas != null && datas.Count > 0)
                    {
                        foreach (var item in datas)
                        {
                            item_no++;
                            SqlSugarModel.OptPayMxLock Modelopt_pay_mx_lock = new SqlSugarModel.OptPayMxLock();

                            Modelopt_pay_mx_lock.PAY_ID = Modelopt_pay_lock.PAY_ID;
                            Modelopt_pay_mx_lock.ITEM_NO = item_no;
                            Modelopt_pay_mx_lock.DAID = item.moOrder;
                            Modelopt_pay_mx_lock.ITEM_TYPE = "0";
                            Modelopt_pay_mx_lock.ITEM_CODE = item.itemCode;
                            Modelopt_pay_mx_lock.ITEM_NAME = item.itemName;
                            Modelopt_pay_mx_lock.ITEM_SPEC = item.specs;
                            Modelopt_pay_mx_lock.ITEM_UNITS = item.priceUnit;
                            Modelopt_pay_mx_lock.ITEM_PRICE = item.unitPrice;
                            Modelopt_pay_mx_lock.AMOUNT = item.qty;
                            Modelopt_pay_mx_lock.COSTS = item.payCost;
                            Modelopt_pay_mx_lock.CHARGES = 0;
                            Modelopt_pay_mx_lock.ZFBL = null;

                            Modelopt_pay_mx_locks.Add(Modelopt_pay_mx_lock);
                        }

                    }

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
                #endregion

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
