using CommonModel;
using ConstData;
using Hos185_His.Models;
using Hos185_His.Models.Einvoice;
using Hos185_His.Models.Report;
using Newtonsoft.Json;
using OnlineBusHos185_EInvoice.Model;

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace OnlineBusHos185_EInvoice.BUS
{
    internal class GetHisIssueBySfzno
    {
        public static string B_GetHisIssueBySfzno(string json_in)
        {
            DataReturn dataReturn = new DataReturn();

            try
            {
                GetHisIssueBySfzno_IN _in = JsonConvert.DeserializeObject<GetHisIssueBySfzno_IN>(json_in);
                GetHisIssueBySfzno_OUT _out = new GetHisIssueBySfzno_OUT();

                //挂号收费取配置预留
                //var db = new DbMySQLZZJ().Client;
                //int GHEInvoice = 3;
                //SqlSugarModel.SysConfig model = db.Queryable<SqlSugarModel.SysConfig>().Where(t => t.HOS_ID == _in.HOS_ID && t.config_key == "GHEInvoice").First();
                //if (model != null)
                //{
                //    GHEInvoice = FormatHelper.GetInt(model.config_value);
                //}
                //int MZEInvoice = 3;
                //SqlSugarModel.SysConfig modelMZEInvoice = db.Queryable<SqlSugarModel.SysConfig>().Where(t => t.HOS_ID == _in.HOS_ID && t.config_key == "MZEInvoice").First();
                //if (modelMZEInvoice != null)
                //{
                //    MZEInvoice = FormatHelper.GetInt(modelMZEInvoice.config_value);
                //}

                //测试  手输 病历号
                if (_in.YLCARD_TYPE=="1")
                {
                    _in.HOSPATID = _in.YLCARD_NO;

                }


                if (string.IsNullOrEmpty(_in.BEGIN_DATE))
                {
                    _in.BEGIN_DATE = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd");
                    _in.END_DATE = DateTime.Now.ToString("yyyy-MM-dd");
                }

                string out_data = "";
                int status = 0;
                #region 挂号
                GH.GH_IN gh_in = new GH.GH_IN();
                gh_in.cardNo = _in.HOSPATID;
                gh_in.clinicCode = "";
                gh_in.validFlag = "1";
                gh_in.transType = "1";
                gh_in.ynSee = "";
                gh_in.startTime = _in.BEGIN_DATE + " 00:00:00";
                gh_in.endTime = _in.END_DATE + " 23:59:59";
                gh_in.idCardType = "";
                gh_in.idCardNo = "";
                gh_in.invoiceNo = "";


                string jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(gh_in);

                Output<List<GH.GHData>> ghoutput
          = GlobalVar.CallAPI<List<GH.GHData>>("/smartmedical/eInvoice/regEInvoiceList", jsonstr);




                #endregion

                #region 门诊

                MZ.MZ_IN mz_in = new MZ.MZ_IN();
                mz_in.cardNo = _in.HOSPATID;
                mz_in.cardType = "1";
                mz_in.startTime = _in.BEGIN_DATE + " 00:00:00";
                mz_in.endTime = _in.END_DATE + " 23:59:59";
                mz_in.validFlag = "1";

                jsonstr = Newtonsoft.Json.JsonConvert.SerializeObject(mz_in);

                Output<List<MZ.DataItem>> mzoutput
          = GlobalVar.CallAPI<List<MZ.DataItem>>("/smartmedical/eInvoice/outpFeeEInvoiceList", jsonstr);

                #endregion



                _out.HISISSUELISTS = new List<GetHisIssueBySfzno_OUT.Hisissuelist>();


                if (ghoutput.code == 0)
                {
                    foreach (var item in ghoutput.data)
                    {
                        if (item.printFlagQH == "1" || item.printFlagQH == "3" )
                        {
                            continue;
                        }

                        //pringflagqh:0-已开立，未打印，1-已打印，2-应该开立，还未开立，3-其他可能不需要开立或者已经过了时间的

                        GetHisIssueBySfzno_OUT.Hisissuelist list = new GetHisIssueBySfzno_OUT.Hisissuelist();
                        list.INVOICE_CODE = item.invoiceNo;
                        list.INVOICE_NUMBER = item.queryid;
                        list.INVOICING_PARTY_NAME = "";
                        list.PAYER_PARTY_NAME = item.name;
                        list.TOTAL_AMOUNT = item.diagFee;
                        list.SFTYPENAME = "挂号";
                        list.STATUS = "";
                        list.SAVEDDATE_TIME = item.operDate;
                        list.ISPRINT = item.printState;
                        list.print_times = "";
                        list.IS_CHECK = "0";
                        list.IS_ISSUE =item.printFlagQH=="0"? "1":"0";

                        list.SETTLECODE = item.queryid;
                        list.BUSINESS_TYPE = "0";
                        list.INPATIENTNO = item.clinicCode;
                        list.invoiceSource = item.invoiceSource;



                        _out.HISISSUELISTS.Add(list);


                    }

                }


                if (mzoutput.code==0)
                {
                    foreach (var item in mzoutput.data)
                    {
                        if (item.printFlagQH == "1" || item.printFlagQH == "3")
                        {
                            continue;
                        }

                        //pringflagqh:0-已开立，未打印，1-已打印，2-应该开立，还未开立，3-其他可能不需要开立或者已经过了时间的

                        GetHisIssueBySfzno_OUT.Hisissuelist list = new GetHisIssueBySfzno_OUT.Hisissuelist();
                        list.INVOICE_CODE = item.invoiceNo ;
                        list.INVOICE_NUMBER = item.invoiceSeq;
                        list.INVOICING_PARTY_NAME = "";
                        list.PAYER_PARTY_NAME = item.name;
                        list.TOTAL_AMOUNT = item.totCost;
                        list.SFTYPENAME = "门诊";
                        list.STATUS = "";
                        list.SAVEDDATE_TIME = item.feeDate;
                        list.ISPRINT = item.printState;
                        list.print_times = "";
                        list.IS_ISSUE = item.printFlagQH == "0" ? "1" : "0";
                        list.IS_CHECK = "0";
                        list.SETTLECODE = item.invoiceSeq;
                        list.BUSINESS_TYPE = "1";
                        list.INPATIENTNO = item.clinicCode;
                        list.invoiceSource = item.invoiceSource;

                        _out.HISISSUELISTS.Add(list);


                    }
                }

                if (_out.HISISSUELISTS.Count == 0)
                {
                    dataReturn.Code = CodeDefine.UIMessageType_Error;
                    dataReturn.Msg = "无可打印的发票!";
                    goto EndPoint;
                }
                dataReturn.Code = 0;
                dataReturn.Msg = "SUCCESS";
                dataReturn.Param = JsonConvert.SerializeObject(_out);
            }
            catch (Exception ex)
            {
                dataReturn.Code = 7;
                dataReturn.Msg = "程序处理异常";
                dataReturn.Param = ex.ToString();
            }
        EndPoint:
            string json_out = JsonConvert.SerializeObject(dataReturn);
            return json_out;
        }

        public class BODY
        {
            public string CLBZ { get; set; }
            public string CLJG { get; set; }
            public List<HisissuelistItem> hisissuelist { get; set; }
        }

        public class HisissuelistItem
        {
            /// <summary>
            ///
            /// </summary>
            public string invoice_code { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string invoice_number { get; set; }

            /// <summary>
            ///
            /// </summary>
            public decimal total_amount { get; set; }

            /// <summary>
            /// 南医大二附院
            /// </summary>
            public string invoicing_party_name { get; set; }

            /// <summary>
            ///
            /// </summary>
            public string payer_party_name { get; set; }

            /// <summary>
            /// 门诊
            /// </summary>
            public string sftypename { get; set; }

            /// <summary>
            /// 已红冲
            /// </summary>
            public string STATUS { get; set; }

            public string saveddate_time { get; set; }
            public string isprint { get; set; }

            /// <summary>
            /// 打印次数
            /// </summary>
            public string print_times { get; set; }
        }
    }
}